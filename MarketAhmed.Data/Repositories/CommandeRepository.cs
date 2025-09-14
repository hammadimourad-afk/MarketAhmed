using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketAhmed.Data.Repositories
{
    public class CommandeRepository : ICommandeRepository
    {
        private readonly string _connectionString; // Add this field

        // Add this constructor if your repository needs the connection string
        public CommandeRepository(string connectionString)
        {
            _connectionString = connectionString;
            // You would then use _connectionString when creating your DbConnection
        }
        public Commande GetById(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT IdCommande, DateCommande, Statut, MontantTotal, AdresseLivraison, AdresseFacturation, IdClient, DateModification FROM Commande WHERE IdCommande = @IdCommande";
            command.Parameters.AddWithValue("@IdCommande", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapCommandeFromReader(reader);
            }
            return null;
        }

        public IEnumerable<Commande> GetAll()
        {
            var commandes = new List<Commande>();
            using var connection = Database.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT IdCommande, DateCommande, Statut, MontantTotal, AdresseLivraison, AdresseFacturation, IdClient, DateModification FROM Commande";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                commandes.Add(MapCommandeFromReader(reader));
            }
            return commandes;
        }

        public void Add(Commande commande)
        {
            using var connection = Database.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO Commande (DateCommande, Statut, MontantTotal, AdresseLivraison, AdresseFacturation, IdClient, DateModification)
                VALUES (@DateCommande, @Statut, @MontantTotal, @AdresseLivraison, @AdresseFacturation, @IdClient, @DateModification);
                SELECT last_insert_rowid();";

            // Assurez-vous que les dates sont définies avant d'appeler SetCommandeParameters
            commande.DateCommande = commande.DateCommande == default ? DateTime.Now : commande.DateCommande;
            commande.DateModification = DateTime.Now;

            // --- CORRECTION MAJEURE ICI : Appeler SetCommandeParameters UNE SEULE FOIS ---
            SetCommandeParameters(command, commande);
            // Supprimez tout le bloc de code qui ajoutait manuellement les paramètres un par un après cet appel.

            commande.IdCommande = (int)(long)command.ExecuteScalar();
            transaction.Commit();
        }

        public void Update(Commande commande)
        {
            using var connection = Database.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE Commande SET
                DateCommande = @DateCommande,
                Statut = @Statut,
                MontantTotal = @MontantTotal,
                AdresseLivraison = @AdresseLivraison,
                AdresseFacturation = @AdresseFacturation,
                IdClient = @IdClient,
                DateModification = @DateModification
                WHERE IdCommande = @IdCommande";

            // Important : Mettre à jour la date de modification AVANT d'appeler SetCommandeParameters
            commande.DateModification = DateTime.Now;

            // Utiliser la méthode d'aide pour ajouter tous les paramètres de la commande
            SetCommandeParameters(command, commande);

            // Ajouter le paramètre IdCommande, spécifique à la clause WHERE de la mise à jour
            // Ce paramètre est unique à la mise à jour et ne doit pas être dans SetCommandeParameters si elle n'est pas utilisée pour l'ajout.
            command.Parameters.AddWithValue("@IdCommande", commande.IdCommande);

            command.ExecuteNonQuery();
            transaction.Commit();
        }
        public void Delete(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM Commande WHERE IdCommande = @IdCommande";
            command.Parameters.AddWithValue("@IdCommande", id);
            command.ExecuteNonQuery();
            transaction.Commit();
        }

        private Commande MapCommandeFromReader(SqliteDataReader reader)
        {
            // Correction pour le statut : il doit être lu comme un INT si vous le stockez comme INT
            // Correction pour la date : il doit être lu comme un String puis parse si vous le stockez comme TEXT
            return new Commande
            {
                IdCommande = reader.GetInt32(reader.GetOrdinal("IdCommande")),
                DateCommande = DateTime.Parse(reader.GetString(reader.GetOrdinal("DateCommande"))),
                // Ici, Statut est lu comme un entier puis casté en StatutCommande
                Statut = (StatutCommande)reader.GetInt32(reader.GetOrdinal("Statut")),
                MontantTotal = reader.GetDecimal(reader.GetOrdinal("MontantTotal")),
                AdresseLivraison = reader.IsDBNull(reader.GetOrdinal("AdresseLivraison")) ? null : reader.GetString(reader.GetOrdinal("AdresseLivraison")),
                AdresseFacturation = reader.IsDBNull(reader.GetOrdinal("AdresseFacturation")) ? null : reader.GetString(reader.GetOrdinal("AdresseFacturation")),
                IdClient = reader.GetInt32(reader.GetOrdinal("IdClient")),
                DateModification = reader.IsDBNull(reader.GetOrdinal("DateModification")) ? (DateTime?)null : DateTime.Parse(reader.GetString(reader.GetOrdinal("DateModification")))
            };
        }

        private void SetCommandeParameters(SqliteCommand command, Commande commande)
        {
            // Vider les paramètres existants si la commande est réutilisée, même si ce n'est pas le cas ici, c'est une bonne pratique.
            command.Parameters.Clear();

            // S'assurer que les dates sont au format TEXT compatible SQLite
            command.Parameters.AddWithValue("@DateCommande", commande.DateCommande.ToString("yyyy-MM-dd HH:mm:ss"));
            // Stocker l'énumération Statut comme un INTEGER en base de données pour la cohérence et l'efficacité
            command.Parameters.AddWithValue("@Statut", (int)commande.Statut);
            command.Parameters.AddWithValue("@MontantTotal", commande.MontantTotal);
            command.Parameters.AddWithValue("@AdresseLivraison", commande.AdresseLivraison ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@AdresseFacturation", commande.AdresseFacturation ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@IdClient", commande.IdClient);
            command.Parameters.AddWithValue("@DateModification", commande.DateModification?.ToString("yyyy-MM-dd HH:mm:ss") ?? (object)DBNull.Value);
        }
    }

    public class CommandeDetailRepository : ICommandeDetailRepository
    {
        private readonly string _connectionString; // Add this field
        public CommandeDetailRepository(string connectionString)
        {
            _connectionString = connectionString;
            // You would then use _connectionString when creating your DbConnection
            // Constructeur sans argument
        }

        public IEnumerable<CommandeDetail> GetDetailsByCommandeId(int commandeId)
        {
            var details = new List<CommandeDetail>();
            using var connection = Database.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT IdCommandeDetail, Quantite, PrixUnitaire, IdCommande, IdProduit FROM CommandeDetail WHERE IdCommande = @IdCommande";
            command.Parameters.AddWithValue("@IdCommande", commandeId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                details.Add(MapCommandeDetailFromReader(reader));
            }
            return details;
        }

        public CommandeDetail GetById(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT IdCommandeDetail, Quantite, PrixUnitaire, IdCommande, IdProduit FROM CommandeDetail WHERE IdCommandeDetail = @IdCommandeDetail";
            command.Parameters.AddWithValue("@IdCommandeDetail", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapCommandeDetailFromReader(reader);
            }
            return null;
        }

        public IEnumerable<CommandeDetail> GetAll()
        {
            var details = new List<CommandeDetail>();
            using var connection = Database.GetConnection();
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT IdCommandeDetail, Quantite, PrixUnitaire, IdCommande, IdProduit FROM CommandeDetail";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                details.Add(MapCommandeDetailFromReader(reader));
            }
            return details;
        }

        public void Add(CommandeDetail entity)
        {
            using var connection = Database.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO CommandeDetail (Quantite, PrixUnitaire, IdCommande, IdProduit)
                VALUES (@Quantite, @PrixUnitaire, @IdCommande, @IdProduit);
                SELECT last_insert_rowid();";

            SetCommandeDetailParameters(command, entity);

            entity.IdCommandeDetail = (int)(long)command.ExecuteScalar();
            transaction.Commit();
        }

        public void Update(CommandeDetail entity)
        {
            using var connection = Database.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE CommandeDetail SET
                Quantite = @Quantite,
                PrixUnitaire = @PrixUnitaire,
                IdCommande = @IdCommande,
                IdProduit = @IdProduit
                WHERE IdCommandeDetail = @IdCommandeDetail";

            SetCommandeDetailParameters(command, entity);
            command.Parameters.AddWithValue("@IdCommandeDetail", entity.IdCommandeDetail);

            command.ExecuteNonQuery();
            transaction.Commit();
        }

        public void Delete(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM CommandeDetail WHERE IdCommandeDetail = @IdCommandeDetail";
            command.Parameters.AddWithValue("@IdCommandeDetail", id);
            command.ExecuteNonQuery();
            transaction.Commit();
        }

        private CommandeDetail MapCommandeDetailFromReader(SqliteDataReader reader)
        {
            return new CommandeDetail
            {
                IdCommandeDetail = reader.GetInt32(reader.GetOrdinal("IdCommandeDetail")),
                Quantite = reader.GetInt32(reader.GetOrdinal("Quantite")),
                PrixUnitaire = reader.GetDecimal(reader.GetOrdinal("PrixUnitaire")), // Changé en GetDecimal
                IdCommande = reader.GetInt32(reader.GetOrdinal("IdCommande")),
                IdProduit = reader.GetInt32(reader.GetOrdinal("IdProduit"))
            };
        }

        private void SetCommandeDetailParameters(SqliteCommand command, CommandeDetail entity)
        {
            command.Parameters.AddWithValue("@Quantite", entity.Quantite);
            command.Parameters.AddWithValue("@PrixUnitaire", entity.PrixUnitaire);
            command.Parameters.AddWithValue("@IdCommande", entity.IdCommande);
            command.Parameters.AddWithValue("@IdProduit", entity.IdProduit);
        }
    }

}
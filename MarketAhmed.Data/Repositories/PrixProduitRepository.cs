using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;

namespace MarketAhmed.Data.Repositories
{
    public class PrixProduitRepository : IPrixProduitRepository
    {
        private readonly string _connectionString;

        public PrixProduitRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Récupère le prix courant d’un produit (DateFin IS NULL).
        /// </summary>
        public PrixProduit GetCurrentPrix(int idProduit)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT IdPrixProduit, IdProduit, PrixAchat, PrixVente, DateDebut, DateFin
                FROM PrixProduit
                WHERE IdProduit = $id AND DateFin IS NULL
                ORDER BY DateDebut DESC
                LIMIT 1";
            cmd.Parameters.AddWithValue("$id", idProduit);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new PrixProduit
                {
                    IdPrixProduit = reader.GetInt32(0),
                    IdProduit = reader.GetInt32(1),
                    PrixAchat = reader.GetDecimal(2),
                    PrixVente = reader.GetDecimal(3),
                    DateDebut = DateTime.Parse(reader.GetString(4)),
                    DateFin = reader.IsDBNull(5) ? (DateTime?)null : DateTime.Parse(reader.GetString(5))
                };
            }
            return null;
        }

        /// <summary>
        /// Retourne l’historique des prix d’un produit (tous les enregistrements).
        /// </summary>
        public IEnumerable<PrixProduit> GetHistorique(int idProduit)
        {
            var list = new List<PrixProduit>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT IdPrixProduit, IdProduit, PrixAchat, PrixVente, DateDebut, DateFin
                FROM PrixProduit
                WHERE IdProduit = $id
                ORDER BY DateDebut DESC";
            cmd.Parameters.AddWithValue("$id", idProduit);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new PrixProduit
                {
                    IdPrixProduit = reader.GetInt32(0),
                    IdProduit = reader.GetInt32(1),
                    PrixAchat = reader.GetDecimal(2),
                    PrixVente = reader.GetDecimal(3),
                    DateDebut = DateTime.Parse(reader.GetString(4)),
                    DateFin = reader.IsDBNull(5) ? (DateTime?)null : DateTime.Parse(reader.GetString(5))
                });
            }
            return list;
        }

        /// <summary>
        /// Ajoute un nouveau prix (et retourne l’ID inséré).
        /// </summary>
        public int Insert(PrixProduit prix)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO PrixProduit (IdProduit, PrixAchat, PrixVente, DateDebut, DateFin)
                VALUES ($idProduit, $prixAchat, $prixVente, $dateDebut, $dateFin);
                SELECT last_insert_rowid();";

            cmd.Parameters.AddWithValue("$idProduit", prix.IdProduit);
            cmd.Parameters.AddWithValue("$prixAchat", prix.PrixAchat);
            cmd.Parameters.AddWithValue("$prixVente", prix.PrixVente);
            cmd.Parameters.AddWithValue("$dateDebut", prix.DateDebut.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("$dateFin", prix.DateFin.HasValue ? prix.DateFin.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        // ✅ Supprimer un prix par son ID
        public void SupprimerPrix(int idPrixProduit)
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"DELETE FROM PrixProduit WHERE IdPrixProduit = @id";
                cmd.Parameters.AddWithValue("@id", idPrixProduit);
                cmd.ExecuteNonQuery();
            }
        }

        // ✅ Implémentation modification
        public void ModifierPrix(int idPrixProduit, decimal prixAchat, decimal prixVente)
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                string sql = @"UPDATE PrixProduit 
                               SET PrixAchat = @prixAchat, 
                                   PrixVente = @prixVente 
                               WHERE IdPrixProduit = @id";

                using (var cmd = new SqliteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@prixAchat", prixAchat);
                    cmd.Parameters.AddWithValue("@prixVente", prixVente);
                    cmd.Parameters.AddWithValue("@id", idPrixProduit);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Ferme le prix courant (en mettant une DateFin).
        /// </summary>
        public void CloseCurrentPrix(int idProduit, DateTime dateFin)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE PrixProduit
                        SET DateFin = $dateFin
                        WHERE IdProduit = $id AND (DateFin IS NULL OR DateFin = '')";

            cmd.Parameters.AddWithValue("$id", idProduit);
            cmd.Parameters.AddWithValue("$dateFin", dateFin.ToString("yyyy-MM-dd"));

            cmd.ExecuteNonQuery();
        }
    }
}

using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace MarketAhmed.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Client> GetAll()
        {
            var clients = new List<Client>();
            using var connection = Database.GetConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT IdClient, Nom, Prenom, Adresse, Telephone, Email, StatutCompte, DateAjout, DateDerniereModification, Latitude, Longitude FROM Client";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                clients.Add(MapClientFromReader(reader));
            }
            return clients;
        }

        public Client GetById(int id)
        {
            using var connection = Database.GetConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT IdClient, Nom, Prenom, Adresse, Telephone, Email, StatutCompte, DateAjout, DateDerniereModification, Latitude, Longitude FROM Client WHERE IdClient = @IdClient";
            command.Parameters.AddWithValue("@IdClient", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapClientFromReader(reader);
            }
            return null;
        }

        public Client GetByEmail(string email)
        {
            using var connection = Database.GetConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT IdClient, Nom, Prenom, Adresse, Telephone, Email, StatutCompte, DateAjout, DateDerniereModification, Latitude, Longitude FROM Client WHERE Email = @Email";
            command.Parameters.AddWithValue("@Email", email);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapClientFromReader(reader);
            }
            return null;
        }

        public void Add(Client client)
        {
            using var connection = Database.GetConnection();
            connection.Open(); // Open connection for transaction
            using var transaction = connection.BeginTransaction(); // Start transaction
            using var command = connection.CreateCommand();
            command.Transaction = transaction; // Assign transaction to command
            command.CommandText = @"
                INSERT INTO Client (Nom, Prenom, Adresse, Telephone, Email, StatutCompte, DateAjout, DateDerniereModification, Latitude, Longitude)
                VALUES (@Nom, @Prenom, @Adresse, @Telephone, @Email, @StatutCompte, @DateAjout, @DateDerniereModification, @Latitude, @Longitude);
                SELECT last_insert_rowid();"; // Get the last inserted row ID

            SetClientParameters(command, client);
            // Dates are now set by default in the Client model, but we can ensure they are up-to-date here.
            client.DateAjout = DateTime.Now;
            client.DateDerniereModification = DateTime.Now;
            command.Parameters["@DateAjout"].Value = client.DateAjout.ToString("yyyy-MM-dd HH:mm:ss");
            command.Parameters["@DateDerniereModification"].Value = client.DateDerniereModification.ToString("yyyy-MM-dd HH:mm:ss");


            client.IdClient = (int)(long)command.ExecuteScalar(); // ExecuteScalar to get the new ID
            transaction.Commit(); // Commit transaction
        }

        public void Update(Client client)
        {
            using var connection = Database.GetConnection();
            connection.Open(); // Open connection for transaction
            using var transaction = connection.BeginTransaction(); // Start transaction
            using var command = connection.CreateCommand();
            command.Transaction = transaction; // Assign transaction to command
            command.CommandText = @"
                UPDATE Client SET
                Nom = @Nom,
                Prenom = @Prenom,
                Adresse = @Adresse,
                Telephone = @Telephone,
                Email = @Email,
                StatutCompte = @StatutCompte,
                DateDerniereModification = @DateDerniereModification,
                Latitude = @Latitude,
                Longitude = @Longitude
                WHERE IdClient = @IdClient";

            SetClientParameters(command, client);
            client.DateDerniereModification = DateTime.Now; // Update modification date
            command.Parameters["@DateDerniereModification"].Value = client.DateDerniereModification.ToString("yyyy-MM-dd HH:mm:ss");
            command.Parameters.AddWithValue("@IdClient", client.IdClient);

            command.ExecuteNonQuery();
            transaction.Commit(); // Commit transaction
        }

        public void Delete(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open(); // Open connection for transaction
            using var transaction = connection.BeginTransaction(); // Start transaction
            using var command = connection.CreateCommand();
            command.Transaction = transaction; // Assign transaction to command
            command.CommandText = "DELETE FROM Client WHERE IdClient = @IdClient";
            command.Parameters.AddWithValue("@IdClient", id);
            command.ExecuteNonQuery();
            transaction.Commit(); // Commit transaction
        }

        private Client MapClientFromReader(SqliteDataReader reader)
        {
            return new Client
            {
                IdClient = reader.GetInt32(reader.GetOrdinal("IdClient")),
                Nom = reader.GetString(reader.GetOrdinal("Nom")),
                Prenom = reader.GetString(reader.GetOrdinal("Prenom")),
                Adresse = reader.IsDBNull(reader.GetOrdinal("Adresse")) ? null : reader.GetString(reader.GetOrdinal("Adresse")),
                Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? null : reader.GetString(reader.GetOrdinal("Telephone")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                StatutCompte = reader.GetString(reader.GetOrdinal("StatutCompte")),
                DateAjout = DateTime.Parse(reader.GetString(reader.GetOrdinal("DateAjout"))),
                DateDerniereModification = DateTime.Parse(reader.GetString(reader.GetOrdinal("DateDerniereModification"))),
                Latitude = reader.IsDBNull(reader.GetOrdinal("Latitude")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("Latitude")),
                Longitude = reader.IsDBNull(reader.GetOrdinal("Longitude")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("Longitude"))
            };
        }

        private void SetClientParameters(SqliteCommand command, Client client)
        {
            command.Parameters.AddWithValue("@Nom", client.Nom);
            command.Parameters.AddWithValue("@Prenom", client.Prenom);
            command.Parameters.AddWithValue("@Adresse", client.Adresse ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Telephone", client.Telephone ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Email", client.Email);
            command.Parameters.AddWithValue("@StatutCompte", client.StatutCompte);
            command.Parameters.AddWithValue("@DateAjout", client.DateAjout.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@DateDerniereModification", client.DateDerniereModification.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@Latitude", client.Latitude ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Longitude", client.Longitude ?? (object)DBNull.Value);
        }
    }
}
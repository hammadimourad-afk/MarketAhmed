using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;

namespace MarketAhmed.Data.Repositories
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly string _connectionString;

        public CategorieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Categorie> GetAll()
        {
            var list = new List<Categorie>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT IdCategorie, Nom, Description FROM Categorie";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Categorie
                {
                    IdCategorie = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? "" : reader.GetString(2)
                });
            }
            return list;
        }

        public Categorie GetById(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT IdCategorie, Nom, Description FROM Categorie WHERE IdCategorie=$id";
            cmd.Parameters.AddWithValue("$id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Categorie
                {
                    IdCategorie = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? "" : reader.GetString(2)
                };
            }
            return null;
        }

        public int Insert(Categorie categorie)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Categorie (Nom, Description) VALUES ($nom, $desc);
                                SELECT last_insert_rowid();";

            cmd.Parameters.AddWithValue("$nom", categorie.Nom);
            cmd.Parameters.AddWithValue("$desc", categorie.Description ?? "");

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Categorie categorie)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE Categorie SET Nom=$nom, Description=$desc WHERE IdCategorie=$id";

            cmd.Parameters.AddWithValue("$nom", categorie.Nom);
            cmd.Parameters.AddWithValue("$desc", categorie.Description ?? "");
            cmd.Parameters.AddWithValue("$id", categorie.IdCategorie);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Categorie WHERE IdCategorie=$id";
            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();
        }
    }
}

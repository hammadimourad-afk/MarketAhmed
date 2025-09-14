using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;

namespace MarketAhmed.Data.Repositories
{
    public class UniteRepository : IUniteRepository
    {
        private readonly string _connectionString;

        public UniteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Unite> GetAll()
        {
            var list = new List<Unite>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT IdUnite, Nom FROM Unite";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Unite
                {
                    IdUnite = reader.GetInt32(0),
                    Nom = reader.GetString(1)
                });
            }
            return list;
        }

        public Unite GetById(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT IdUnite, Nom FROM Unite WHERE IdUnite=$id";
            cmd.Parameters.AddWithValue("$id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Unite
                {
                    IdUnite = reader.GetInt32(0),
                    Nom = reader.GetString(1)
                };
            }
            return null;
        }

        public int Insert(Unite unite)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Unite (Nom) VALUES ($nom);
                                SELECT last_insert_rowid();";

            cmd.Parameters.AddWithValue("$nom", unite.Nom);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Unite unite)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE Unite SET Nom=$nom WHERE IdUnite=$id";

            cmd.Parameters.AddWithValue("$nom", unite.Nom);
            cmd.Parameters.AddWithValue("$id", unite.IdUnite);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Unite WHERE IdUnite=$id";
            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();
        }
    }
}

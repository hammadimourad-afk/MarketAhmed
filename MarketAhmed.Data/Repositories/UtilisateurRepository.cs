using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;


namespace MarketAhmed.Data.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly SqliteConnection _connection;

        public UtilisateurRepository(SqliteConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Utilisateur> GetAll()
        {
            var list = new List<Utilisateur>();
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT IdUtilisateur, Nom, MotDePasse, Role, Actif, DateAjout FROM Utilisateur";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Utilisateur
                {
                    IdUtilisateur = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    MotDePasse = reader.GetString(2),
                    Role = reader.GetString(3),
                    Actif = reader.GetBoolean(4),
                    DateAjout = DateTime.Parse(reader.GetString(5))
                });
            }
            return list;
        }

        public Utilisateur? GetById(int id)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT IdUtilisateur, Nom, MotDePasse, Role, Actif, DateAjout FROM Utilisateur WHERE IdUtilisateur = $id";
            cmd.Parameters.AddWithValue("$id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Utilisateur
                {
                    IdUtilisateur = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    MotDePasse = reader.GetString(2),
                    Role = reader.GetString(3),
                    Actif = reader.GetBoolean(4),
                    DateAjout = DateTime.Parse(reader.GetString(5))
                };
            }
            return null;
        }

        public Utilisateur? GetByNom(string nom)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT IdUtilisateur, Nom, MotDePasse, Role, Actif, DateAjout FROM Utilisateur WHERE Nom = $nom";
            cmd.Parameters.AddWithValue("$nom", nom);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Utilisateur
                {
                    IdUtilisateur = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    MotDePasse = reader.GetString(2),
                    Role = reader.GetString(3),
                    Actif = reader.GetBoolean(4),
                    DateAjout = DateTime.Parse(reader.GetString(5))
                };
            }
            return null;
        }

        public int Inserer(Utilisateur utilisateur)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO Utilisateur (Nom, MotDePasse, Role, Actif, DateAjout)
                                VALUES ($nom, $motDePasse, $role, $actif, $dateAjout);
                                SELECT last_insert_rowid();";

            cmd.Parameters.AddWithValue("$nom", utilisateur.Nom);
            cmd.Parameters.AddWithValue("$motDePasse", utilisateur.MotDePasse);
            cmd.Parameters.AddWithValue("$role", utilisateur.Role);
            cmd.Parameters.AddWithValue("$actif", utilisateur.Actif);
            cmd.Parameters.AddWithValue("$dateAjout", utilisateur.DateAjout.ToString("yyyy-MM-dd"));

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool Modifier(Utilisateur utilisateur)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = @"UPDATE Utilisateur
                                SET Nom = $nom,
                                    MotDePasse = $motDePasse,
                                    Role = $role,
                                    Actif = $actif
                                WHERE IdUtilisateur = $id";

            cmd.Parameters.AddWithValue("$nom", utilisateur.Nom);
            cmd.Parameters.AddWithValue("$motDePasse", utilisateur.MotDePasse);
            cmd.Parameters.AddWithValue("$role", utilisateur.Role);
            cmd.Parameters.AddWithValue("$actif", utilisateur.Actif);
            cmd.Parameters.AddWithValue("$id", utilisateur.IdUtilisateur);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Supprimer(int id)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Utilisateur WHERE IdUtilisateur = $id";
            cmd.Parameters.AddWithValue("$id", id);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}

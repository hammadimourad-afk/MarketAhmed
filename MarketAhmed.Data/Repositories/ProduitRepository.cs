using System;

using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using MarketAhmed.Core.Models;
using MarketAhmed.Core.Interfaces;

namespace MarketAhmed.Data.Repositories
{
    public class ProduitRepository : IProduitRepository
    {
        private readonly string _connectionString;

        public ProduitRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Produit> GetAll()
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT 
                    p.IdProduit,
                    p.Nom,
                    p.Description,
                    p.CodeBarre,
                    p.Quantite,
                    p.SeuilAlerte,
                    p.IsActif,
                    p.DateAjout,
                    p.IdCategorie,
                    c.Nom AS CategorieNom,
                    p.IdUnite,
                    u.Nom AS UniteNom,
                    p.ImagePath
                FROM Produit p
                LEFT JOIN Categorie c ON p.IdCategorie = c.IdCategorie
                LEFT JOIN Unite u ON p.IdUnite = u.IdUnite;
            ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Produit
                            {
                                IdProduit = Convert.ToInt32(reader["IdProduit"]),
                                Nom = reader["Nom"].ToString(),
                                Description = reader["Description"]?.ToString(),
                                CodeBarre = reader["CodeBarre"]?.ToString(),
                                Quantite = reader["Quantite"] != DBNull.Value
                                           ? Convert.ToInt32(reader["Quantite"])
                                           : 0,
                                SeuilAlerte = reader["SeuilAlerte"] != DBNull.Value
                                              ? Convert.ToInt32(reader["SeuilAlerte"])
                                              : 0,
                                IsActif = reader["IsActif"] != DBNull.Value && Convert.ToInt32(reader["IsActif"]) == 1,
                                DateAjout = reader["DateAjout"] != DBNull.Value
                                            ? DateTime.Parse(reader["DateAjout"].ToString())
                                            : (DateTime?)null,
                                IdCategorie = reader["IdCategorie"] != DBNull.Value
                                              ? Convert.ToInt32(reader["IdCategorie"])
                                              : 0,
                                CategorieNom = reader["CategorieNom"]?.ToString() ?? "",
                                IdUnite = reader["IdUnite"] != DBNull.Value
                                          ? Convert.ToInt32(reader["IdUnite"])
                                          : 0,
                                UniteNom = reader["UniteNom"]?.ToString() ?? "",
                                ImagePath = reader["ImagePath"]?.ToString()
                            };
                        }
                    }
                }
            }
        }
        public Produit GetById(int id)
                {
                    using var conn = new SqliteConnection(_connectionString);
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"SELECT IdProduit, Nom, Description, CodeBarre, Quantite, SeuilAlerte, IsActif, DateAjout, IdCategorie, IdUnite, ImagePath
                                        FROM Produit
                                        WHERE IdProduit = $id";
                    cmd.Parameters.AddWithValue("$id", id);
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Produit
                        {
                            IdProduit = reader.GetInt32(0),
                            Nom = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            CodeBarre = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Quantite = reader.GetInt32(4),
                            SeuilAlerte = reader.GetInt32(5),
                            IsActif = reader.GetInt32(6) == 1,
                            DateAjout = reader.GetDateTime(7),
                            IdCategorie = reader.GetInt32(8),
                            IdUnite = reader.GetInt32(9),
                            ImagePath = reader.IsDBNull(10) ? "" : reader.GetString(10)
                        };
                    }
                    return null;
                }
        public Produit GetByCodeBarre(string codeBarre)
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT IdProduit, Nom, Description, CodeBarre, Quantite, SeuilAlerte, IsActif, DateAjout, IdCategorie, IdUnite, ImagePath
                        FROM Produit
                        WHERE CodeBarre = $codeBarre";
                    cmd.Parameters.AddWithValue("$codeBarre", codeBarre);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Produit
                            {
                                IdProduit = reader.GetInt32(0),
                                Nom = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                CodeBarre = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Quantite = reader.GetInt32(4),
                                SeuilAlerte = reader.GetInt32(5),
                                IsActif = reader.GetInt32(6) == 1,
                                DateAjout = reader.GetDateTime(7),
                                IdCategorie = reader.GetInt32(8),
                                IdUnite = reader.GetInt32(9),
                                ImagePath = reader.IsDBNull(10) ? "" : reader.GetString(10)
                            };
                        }
                    }
                }
            }
            return null;
        }

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


        public int Insert(Produit produit)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Produit (Nom, Description, CodeBarre, Quantite, SeuilAlerte, IsActif, DateAjout, IdCategorie, IdUnite, ImagePath)
                VALUES ($nom, $desc, $code, $qte, $seuil, $actif, $date, $idCat, $idUnite, $img);
                SELECT last_insert_rowid();
            ";
            cmd.Parameters.AddWithValue("$nom", produit.Nom);
            cmd.Parameters.AddWithValue("$desc", produit.Description ?? "");
            cmd.Parameters.AddWithValue("$code", produit.CodeBarre ?? "");
            cmd.Parameters.AddWithValue("$qte", produit.Quantite);
            cmd.Parameters.AddWithValue("$seuil", produit.SeuilAlerte);
            cmd.Parameters.AddWithValue("$actif", produit.IsActif ? 1 : 0);
            cmd.Parameters.AddWithValue("$date", produit.DateAjout);
            cmd.Parameters.AddWithValue("$idCat", produit.IdCategorie);
            cmd.Parameters.AddWithValue("$idUnite", produit.IdUnite);
            cmd.Parameters.AddWithValue("$img", produit.ImagePath ?? "");

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Produit produit)
        {
            if (produit == null)
                throw new ArgumentNullException(nameof(produit));

            // Vérification des valeurs obligatoires
            // Vérification minimale sur l'objet
            if (string.IsNullOrWhiteSpace(produit.Nom))
                throw new Exception("Le nom du produit est obligatoire.");
            if (produit.IdCategorie <= 0)
                throw new Exception("La catégorie du produit est invalide.");
            if (produit.IdUnite <= 0)
                throw new Exception("L'unité du produit est invalide.");

            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Produit
                        SET Nom = @Nom,
                            Description = @Description,
                            CodeBarre = @CodeBarre,
                            Quantite = @Quantite,
                            SeuilAlerte = @SeuilAlerte,
                            IsActif = @IsActif,
                            DateAjout = @DateAjout,
                            IdCategorie = @IdCategorie,
                            IdUnite = @IdUnite,
                            ImagePath = @ImagePath
                        WHERE IdProduit = @IdProduit;
                    ";

                    cmd.Parameters.AddWithValue("@Nom", produit.Nom);
                    cmd.Parameters.AddWithValue("@Description", produit.Description ?? "");
                    cmd.Parameters.AddWithValue("@CodeBarre", produit.CodeBarre ?? "");
                    cmd.Parameters.AddWithValue("@Quantite", produit.Quantite);
                    cmd.Parameters.AddWithValue("@SeuilAlerte", produit.SeuilAlerte);
                    cmd.Parameters.AddWithValue("@IsActif", produit.IsActif ? 1 : 0);
                    cmd.Parameters.AddWithValue("@DateAjout", produit.DateAjout?.ToString("yyyy-MM-dd") ?? DBNull.Value.ToString());
                    cmd.Parameters.AddWithValue("@IdCategorie", produit.IdCategorie);
                    cmd.Parameters.AddWithValue("@IdUnite", produit.IdUnite);
                    cmd.Parameters.AddWithValue("@ImagePath", produit.ImagePath ?? "");
                    cmd.Parameters.AddWithValue("@IdProduit", produit.IdProduit);

                    int affected = cmd.ExecuteNonQuery();
                    if (affected == 0)
                        throw new Exception("Aucune ligne modifiée : le produit n'existe pas.");
                }
            }
        }
        public void Delete(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Produit WHERE IdProduit=$id";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }
    }
}

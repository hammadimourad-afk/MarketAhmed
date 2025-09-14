using System;
using System.IO;
using Microsoft.Data.Sqlite;

using MarketAhmed.Core.Services;

namespace MarketAhmed.Data
{
    public static class Database
    {
        public static readonly string DbFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        public static readonly string DbPath = Path.Combine(DbFolder, "MarketAhmed.db");
        public static readonly string ConnectionString = $"Data Source={DbPath};";

        public static void Initialize()
        {
            // Crée le dossier Data si inexistant
            if (!Directory.Exists(DbFolder))
                Directory.CreateDirectory(DbFolder);

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();

            // Création de toutes les tables
            cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Unite (
                IdUnite INTEGER PRIMARY KEY AUTOINCREMENT,
                Nom TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Categorie (
                IdCategorie INTEGER PRIMARY KEY AUTOINCREMENT,
                Nom TEXT NOT NULL,
                Description TEXT
            );

            CREATE TABLE IF NOT EXISTS Produit (
                IdProduit INTEGER PRIMARY KEY AUTOINCREMENT,
                Nom TEXT NOT NULL,
                Description TEXT,
                CodeBarre TEXT,
                Quantite INTEGER,
                SeuilAlerte INTEGER,
                IsActif INTEGER,
                DateAjout TEXT,
                IdCategorie INTEGER,
                IdUnite INTEGER,
                ImagePath TEXT,
                FOREIGN KEY (IdCategorie) REFERENCES Categorie(IdCategorie),
                FOREIGN KEY (IdUnite) REFERENCES Unite(IdUnite)
            );

            CREATE TABLE IF NOT EXISTS PrixProduit (
                IdPrixProduit INTEGER PRIMARY KEY AUTOINCREMENT,
                IdProduit INTEGER NOT NULL,
                PrixAchat REAL NOT NULL,
                PrixVente REAL NOT NULL,
                DateDebut TEXT NOT NULL,
                DateFin TEXT,
                FOREIGN KEY (IdProduit) REFERENCES Produit(IdProduit)
            );


            CREATE TABLE IF NOT EXISTS Fournisseur (
                IdFournisseur INTEGER PRIMARY KEY AUTOINCREMENT,
                Nom TEXT,
                Adresse TEXT,
                Telephone TEXT,
                Email TEXT,
                DateAjout TEXT
            );

            CREATE TABLE IF NOT EXISTS Client (
                IdClient INTEGER PRIMARY KEY AUTOINCREMENT,
                Nom TEXT NOT NULL,
                Prenom TEXT NOT NULL,
                Adresse TEXT,
                Telephone TEXT,
                Email TEXT UNIQUE NOT NULL,
                StatutCompte TEXT DEFAULT 'Actif',
                DateAjout TEXT DEFAULT (strftime('%Y-%m-%d %H:%M:%S', 'now')),
                DateDerniereModification TEXT DEFAULT (strftime('%Y-%m-%d %H:%M:%S', 'now')),
                Latitude REAL,
                Longitude REAL
            );

            CREATE TABLE IF NOT EXISTS Utilisateur (
                IdUtilisateur INTEGER PRIMARY KEY AUTOINCREMENT,
                Nom TEXT NOT NULL,
                MotDePasse TEXT NOT NULL,
                Role TEXT NOT NULL,
                Actif INTEGER NOT NULL,
                DateAjout TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Achat (
                IdAchat INTEGER PRIMARY KEY AUTOINCREMENT,
                DateAchat TEXT,
                MontantTotal REAL,
                Statut TEXT,
                IdFournisseur INTEGER,
                IdUtilisateur INTEGER,
                FOREIGN KEY (IdFournisseur) REFERENCES Fournisseur(IdFournisseur),
                FOREIGN KEY (IdUtilisateur) REFERENCES Utilisateur(IdUtilisateur)
            );

            CREATE TABLE IF NOT EXISTS AchatDetail (
                IdAchatDetail INTEGER PRIMARY KEY AUTOINCREMENT,
                Quantite INTEGER,
                PrixUnitaire REAL,
                IdAchat INTEGER,
                IdProduit INTEGER,
                FOREIGN KEY (IdAchat) REFERENCES Achat(IdAchat),
                FOREIGN KEY (IdProduit) REFERENCES Produit(IdProduit)
            );

            CREATE TABLE IF NOT EXISTS Vente (
                IdVente INTEGER PRIMARY KEY AUTOINCREMENT,
                DateVente TEXT,
                MontantTotal REAL,
                Statut TEXT,
                ModePaiement TEXT,
                IdClient INTEGER,
                IdUtilisateur INTEGER,
                FOREIGN KEY (IdClient) REFERENCES Client(IdClient),
                FOREIGN KEY (IdUtilisateur) REFERENCES Utilisateur(IdUtilisateur)
            );

            CREATE TABLE IF NOT EXISTS VenteDetail (
                IdVenteDetail INTEGER PRIMARY KEY AUTOINCREMENT,
                Quantite INTEGER,
                PrixUnitaire REAL,
                IdVente INTEGER,
                IdProduit INTEGER,
                FOREIGN KEY (IdVente) REFERENCES Vente(IdVente),
                FOREIGN KEY (IdProduit) REFERENCES Produit(IdProduit)
            );

            -- Correction de la table Commande
            CREATE TABLE IF NOT EXISTS Commande (
                IdCommande INTEGER PRIMARY KEY AUTOINCREMENT,
                DateCommande TEXT NOT NULL,                 -- Correspond à DateTime
                Statut TEXT NOT NULL,                       -- Correspond à StatutCommande (enum)
                MontantTotal REAL NOT NULL,                 -- Correspond à decimal
                AdresseLivraison TEXT,
                AdresseFacturation TEXT,                    -- Nouveau champ
                IdClient INTEGER NOT NULL,
                DateModification TEXT,                      -- Nouveau champ
                FOREIGN KEY (IdClient) REFERENCES Client(IdClient)
            );

            -- Correction de la table CommandeDetail
            CREATE TABLE IF NOT EXISTS CommandeDetail (
                IdCommandeDetail INTEGER PRIMARY KEY AUTOINCREMENT,
                Quantite INTEGER NOT NULL,
                PrixUnitaire REAL NOT NULL,                 -- Correspond à decimal
                IdCommande INTEGER NOT NULL,
                IdProduit INTEGER NOT NULL,
                FOREIGN KEY (IdCommande) REFERENCES Commande(IdCommande),
                FOREIGN KEY (IdProduit) REFERENCES Produit(IdProduit)
            );

            CREATE TABLE IF NOT EXISTS ProduitFournisseur (
                IdProduit INTEGER,
                IdFournisseur INTEGER,
                PrixAchat REAL,
                DelaiLivraison TEXT,
                PRIMARY KEY (IdProduit, IdFournisseur),
                FOREIGN KEY (IdProduit) REFERENCES Produit(IdProduit),
                FOREIGN KEY (IdFournisseur) REFERENCES Fournisseur(IdFournisseur)
            );

            CREATE TABLE IF NOT EXISTS MouvementStock (
                IdMouvement INTEGER PRIMARY KEY AUTOINCREMENT,
                TypeMouvement TEXT,
                Quantite INTEGER,
                DateMouvement TEXT,
                Commentaire TEXT,
                IdProduit INTEGER,
                IdUtilisateur INTEGER,
                FOREIGN KEY (IdProduit) REFERENCES Produit(IdProduit),
                FOREIGN KEY (IdUtilisateur) REFERENCES Utilisateur(IdUtilisateur)
            );

            CREATE TABLE IF NOT EXISTS JournalCaisse (
                IdOpCaisse INTEGER PRIMARY KEY AUTOINCREMENT,
                DateOp TEXT,
                TypeOp TEXT,
                Montant REAL,
                Commentaire TEXT,
                IdUtilisateur INTEGER,
                FOREIGN KEY (IdUtilisateur) REFERENCES Utilisateur(IdUtilisateur)
            );
            ";
            cmd.ExecuteNonQuery();

            // MIGRATIONS pour la table Client
            try
            {
                cmd.CommandText = "PRAGMA table_info(Client);";
                using (var reader = cmd.ExecuteReader())
                {
                    bool hasPrenom = false;
                    bool hasStatutCompte = false;
                    bool hasDateDerniereModification = false;
                    bool hasLatitude = false;
                    bool hasLongitude = false;

                    while (reader.Read())
                    {
                        if (reader.GetString(1) == "Prenom") hasPrenom = true;
                        if (reader.GetString(1) == "StatutCompte") hasStatutCompte = true;
                        if (reader.GetString(1) == "DateDerniereModification") hasDateDerniereModification = true;
                        if (reader.GetString(1) == "Latitude") hasLatitude = true;
                        if (reader.GetString(1) == "Longitude") hasLongitude = true;
                    }
                    reader.Close();

                    if (!hasPrenom)
                    {
                        cmd.CommandText = "ALTER TABLE Client ADD COLUMN Prenom TEXT NOT NULL DEFAULT '';";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Colonne 'Prenom' ajoutée à la table Client.");
                    }
                    if (!hasStatutCompte)
                    {
                        cmd.CommandText = "ALTER TABLE Client ADD COLUMN StatutCompte TEXT DEFAULT 'Actif';";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Colonne 'StatutCompte' ajoutée à la table Client.");
                    }
                    if (!hasDateDerniereModification)
                    {
                        cmd.CommandText = "ALTER TABLE Client ADD COLUMN DateDerniereModification TEXT DEFAULT (strftime('%Y-%m-%d %H:%M:%S', 'now'));";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Colonne 'DateDerniereModification' ajoutée à la table Client.");
                    }
                    if (!hasLatitude)
                    {
                        cmd.CommandText = "ALTER TABLE Client ADD COLUMN Latitude REAL;";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Colonne 'Latitude' ajoutée à la table Client.");
                    }
                    if (!hasLongitude)
                    {
                        cmd.CommandText = "ALTER TABLE Client ADD COLUMN Longitude REAL;";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Colonne 'Longitude' ajoutée à la table Client.");
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Erreur lors de la migration de la table Client: {ex.Message}");
            }

            // MIGRATIONS pour la table Commande
            // Ces migrations sont importantes si la table existe déjà avec l'ancien schéma.
            try
            {
                cmd.CommandText = "PRAGMA table_info(Commande);";
                using (var reader = cmd.ExecuteReader())
                {
                    bool hasAdresseFacturation = false;
                    bool hasDateModification = false;

                    while (reader.Read())
                    {
                        if (reader.GetString(1) == "AdresseFacturation") hasAdresseFacturation = true;
                        if (reader.GetString(1) == "DateModification") hasDateModification = true;
                    }
                    reader.Close();

                    if (!hasAdresseFacturation)
                    {
                        cmd.CommandText = "ALTER TABLE Commande ADD COLUMN AdresseFacturation TEXT;";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Colonne 'AdresseFacturation' ajoutée à la table Commande.");
                    }
                    if (!hasDateModification)
                    {
                        cmd.CommandText = "ALTER TABLE Commande ADD COLUMN DateModification TEXT;";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Colonne 'DateModification' ajoutée à la table Commande.");
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Erreur lors de la migration de la table Commande: {ex.Message}");
            }


            // Insertion de l'utilisateur admin si aucun utilisateur n'existe
            cmd.CommandText = "SELECT COUNT(*) FROM Utilisateur";
            long count = (long)cmd.ExecuteScalar();

            if (count == 0)
            {
                string motDePasseHache = UtilisateurService.HacherMotDePasse("admin");

                cmd.CommandText = @"
                    INSERT INTO Utilisateur (Nom, MotDePasse, Role, Actif, DateAjout)
                    VALUES ('admin', $motDePasse, 'Admin', 1, date('now'))
                ";
                cmd.Parameters.AddWithValue("$motDePasse", motDePasseHache);
                cmd.ExecuteNonQuery();
            }
        }

        public static SqliteConnection GetConnection()
        {
            var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            return conn;
        }
        public static string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
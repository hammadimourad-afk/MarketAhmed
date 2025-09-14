using System;
using System.Windows.Forms;
using MarketAhmed.Core.Services;
using MarketAhmed.Data;
using MarketAhmed.UI;

namespace MarketAhmed
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string connectionString;
            try
            {
                // Initialise et récupère la chaîne de connexion depuis Database.cs
                Database.Initialize();
                connectionString = Database.GetConnectionString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'initialisation de la base : {ex.Message}", "Erreur DB");
                return;
            }

            // Insérer des données de test (optionnel)
            //try
            //{
            //    using var conn = Database.GetConnection();
            //    using var cmd = conn.CreateCommand();

            //    cmd.CommandText = @"INSERT OR IGNORE INTO Unite (IdUnite, Nom) VALUES (1, 'Pièce');";
            //    cmd.ExecuteNonQuery();

            //    cmd.CommandText = @"INSERT OR IGNORE INTO Categorie (IdCategorie, Nom, Description) VALUES (1, 'Catégorie Test', 'Description Test');";
            //    cmd.ExecuteNonQuery();

            //    cmd.CommandText = @"
            //        INSERT OR IGNORE INTO Produit 
            //        (Nom, Description, CodeBarre, Quantite, SeuilAlerte, IsActif, DateAjout, IdCategorie, IdUnite, ImagePath)
            //        VALUES ('Produit Test', 'Description Test', '123456', 10, 5, 1, date('now'), 1, 1, NULL);
            //    ";
            //    cmd.ExecuteNonQuery();

            //    cmd.CommandText = "SELECT COUNT(*) FROM Produit;";
            //    long count = (long)cmd.ExecuteScalar();
            //    MessageBox.Show($"Nombre de produits dans la table : {count}", "Test SQLite");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Erreur SQLite");
            //}

            // Ouverture du login
            FrmLogin frmLogin = new FrmLogin();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                var utilisateur = frmLogin.UtilisateurConnecte;

                // On passe le connectionString au dashboard
                FrmTableauDeBord dashboard = new FrmTableauDeBord(utilisateur, connectionString)
                {
                    IsMdiContainer = true,
                    StartPosition = FormStartPosition.CenterScreen,
                    Size = new System.Drawing.Size(1000, 600),
                        WindowState = FormWindowState.Maximized // ✅ affichage maximisé
                };

                Application.Run(dashboard);
            }
        }
    }
}

using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;
//using MarketAhmed.Core.Repositories;
using MarketAhmed.Core.Services;
using MarketAhmed.Data.Repositories;
using MarketAhmed.UI.Helpers;
using System;
using System.Windows.Forms;

namespace MarketAhmed.UI
{
    public partial class FrmTableauDeBord : Form
    {
        private readonly Utilisateur _utilisateurConnecte;
        private readonly string _connectionString;

        private Form activeForm = null;

        public FrmTableauDeBord(Utilisateur utilisateur, string connectionString)
        {
            InitializeComponent();

            _utilisateurConnecte = utilisateur;
            lblBienvenue.Text = $"Bienvenue {_utilisateurConnecte.Nom} ({_utilisateurConnecte.Role})";

            _connectionString = connectionString;
        }

        // Méthode générique pour ouvrir un formulaire dans panelPrincipal
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelPrincipal.Controls.Add(childForm);
            panelPrincipal.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void BtnProduits_Click(object sender, EventArgs e)
        {
            try
            {
                // ⚠️ Ici il faut créer toutes les instances des repositories
                IProduitRepository produitRepo = new ProduitRepository(_connectionString);
                ICategorieRepository categorieRepo = new CategorieRepository(_connectionString);
                IUniteRepository uniteRepo = new UniteRepository(_connectionString);
                IPrixProduitRepository prixRepo = new PrixProduitRepository(_connectionString);

                // Maintenant on peut créer le service avec **tous les repositories**
                var produitService = new ProduitService(produitRepo, categorieRepo, uniteRepo, prixRepo);
                var _prixService = new PrixProduitService(prixRepo);
                // On passe ce service au formulaire FrmProduit
                FrmProduit frmProduit = new FrmProduit(produitService, _prixService);
                OpenChildForm(frmProduit);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ouverture des produits : {ex.Message}", "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUtilisateurs_Click(object sender, EventArgs e)
        {
            // Exemple pour utilisateurs, à compléter si besoin
            // OpenChildForm(new FrmUtilisateurs(_connectionString));
        }
        // --- NOUVEAU GESTIONNAIRE POUR LES CLIENTS ---
        private void BtnClients_Click(object sender, EventArgs e) // <-- AJOUTEZ CETTE MÉTHODE
        {
            try
            {
                // 1. Instanciez le ClientRepository
                IClientRepository clientRepo = new ClientRepository(_connectionString); // ClientRepository n'a pas besoin de connectionString si Database.GetConnection() est déjà configuré globalement. Si c'est le cas, ne passez pas connectionString.
                                                                       // Si ClientRepository doit recevoir la connectionString, alors : new ClientRepository(_connectionString)

                // 2. Instanciez le ClientService en lui passant le repository
                ClientService clientService = new ClientService(clientRepo);

                // 3. Instanciez le FrmClients en lui passant le service
                FrmClient frmClients = new FrmClient(_connectionString);

                // 4. Ouvrez le formulaire enfant
                OpenChildForm(frmClients);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ouverture des clients : {ex.Message}", "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- NEW HANDLER FOR COMMANDES ---
        // --- CORRECTED HANDLER FOR COMMANDES ---
        private void BtnCommandes_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Instantiate Repositories (assuming NO connection string needed if they use a static helper)
                //    If your repositories *do* need the connection string, you MUST add a constructor
                //    like 'public CommandeRepository(string connectionString)' to each repository class.
                // MODIFICATION POTENTIELLE ICI :
                // Si CommandeRepository et CommandeDetailRepository N'ONT PAS de constructeur prenant string connectionString,
                // alors ils devraient être instanciés sans argument, ou avec un helper statique.
                // Je vais les supposer comme nécessitant la connection string pour être cohérent avec votre correction précédente,
                // mais vérifiez leurs constructeurs.
                ICommandeRepository commandeRepo = new CommandeRepository(_connectionString);
                ICommandeDetailRepository commandeDetailRepo = new CommandeDetailRepository(_connectionString); // Ajouté _connectionString ici si nécessaire
                IClientRepository clientRepo = new ClientRepository(_connectionString); // Ajouté _connectionString ici si nécessaire
                IProduitRepository produitRepo = new ProduitRepository(_connectionString);

                // 2. Instantiate Services
                ClientService clientService = new ClientService(clientRepo);

                ICategorieRepository categorieRepo = new CategorieRepository(_connectionString);
                IUniteRepository uniteRepo = new UniteRepository(_connectionString);
                IPrixProduitRepository prixRepo = new PrixProduitRepository(_connectionString);
                ProduitService produitService = new ProduitService(produitRepo, categorieRepo, uniteRepo, prixRepo);

                // 3. Instantiate CommandeService
                CommandeService commandeService = new CommandeService(commandeRepo, commandeDetailRepo, produitRepo);

                // 4. Instantiate and open the FrmGestionCommandes
                FrmGestionCommandes frmGestionCommandes = new FrmGestionCommandes(commandeService, clientService, produitService);
                OpenChildForm(frmGestionCommandes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ouverture des commandes : {ex.Message}", "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnImporterProduits_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Fichier texte|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var produitRepo = new   ProduitRepository(_connectionString);
                var categorieRepo = new CategorieRepository(_connectionString);
                var uniteRepo = new UniteRepository(_connectionString);
                var prixRepo = new PrixProduitRepository(_connectionString);

                var produitService = new ProduitService(produitRepo, categorieRepo, uniteRepo, prixRepo);

                var import = new ImportProduit(produitService, categorieRepo, uniteRepo);
                import.ImporterDepuisFichier(ofd.FileName);

                MessageBox.Show("Importation terminée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recharger la grille si nécessaire
                //RechargerDataGrid();
            }
        }


    }
}

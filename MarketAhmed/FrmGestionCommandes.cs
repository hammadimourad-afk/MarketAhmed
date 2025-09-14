using MarketAhmed.Core.Models;
using MarketAhmed.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace MarketAhmed.UI
{
    public partial class FrmGestionCommandes : Form
    {
        private readonly ProduitService _produitService;
        private readonly ClientService _clientService;
        private readonly CommandeService _commandeService;

        private Commande _currentCommande;
        private BindingSource bsCommandeDetails;

        // Déclaration des contrôles ajoutés, si ce n'est pas fait dans le Designer.cs
        // private System.Windows.Forms.TextBox txtNomClient; // Déjà présent si vous avez suivi l'étape 2
        // private System.Windows.Forms.Button btnAccederClient; // Déjà présent si vous avez suivi l'étape 2


        public FrmGestionCommandes(CommandeService commandeService, ClientService clientService, ProduitService produitService)
        {
            _commandeService = commandeService;
            _clientService = clientService;
            _produitService = produitService;
            InitializeComponent();
            InitializeCustomControls(); // Appel pour initialiser les contrôles déclarés ici
            InitializeForm();
        }

        private void InitializeForm()
        {
            if (cmbStatut != null)
            {
                cmbStatut.DataSource = Enum.GetValues(typeof(StatutCommande));
            }

            if (cmbClient != null)
            {
                cmbClient.SelectedIndexChanged += cmbClient_SelectedIndexChanged;
                cmbClient.Visible = false; // Cachez le ComboBox client si vous utilisez un TextBox pour l'affichage et un bouton de sélection
            }

            // Assurez-vous que txtNomClient est accessible
            if (txtNomClient != null)
            {
                txtNomClient.Click += txtNomClient_Click; // Pour ouvrir la liste des clients au clic sur le nom
            }


            bsCommandeDetails = new BindingSource();
            if (dgvCommandeDetails != null)
            {
                dgvCommandeDetails.DataSource = bsCommandeDetails;
                ConfigureDataGridView();
            }

            // Attacher les événements pour les deux modes de saisie de produit
            if (txtCodeBarre != null)
            {
                txtCodeBarre.KeyDown += txtCodeBarre_KeyDown;
            }
            if (cmbProduitDetail != null)
            {
                cmbProduitDetail.SelectedIndexChanged += cmbProduitDetail_SelectedIndexChanged;
            }

            SetNewCommandeMode();
            // LoadClientsForLookup(); // Nous n'avons plus besoin de charger tous les clients dans un combobox si on utilise un bouton de sélection
            LoadProductsForLookup(); // Charge tous les produits pour le ComboBox
        }



        private void ConfigureDataGridView()
        {
            if (dgvCommandeDetails == null) return;

            dgvCommandeDetails.AutoGenerateColumns = false;
            dgvCommandeDetails.Columns.Clear();

            dgvCommandeDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IdCommandeDetail", HeaderText = "ID Détail", Visible = false });
            dgvCommandeDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IdProduit", HeaderText = "ID Produit", Visible = false });
            dgvCommandeDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NomProduit", HeaderText = "Produit", ReadOnly = true });
            dgvCommandeDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantite", HeaderText = "Qté" });
            dgvCommandeDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrixUnitaire", HeaderText = "Prix Unitaire", DefaultCellStyle = { Format = "N2", FormatProvider = new System.Globalization.CultureInfo("fr-MA"), NullValue = "0.00 MAD" } });
            dgvCommandeDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalLigne", HeaderText = "Total Ligne", ReadOnly = true, DefaultCellStyle = { Format = "N2", FormatProvider = new System.Globalization.CultureInfo("fr-MA"), NullValue = "0.00 MAD" } });
        }

        // Cette méthode n'est plus utile si nous utilisons un TextBox et un bouton pour la sélection du client
        // private void LoadClientsForLookup()
        // {
        //     if (cmbClient == null) return;

        //     var clients = _clientService.GetAllClients().ToList();
        //     cmbClient.DataSource = clients;
        //     cmbClient.DisplayMember = "NomComplet";
        //     cmbClient.ValueMember = "IdClient";
        // }

        private void cmbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cette méthode peut être déclenchée si vous réutilisez le cmbClient en interne pour la logique
            // Mais l'affichage se fera via txtNomClient.
            if (cmbClient != null && cmbClient.SelectedItem is Client selectedClient)
            {
                if (txtNomClient != null)
                {
                    txtNomClient.Text = selectedClient.NomComplet;
                }
                if (txtAdresseLivraison != null)
                {
                    txtAdresseLivraison.Text = selectedClient.Adresse;
                }
                if (txtAdresseFacturation != null)
                {
                    txtAdresseFacturation.Text = selectedClient.Adresse;
                }
            }
            else
            {
                if (txtNomClient != null)
                {
                    txtNomClient.Clear();
                }
                if (txtAdresseLivraison != null)
                {
                    txtAdresseLivraison.Clear();
                }
                if (txtAdresseFacturation != null)
                {
                    txtAdresseFacturation.Clear();
                }
            }
        }

        private void LoadProductsForLookup()
        {
            if (cmbProduitDetail == null) return;

            var produits = _produitService.GetAllProduits().ToList();
            cmbProduitDetail.DataSource = produits;
            cmbProduitDetail.DisplayMember = "Nom";
            cmbProduitDetail.ValueMember = "IdProduit";
        }

        private void SetNewCommandeMode()
        {
            _currentCommande = new Commande();
            _currentCommande.Details = new List<CommandeDetail>();

            if (txtIdCommande != null) txtIdCommande.Text = "Nouveau";
            if (dtpDateCommande != null) dtpDateCommande.Value = DateTime.Now;
            if (cmbStatut != null) cmbStatut.SelectedItem = StatutCommande.EnAttente;
            if (txtAdresseLivraison != null) txtAdresseLivraison.Clear();
            if (txtAdresseFacturation != null) txtAdresseFacturation.Clear();
            if (txtMontantTotal != null) txtMontantTotal.Text = "0.00 MAD";
            // Mise à jour pour le nouveau champ NomClient et réinitialisation de l'IdClient
            if (txtNomClient != null) txtNomClient.Clear();
            if (cmbClient != null) cmbClient.SelectedIndex = -1; // Pour s'assurer que l'IdClient est réinitialisé

            if (bsCommandeDetails != null)
            {
                bsCommandeDetails.DataSource = _currentCommande.Details;
                bsCommandeDetails.ResetBindings(false);
            }

            EnableCommandeControls(true);
            EnableDetailControls(false);
            ClearDetailInputs(); // Assurez-vous que les champs de détail sont vides
        }

        private void LoadCommande(int idCommande)
        {
            _currentCommande = _commandeService.GetCommandeById(idCommande);
            if (_currentCommande != null)
            {
                // Récupérez le client pour afficher son nom
                var client = _clientService.GetClientById(_currentCommande.IdClient);

                if (txtIdCommande != null) txtIdCommande.Text = _currentCommande.IdCommande.ToString();
                if (dtpDateCommande != null) dtpDateCommande.Value = _currentCommande.DateCommande;
                if (cmbStatut != null) cmbStatut.SelectedItem = _currentCommande.Statut;
                if (txtAdresseLivraison != null) txtAdresseLivraison.Text = _currentCommande.AdresseLivraison;
                if (txtAdresseFacturation != null) txtAdresseFacturation.Text = _currentCommande.AdresseFacturation;
                if (txtMontantTotal != null) txtMontantTotal.Text = _currentCommande.MontantTotal.ToString("N2", new System.Globalization.CultureInfo("fr-MA")) + " MAD";                // Afficher le nom du client et l'IdClient interne
                if (txtNomClient != null) txtNomClient.Text = client?.NomComplet ?? "Client inconnu";
                // Conservez l'IdClient dans l'objet Commande
                // _currentCommande.IdClient est déjà défini par GetCommandeById


                foreach (var detail in _currentCommande.Details)
                {
                    var produit = _produitService.GetProduitById(detail.IdProduit);
                    if (produit != null)
                    {
                        detail.NomProduit = produit.Nom;
                    }
                }

                if (bsCommandeDetails != null)
                {
                    bsCommandeDetails.DataSource = _currentCommande.Details;
                    bsCommandeDetails.ResetBindings(false);
                }

                EnableCommandeControls(true);
                EnableDetailControls(true);
            }
            else
            {
                MessageBox.Show("Commande non trouvée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetNewCommandeMode();
            }
        }

        private void EnableCommandeControls(bool enable)
        {
            if (dtpDateCommande != null) dtpDateCommande.Enabled = enable;
            if (cmbStatut != null) cmbStatut.Enabled = enable;
            if (txtAdresseLivraison != null) txtAdresseLivraison.Enabled = enable;
            if (txtAdresseFacturation != null) txtAdresseFacturation.Enabled = enable;

            // Contrôles client
            if (txtNomClient != null) txtNomClient.Enabled = enable;
            if (btnAccederClient != null) btnAccederClient.Enabled = enable;

            if (btnEnregistrerCommande != null) btnEnregistrerCommande.Enabled = enable;
            if (btnSupprimerCommande != null) btnSupprimerCommande.Enabled = enable && _currentCommande.IdCommande != 0;
        }

        private void EnableDetailControls(bool enable)
        {
            // Les contrôles de saisie de produit (Code Barre et ComboBox) seront gérés séparément
            // btnAjouterLigne sera activé si un produit est sélectionné par un des deux moyens
            if (txtQuantiteDetail != null) txtQuantiteDetail.Enabled = enable;
            if (txtPrixUnitaireDetail != null) txtPrixUnitaireDetail.Enabled = enable;
            if (btnAjouterLigne != null) btnAjouterLigne.Enabled = enable && (cmbProduitDetail.SelectedIndex != -1 || !string.IsNullOrWhiteSpace(txtCodeBarre.Text));
            if (btnModifierLigne != null) btnModifierLigne.Enabled = enable && dgvCommandeDetails != null && dgvCommandeDetails.CurrentRow != null;
            if (btnSupprimerLigne != null) btnSupprimerLigne.Enabled = enable && dgvCommandeDetails != null && dgvCommandeDetails.CurrentRow != null;
        }

        private void RecalculateTotal()
        {
            _currentCommande.MontantTotal = _currentCommande.Details.Sum(d => d.TotalLigne);
            if (txtMontantTotal != null) txtMontantTotal.Text = _currentCommande.MontantTotal.ToString("N2", new System.Globalization.CultureInfo("fr-MA")) + " MAD";
        }

        private void btnNouvelleCommande_Click(object sender, EventArgs e)
        {
            SetNewCommandeMode();
        }

        private void btnEnregistrerCommande_Click(object sender, EventArgs e)
        {
            if (_currentCommande.IdClient == 0) // Vérifiez l'IdClient directement
            {
                MessageBox.Show("Veuillez sélectionner un client.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _currentCommande.DateCommande = dtpDateCommande.Value;
            _currentCommande.Statut = (StatutCommande)cmbStatut.SelectedItem;
            _currentCommande.AdresseLivraison = txtAdresseLivraison.Text;
            _currentCommande.AdresseFacturation = txtAdresseFacturation.Text;
            // _currentCommande.IdClient est déjà défini via la sélection du client

            try
            {
                if (_currentCommande.IdCommande == 0)
                {
                    _commandeService.AddCommande(_currentCommande);
                    MessageBox.Show("Commande ajoutée avec succès!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _commandeService.UpdateCommande(_currentCommande);
                    MessageBox.Show("Commande mise à jour avec succès!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadCommande(_currentCommande.IdCommande);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'enregistrement de la commande: " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimerCommande_Click(object sender, EventArgs e)
        {
            if (_currentCommande.IdCommande == 0) return;

            if (MessageBox.Show("Voulez-vous vraiment supprimer cette commande?", "Confirmer Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _commandeService.DeleteCommande(_currentCommande.IdCommande);
                    MessageBox.Show("Commande supprimée avec succès!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetNewCommandeMode();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la suppression de la commande: " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Nouveau gestionnaire d'événements pour la saisie par code-barres
        private void txtCodeBarre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtCodeBarre.Text))
            {
                string codeBarreSaisi = txtCodeBarre.Text.Trim();
                string codeBarreAvecApostrophe = codeBarreSaisi;
                string codeBarreSansApostrophe = codeBarreSaisi;

                // Si le code-barres saisi ne commence PAS par une apostrophe,
                // créer une version avec apostrophe pour la recherche
                if (!codeBarreSaisi.StartsWith("'"))
                {
                    codeBarreAvecApostrophe = "'" + codeBarreSaisi;
                }
                else // Si le code-barres saisi COMMENCE par une apostrophe,
                     // créer une version sans apostrophe pour la recherche
                {
                    codeBarreSansApostrophe = codeBarreSaisi.Substring(1);
                }

                // Initialiser le produit trouvé à null
                Produit produit = null;

                // 1. Tenter de trouver le produit avec la version "avec apostrophe"
                produit = _produitService.GetProduitByCodeBarre(codeBarreAvecApostrophe);

                // 2. Si non trouvé, tenter de trouver le produit avec la version "sans apostrophe"
                if (produit == null)
                {
                    produit = _produitService.GetProduitByCodeBarre(codeBarreSansApostrophe);
                }

                if (produit != null)
                {
                    // Sélectionner le produit dans le ComboBox pour uniformiser l'affichage
                    if (cmbProduitDetail != null) cmbProduitDetail.SelectedValue = produit.IdProduit;
                    // Les champs quantité et prix seront mis à jour par cmbProduitDetail_SelectedIndexChanged
                    // Ou vous pouvez les mettre à jour directement ici
                    var prixActuel = _produitService.GetPrixActuel(produit.IdProduit);
                    if (txtPrixUnitaireDetail != null) txtPrixUnitaireDetail.Text = (prixActuel?.PrixVente ?? 0M).ToString("N2", new System.Globalization.CultureInfo("fr-MA")); // Le "F2" est remplacé par "N2" et la culture                    if (txtQuantiteDetail != null) txtQuantiteDetail.Text = "1";
                    EnableDetailControls(true);
                    txtCodeBarre.Clear(); // Effacer le champ après la recherche réussie
                    txtQuantiteDetail.Focus(); // Mettre le focus sur la quantité
                }
                else
                {
                    MessageBox.Show("Produit non trouvé avec ce code-barres.", "Erreur de recherche", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearDetailInputs();
                    EnableDetailControls(false);
                }
                e.SuppressKeyPress = true; // Empêche le son "ding"
            }
        }
        private void cmbProduitDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cette méthode est appelée lors de la sélection manuelle dans le ComboBox
            // ou lorsque le ComboBox est mis à jour par la saisie du code-barres.
            if (cmbProduitDetail == null || cmbProduitDetail.SelectedItem == null)
            {
                ClearDetailInputs();
                EnableDetailControls(false);
                return;
            }

            if (cmbProduitDetail.SelectedItem is Produit selectedProduit)
            {
                var prixActuel = _produitService.GetPrixActuel(selectedProduit.IdProduit);
                //if (txtPrixUnitaireDetail != null) txtPrixUnitaireDetail.Text = prixActuel?.PrixVente.ToString("F2") ?? "0.00";
                if (txtPrixUnitaireDetail != null) txtPrixUnitaireDetail.Text = (prixActuel?.PrixVente ?? 0M).ToString("N2", new System.Globalization.CultureInfo("fr-MA")); if (txtQuantiteDetail != null) txtQuantiteDetail.Text = "1";
                EnableDetailControls(true);
                txtQuantiteDetail.Focus(); // Mettre le focus sur la quantité
            }
            else
            {
                ClearDetailInputs();
                EnableDetailControls(false);
            }
        }

        private void btnAjouterLigne_Click(object sender, EventArgs e)
        {
            if (cmbProduitDetail == null || cmbProduitDetail.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un produit ou scanner un code-barres.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtQuantiteDetail == null || !int.TryParse(txtQuantiteDetail.Text, out int quantite) || quantite <= 0 ||
                txtPrixUnitaireDetail == null || !decimal.TryParse(txtPrixUnitaireDetail.Text, out decimal prixUnitaire) || prixUnitaire <= 0)
            {
                MessageBox.Show("Veuillez entrer une quantité et un prix valides.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedProduit = (Produit)cmbProduitDetail.SelectedItem;

            var detail = new CommandeDetail
            {
                IdProduit = selectedProduit.IdProduit,
                Quantite = quantite,
                PrixUnitaire = prixUnitaire,
                NomProduit = selectedProduit.Nom
            };

            _currentCommande.Details.Add(detail);
            if (bsCommandeDetails != null) bsCommandeDetails.ResetBindings(false);
            RecalculateTotal();
            ClearDetailInputs();
            txtCodeBarre.Focus(); // Revenir au champ code-barres pour la prochaine saisie
        }

        private void btnModifierLigne_Click(object sender, EventArgs e)
        {
            if (dgvCommandeDetails == null || dgvCommandeDetails.CurrentRow == null) return;

            var selectedDetail = dgvCommandeDetails.CurrentRow.DataBoundItem as CommandeDetail;
            if (selectedDetail == null) return;

            if (cmbProduitDetail == null || cmbProduitDetail.SelectedItem == null ||
                txtQuantiteDetail == null || !int.TryParse(txtQuantiteDetail.Text, out int quantite) || quantite <= 0 ||
                txtPrixUnitaireDetail == null || !decimal.TryParse(txtPrixUnitaireDetail.Text, out decimal prixUnitaire) || prixUnitaire <= 0)
            {
                MessageBox.Show("Veuillez sélectionner un produit et entrer une quantité et un prix valides pour la modification.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedProduit = (Produit)cmbProduitDetail.SelectedItem;

            selectedDetail.IdProduit = selectedProduit.IdProduit;
            selectedDetail.Quantite = quantite;
            selectedDetail.PrixUnitaire = prixUnitaire;
            selectedDetail.NomProduit = selectedProduit.Nom;

            if (bsCommandeDetails != null) bsCommandeDetails.ResetBindings(false);
            RecalculateTotal();
            ClearDetailInputs();
            txtCodeBarre.Focus();
        }

        private void btnSupprimerLigne_Click(object sender, EventArgs e)
        {
            if (dgvCommandeDetails == null || dgvCommandeDetails.CurrentRow == null) return;

            var selectedDetail = dgvCommandeDetails.CurrentRow.DataBoundItem as CommandeDetail;
            if (selectedDetail != null)
            {
                _currentCommande.Details.Remove(selectedDetail);
                if (bsCommandeDetails != null) bsCommandeDetails.ResetBindings(false);
                RecalculateTotal();
                ClearDetailInputs();
                txtCodeBarre.Focus();
            }
        }

        private void dgvCommandeDetails_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCommandeDetails != null && dgvCommandeDetails.CurrentRow != null)
            {
                var selectedDetail = dgvCommandeDetails.CurrentRow.DataBoundItem as CommandeDetail;
                if (selectedDetail != null)
                {
                    if (cmbProduitDetail != null) cmbProduitDetail.SelectedValue = selectedDetail.IdProduit;
                    if (txtQuantiteDetail != null) txtQuantiteDetail.Text = selectedDetail.Quantite.ToString();
                    if (txtPrixUnitaireDetail != null) txtPrixUnitaireDetail.Text = selectedDetail.PrixUnitaire.ToString("N2", new System.Globalization.CultureInfo("fr-MA")); txtCodeBarre.Clear(); // Efface le champ de code-barres si une ligne est sélectionnée manuellement
                }
            }
            else
            {
                ClearDetailInputs();
                EnableDetailControls(false);
            }
        }

        private void ClearDetailInputs()
        {
            if (cmbProduitDetail != null) cmbProduitDetail.SelectedIndex = -1;
            if (txtQuantiteDetail != null) txtQuantiteDetail.Clear();
            if (txtPrixUnitaireDetail != null) txtPrixUnitaireDetail.Clear();
            if (txtCodeBarre != null) txtCodeBarre.Clear(); // Assurez-vous d'effacer le champ code-barres aussi
        }

        private void btnChargerCommande_Click(object sender, EventArgs e)
        {
            if (txtCommandeIdSearch != null && int.TryParse(txtCommandeIdSearch.Text, out int id))
            {
                LoadCommande(id);
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ID de commande valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nouvelle méthode pour gérer la sélection du client via un formulaire dédié
        private void SelectClient()
        {
            // Ici, vous devriez instancier et afficher votre formulaire de gestion des clients (FrmClient)
            // en mode sélection. Ce formulaire devrait retourner le Client sélectionné.
            using (var frmClientSelection = new FrmClient(_clientService)) // Assurez-vous que FrmClient peut être utilisé pour la sélection
            {
                // Ajoutez une propriété ou une méthode à FrmClient pour indiquer qu'il est en mode sélection
                // et pour récupérer le client sélectionné.
                // Par exemple, vous pourriez avoir un bouton "Sélectionner" dans FrmClient
                // et une propriété publique SelectedClient.
                frmClientSelection.IsSelectionMode = true; // Supposons que vous ayez une telle propriété
                if (frmClientSelection.ShowDialog() == DialogResult.OK)
                {
                    Client selectedClient = frmClientSelection.SelectedClient; // Supposons que vous ayez une propriété SelectedClient
                    if (selectedClient != null)
                    {
                        _currentCommande.IdClient = selectedClient.IdClient;
                        if (txtNomClient != null) txtNomClient.Text = selectedClient.NomComplet;
                        if (txtAdresseLivraison != null) txtAdresseLivraison.Text = selectedClient.Adresse;
                        if (txtAdresseFacturation != null) txtAdresseFacturation.Text = selectedClient.Adresse;
                    }
                }
            }
        }

        // Événement Click pour le bouton "Accéder au client"
        private void btnAccederClient_Click(object sender, EventArgs e)
        {
            SelectClient();
        }

        // Événement Click pour le TextBox NomClient pour ouvrir la sélection
        private void txtNomClient_Click(object sender, EventArgs e)
        {
            SelectClient();
        }
    }
}
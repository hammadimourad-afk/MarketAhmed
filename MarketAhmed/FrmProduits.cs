using MarketAhmed.Core.Models;
using MarketAhmed.Core.Services;
using MarketAhmed.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MarketAhmed.UI
{
    public partial class FrmProduit : Form
    {
        private readonly ProduitService _produitService;
        private readonly PrixProduitService _prixService;

        private BindingSource _bindingSource = new BindingSource();
        // private bool colonnesInitialisees = false; // Plus nécessaire si les colonnes ComboBox ne sont pas utilisées ainsi

        public FrmProduit(ProduitService produitService, PrixProduitService prixService)
        {
            _produitService = produitService ?? throw new ArgumentNullException(nameof(produitService));
            _prixService = prixService;
            InitializeComponent();
            InitializeDataGridView(); // Va créer toutes les colonnes, y compris CategorieNom et UniteNom

            // Charger uniquement les ComboBox
            ChargerCategories();
            ChargerUnites();

            // Recharger DataGridView (il créera les colonnes une seule fois)
            RechargerDataGrid();

            // Événements
            btnAjouter.Click += BtnAjouter_Click;
            btnModifier.Click += BtnModifier_Click;
            btnSupprimer.Click += BtnSupprimer_Click;
            btnNouveau.Click += BtnNouveau_Click;
            dgvProduits.SelectionChanged += DgvProduits_SelectionChanged;
            txtRecherche.TextChanged += TxtRecherche_TextChanged;
            btnGererCategories.Click += BtnCategories_Click;
            btnGererUnites.Click += BtnUnites_Click;
            btnChoisirImage.Click += BtnChoisirImage_Click;
            //btnGererPrix.Click += BtnGererPrix_Click; // Ajouté explicitement l'événement pour Gérer Prix
        }


        #region Chargement ComboBox
        private void ChargerCategories()
        {
            var categories = _produitService.GetAllCategories().ToList();
            cbCategorie.DataSource = categories;
            cbCategorie.DisplayMember = "Nom";
            cbCategorie.ValueMember = "IdCategorie";
        }

        private void ChargerUnites()
        {
            var unites = _produitService.GetAllUnites().ToList();
            cbUnite.DataSource = unites;
            cbUnite.DisplayMember = "Nom";
            cbUnite.ValueMember = "IdUnite";
        }
        #endregion

        #region Rechargement DataGridView
        private void RechargerDataGrid(IEnumerable<Produit>? produits = null, int? selectId = null)
        {
            // 1️⃣ Récupération des produits
            // Assurez-vous que GetAllProduits() retourne des objets Produit
            // qui incluent CategorieNom, UniteNom, PrixAchat et PrixVente.
            var data = (produits == null) ? _produitService.GetAllProduits().ToList() : produits.ToList();

            // 2️⃣ Désactiver le binding temporairement
            dgvProduits.DataSource = null; // Important de nullifier avant de réassigner
            _bindingSource.DataSource = data;
            dgvProduits.DataSource = _bindingSource; // Réassigner le DataSource

            // 3️⃣ Alternance de couleur
            // Note: Ceci doit être fait APRES que le DataSource est lié et que les lignes sont générées.
            bool alternate = false;
            foreach (DataGridViewRow row in dgvProduits.Rows)
            {
                row.DefaultCellStyle.BackColor = alternate ? Color.AliceBlue : Color.White;
                alternate = !alternate;
            }

            // 4️⃣ Sélection automatique
            if (dgvProduits.Rows.Count > 0)
            {
                dgvProduits.ClearSelection();
                DataGridViewRow rowToSelect = null;

                if (selectId.HasValue)
                {
                    rowToSelect = dgvProduits.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r => (r.DataBoundItem as Produit)?.IdProduit == selectId.Value);
                }

                if (rowToSelect == null)
                    rowToSelect = dgvProduits.Rows[0]; // Sélectionne la première ligne si aucune correspondance ou pas d'ID

                if (rowToSelect != null) // S'assurer qu'une ligne a été trouvée
                {
                    rowToSelect.Selected = true;
                    dgvProduits.FirstDisplayedScrollingRowIndex = rowToSelect.Index;

                    // 5️⃣ Remplir les champs du formulaire avec le produit sélectionné
                    RemplirChampsProduit(rowToSelect);
                }
            }
            else
            {
                // Si aucune ligne, vider les champs du formulaire
                BtnNouveau_Click(null, null);
                pictureBoxProduit.Image = null; // Vider l'image
            }
        }

        // Méthode pour remplir les champs du formulaire à partir d'une ligne DataGridView
        private void RemplirChampsProduit(DataGridViewRow row)
        {
            if (row.DataBoundItem is Produit p)
            {
                txtNom.Text = p.Nom;
                txtDescription.Text = p.Description;
                txtCodeBarre.Text = p.CodeBarre;
                cbCategorie.SelectedValue = p.IdCategorie;
                cbUnite.SelectedValue = p.IdUnite;
                nudQuantite.Value = p.Quantite;
                nudSeuilAlerte.Value = p.SeuilAlerte;
                chkIsActif.Checked = p.IsActif;
                dtpDateAjout.Value = p.DateAjout ?? DateTime.Now;

                string path = p.ImagePath;
                // Gérer le cas où le fichier n'existe plus
                pictureBoxProduit.Image = !string.IsNullOrEmpty(path) && File.Exists(path)
                    ? Image.FromFile(path)
                    : null;
                txtImagePath.Text = path ?? "";

                // ⚡ Afficher les prix actuels (s'ils sont des decimal? dans le modèle Produit)
                txtPrixAchat.Text = (p.PrixAchat ?? 0M).ToString("0.00");
                txtPrixVente.Text = (p.PrixVente ?? 0M).ToString("0.00");
            }
        }

        #endregion

        // #region DataGridView ComboBox Columns
        // Cette région est commentée car nous allons utiliser des DataGridViewTextBoxColumn pour CategorieNom et UniteNom
        // #endregion

        #region Boutons
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation de base
                if (string.IsNullOrWhiteSpace(txtNom.Text) || cbCategorie.SelectedValue == null || cbUnite.SelectedValue == null)
                {
                    MessageBox.Show("Le nom, la catégorie et l'unité sont obligatoires.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var produit = new Produit
                {
                    Nom = txtNom.Text,
                    Description = txtDescription.Text,
                    CodeBarre = txtCodeBarre.Text,
                    IdCategorie = (int)cbCategorie.SelectedValue,
                    IdUnite = (int)cbUnite.SelectedValue,
                    Quantite = (int)nudQuantite.Value,
                    SeuilAlerte = (int)nudSeuilAlerte.Value,
                    IsActif = chkIsActif.Checked,
                    DateAjout = dtpDateAjout.Value,
                    ImagePath = txtImagePath.Text
                };

                _produitService.AjouterProduit(produit);
                MessageBox.Show("Produit ajouté avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RechargerDataGrid(selectId: produit.IdProduit); // Sélectionne le nouveau produit
                // Pas besoin d'appeler BtnNouveau_Click ici, RechargerDataGrid s'en charge si vide ou remplit si sélectionné
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout du produit : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (dgvProduits.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un produit à modifier.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvProduits.SelectedRows[0];
            var selectedProduit = row.DataBoundItem as Produit;
            if (selectedProduit == null) return;

            try
            {
                // Validation de base
                if (string.IsNullOrWhiteSpace(txtNom.Text) || cbCategorie.SelectedValue == null || cbUnite.SelectedValue == null)
                {
                    MessageBox.Show("Le nom, la catégorie et l'unité sont obligatoires.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                selectedProduit.Nom = txtNom.Text;
                selectedProduit.Description = txtDescription.Text;
                selectedProduit.CodeBarre = txtCodeBarre.Text;
                selectedProduit.Quantite = (int)nudQuantite.Value;
                selectedProduit.SeuilAlerte = (int)nudSeuilAlerte.Value;
                selectedProduit.IsActif = chkIsActif.Checked;
                selectedProduit.DateAjout = dtpDateAjout.Value;
                selectedProduit.IdCategorie = (int)cbCategorie.SelectedValue;
                selectedProduit.IdUnite = (int)cbUnite.SelectedValue;
                selectedProduit.ImagePath = txtImagePath.Text;

                _produitService.UpdateProduit(selectedProduit);
                MessageBox.Show("Produit modifié avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RechargerDataGrid(selectId: selectedProduit.IdProduit);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la modification du produit : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvProduits.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un produit à supprimer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int idProduit = Convert.ToInt32(dgvProduits.SelectedRows[0].Cells["IdProduit"].Value);
                var confirm = MessageBox.Show("Voulez-vous vraiment supprimer ce produit ? Cette action est irréversible et pourrait affecter d'autres données liées (ex: prix, commandes).", "Confirmer la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    _produitService.SupprimerProduit(idProduit);
                    MessageBox.Show("Produit supprimé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RechargerDataGrid();
                    BtnNouveau_Click(null, null); // Vider les champs après suppression
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression du produit : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            txtNom.Clear();
            txtDescription.Clear();
            txtCodeBarre.Clear();
            if (cbCategorie.Items.Count > 0) cbCategorie.SelectedIndex = 0;
            if (cbUnite.Items.Count > 0) cbUnite.SelectedIndex = 0;
            nudQuantite.Value = 0;
            nudSeuilAlerte.Value = 0;
            chkIsActif.Checked = true;
            dtpDateAjout.Value = DateTime.Now;
            txtImagePath.Clear();
            pictureBoxProduit.Image = null; // Effacer l'image
            txtPrixAchat.Text = "0.00"; // Réinitialiser l'affichage des prix
            txtPrixVente.Text = "0.00";
            dgvProduits.ClearSelection(); // Désélectionner toute ligne dans le DataGridView
        }

        private void BtnCategories_Click(object sender, EventArgs e)
        {
            // Assurez-vous que FrmCategories est correctement initialisé.
            // S'il dépend de ICategorieRepository, il faut le passer en paramètre.
            // Si _produitService.CategorieRepo retourne un ICategorieRepository, c'est bon.
            using var frm = new FrmCategories(_produitService.CategorieRepo);
            frm.ShowDialog();
            ChargerCategories(); // Recharger la liste des catégories après la fermeture du formulaire
            RechargerDataGrid(); // Recharger le DataGridView des produits pour mettre à jour les noms de catégories
        }

        private void BtnUnites_Click(object sender, EventArgs e)
        {
            using var frm = new FrmUnites(_produitService.UniteRepo);
            frm.ShowDialog();
            ChargerUnites(); // Recharger la liste des unités
            RechargerDataGrid(); // Recharger le DataGridView des produits pour mettre à jour les noms d'unités
        }

        private void BtnChoisirImage_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtImagePath.Text = ofd.FileName;
                try
                {
                    pictureBoxProduit.Image = Image.FromFile(ofd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors du chargement de l'image : {ex.Message}", "Erreur Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pictureBoxProduit.Image = null;
                }
            }
        }
        #endregion

        #region Sélection DataGridView
        private void DgvProduits_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProduits.SelectedRows.Count == 0)
            {
                BtnNouveau_Click(null, null); // Vider les champs si plus rien n'est sélectionné
                return;
            }

            // Appeler RemplirChampsProduit pour le produit sélectionné
            RemplirChampsProduit(dgvProduits.SelectedRows[0]);
        }
        #endregion

        private void BtnGererPrix_Click(object sender, EventArgs e)
        {
            if (dgvProduits.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un produit pour gérer ses prix.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Get the IdProduit from the selected row in FrmProduit's DataGridView
            int selectedProductId = Convert.ToInt32(dgvProduits.SelectedRows[0].Cells["IdProduit"].Value);
            //int idProduit = selectedProductId;

            int idProduit = Convert.ToInt32(dgvProduits.SelectedRows[0].Cells["IdProduit"].Value);
            string nomProduit = (dgvProduits.SelectedRows[0].DataBoundItem as Produit)?.Nom ?? "Produit Inconnu";


            using (var frmPrix = new FrmPrixProduit(_produitService, _prixService ,selectedProductId)) // Assurez-vous que FrmPrixProduit peut prendre ces paramètres
            {
                frmPrix.ShowDialog();
            }

            // 🔹 Rafraîchir le DataGridView pour mettre à jour les prix après la fermeture de FrmPrixProduit
            RechargerDataGrid(selectId: idProduit);
        }


        #region Recherche
        private void TxtRecherche_TextChanged(object sender, EventArgs e)
        {
            string filtre = txtRecherche.Text.Trim().ToLower();

            // S'assurer que GetAllProduits() retourne des objets Produit
            // qui ont les propriétés CategorieNom et UniteNom remplies.
            var produitsFiltres = _produitService.GetAllProduits()
                .Where(p => p.Nom.ToLower().Contains(filtre)
                         || (p.CodeBarre != null && p.CodeBarre.ToLower().Contains(filtre))
                         || (p.CategorieNom != null && p.CategorieNom.ToLower().Contains(filtre)) // Assurez-vous que CategorieNom n'est pas null
                         || (p.UniteNom != null && p.UniteNom.ToLower().Contains(filtre)))        // Assurez-vous que UniteNom n'est pas null
                .ToList();

            // Recharger le DataGridView avec la liste filtrée
            RechargerDataGrid(produitsFiltres);
        }
        #endregion

        // Déclarations des contrôles manquants (Designer.cs part)
        private Label label1;
        private Label label2;
    }
}
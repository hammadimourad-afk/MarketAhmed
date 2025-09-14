using System.Globalization;
using MarketAhmed.Core.Models;
using MarketAhmed.Core.Services;
using MarketAhmed.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MarketAhmed.UI
{
    public partial class FrmPrixProduit : Form
    {
        private readonly ProduitService _produitService;
        private readonly PrixProduitService _prixService;

        public FrmPrixProduit(ProduitService produitService, PrixProduitService prixService, int? initialSelectedProductId = null)
        {
            _produitService = produitService;
            _prixService = prixService;

            InitializeComponent();
            dgvPrix.SelectionChanged += DgvPrix_SelectionChanged;

            InitializeDataGridViewPrix(); // ✅ initialisation colonnes
            ChargerProduits();
            // Select the product if an ID was provided
            if (initialSelectedProductId.HasValue)
            {
                SelectionnerProduit(initialSelectedProductId.Value);
            }
        }

        private void InitializeDataGridViewPrix()
        {
            dgvPrix.Columns.Clear();

            dgvPrix.Columns.Add("IdPrixProduit", "ID");
            dgvPrix.Columns.Add("PrixAchat", "Prix d'achat");
            dgvPrix.Columns.Add("PrixVente", "Prix de vente");
            dgvPrix.Columns.Add("DateDebut", "Date début");
            dgvPrix.Columns.Add("DateFin", "Date fin");

            // ✅ mise en forme
            dgvPrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPrix.AllowUserToAddRows = false;
            dgvPrix.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ✅ cacher l'ID
            dgvPrix.Columns["IdPrixProduit"].Visible = false;

            // ✅ format monétaire

            var cultureDH = new CultureInfo("fr-MA"); // Francophone Maroc
            dgvPrix.Columns["PrixAchat"].DefaultCellStyle.Format = "C2";
            dgvPrix.Columns["PrixAchat"].DefaultCellStyle.FormatProvider = cultureDH;

            dgvPrix.Columns["PrixVente"].DefaultCellStyle.Format = "C2";
            dgvPrix.Columns["PrixVente"].DefaultCellStyle.FormatProvider = cultureDH;
        }

        private void ChargerProduits()
        {
            cbProduits.Items.Clear();
            var produits = _produitService.GetAllProduits();
            foreach (var p in produits)
            {
                cbProduits.Items.Add(new ComboBoxItem { Text = p.Nom, Value = p.IdProduit });
            }

            if (cbProduits.Items.Count > 0)
                cbProduits.SelectedIndex = 0;

            cbProduits.SelectedIndexChanged += CbProduits_SelectedIndexChanged;
        }

        private void CbProduits_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChargerPrix();
        }

        private void ChargerPrix()
        {
            dgvPrix.Rows.Clear();
            if (cbProduits.SelectedItem is ComboBoxItem item)
            {
                var prixListe = _prixService.HistoriquePrix(item.Value);
                foreach (var prix in prixListe)
                {
                    dgvPrix.Rows.Add(
                        prix.IdPrixProduit,
                        prix.PrixAchat,
                        prix.PrixVente,
                        prix.DateDebut.ToShortDateString(),
                        prix.DateFin?.ToShortDateString());
                }
            }
        }

        private void DgvPrix_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPrix.SelectedRows.Count > 0)
            {
                var row = dgvPrix.SelectedRows[0];

                // Remplir les TextBox avec le prix sélectionné
                txtPrixAchat.Text = row.Cells["PrixAchat"].Value?.ToString() ?? "0.00 DH";
                txtPrixVente.Text = row.Cells["PrixVente"].Value?.ToString() ?? "0.00 DH";
            }
        }


        private void btnAjouterPrix_Click(object sender, EventArgs e)
        {
            if (cbProduits.SelectedItem is ComboBoxItem item)
            {
                if (!decimal.TryParse(txtPrixAchat.Text, out decimal prixAchat))
                    prixAchat = 0;

                if (!decimal.TryParse(txtPrixVente.Text, out decimal prixVente))
                    prixVente = 0;

                _prixService.AjouterPrix(item.Value, prixAchat, prixVente);
                ChargerPrix();
            }
        }

        private void btnModifierPrix_Click(object sender, EventArgs e)
        {
            if (dgvPrix.SelectedRows.Count > 0)
            {
                int idPrixProduit = Convert.ToInt32(dgvPrix.SelectedRows[0].Cells["IdPrixProduit"].Value);

                if (!decimal.TryParse(txtPrixAchat.Text, out decimal prixAchat))
                    prixAchat = 0;

                if (!decimal.TryParse(txtPrixVente.Text, out decimal prixVente))
                    prixVente = 0;

                _prixService.ModifierPrix(idPrixProduit, prixAchat, prixVente);
                ChargerPrix();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un prix à modifier.");
            }
        }

        private void btnSupprimerPrix_Click(object sender, EventArgs e)
        {
            if (dgvPrix.SelectedRows.Count == 0) return;

            var row = dgvPrix.SelectedRows[0];
            int idPrixProduit = Convert.ToInt32(row.Cells["IdPrixProduit"].Value);

            if (MessageBox.Show("Voulez-vous vraiment supprimer ce prix ?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _prixService.SupprimerPrix(idPrixProduit);  // ✅ appel service
                ChargerPrix();
            }
        }

        public void SelectionnerProduit(int idProduit)
        {
            for (int i = 0; i < cbProduits.Items.Count; i++)
            {
                if (cbProduits.Items[i] is ComboBoxItem item && item.Value == idProduit)
                {
                    cbProduits.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnFermerPrix_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

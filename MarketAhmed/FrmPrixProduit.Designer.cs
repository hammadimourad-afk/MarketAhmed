using System.Windows.Forms;

namespace MarketAhmed.UI
{
    partial class FrmPrixProduit
    {
        private System.ComponentModel.IContainer components = null;

        private ComboBox cbProduits;
        private DataGridView dgvPrix;
        private Button btnAjouterPrix;
        private Button btnModifierPrix;
        private Button btnSupprimerPrix;
        private Button btnFermerPrix;
        private TextBox txtPrixAchat;
        private TextBox txtPrixVente;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbProduits = new ComboBox();
            this.dgvPrix = new DataGridView();
            this.btnAjouterPrix = new Button();
            this.btnModifierPrix = new Button();
            this.btnSupprimerPrix = new Button();
            this.btnFermerPrix = new Button();
            this.txtPrixAchat = new TextBox();
            this.txtPrixVente = new TextBox();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPrix)).BeginInit();
            this.SuspendLayout();

            // cbProduits
            this.cbProduits.Location = new System.Drawing.Point(20, 20);
            this.cbProduits.Size = new System.Drawing.Size(200, 21);
            this.cbProduits.DropDownStyle = ComboBoxStyle.DropDownList;

            // txtPrixAchat
            this.txtPrixAchat.Location = new System.Drawing.Point(240, 20);
            this.txtPrixAchat.Size = new System.Drawing.Size(100, 20);
            this.txtPrixAchat.PlaceholderText = "Prix Achat";

            // txtPrixVente
            this.txtPrixVente.Location = new System.Drawing.Point(360, 20);
            this.txtPrixVente.Size = new System.Drawing.Size(100, 20);
            this.txtPrixVente.PlaceholderText = "Prix Vente";

            // btnAjouterPrix
            this.btnAjouterPrix.Text = "Ajouter";
            this.btnAjouterPrix.Location = new System.Drawing.Point(480, 18);
            this.btnAjouterPrix.Size = new System.Drawing.Size(80, 25);
            this.btnAjouterPrix.Click += new System.EventHandler(this.btnAjouterPrix_Click);

            // btnModifierPrix
            this.btnModifierPrix.Text = "Modifier";
            this.btnModifierPrix.Location = new System.Drawing.Point(570, 18);
            this.btnModifierPrix.Size = new System.Drawing.Size(80, 25);
            this.btnModifierPrix.Click += new System.EventHandler(this.btnModifierPrix_Click);

            // btnSupprimerPrix
            this.btnSupprimerPrix.Text = "Supprimer";
            this.btnSupprimerPrix.Location = new System.Drawing.Point(660, 18);
            this.btnSupprimerPrix.Size = new System.Drawing.Size(80, 25);
            this.btnSupprimerPrix.Click += new System.EventHandler(this.btnSupprimerPrix_Click);

            // dgvPrix
            this.dgvPrix.Location = new System.Drawing.Point(20, 60);
            this.dgvPrix.Size = new System.Drawing.Size(720, 300);
            this.dgvPrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPrix.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrix.ReadOnly = true;
            this.dgvPrix.AllowUserToAddRows = false;

            // btnFermerPrix
            this.btnFermerPrix.Text = "Fermer";
            this.btnFermerPrix.Location = new System.Drawing.Point(640, 370);
            this.btnFermerPrix.Size = new System.Drawing.Size(100, 30);
            this.btnFermerPrix.Click += new System.EventHandler(this.btnFermerPrix_Click);

            // Ajouter au formulaire
            this.Controls.Add(this.cbProduits);
            this.Controls.Add(this.txtPrixAchat);
            this.Controls.Add(this.txtPrixVente);
            this.Controls.Add(this.dgvPrix);
            this.Controls.Add(this.btnAjouterPrix);
            this.Controls.Add(this.btnModifierPrix);
            this.Controls.Add(this.btnSupprimerPrix);
            this.Controls.Add(this.btnFermerPrix);

            // Form properties
            this.ClientSize = new System.Drawing.Size(770, 420);
            this.Text = "Gestion des Prix Produits";
            this.StartPosition = FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.dgvPrix)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

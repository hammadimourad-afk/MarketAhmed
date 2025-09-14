using MarketAhmed.Core.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MarketAhmed.UI
{
    partial class FrmProduit
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelRecherche;
        private TextBox txtRecherche;

        private Panel panelDetails;
        private TextBox txtNom;
        private TextBox txtDescription;
        private TextBox txtCodeBarre;
        //private TextBox txtQuantite;
        //private TextBox txtSeuilAlerte;

        private System.Windows.Forms.TextBox txtPrixAchat;
        private System.Windows.Forms.TextBox txtPrixVente;
        private System.Windows.Forms.Label lblPrixAchat;
        private System.Windows.Forms.Label lblPrixVente;



        private NumericUpDown numQuantite;
        private NumericUpDown numSeuilAlerte;
        private CheckBox chkIsActif;
        private ComboBox cbCategorie;
        private ComboBox cbUnite;
        private PictureBox pictureBoxProduit;
        private Button btnChoisirImage;
        private DateTimePicker dtpDateAjout;
        private Button btnAjouter;
        private Button btnModifier;
        private Button btnSupprimer;
        private Button btnNouveau;
        private Button btnCategories;
        private Button btnUnites;
        private Button btnGererCategories;
        private Button btnGererUnites;

        private Button btnGererPrix;

        private DataGridView dgvProduits;

        private NumericUpDown nudQuantite;
        private NumericUpDown nudSeuilAlerte;
        private TextBox txtImagePath;
        private System.Windows.Forms.Button btnImporterProduits;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelDetails = new Panel();
            label2 = new Label();
            label1 = new Label();
            lblPrixAchat = new Label();
            txtNom = new TextBox();
            txtPrixAchat = new TextBox();
            txtDescription = new TextBox();
            lblPrixVente = new Label();
            txtPrixVente = new TextBox();
            txtCodeBarre = new TextBox();
            nudQuantite = new NumericUpDown();
            nudSeuilAlerte = new NumericUpDown();
            cbCategorie = new ComboBox();
            btnGererCategories = new Button();
            cbUnite = new ComboBox();
            btnGererUnites = new Button();
            chkIsActif = new CheckBox();
            dtpDateAjout = new DateTimePicker();
            txtImagePath = new TextBox();
            pictureBoxProduit = new PictureBox();
            btnChoisirImage = new Button();
            btnAjouter = new Button();
            btnModifier = new Button();
            btnSupprimer = new Button();
            btnNouveau = new Button();
            txtRecherche = new TextBox();
            btnGererPrix = new Button();
            dgvProduits = new DataGridView();
            panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSeuilAlerte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxProduit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProduits).BeginInit();
            SuspendLayout();
            // 
            // panelDetails
            // 
            panelDetails.Controls.Add(label2);
            panelDetails.Controls.Add(label1);
            panelDetails.Controls.Add(lblPrixAchat);
            panelDetails.Controls.Add(txtNom);
            panelDetails.Controls.Add(txtPrixAchat);
            panelDetails.Controls.Add(txtDescription);
            panelDetails.Controls.Add(lblPrixVente);
            panelDetails.Controls.Add(txtPrixVente);
            panelDetails.Controls.Add(txtCodeBarre);
            panelDetails.Controls.Add(nudQuantite);
            panelDetails.Controls.Add(nudSeuilAlerte);
            panelDetails.Controls.Add(cbCategorie);
            panelDetails.Controls.Add(btnGererCategories);
            panelDetails.Controls.Add(cbUnite);
            panelDetails.Controls.Add(btnGererUnites);
            panelDetails.Controls.Add(chkIsActif);
            panelDetails.Controls.Add(dtpDateAjout);
            panelDetails.Controls.Add(txtImagePath);
            panelDetails.Controls.Add(pictureBoxProduit);
            panelDetails.Controls.Add(btnChoisirImage);
            panelDetails.Controls.Add(btnAjouter);
            panelDetails.Controls.Add(btnModifier);
            panelDetails.Controls.Add(btnSupprimer);
            panelDetails.Controls.Add(btnNouveau);
            panelDetails.Controls.Add(txtRecherche);
            panelDetails.Controls.Add(btnGererPrix);
            panelDetails.Dock = DockStyle.Top;
            panelDetails.Location = new Point(0, 0);
            panelDetails.Name = "panelDetails";
            panelDetails.Size = new Size(900, 280);
            panelDetails.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 153);
            label2.Name = "label2";
            label2.Size = new Size(49, 25);
            label2.TabIndex = 21;
            label2.Text = "Seuil";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 120);
            label1.Name = "label1";
            label1.Size = new Size(80, 25);
            label1.TabIndex = 20;
            label1.Text = "Quantité";
            // 
            // lblPrixAchat
            // 
            lblPrixAchat.AutoSize = true;
            lblPrixAchat.Location = new Point(233, 121);
            lblPrixAchat.Name = "lblPrixAchat";
            lblPrixAchat.Size = new Size(102, 25);
            lblPrixAchat.TabIndex = 0;
            lblPrixAchat.Text = "Prix d'achat";
            // 
            // txtNom
            // 
            txtNom.Location = new Point(10, 10);
            txtNom.Name = "txtNom";
            txtNom.PlaceholderText = "Nom";
            txtNom.Size = new Size(200, 31);
            txtNom.TabIndex = 0;
            // 
            // txtPrixAchat
            // 
            txtPrixAchat.Location = new Point(233, 141);
            txtPrixAchat.Name = "txtPrixAchat";
            txtPrixAchat.ReadOnly = true;
            txtPrixAchat.Size = new Size(100, 31);
            txtPrixAchat.TabIndex = 1;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(10, 45);
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Description";
            txtDescription.Size = new Size(200, 31);
            txtDescription.TabIndex = 1;
            // 
            // lblPrixVente
            // 
            lblPrixVente.AutoSize = true;
            lblPrixVente.Location = new Point(353, 121);
            lblPrixVente.Name = "lblPrixVente";
            lblPrixVente.Size = new Size(113, 25);
            lblPrixVente.TabIndex = 2;
            lblPrixVente.Text = "Prix de vente";
            // 
            // txtPrixVente
            // 
            txtPrixVente.Location = new Point(353, 141);
            txtPrixVente.Name = "txtPrixVente";
            txtPrixVente.ReadOnly = true;
            txtPrixVente.Size = new Size(100, 31);
            txtPrixVente.TabIndex = 3;
            // 
            // txtCodeBarre
            // 
            txtCodeBarre.Location = new Point(10, 80);
            txtCodeBarre.Name = "txtCodeBarre";
            txtCodeBarre.PlaceholderText = "CodeBarre";
            txtCodeBarre.Size = new Size(200, 31);
            txtCodeBarre.TabIndex = 2;
            // 
            // nudQuantite
            // 
            nudQuantite.Location = new Point(95, 115);
            nudQuantite.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudQuantite.Name = "nudQuantite";
            nudQuantite.Size = new Size(111, 31);
            nudQuantite.TabIndex = 3;
            // 
            // nudSeuilAlerte
            // 
            nudSeuilAlerte.Location = new Point(95, 150);
            nudSeuilAlerte.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudSeuilAlerte.Name = "nudSeuilAlerte";
            nudSeuilAlerte.Size = new Size(111, 31);
            nudSeuilAlerte.TabIndex = 4;
            // 
            // cbCategorie
            // 
            cbCategorie.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategorie.Location = new Point(220, 10);
            cbCategorie.Name = "cbCategorie";
            cbCategorie.Size = new Size(150, 33);
            cbCategorie.TabIndex = 5;
            // 
            // btnGererCategories
            // 
            btnGererCategories.Location = new Point(370, 10);
            btnGererCategories.Name = "btnGererCategories";
            btnGererCategories.Size = new Size(120, 33);
            btnGererCategories.TabIndex = 6;
            btnGererCategories.Text = "Gérer Catégories";
            // 
            // cbUnite
            // 
            cbUnite.DropDownStyle = ComboBoxStyle.DropDownList;
            cbUnite.Location = new Point(220, 50);
            cbUnite.Name = "cbUnite";
            cbUnite.Size = new Size(150, 33);
            cbUnite.TabIndex = 7;
            // 
            // btnGererUnites
            // 
            btnGererUnites.Location = new Point(370, 50);
            btnGererUnites.Name = "btnGererUnites";
            btnGererUnites.Size = new Size(120, 33);
            btnGererUnites.TabIndex = 8;
            btnGererUnites.Text = "Gérer Unités";
            // 
            // chkIsActif
            // 
            chkIsActif.AutoSize = true;
            chkIsActif.Location = new Point(220, 90);
            chkIsActif.Name = "chkIsActif";
            chkIsActif.Size = new Size(74, 29);
            chkIsActif.TabIndex = 9;
            chkIsActif.Text = "Actif";
            // 
            // dtpDateAjout
            // 
            dtpDateAjout.Format = DateTimePickerFormat.Short;
            dtpDateAjout.Location = new Point(370, 88);
            dtpDateAjout.Name = "dtpDateAjout";
            dtpDateAjout.Size = new Size(150, 31);
            dtpDateAjout.TabIndex = 10;
            // 
            // txtImagePath
            // 
            txtImagePath.Location = new Point(573, 150);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.PlaceholderText = "Chemin image";
            txtImagePath.Size = new Size(200, 31);
            txtImagePath.TabIndex = 11;
            // 
            // pictureBoxProduit
            // 
            pictureBoxProduit.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxProduit.Location = new Point(573, 3);
            pictureBoxProduit.Name = "pictureBoxProduit";
            pictureBoxProduit.Size = new Size(100, 100);
            pictureBoxProduit.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxProduit.TabIndex = 12;
            pictureBoxProduit.TabStop = false;
            // 
            // btnChoisirImage
            // 
            btnChoisirImage.Location = new Point(573, 113);
            btnChoisirImage.Name = "btnChoisirImage";
            btnChoisirImage.Size = new Size(120, 31);
            btnChoisirImage.TabIndex = 13;
            btnChoisirImage.Text = "Choisir Image";
            btnChoisirImage.Click += BtnChoisirImage_Click;
            // 
            // btnAjouter
            // 
            btnAjouter.Location = new Point(10, 224);
            btnAjouter.Name = "btnAjouter";
            btnAjouter.Size = new Size(100, 31);
            btnAjouter.TabIndex = 14;
            btnAjouter.Text = "Ajouter";
            // 
            // btnModifier
            // 
            btnModifier.Location = new Point(120, 224);
            btnModifier.Name = "btnModifier";
            btnModifier.Size = new Size(100, 31);
            btnModifier.TabIndex = 15;
            btnModifier.Text = "Modifier";
            // 
            // btnSupprimer
            // 
            btnSupprimer.Location = new Point(230, 224);
            btnSupprimer.Name = "btnSupprimer";
            btnSupprimer.Size = new Size(100, 31);
            btnSupprimer.TabIndex = 16;
            btnSupprimer.Text = "Supprimer";
            // 
            // btnNouveau
            // 
            btnNouveau.Location = new Point(340, 224);
            btnNouveau.Name = "btnNouveau";
            btnNouveau.Size = new Size(100, 31);
            btnNouveau.TabIndex = 17;
            btnNouveau.Text = "Nouveau";
            // 
            // txtRecherche
            // 
            txtRecherche.Location = new Point(680, 10);
            txtRecherche.Name = "txtRecherche";
            txtRecherche.PlaceholderText = "Recherche...";
            txtRecherche.Size = new Size(200, 31);
            txtRecherche.TabIndex = 18;
            txtRecherche.TextChanged += TxtRecherche_TextChanged;
            // 
            // btnGererPrix
            // 
            btnGererPrix.Location = new Point(459, 130);
            btnGererPrix.Name = "btnGererPrix";
            btnGererPrix.Size = new Size(108, 47);
            btnGererPrix.TabIndex = 19;
            btnGererPrix.Text = "Gérer Prix";
            btnGererPrix.Click += BtnGererPrix_Click;
            // 
            // dgvProduits
            // 
            dgvProduits.AllowUserToAddRows = false;
            dgvProduits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProduits.ColumnHeadersHeight = 34;
            dgvProduits.Dock = DockStyle.Fill;
            dgvProduits.Location = new Point(0, 280);
            dgvProduits.Name = "dgvProduits";
            dgvProduits.RowHeadersWidth = 62;
            dgvProduits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProduits.Size = new Size(900, 320);
            dgvProduits.TabIndex = 0;
            // 
            // FrmProduit
            // 
            ClientSize = new Size(900, 600);
            Controls.Add(dgvProduits);
            Controls.Add(panelDetails);
            Name = "FrmProduit";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestion des Produits";
            panelDetails.ResumeLayout(false);
            panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantite).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSeuilAlerte).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxProduit).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProduits).EndInit();
            ResumeLayout(false);
        }
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private void InitializeDataGridView()
        {
            dgvProduits.Columns.Clear();

            // 🔹 Colonnes visibles
            // Colonnes textuelles
            dgvProduits.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "IdProduit",
                Name = "IdProduit",
                Visible = false // tu peux cacher l'ID
            });

            dgvProduits.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nom",
                DataPropertyName = "Nom",
                Name = "Nom"
            });

            dgvProduits.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Description",
                DataPropertyName = "Description",
                Name = "Description"
            });

            dgvProduits.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Code Barre",
                DataPropertyName = "CodeBarre",
                Name = "CodeBarre"
            });

            // Colonnes ComboBox
            //var categories = _produitService.GetAllCategories().ToList();
            //dgvProduits.Columns.Add(new DataGridViewComboBoxColumn
            //{
            //    HeaderText = "Catégorie",
            //    DataPropertyName = "IdCategorie",
            //    DataSource = categories,
            //    ValueMember = "IdCategorie",
            //    DisplayMember = "Nom",
            //    Name = "IdCategorie"
            //});

            //var unites = _produitService.GetAllUnites().ToList();
            //dgvProduits.Columns.Add(new DataGridViewComboBoxColumn
            //{
            //    HeaderText = "Unité",
            //    DataPropertyName = "IdUnite",
            //    DataSource = unites,
            //    ValueMember = "IdUnite",
            //    DisplayMember = "Nom",
            //    Name = "IdUnite"
            //});

            // Colonnes numériques et booléennes
            dgvProduits.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Quantité",
                DataPropertyName = "Quantite",
                Name = "Quantite"
            });

            dgvProduits.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Seuil Alerte",
                DataPropertyName = "SeuilAlerte",
                Name = "SeuilAlerte"
            });

            dgvProduits.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Actif",
                DataPropertyName = "IsActif",
                Name = "IsActif"
            });

            dgvProduits.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Date Ajout",
                DataPropertyName = "DateAjout",
                Name = "DateAjout"
            });

            dgvProduits.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Image",
                DataPropertyName = "ImagePath",
                Name = "ImagePath",
                Visible = false
            });

            dgvProduits.Columns.Add("PrixAchat", "Prix d'achat");
            dgvProduits.Columns.Add("PrixVente", "Prix de vente");


            dgvProduits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProduits.AllowUserToAddRows = false;
            dgvProduits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        //private Label label1;
        //private Label label2;
    }
}

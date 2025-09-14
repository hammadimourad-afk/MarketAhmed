using System.Linq;
using System.Windows.Forms;

namespace MarketAhmed.UI
{
    partial class FrmGestionCommandes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Déclarations des contrôles
        private System.Windows.Forms.TextBox txtIdCommande;
        private System.Windows.Forms.DateTimePicker dtpDateCommande;
        private System.Windows.Forms.ComboBox cmbStatut;
        private System.Windows.Forms.TextBox txtAdresseLivraison;
        private System.Windows.Forms.TextBox txtAdresseFacturation;
        private System.Windows.Forms.TextBox txtMontantTotal;
        private System.Windows.Forms.ComboBox cmbClient;
        private System.Windows.Forms.DataGridView dgvCommandeDetails;
        private System.Windows.Forms.ComboBox cmbProduitDetail;
        private System.Windows.Forms.TextBox txtQuantiteDetail;
        private System.Windows.Forms.TextBox txtPrixUnitaireDetail;
        private System.Windows.Forms.Button btnEnregistrerCommande;
        private System.Windows.Forms.Button btnSupprimerCommande;
        private System.Windows.Forms.Button btnAjouterLigne;
        private System.Windows.Forms.Button btnModifierLigne;
        private System.Windows.Forms.Button btnSupprimerLigne;
        private System.Windows.Forms.Button btnNouvelleCommande;
        private System.Windows.Forms.Button btnChargerCommande;
        private System.Windows.Forms.TextBox txtCommandeIdSearch;

        // NOUVEAU: Champ pour la saisie du code-barres
        private System.Windows.Forms.TextBox txtCodeBarre;
        private System.Windows.Forms.Label lblCodeBarre;

        private System.Windows.Forms.TextBox txtNomClient;
        private System.Windows.Forms.Button btnAccederClient;

        // Labels pour les champs
        private System.Windows.Forms.Label lblIdCommande;
        private System.Windows.Forms.Label lblDateCommande;
        private System.Windows.Forms.Label lblStatut;
        private System.Windows.Forms.Label lblAdresseLivraison;
        private System.Windows.Forms.Label lblAdresseFacturation;
        private System.Windows.Forms.Label lblMontantTotal;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.Label lblProduitDetail;
        private System.Windows.Forms.Label lblQuantiteDetail;
        private System.Windows.Forms.Label lblPrixUnitaireDetail;
        private System.Windows.Forms.Label lblCommandeIdSearch;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtIdCommande = new TextBox();
            dtpDateCommande = new DateTimePicker();
            cmbStatut = new ComboBox();
            txtAdresseLivraison = new TextBox();
            txtAdresseFacturation = new TextBox();
            txtMontantTotal = new TextBox();
            cmbClient = new ComboBox();
            dgvCommandeDetails = new DataGridView();
            cmbProduitDetail = new ComboBox();
            txtQuantiteDetail = new TextBox();
            txtPrixUnitaireDetail = new TextBox();
            btnEnregistrerCommande = new Button();
            btnSupprimerCommande = new Button();
            btnAjouterLigne = new Button();
            btnModifierLigne = new Button();
            btnSupprimerLigne = new Button();
            btnNouvelleCommande = new Button();
            btnChargerCommande = new Button();
            txtCommandeIdSearch = new TextBox();
            txtCodeBarre = new TextBox();
            lblCodeBarre = new Label();
            lblIdCommande = new Label();
            lblDateCommande = new Label();
            lblStatut = new Label();
            lblAdresseLivraison = new Label();
            lblAdresseFacturation = new Label();
            lblMontantTotal = new Label();
            lblClient = new Label();
            lblProduitDetail = new Label();
            lblQuantiteDetail = new Label();
            lblPrixUnitaireDetail = new Label();
            lblCommandeIdSearch = new Label();
            txtNomClient = new TextBox();
            btnAccederClient = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCommandeDetails).BeginInit();
            SuspendLayout();
            // 
            // txtIdCommande
            // 
            txtIdCommande.Location = new System.Drawing.Point(167, 96);
            txtIdCommande.Margin = new Padding(5, 6, 5, 6);
            txtIdCommande.Name = "txtIdCommande";
            txtIdCommande.ReadOnly = true;
            txtIdCommande.Size = new System.Drawing.Size(247, 31);
            txtIdCommande.TabIndex = 0;
            // 
            // dtpDateCommande
            // 
            dtpDateCommande.Location = new System.Drawing.Point(167, 154);
            dtpDateCommande.Margin = new Padding(5, 6, 5, 6);
            dtpDateCommande.Name = "dtpDateCommande";
            dtpDateCommande.Size = new System.Drawing.Size(331, 31);
            dtpDateCommande.TabIndex = 1;
            // 
            // cmbStatut
            // 
            cmbStatut.FormattingEnabled = true;
            cmbStatut.Location = new System.Drawing.Point(167, 212);
            cmbStatut.Margin = new Padding(5, 6, 5, 6);
            cmbStatut.Name = "cmbStatut";
            cmbStatut.Size = new System.Drawing.Size(247, 33);
            cmbStatut.TabIndex = 2;
            // 
            // txtAdresseLivraison
            // 
            txtAdresseLivraison.Location = new System.Drawing.Point(700, 96);
            txtAdresseLivraison.Margin = new Padding(5, 6, 5, 6);
            txtAdresseLivraison.Name = "txtAdresseLivraison";
            txtAdresseLivraison.Size = new System.Drawing.Size(581, 31);
            txtAdresseLivraison.TabIndex = 4;
            // 
            // txtAdresseFacturation
            // 
            txtAdresseFacturation.Location = new System.Drawing.Point(700, 154);
            txtAdresseFacturation.Margin = new Padding(5, 6, 5, 6);
            txtAdresseFacturation.Name = "txtAdresseFacturation";
            txtAdresseFacturation.Size = new System.Drawing.Size(581, 31);
            txtAdresseFacturation.TabIndex = 5;
            // 
            // txtMontantTotal
            // 
            txtMontantTotal.Location = new System.Drawing.Point(700, 212);
            txtMontantTotal.Margin = new Padding(5, 6, 5, 6);
            txtMontantTotal.Name = "txtMontantTotal";
            txtMontantTotal.ReadOnly = true;
            txtMontantTotal.Size = new System.Drawing.Size(247, 31);
            txtMontantTotal.TabIndex = 6;
            // 
            // cmbClient
            // 
            cmbClient.FormattingEnabled = true;
            cmbClient.Location = new System.Drawing.Point(1177, 260);
            cmbClient.Margin = new Padding(5, 6, 5, 6);
            cmbClient.Name = "cmbClient";
            cmbClient.Size = new System.Drawing.Size(116, 33);
            cmbClient.TabIndex = 3;
            // 
            // dgvCommandeDetails
            // 
            dgvCommandeDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCommandeDetails.Location = new System.Drawing.Point(33, 538);
            dgvCommandeDetails.Margin = new Padding(5, 6, 5, 6);
            dgvCommandeDetails.Name = "dgvCommandeDetails";
            dgvCommandeDetails.RowHeadersWidth = 62;
            dgvCommandeDetails.Size = new System.Drawing.Size(1250, 288);
            dgvCommandeDetails.TabIndex = 17;
            dgvCommandeDetails.SelectionChanged += dgvCommandeDetails_SelectionChanged;
            // 
            // cmbProduitDetail
            // 
            cmbProduitDetail.FormattingEnabled = true;
            cmbProduitDetail.Location = new System.Drawing.Point(195, 398);
            cmbProduitDetail.Margin = new Padding(5, 6, 5, 6);
            cmbProduitDetail.Name = "cmbProduitDetail";
            cmbProduitDetail.Size = new System.Drawing.Size(303, 33);
            cmbProduitDetail.TabIndex = 9;
            // 
            // txtQuantiteDetail
            // 
            txtQuantiteDetail.Location = new System.Drawing.Point(633, 398);
            txtQuantiteDetail.Margin = new Padding(5, 6, 5, 6);
            txtQuantiteDetail.Name = "txtQuantiteDetail";
            txtQuantiteDetail.Size = new System.Drawing.Size(114, 31);
            txtQuantiteDetail.TabIndex = 11;
            // 
            // txtPrixUnitaireDetail
            // 
            txtPrixUnitaireDetail.Location = new System.Drawing.Point(883, 398);
            txtPrixUnitaireDetail.Margin = new Padding(5, 6, 5, 6);
            txtPrixUnitaireDetail.Name = "txtPrixUnitaireDetail";
            txtPrixUnitaireDetail.Size = new System.Drawing.Size(114, 31);
            txtPrixUnitaireDetail.TabIndex = 13;
            // 
            // btnEnregistrerCommande
            // 
            btnEnregistrerCommande.Location = new System.Drawing.Point(300, 865);
            btnEnregistrerCommande.Margin = new Padding(5, 6, 5, 6);
            btnEnregistrerCommande.Name = "btnEnregistrerCommande";
            btnEnregistrerCommande.Size = new System.Drawing.Size(250, 58);
            btnEnregistrerCommande.TabIndex = 19;
            btnEnregistrerCommande.Text = "Enregistrer Commande";
            btnEnregistrerCommande.UseVisualStyleBackColor = true;
            btnEnregistrerCommande.Click += btnEnregistrerCommande_Click;
            // 
            // btnSupprimerCommande
            // 
            btnSupprimerCommande.Location = new System.Drawing.Point(567, 865);
            btnSupprimerCommande.Margin = new Padding(5, 6, 5, 6);
            btnSupprimerCommande.Name = "btnSupprimerCommande";
            btnSupprimerCommande.Size = new System.Drawing.Size(250, 58);
            btnSupprimerCommande.TabIndex = 20;
            btnSupprimerCommande.Text = "Supprimer Commande";
            btnSupprimerCommande.UseVisualStyleBackColor = true;
            btnSupprimerCommande.Click += btnSupprimerCommande_Click;
            // 
            // btnAjouterLigne
            // 
            btnAjouterLigne.Location = new System.Drawing.Point(33, 462);
            btnAjouterLigne.Margin = new Padding(5, 6, 5, 6);
            btnAjouterLigne.Name = "btnAjouterLigne";
            btnAjouterLigne.Size = new System.Drawing.Size(200, 58);
            btnAjouterLigne.TabIndex = 14;
            btnAjouterLigne.Text = "Ajouter Ligne";
            btnAjouterLigne.UseVisualStyleBackColor = true;
            btnAjouterLigne.Click += btnAjouterLigne_Click;
            // 
            // btnModifierLigne
            // 
            btnModifierLigne.Location = new System.Drawing.Point(242, 462);
            btnModifierLigne.Margin = new Padding(5, 6, 5, 6);
            btnModifierLigne.Name = "btnModifierLigne";
            btnModifierLigne.Size = new System.Drawing.Size(200, 58);
            btnModifierLigne.TabIndex = 15;
            btnModifierLigne.Text = "Modifier Ligne";
            btnModifierLigne.UseVisualStyleBackColor = true;
            btnModifierLigne.Click += btnModifierLigne_Click;
            // 
            // btnSupprimerLigne
            // 
            btnSupprimerLigne.Location = new System.Drawing.Point(450, 462);
            btnSupprimerLigne.Margin = new Padding(5, 6, 5, 6);
            btnSupprimerLigne.Name = "btnSupprimerLigne";
            btnSupprimerLigne.Size = new System.Drawing.Size(200, 58);
            btnSupprimerLigne.TabIndex = 16;
            btnSupprimerLigne.Text = "Supprimer Ligne";
            btnSupprimerLigne.UseVisualStyleBackColor = true;
            btnSupprimerLigne.Click += btnSupprimerLigne_Click;
            // 
            // btnNouvelleCommande
            // 
            btnNouvelleCommande.Location = new System.Drawing.Point(33, 865);
            btnNouvelleCommande.Margin = new Padding(5, 6, 5, 6);
            btnNouvelleCommande.Name = "btnNouvelleCommande";
            btnNouvelleCommande.Size = new System.Drawing.Size(250, 58);
            btnNouvelleCommande.TabIndex = 18;
            btnNouvelleCommande.Text = "Nouvelle Commande";
            btnNouvelleCommande.UseVisualStyleBackColor = true;
            btnNouvelleCommande.Click += btnNouvelleCommande_Click;
            // 
            // btnChargerCommande
            // 
            btnChargerCommande.Location = new System.Drawing.Point(1158, 865);
            btnChargerCommande.Margin = new Padding(5, 6, 5, 6);
            btnChargerCommande.Name = "btnChargerCommande";
            btnChargerCommande.Size = new System.Drawing.Size(125, 58);
            btnChargerCommande.TabIndex = 23;
            btnChargerCommande.Text = "Charger";
            btnChargerCommande.UseVisualStyleBackColor = true;
            btnChargerCommande.Click += btnChargerCommande_Click;
            // 
            // txtCommandeIdSearch
            // 
            txtCommandeIdSearch.Location = new System.Drawing.Point(1025, 875);
            txtCommandeIdSearch.Margin = new Padding(5, 6, 5, 6);
            txtCommandeIdSearch.Name = "txtCommandeIdSearch";
            txtCommandeIdSearch.Size = new System.Drawing.Size(114, 31);
            txtCommandeIdSearch.TabIndex = 22;
            // 
            // txtCodeBarre
            // 
            txtCodeBarre.Location = new System.Drawing.Point(167, 340);
            txtCodeBarre.Margin = new Padding(5, 6, 5, 6);
            txtCodeBarre.Name = "txtCodeBarre";
            txtCodeBarre.Size = new System.Drawing.Size(247, 31);
            txtCodeBarre.TabIndex = 7;
            // 
            // lblCodeBarre
            // 
            lblCodeBarre.AutoSize = true;
            lblCodeBarre.Location = new System.Drawing.Point(33, 346);
            lblCodeBarre.Margin = new Padding(5, 0, 5, 0);
            lblCodeBarre.Name = "lblCodeBarre";
            lblCodeBarre.Size = new System.Drawing.Size(103, 25);
            lblCodeBarre.TabIndex = 7;
            lblCodeBarre.Text = "Code Barre:";
            // 
            // lblIdCommande
            // 
            lblIdCommande.AutoSize = true;
            lblIdCommande.Location = new System.Drawing.Point(33, 102);
            lblIdCommande.Margin = new Padding(5, 0, 5, 0);
            lblIdCommande.Name = "lblIdCommande";
            lblIdCommande.Size = new System.Drawing.Size(132, 25);
            lblIdCommande.TabIndex = 0;
            lblIdCommande.Text = "ID Commande:";
            // 
            // lblDateCommande
            // 
            lblDateCommande.AutoSize = true;
            lblDateCommande.Location = new System.Drawing.Point(33, 160);
            lblDateCommande.Margin = new Padding(5, 0, 5, 0);
            lblDateCommande.Name = "lblDateCommande";
            lblDateCommande.Size = new System.Drawing.Size(151, 25);
            lblDateCommande.TabIndex = 1;
            lblDateCommande.Text = "Date Commande:";
            // 
            // lblStatut
            // 
            lblStatut.AutoSize = true;
            lblStatut.Location = new System.Drawing.Point(33, 217);
            lblStatut.Margin = new Padding(5, 0, 5, 0);
            lblStatut.Name = "lblStatut";
            lblStatut.Size = new System.Drawing.Size(62, 25);
            lblStatut.TabIndex = 2;
            lblStatut.Text = "Statut:";
            // 
            // lblAdresseLivraison
            // 
            lblAdresseLivraison.AutoSize = true;
            lblAdresseLivraison.Location = new System.Drawing.Point(533, 102);
            lblAdresseLivraison.Margin = new Padding(5, 0, 5, 0);
            lblAdresseLivraison.Name = "lblAdresseLivraison";
            lblAdresseLivraison.Size = new System.Drawing.Size(153, 25);
            lblAdresseLivraison.TabIndex = 4;
            lblAdresseLivraison.Text = "Adresse Livraison:";
            // 
            // lblAdresseFacturation
            // 
            lblAdresseFacturation.AutoSize = true;
            lblAdresseFacturation.Location = new System.Drawing.Point(533, 160);
            lblAdresseFacturation.Margin = new Padding(5, 0, 5, 0);
            lblAdresseFacturation.Name = "lblAdresseFacturation";
            lblAdresseFacturation.Size = new System.Drawing.Size(171, 25);
            lblAdresseFacturation.TabIndex = 5;
            lblAdresseFacturation.Text = "Adresse Facturation:";
            // 
            // lblMontantTotal
            // 
            lblMontantTotal.AutoSize = true;
            lblMontantTotal.Location = new System.Drawing.Point(533, 217);
            lblMontantTotal.Margin = new Padding(5, 0, 5, 0);
            lblMontantTotal.Name = "lblMontantTotal";
            lblMontantTotal.Size = new System.Drawing.Size(126, 25);
            lblMontantTotal.TabIndex = 6;
            lblMontantTotal.Text = "Montant Total:";
            // 
            // lblClient
            // 
            lblClient.AutoSize = true;
            lblClient.Location = new System.Drawing.Point(33, 275);
            lblClient.Margin = new Padding(5, 0, 5, 0);
            lblClient.Name = "lblClient";
            lblClient.Size = new System.Drawing.Size(60, 25);
            lblClient.TabIndex = 3;
            lblClient.Text = "Client:";
            // 
            // lblProduitDetail
            // 
            lblProduitDetail.AutoSize = true;
            lblProduitDetail.Location = new System.Drawing.Point(33, 404);
            lblProduitDetail.Margin = new Padding(5, 0, 5, 0);
            lblProduitDetail.Name = "lblProduitDetail";
            lblProduitDetail.Size = new System.Drawing.Size(166, 25);
            lblProduitDetail.TabIndex = 8;
            lblProduitDetail.Text = "Produit (ou choisir):";
            // 
            // lblQuantiteDetail
            // 
            lblQuantiteDetail.AutoSize = true;
            lblQuantiteDetail.Location = new System.Drawing.Point(533, 404);
            lblQuantiteDetail.Margin = new Padding(5, 0, 5, 0);
            lblQuantiteDetail.Name = "lblQuantiteDetail";
            lblQuantiteDetail.Size = new System.Drawing.Size(84, 25);
            lblQuantiteDetail.TabIndex = 10;
            lblQuantiteDetail.Text = "Quantité:";
            // 
            // lblPrixUnitaireDetail
            // 
            lblPrixUnitaireDetail.AutoSize = true;
            lblPrixUnitaireDetail.Location = new System.Drawing.Point(767, 404);
            lblPrixUnitaireDetail.Margin = new Padding(5, 0, 5, 0);
            lblPrixUnitaireDetail.Name = "lblPrixUnitaireDetail";
            lblPrixUnitaireDetail.Size = new System.Drawing.Size(109, 25);
            lblPrixUnitaireDetail.TabIndex = 12;
            lblPrixUnitaireDetail.Text = "Prix Unitaire:";
            // 
            // lblCommandeIdSearch
            // 
            lblCommandeIdSearch.AutoSize = true;
            lblCommandeIdSearch.Location = new System.Drawing.Point(867, 881);
            lblCommandeIdSearch.Margin = new Padding(5, 0, 5, 0);
            lblCommandeIdSearch.Name = "lblCommandeIdSearch";
            lblCommandeIdSearch.Size = new System.Drawing.Size(124, 25);
            lblCommandeIdSearch.TabIndex = 21;
            lblCommandeIdSearch.Text = "Rechercher ID:";
            // 
            // txtNomClient
            // 
            txtNomClient.Location = new System.Drawing.Point(167, 275);
            txtNomClient.Name = "txtNomClient";
            txtNomClient.ReadOnly = true;
            txtNomClient.Size = new System.Drawing.Size(349, 31);
            txtNomClient.TabIndex = 2;
            // 
            // btnAccederClient
            // 
            btnAccederClient.Location = new System.Drawing.Point(553, 279);
            btnAccederClient.Name = "btnAccederClient";
            btnAccederClient.Size = new System.Drawing.Size(30, 23);
            btnAccederClient.TabIndex = 3;
            btnAccederClient.Text = "...";
            btnAccederClient.UseVisualStyleBackColor = true;
            btnAccederClient.Click += btnAccederClient_Click;
            // 
            // FrmGestionCommandes
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1333, 962);
            Controls.Add(btnAccederClient);
            Controls.Add(txtNomClient);
            Controls.Add(lblCommandeIdSearch);
            Controls.Add(txtCommandeIdSearch);
            Controls.Add(btnChargerCommande);
            Controls.Add(btnNouvelleCommande);
            Controls.Add(btnSupprimerCommande);
            Controls.Add(btnEnregistrerCommande);
            Controls.Add(btnSupprimerLigne);
            Controls.Add(btnModifierLigne);
            Controls.Add(btnAjouterLigne);
            Controls.Add(lblPrixUnitaireDetail);
            Controls.Add(txtPrixUnitaireDetail);
            Controls.Add(lblQuantiteDetail);
            Controls.Add(txtQuantiteDetail);
            Controls.Add(lblProduitDetail);
            Controls.Add(cmbProduitDetail);
            Controls.Add(lblCodeBarre);
            Controls.Add(txtCodeBarre);
            Controls.Add(dgvCommandeDetails);
            Controls.Add(lblMontantTotal);
            Controls.Add(txtMontantTotal);
            Controls.Add(lblAdresseFacturation);
            Controls.Add(txtAdresseFacturation);
            Controls.Add(lblAdresseLivraison);
            Controls.Add(txtAdresseLivraison);
            Controls.Add(lblClient);
            Controls.Add(cmbClient);
            Controls.Add(lblStatut);
            Controls.Add(cmbStatut);
            Controls.Add(lblDateCommande);
            Controls.Add(dtpDateCommande);
            Controls.Add(lblIdCommande);
            Controls.Add(txtIdCommande);
            Margin = new Padding(5, 6, 5, 6);
            Name = "FrmGestionCommandes";
            Text = "Gestion des Commandes";
            ((System.ComponentModel.ISupportInitialize)dgvCommandeDetails).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        // La méthode InitializeCustomControls() n'est plus nécessaire si InitializeComponent() est complet.
        // J'ai transféré toutes les initialisations et l'attachement des événements directement
        // dans InitializeComponent() pour une meilleure conformité avec le fonctionnement du designer.
        private void InitializeCustomControls()
        {
            // Cette méthode est maintenant vide ou peut être supprimée,
            // car InitializeComponent() gère toutes les initialisations et l'attachement des événements.
            // Si vous aviez des contrôles ajoutés dynamiquement ou une logique très spécifique
            // non gérée par le designer, elle pourrait encore servir.
            // Pour ce scénario, nous nous appuyons sur InitializeComponent().
        }


        #endregion
    }
}
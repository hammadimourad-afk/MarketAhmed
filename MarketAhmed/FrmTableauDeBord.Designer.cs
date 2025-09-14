namespace MarketAhmed.UI
{
    partial class FrmTableauDeBord
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Label lblBienvenue;
        private FontAwesome.Sharp.IconButton btnProduits;
        private FontAwesome.Sharp.IconButton btnUtilisateurs;
        private FontAwesome.Sharp.IconButton btnImporterProduits;
        private FontAwesome.Sharp.IconButton btnClients;
        private FontAwesome.Sharp.IconButton btnCommandes; // <--- ADD THIS LINE



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelMenu = new System.Windows.Forms.Panel();
            btnClients = new FontAwesome.Sharp.IconButton();
            btnCommandes = new FontAwesome.Sharp.IconButton(); // <--- ADD THIS LINE
            btnUtilisateurs = new FontAwesome.Sharp.IconButton();
            btnProduits = new FontAwesome.Sharp.IconButton();
            panelPrincipal = new System.Windows.Forms.Panel();
            lblBienvenue = new System.Windows.Forms.Label();
            btnImporterProduits = new FontAwesome.Sharp.IconButton();
            panelMenu.SuspendLayout();
            panelPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = System.Drawing.Color.FromArgb(51, 51, 76);
            panelMenu.Controls.Add(btnCommandes); // <--- ADD THIS LINE (Order it where you want it in the menu)
            panelMenu.Controls.Add(btnClients);
            panelMenu.Controls.Add(btnUtilisateurs);
            panelMenu.Controls.Add(btnProduits);
            panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            panelMenu.Location = new System.Drawing.Point(0, 0);
            panelMenu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new System.Drawing.Size(250, 938);
            panelMenu.TabIndex = 2;

            //
            // btnCommandes // <--- ADD THIS BLOCK
            //
            btnCommandes.Dock = System.Windows.Forms.DockStyle.Top;
            btnCommandes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCommandes.ForeColor = System.Drawing.Color.Gainsboro;
            btnCommandes.IconChar = FontAwesome.Sharp.IconChar.ShoppingCart; // Or another suitable icon
            btnCommandes.IconColor = System.Drawing.Color.Gainsboro;
            btnCommandes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCommandes.Location = new System.Drawing.Point(0, 234); // Adjust location based on other buttons
            btnCommandes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnCommandes.Name = "btnCommandes";
            btnCommandes.Size = new System.Drawing.Size(250, 78);
            btnCommandes.TabIndex = 3; // Adjust TabIndex
            btnCommandes.Text = "  Commandes";
            btnCommandes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnCommandes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnCommandes.UseVisualStyleBackColor = true;
            btnCommandes.Click += BtnCommandes_Click; // <--- ADD THIS EVENT HANDLER

            // 
            // btnClients
            // 
            btnClients.Dock = System.Windows.Forms.DockStyle.Top;
            btnClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClients.ForeColor = System.Drawing.Color.Gainsboro;
            btnClients.IconChar = FontAwesome.Sharp.IconChar.Users;
            btnClients.IconColor = System.Drawing.Color.Gainsboro;
            btnClients.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClients.Location = new System.Drawing.Point(0, 156);
            btnClients.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnClients.Name = "btnClients";
            btnClients.Size = new System.Drawing.Size(250, 78);
            btnClients.TabIndex = 2;
            btnClients.Text = "  Clients";
            btnClients.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnClients.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnClients.UseVisualStyleBackColor = true;
            btnClients.Click += BtnClients_Click;
            // 
            // btnUtilisateurs
            // 
            btnUtilisateurs.Dock = System.Windows.Forms.DockStyle.Top;
            btnUtilisateurs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnUtilisateurs.ForeColor = System.Drawing.Color.Gainsboro;
            btnUtilisateurs.IconChar = FontAwesome.Sharp.IconChar.UserShield;
            btnUtilisateurs.IconColor = System.Drawing.Color.Gainsboro;
            btnUtilisateurs.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnUtilisateurs.Location = new System.Drawing.Point(0, 78);
            btnUtilisateurs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnUtilisateurs.Name = "btnUtilisateurs";
            btnUtilisateurs.Size = new System.Drawing.Size(250, 78);
            btnUtilisateurs.TabIndex = 0;
            btnUtilisateurs.Text = "  Utilisateurs";
            btnUtilisateurs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnUtilisateurs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnUtilisateurs.Click += BtnUtilisateurs_Click;
            // 
            // btnProduits
            // 
            btnProduits.Dock = System.Windows.Forms.DockStyle.Top;
            btnProduits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnProduits.ForeColor = System.Drawing.Color.Gainsboro;
            btnProduits.IconChar = FontAwesome.Sharp.IconChar.Box;
            btnProduits.IconColor = System.Drawing.Color.Gainsboro;
            btnProduits.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnProduits.Location = new System.Drawing.Point(0, 0);
            btnProduits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnProduits.Name = "btnProduits";
            btnProduits.Size = new System.Drawing.Size(250, 78);
            btnProduits.TabIndex = 1;
            btnProduits.Text = "  Produits";
            btnProduits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnProduits.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnProduits.Click += BtnProduits_Click;
            // 
            // panelPrincipal
            // 
            panelPrincipal.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            panelPrincipal.Controls.Add(lblBienvenue);
            panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            panelPrincipal.Location = new System.Drawing.Point(250, 0);
            panelPrincipal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            panelPrincipal.Name = "panelPrincipal";
            panelPrincipal.Size = new System.Drawing.Size(1000, 938);
            panelPrincipal.TabIndex = 1;
            // 
            // lblBienvenue
            // 
            lblBienvenue.Dock = System.Windows.Forms.DockStyle.Top;
            lblBienvenue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblBienvenue.Location = new System.Drawing.Point(0, 0);
            lblBienvenue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblBienvenue.Name = "lblBienvenue";
            lblBienvenue.Padding = new System.Windows.Forms.Padding(12, 16, 0, 0);
            lblBienvenue.Size = new System.Drawing.Size(1000, 62);
            lblBienvenue.TabIndex = 0;
            lblBienvenue.Text = "Bienvenue [Utilisateur]";
            // 
            // btnImporterProduits
            // 
            btnImporterProduits.IconChar = FontAwesome.Sharp.IconChar.None;
            btnImporterProduits.IconColor = System.Drawing.Color.Black;
            btnImporterProduits.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnImporterProduits.Location = new System.Drawing.Point(25, 383);
            btnImporterProduits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnImporterProduits.Name = "btnImporterProduits";
            btnImporterProduits.Size = new System.Drawing.Size(188, 47);
            btnImporterProduits.TabIndex = 0;
            btnImporterProduits.Text = "Importer Produits";
            btnImporterProduits.Click += BtnImporterProduits_Click;
            // 
            // FrmTableauDeBord
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1250, 938);
            Controls.Add(btnImporterProduits);
            Controls.Add(panelPrincipal);
            Controls.Add(panelMenu);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "FrmTableauDeBord";
            Text = "Tableau de bord - MarketAhmed";
            panelMenu.ResumeLayout(false);
            panelPrincipal.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}

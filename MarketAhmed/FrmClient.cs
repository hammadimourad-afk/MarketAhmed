using MarketAhmed.Core.Models;
using MarketAhmed.Core.Services;
using MarketAhmed.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MarketAhmed.Data.Repositories;

namespace MarketAhmed.UI
{
    public partial class FrmClient : Form
    {
        private readonly ClientService _clientService;
        private Client _selectedClient;

        public bool IsSelectionMode { get; set; }
        public Client SelectedClient { get; private set; }

        // Déclaration des contrôles
        private Label lblNom;
        private TextBox txtNom;
        private Label lblPrenom;
        private TextBox txtPrenom;
        private Label lblAdresse;
        private TextBox txtAdresse;
        private Label lblTelephone;
        private TextBox txtTelephone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblStatutCompte;
        private ComboBox cbStatutCompte;
        private Label lblLatitude;
        private TextBox txtLatitude;
        private Label lblLongitude;
        private TextBox txtLongitude;

        private DataGridView dgvClients;
        private Button btnAjouter;
        private Button btnModifier;
        private Button btnSupprimer;
        private Button btnAnnuler;
        private Button btnFermer;
        private Button btnShowOnMap;
        private Button btnSelect;

        private WebBrowser webBrowserMap;
        private readonly string _connectionString;

        // DÉCLARATION DES VARIABLES DE DIMENSION EN TANT QUE CHAMPS DE CLASSE
        private int _padding;
        private int _labelWidth;
        private int _textBoxWidth;
        private int _longTextBoxWidth;
        private int _buttonWidth;
        private int _buttonHeight;
        private int _rowHeight;

        // CONSTRUCTEUR EXISTANT
        public FrmClient(string connectionString)
        {
            _connectionString = connectionString;
            _clientService = new ClientService(new ClientRepository(_connectionString));
            InitializeLayoutVariables(); // Initialiser les variables de layout
            InitializeComponent();
            ApplySelectionMode();
            LoadMapHtml();
            webBrowserMap.ScriptErrorsSuppressed = true;
            SetBrowserFeatureControl();
            LoadClients();
            ClearForm();
        }

        // NOUVEAU CONSTRUCTEUR
        public FrmClient(ClientService clientService)
        {
            _clientService = clientService;
            this.IsSelectionMode = true;
            InitializeLayoutVariables(); // Initialiser les variables de layout
            InitializeComponent();
            ApplySelectionMode();
            LoadMapHtml();
            webBrowserMap.ScriptErrorsSuppressed = true;
            SetBrowserFeatureControl();
            LoadClients();
            ClearForm();
        }

        // NOUVELLE MÉTHODE POUR INITIALISER CES VARIABLES
        private void InitializeLayoutVariables()
        {
            _padding = 20;
            _labelWidth = 80;
            _textBoxWidth = 200;
            _longTextBoxWidth = 300;
            _buttonWidth = 90;
            _buttonHeight = 30;
            _rowHeight = 30;
        }

        private void InitializeComponent()
        {
            // Initialisation des contrôles
            this.lblNom = new Label();
            this.txtNom = new TextBox();
            this.lblPrenom = new Label();
            this.txtPrenom = new TextBox();
            this.lblAdresse = new Label();
            this.txtAdresse = new TextBox();
            this.lblTelephone = new Label();
            this.txtTelephone = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblStatutCompte = new Label();
            this.cbStatutCompte = new ComboBox();
            this.dgvClients = new DataGridView();
            this.btnAjouter = new Button();
            this.btnModifier = new Button();
            this.btnSupprimer = new Button();
            this.btnAnnuler = new Button();
            this.btnFermer = new Button();
            this.lblLatitude = new Label();
            this.txtLatitude = new TextBox();
            this.lblLongitude = new Label();
            this.txtLongitude = new TextBox();
            this.btnShowOnMap = new Button();
            this.webBrowserMap = new WebBrowser();
            this.btnSelect = new Button(); // Initialize btnSelect

            // Configuration du formulaire
            this.SuspendLayout();
            this.Text = "Gestion des Clients";
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(_padding, _padding);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(35, 13);
            this.lblNom.Text = "Nom :";
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(_padding + _labelWidth, _padding - 3);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(_textBoxWidth, 20);
            // 
            // lblPrenom
            // 
            this.lblPrenom.AutoSize = true;
            this.lblPrenom.Location = new System.Drawing.Point(_padding, _padding + _rowHeight);
            this.lblPrenom.Name = "lblPrenom";
            this.lblPrenom.Size = new System.Drawing.Size(51, 13);
            this.lblPrenom.Text = "Prénom :";
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(_padding + _labelWidth, _padding + _rowHeight - 3);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(_textBoxWidth, 20);
            // 
            // lblAdresse
            // 
            this.lblAdresse.AutoSize = true;
            this.lblAdresse.Location = new System.Drawing.Point(_padding, _padding + 2 * _rowHeight);
            this.lblAdresse.Name = "lblAdresse";
            this.lblAdresse.Size = new System.Drawing.Size(51, 13);
            this.lblAdresse.Text = "Adresse :";
            // 
            // txtAdresse
            // 
            this.txtAdresse.Location = new System.Drawing.Point(_padding + _labelWidth, _padding + 2 * _rowHeight - 3);
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(_longTextBoxWidth, 20);
            // 
            // lblTelephone
            // 
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Location = new System.Drawing.Point(_padding, _padding + 3 * _rowHeight);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(64, 13);
            this.lblTelephone.Text = "Téléphone :";
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(_padding + _labelWidth, _padding + 3 * _rowHeight - 3);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(_textBoxWidth, 20);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(_padding, _padding + 4 * _rowHeight);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(38, 13);
            this.lblEmail.Text = "Email :";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(_padding + _labelWidth, _padding + 4 * _rowHeight - 3);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(_textBoxWidth + 50, 20);
            // 
            // lblStatutCompte
            // 
            this.lblStatutCompte.AutoSize = true;
            this.lblStatutCompte.Location = new System.Drawing.Point(_padding, _padding + 5 * _rowHeight);
            this.lblStatutCompte.Name = "lblStatutCompte";
            this.lblStatutCompte.Size = new System.Drawing.Size(84, 13);
            this.lblStatutCompte.Text = "Statut Compte :";
            // 
            // cbStatutCompte
            // 
            this.cbStatutCompte.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbStatutCompte.FormattingEnabled = true;
            this.cbStatutCompte.Items.AddRange(new object[] {
            "Actif",
            "Inactif",
            "Suspendu"});
            this.cbStatutCompte.Location = new System.Drawing.Point(_padding + _labelWidth, _padding + 5 * _rowHeight - 3);
            this.cbStatutCompte.Name = "cbStatutCompte";
            this.cbStatutCompte.Size = new System.Drawing.Size(_textBoxWidth, 21);
            this.cbStatutCompte.SelectedIndex = 0;

            // 
            // lblLatitude
            // 
            this.lblLatitude.AutoSize = true;
            this.lblLatitude.Location = new System.Drawing.Point(_padding, _padding + 6 * _rowHeight);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(54, 13);
            this.lblLatitude.Text = "Latitude :";
            // 
            // txtLatitude
            // 
            this.txtLatitude.Location = new System.Drawing.Point(_padding + _labelWidth, _padding + 6 * _rowHeight - 3);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(_textBoxWidth / 2, 20);
            // 
            // lblLongitude
            // 
            this.lblLongitude.AutoSize = true;
            this.lblLongitude.Location = new System.Drawing.Point(_padding + _labelWidth + _textBoxWidth / 2 + _padding, _padding + 6 * _rowHeight);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(63, 13);
            this.lblLongitude.Text = "Longitude :";
            // 
            // txtLongitude
            // 
            this.txtLongitude.Location = new System.Drawing.Point(_padding + _labelWidth + _textBoxWidth / 2 + _padding + _labelWidth, _padding + 6 * _rowHeight - 3);
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(_textBoxWidth / 2, 20);

            // 
            // btnShowOnMap
            // 
            this.btnShowOnMap.Location = new System.Drawing.Point(_padding + _labelWidth + _textBoxWidth / 2 + _padding + _labelWidth + _textBoxWidth / 2 + _padding + 10, _padding + 6 * _rowHeight - 5);
            this.btnShowOnMap.Name = "btnShowOnMap";
            this.btnShowOnMap.Size = new System.Drawing.Size(_buttonWidth + 30, _buttonHeight);
            this.btnShowOnMap.Text = "Afficher sur la carte";
            this.btnShowOnMap.UseVisualStyleBackColor = true;
            this.btnShowOnMap.Click += new System.EventHandler(this.btnShowOnMap_Click);

            //
            // WebBrowserMap
            //
            this.webBrowserMap.Location = new System.Drawing.Point(_padding + _labelWidth + _longTextBoxWidth + _padding, _padding);
            this.webBrowserMap.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserMap.Name = "webBrowserMap";
            this.webBrowserMap.Size = new System.Drawing.Size(400, 250);
            this.webBrowserMap.TabIndex = 10;

            // --- Action Buttons ---
            int buttonY = _padding + 8 * _rowHeight;

            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(_padding, buttonY);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(_buttonWidth, _buttonHeight);
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(_padding + _buttonWidth + 10, buttonY);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(_buttonWidth, _buttonHeight);
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.Location = new System.Drawing.Point(_padding + 2 * (_buttonWidth + 10), buttonY);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(_buttonWidth, _buttonHeight);
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Location = new System.Drawing.Point(_padding + 3 * (_buttonWidth + 10), buttonY);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(_buttonWidth, _buttonHeight);
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);

            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(_padding + 3 * (_buttonWidth + 10), buttonY); // Même position que Annuler initialement
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(_buttonWidth, _buttonHeight);
            this.btnSelect.Text = "Sélectionner";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // dgvClients
            // 
            this.dgvClients.AllowUserToAddRows = false;
            this.dgvClients.AllowUserToDeleteRows = false;
            this.dgvClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClients.Location = new System.Drawing.Point(_padding, buttonY + _buttonHeight + _padding);
            this.dgvClients.Name = "dgvClients";
            this.dgvClients.ReadOnly = true;
            this.dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvClients.Size = new System.Drawing.Size(this.ClientSize.Width - 2 * _padding, this.ClientSize.Height - (buttonY + _buttonHeight + _padding) - (_buttonHeight + _padding));
            this.dgvClients.TabIndex = 0;
            this.dgvClients.CellClick += new DataGridViewCellEventHandler(this.dgvClients_CellClick);
            // 
            // btnFermer
            // 
            this.btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFermer.Location = new System.Drawing.Point(this.ClientSize.Width - _buttonWidth - _padding, this.ClientSize.Height - _buttonHeight - _padding);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(_buttonWidth, _buttonHeight);
            this.btnFermer.Text = "Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);

            // Ajout des contrôles au formulaire
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.lblPrenom);
            this.Controls.Add(this.txtPrenom);
            this.Controls.Add(this.lblAdresse);
            this.Controls.Add(this.txtAdresse);
            this.Controls.Add(this.lblTelephone);
            this.Controls.Add(this.txtTelephone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblStatutCompte);
            this.Controls.Add(this.cbStatutCompte);
            this.Controls.Add(this.lblLatitude);
            this.Controls.Add(this.txtLatitude);
            this.Controls.Add(this.lblLongitude);
            this.Controls.Add(this.txtLongitude);
            this.Controls.Add(this.btnShowOnMap);
            this.Controls.Add(this.webBrowserMap);

            this.Controls.Add(this.btnAjouter);
            this.Controls.Add(this.btnModifier);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnSelect); // Add the new select button
            this.Controls.Add(this.dgvClients);
            this.Controls.Add(this.btnFermer);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // MAJ de ApplySelectionMode() pour utiliser les champs de classe
        private void ApplySelectionMode()
        {
            // Assurez-vous que InitializeLayoutVariables a été appelé avant si ce n'est pas déjà le cas
            if (_padding == 0) // Simple vérification
            {
                InitializeLayoutVariables();
            }

            if (IsSelectionMode)
            {
                // Cacher les boutons de gestion CRUD
                btnAjouter.Visible = false;
                btnModifier.Visible = false;
                btnSupprimer.Visible = false;
                btnAnnuler.Visible = false;

                // Rendre visible le bouton de sélection et ajuster sa position
                btnSelect.Visible = true;
                // Positionnez btnSelect là où les autres boutons de modification étaient, ou autre.
                // Ici, nous pouvons le mettre à la place de btnAjouter par exemple, ou légèrement décalé.
                // Utilisez les champs privés que nous venons de créer.
                btnSelect.Location = new System.Drawing.Point(_padding, _padding + 8 * _rowHeight);
                btnSelect.Size = new System.Drawing.Size(_buttonWidth, _buttonHeight); // S'assurer que la taille est correcte
                btnSelect.Enabled = false; // Désactiver par défaut tant qu'aucun client n'est sélectionné

                // Rendre les champs de texte en lecture seule pour éviter les modifications accidentelles
                txtNom.ReadOnly = true;
                txtPrenom.ReadOnly = true;
                txtAdresse.ReadOnly = true;
                txtTelephone.ReadOnly = true;
                txtEmail.ReadOnly = true;
                cbStatutCompte.Enabled = false;
                txtLatitude.ReadOnly = true;
                txtLongitude.ReadOnly = true;
                btnShowOnMap.Enabled = true;
            }
            else
            {
                // Mode de gestion normal (par défaut)
                btnAjouter.Visible = true;
                btnModifier.Visible = true;
                btnSupprimer.Visible = true;
                btnAnnuler.Visible = true;
                btnSelect.Visible = false;

                // Réactiver les champs pour l'édition/ajout
                txtNom.ReadOnly = false;
                txtPrenom.ReadOnly = false;
                txtAdresse.ReadOnly = false;
                txtTelephone.ReadOnly = false;
                txtEmail.ReadOnly = false;
                cbStatutCompte.Enabled = true;
                txtLatitude.ReadOnly = false;
                txtLongitude.ReadOnly = false;
                btnShowOnMap.Enabled = true;
            }
        }

        // Gestionnaire d'événement pour le nouveau bouton "Sélectionner"
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (_selectedClient != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client dans la liste.", "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Méthode pour charger le fichier HTML de la carte
        private void LoadMapHtml()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string mapFilePath = Path.Combine(currentDirectory, "index.html");

            if (File.Exists(mapFilePath))
            {
                if (webBrowserMap.Url == null || !webBrowserMap.Url.LocalPath.EndsWith("index.html", StringComparison.OrdinalIgnoreCase))
                {
                    webBrowserMap.DocumentCompleted += WebBrowserMap_DocumentCompleted;
                    webBrowserMap.Navigate(mapFilePath);
                }
            }
            else
            {
                MessageBox.Show($"Le fichier 'index.html' est introuvable à l'emplacement : {mapFilePath}", "Erreur de fichier", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClients()
        {
            try
            {
                var clients = _clientService.GetAllClients();
                dgvClients.DataSource = clients;

                if (dgvClients.Columns.Contains("IdClient"))
                    dgvClients.Columns["IdClient"].Visible = false;

                if (dgvClients.Columns.Contains("Nom")) dgvClients.Columns["Nom"].HeaderText = "Nom";
                if (dgvClients.Columns.Contains("Prenom")) dgvClients.Columns["Prenom"].HeaderText = "Prénom";
                if (dgvClients.Columns.Contains("Adresse")) dgvClients.Columns["Adresse"].HeaderText = "Adresse Principale";
                if (dgvClients.Columns.Contains("Telephone")) dgvClients.Columns["Telephone"].HeaderText = "Téléphone";
                if (dgvClients.Columns.Contains("Email")) dgvClients.Columns["Email"].HeaderText = "Email";
                if (dgvClients.Columns.Contains("StatutCompte")) dgvClients.Columns["StatutCompte"].HeaderText = "Statut";
                if (dgvClients.Columns.Contains("DateAjout")) dgvClients.Columns["DateAjout"].HeaderText = "Date Ajout";
                if (dgvClients.Columns.Contains("DateDerniereModification")) dgvClients.Columns["DateDerniereModification"].HeaderText = "Dernière Modif.";
                if (dgvClients.Columns.Contains("Latitude")) dgvClients.Columns["Latitude"].HeaderText = "Latitude";
                if (dgvClients.Columns.Contains("Longitude")) dgvClients.Columns["Longitude"].HeaderText = "Longitude";

                dgvClients.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des clients : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtNom.Text = string.Empty;
            txtPrenom.Text = string.Empty;
            txtAdresse.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            cbStatutCompte.SelectedIndex = 0;
            txtLatitude.Text = string.Empty;
            txtLongitude.Text = string.Empty;

            _selectedClient = null;
            this.SelectedClient = null;

            if (!IsSelectionMode)
            {
                btnAjouter.Enabled = true;
                btnModifier.Enabled = false;
                btnSupprimer.Enabled = false;
                txtEmail.ReadOnly = false;
            }
            else
            {
                btnSelect.Enabled = false;
            }

            dgvClients.ClearSelection();
        }

        private void FillFormWithClient(Client client)
        {
            txtNom.Text = client.Nom;
            txtPrenom.Text = client.Prenom;
            txtAdresse.Text = client.Adresse;
            txtTelephone.Text = client.Telephone;
            txtEmail.Text = client.Email;
            cbStatutCompte.SelectedItem = client.StatutCompte;

            txtLatitude.Text = client.Latitude?.ToString();
            txtLongitude.Text = client.Longitude?.ToString();

            btnAjouter.Enabled = false;
            btnModifier.Enabled = true;
            btnSupprimer.Enabled = true;
            txtEmail.ReadOnly = true;
        }

        public void SetSelectedClient(Client client)
        {
            _selectedClient = client;

            if (webBrowserMap.Document != null && webBrowserMap.Document.Window != null &&
                _selectedClient != null && _selectedClient.Latitude.HasValue && _selectedClient.Longitude.HasValue)
            {
                try
                {
                    webBrowserMap.Document.InvokeScript("setLocation", new object[] {
                    _selectedClient.Latitude.Value,
                    _selectedClient.Longitude.Value,
                    _selectedClient.Nom + " " + _selectedClient.Prenom
                });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la mise à jour de la carte via SetSelectedClient : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnShowOnMap_Click(object sender, EventArgs e)
        {
            if (_selectedClient != null && _selectedClient.Latitude.HasValue && _selectedClient.Longitude.HasValue)
            {
                if (webBrowserMap.Document != null && webBrowserMap.Document.Window != null)
                {
                    if (webBrowserMap.Document.Window.DomWindow != null)
                    {
                        try
                        {
                            webBrowserMap.Document.InvokeScript("setLocation", new object[] {
                            _selectedClient.Latitude.Value,
                            _selectedClient.Longitude.Value,
                            _selectedClient.Nom + " " + _selectedClient.Prenom
                        });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erreur lors de l'appel de la fonction JavaScript 'setLocation' : " + ex.Message, "Erreur JavaScript", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("La carte n'est pas encore complètement chargée. Veuillez patienter et réessayer.", "Chargement en cours", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client avec des coordonnées GPS valides (Latitude et Longitude).", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void WebBrowserMap_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsolutePath == webBrowserMap.Url.AbsolutePath)
            {
                webBrowserMap.DocumentCompleted -= WebBrowserMap_DocumentCompleted;
                MessageBox.Show("Carte chargée ! Vous pouvez maintenant afficher la position du client.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (_selectedClient != null && _selectedClient.Latitude.HasValue && _selectedClient.Longitude.HasValue)
                {
                    if (webBrowserMap.Document != null && webBrowserMap.Document.Window != null && webBrowserMap.Document.Window.DomWindow != null)
                    {
                        try
                        {
                            webBrowserMap.Document.InvokeScript("setLocation", new object[] {
                            _selectedClient.Latitude.Value,
                            _selectedClient.Longitude.Value,
                            _selectedClient.Nom + " " + _selectedClient.Prenom
                        });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erreur lors de l'appel de setLocation après chargement initial : " + ex.Message, "Erreur JavaScript", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void SetBrowserFeatureControl()
        {
            const string IE_EMULATION_KEY = @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            string appName = AppDomain.CurrentDomain.FriendlyName;
            if (appName.EndsWith(".vshost.exe"))
            {
                appName = appName.Substring(0, appName.Length - ".vshost.exe".Length) + ".exe";
            }

            try
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(IE_EMULATION_KEY))
                {
                    if (key != null)
                    {
                        key.SetValue(appName, 11001, Microsoft.Win32.RegistryValueKind.DWord);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la configuration du moteur de rendu du navigateur : " + ex.Message, "Erreur de configuration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Client GetClientFromForm()
        {
            double? ParseDouble(string text)
            {
                if (double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double value))
                {
                    return value;
                }
                return null;
            }

            return new Client
            {
                IdClient = _selectedClient?.IdClient ?? 0,
                Nom = txtNom.Text.Trim(),
                Prenom = txtPrenom.Text.Trim(),
                Adresse = txtAdresse.Text.Trim(),
                Telephone = txtTelephone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                StatutCompte = cbStatutCompte.SelectedItem?.ToString(),
                Latitude = ParseDouble(txtLatitude.Text),
                Longitude = ParseDouble(txtLongitude.Text)
            };
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            var newClient = GetClientFromForm();
            if (string.IsNullOrEmpty(newClient.Nom) || string.IsNullOrEmpty(newClient.Prenom) || string.IsNullOrEmpty(newClient.Email))
            {
                MessageBox.Show("Le nom, le prénom et l'email sont obligatoires.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(newClient.Email))
            {
                MessageBox.Show("Veuillez entrer une adresse e-mail valide.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newClient.Latitude.HasValue && (newClient.Latitude.Value < -90 || newClient.Latitude.Value > 90))
            {
                MessageBox.Show("La latitude doit être entre -90 et 90.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newClient.Longitude.HasValue && (newClient.Longitude.Value < -180 || newClient.Longitude.Value > 180))
            {
                MessageBox.Show("La longitude doit être entre -180 et 180.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_clientService.AddClient(newClient))
                {
                    MessageBox.Show("Client ajouté avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadClients();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout du client. L'email est peut-être déjà utilisé ou les données sont invalides.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de l'ajout : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (_selectedClient == null)
            {
                MessageBox.Show("Veuillez sélectionner un client à modifier.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var updatedClient = GetClientFromForm();
            updatedClient.IdClient = _selectedClient.IdClient;

            if (string.IsNullOrEmpty(updatedClient.Nom) || string.IsNullOrEmpty(updatedClient.Prenom) || string.IsNullOrEmpty(updatedClient.Email))
            {
                MessageBox.Show("Le nom, le prénom et l'email sont obligatoires.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(updatedClient.Email))
            {
                MessageBox.Show("Veuillez entrer une adresse e-mail valide.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (updatedClient.Latitude.HasValue && (updatedClient.Latitude.Value < -90 || updatedClient.Latitude.Value > 90))
            {
                MessageBox.Show("La latitude doit être entre -90 et 90.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (updatedClient.Longitude.HasValue && (updatedClient.Longitude.Value < -180 || updatedClient.Longitude.Value > 180))
            {
                MessageBox.Show("La longitude doit être entre -180 et 180.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_clientService.UpdateClient(updatedClient))
                {
                    MessageBox.Show("Client modifié avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadClients();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la modification du client. L'email est peut-être déjà utilisé ou les données sont invalides.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la modification : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (_selectedClient == null)
            {
                MessageBox.Show("Veuillez sélectionner un client à supprimer.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le client {_selectedClient.Nom} {_selectedClient.Prenom} ?",
                                                "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    if (_clientService.DeleteClient(_selectedClient.IdClient))
                    {
                        MessageBox.Show("Client supprimé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadClients();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression du client.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est survenue lors de la suppression : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            ClearForm();
            dgvClients.ClearSelection();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvClients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var client = dgvClients.Rows[e.RowIndex].DataBoundItem as Client;
                if (client != null)
                {
                    this.SelectedClient = client;

                    if (IsSelectionMode)
                    {
                        FillFormWithClient(client);
                        btnSelect.Enabled = true;
                    }
                    else
                    {
                        FillFormWithClient(client);
                    }

                    SetSelectedClient(client);
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
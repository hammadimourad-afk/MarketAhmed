using System;
using System.Windows.Forms;

// Core
using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;
using MarketAhmed.Core.Services;

// Data
using MarketAhmed.Data;
using MarketAhmed.Data.Repositories;

namespace MarketAhmed.UI
{
    public partial class FrmLogin : Form
    {
        private readonly UtilisateurService _utilisateurService;

        // Propriété publique pour l'utilisateur connecté
        public Utilisateur UtilisateurConnecte { get; private set; }

        // Constructeur principal
        public FrmLogin(UtilisateurService utilisateurService)
        {
            InitializeComponent();
            _utilisateurService = utilisateurService;
        }

        // Constructeur par défaut pour le Designer
        public FrmLogin() : this(
            new UtilisateurService(
                new UtilisateurRepository(Database.GetConnection()) // repo avec connection
            ))
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nom = txtUsername.Text.Trim();
            string motDePasse = txtPassword.Text.Trim();

            var utilisateur = _utilisateurService.Authentifier(nom, motDePasse);

            if (utilisateur != null)
            {
                // Affecte l'utilisateur connecté à la propriété
                UtilisateurConnecte = utilisateur;

                // Ferme le formulaire avec DialogResult.OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.", "Erreur");
            }
        }
    }
}

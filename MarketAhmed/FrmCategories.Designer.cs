using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MarketAhmed.UI
{
    partial class FrmCategories
    {
        private IContainer components = null;

        private DataGridView dgvCategories;
        private TextBox txtNom;
        private TextBox txtDescription;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnNouveau;
        private TableLayoutPanel tableLayoutPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new Container();

            dgvCategories = new DataGridView();
            txtNom = new TextBox();
            txtDescription = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnNouveau = new Button();
            tableLayoutPanel = new TableLayoutPanel();

            ((ISupportInitialize)dgvCategories).BeginInit();
            SuspendLayout();

            // === tableLayoutPanel configuration ===
            tableLayoutPanel.ColumnCount = 4;
            tableLayoutPanel.RowCount = 3; // 2 lignes pour TextBox, 1 pour boutons
            tableLayoutPanel.Dock = DockStyle.Top;
            tableLayoutPanel.AutoSize = true;
            tableLayoutPanel.Padding = new Padding(10);
            tableLayoutPanel.Margin = new Padding(0);

            tableLayoutPanel.ColumnStyles.Clear();
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // === Labels & TextBoxes ===
            var lblNom = new Label { Text = "Nom:", AutoSize = true, Anchor = AnchorStyles.Right };
            var lblDescription = new Label { Text = "Description:", AutoSize = true, Anchor = AnchorStyles.Right };

            txtNom.Dock = DockStyle.Fill;
            txtDescription.Dock = DockStyle.Fill;

            tableLayoutPanel.Controls.Add(lblNom, 0, 0);
            tableLayoutPanel.Controls.Add(txtNom, 1, 0);
            tableLayoutPanel.Controls.Add(lblDescription, 2, 0);
            tableLayoutPanel.Controls.Add(txtDescription, 3, 0);

            // === Buttons ===
            btnNouveau.Text = "Nouveau";
            btnAdd.Text = "Ajouter";
            btnUpdate.Text = "Modifier";
            btnDelete.Text = "Supprimer";

            btnNouveau.Width = 100;
            btnAdd.Width = 100;
            btnUpdate.Width = 100;
            btnDelete.Width = 100;

            btnNouveau.Anchor = AnchorStyles.Left;
            btnAdd.Anchor = AnchorStyles.Left;
            btnUpdate.Anchor = AnchorStyles.Left;
            btnDelete.Anchor = AnchorStyles.Left;

            // Ajouter les boutons à la 2ème ligne
            tableLayoutPanel.Controls.Add(btnNouveau, 0, 1);
            tableLayoutPanel.Controls.Add(btnAdd, 1, 1);
            tableLayoutPanel.Controls.Add(btnUpdate, 2, 1);
            tableLayoutPanel.Controls.Add(btnDelete, 3, 1);

            // === DataGridView ===
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.ReadOnly = true;
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvCategories.Columns.Clear();
            dgvCategories.Columns.Add("IdCategorie", "ID");
            dgvCategories.Columns.Add("Nom", "Nom");
            dgvCategories.Columns.Add("Description", "Description");

            // === Form properties ===
            ClientSize = new Size(600, 450);
            Text = "Gestion des Catégories";
            StartPosition = FormStartPosition.CenterScreen;

            // Ajouter controls au formulaire
            Controls.Add(dgvCategories);       // DataGridView en bas
            Controls.Add(tableLayoutPanel);    // TableLayoutPanel en haut

            ((ISupportInitialize)dgvCategories).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

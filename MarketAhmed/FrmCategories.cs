using System;
using System.Linq;
using System.Windows.Forms;
using MarketAhmed.Core.Models;
using MarketAhmed.Core.Interfaces;

namespace MarketAhmed.UI
{
    public partial class FrmCategories : Form
    {
        private readonly ICategorieRepository _categorieRepo;

        public FrmCategories(ICategorieRepository categorieRepo)
        {
            _categorieRepo = categorieRepo;
            InitializeComponent();
            InitializeDataGridView();
            ChargerCategories();

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnNouveau.Click += BtnNouveau_Click;
        }

        private void InitializeDataGridView()
        {
            dgvCategories.Columns.Clear();
            dgvCategories.Columns.Add("IdCategorie", "ID");
            dgvCategories.Columns.Add("Nom", "Nom");

            dgvCategories.ReadOnly = true;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ChargerCategories()
        {
            dgvCategories.Rows.Clear();
            var categories = _categorieRepo.GetAll().ToList();
            foreach (var c in categories)
            {
                dgvCategories.Rows.Add(c.IdCategorie, c.Nom);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var cat = new Categorie { Nom = txtNom.Text };
                _categorieRepo.Insert(cat);
                ChargerCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow == null) return;

            try
            {
                int id = Convert.ToInt32(dgvCategories.CurrentRow.Cells[0].Value);
                var cat = new Categorie { IdCategorie = id, Nom = txtNom.Text };
                _categorieRepo.Update(cat);
                ChargerCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow == null) return;

            try
            {
                int id = Convert.ToInt32(dgvCategories.CurrentRow.Cells[0].Value);
                _categorieRepo.Delete(id);
                ChargerCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de supprimer : " + ex.Message);
            }
        }

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            txtNom.Text = string.Empty;
        }
    }
}

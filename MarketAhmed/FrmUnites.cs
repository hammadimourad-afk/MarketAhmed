using System;
using System.Linq;
using System.Windows.Forms;
using MarketAhmed.Core.Models;
using MarketAhmed.Core.Interfaces;

namespace MarketAhmed.UI
{
    public partial class FrmUnites : Form
    {
        private readonly IUniteRepository _uniteRepo;

        public FrmUnites(IUniteRepository uniteRepo)
        {
            _uniteRepo = uniteRepo;
            InitializeComponent();
            InitializeDataGridView();
            ChargerUnites();

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnNouveau.Click += BtnNouveau_Click;
        }

        private void InitializeDataGridView()
        {
            dgvUnites.Columns.Clear();
            dgvUnites.Columns.Add("IdUnite", "ID");
            dgvUnites.Columns.Add("Nom", "Nom");

            dgvUnites.ReadOnly = true;
            dgvUnites.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUnites.AllowUserToAddRows = false;
            dgvUnites.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ChargerUnites()
        {
            dgvUnites.Rows.Clear();
            var unites = _uniteRepo.GetAll().ToList();
            foreach (var u in unites)
            {
                dgvUnites.Rows.Add(u.IdUnite, u.Nom);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var unite = new Unite { Nom = txtNom.Text };
                _uniteRepo.Insert(unite);
                ChargerUnites();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvUnites.CurrentRow == null) return;

            try
            {
                int id = Convert.ToInt32(dgvUnites.CurrentRow.Cells[0].Value);
                var unite = new Unite { IdUnite = id, Nom = txtNom.Text };
                _uniteRepo.Update(unite);
                ChargerUnites();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUnites.CurrentRow == null) return;

            try
            {
                int id = Convert.ToInt32(dgvUnites.CurrentRow.Cells[0].Value);
                _uniteRepo.Delete(id);
                ChargerUnites();
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

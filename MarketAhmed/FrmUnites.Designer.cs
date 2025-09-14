using System.Windows.Forms;

namespace MarketAhmed.UI
{
    partial class FrmUnites
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvUnites;
        private TextBox txtNom;
        private TextBox txtDescription;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnNouveau;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvUnites = new DataGridView();
            this.txtNom = new TextBox();
            this.txtDescription = new TextBox();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnNouveau = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvUnites)).BeginInit();
            this.SuspendLayout();

            // dgvUnites
            this.dgvUnites.Location = new System.Drawing.Point(10, 100);
            this.dgvUnites.Size = new System.Drawing.Size(500, 300);
            this.dgvUnites.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            // txtNom
            this.txtNom.Location = new System.Drawing.Point(80, 10);
            this.txtNom.Size = new System.Drawing.Size(150, 20);

            // txtDescription
            this.txtDescription.Location = new System.Drawing.Point(80, 40);
            this.txtDescription.Size = new System.Drawing.Size(150, 20);

            // btnAdd
            this.btnAdd.Text = "Ajouter";
            this.btnAdd.Location = new System.Drawing.Point(250, 10);

            // btnUpdate
            this.btnUpdate.Text = "Modifier";
            this.btnUpdate.Location = new System.Drawing.Point(330, 10);

            // btnDelete
            this.btnDelete.Text = "Supprimer";
            this.btnDelete.Location = new System.Drawing.Point(410, 10);

            // btnNouveau
            this.btnNouveau.Text = "Nouveau";
            this.btnNouveau.Location = new System.Drawing.Point(250, 40);

            // Ajouter controls
            this.Controls.Add(this.dgvUnites);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnNouveau);

            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Text = "Gestion des Unités";

            ((System.ComponentModel.ISupportInitialize)(this.dgvUnites)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Controller;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Test.Views
{
    public partial class FPenalties : Form
    {
        Ctrl_Penalties ctrlPenaties = new Ctrl_Penalties();
        Ctrl_User ctrlUser = new Ctrl_User();
        Ctrl_PenaltiesDetail ctrlPenatiesDetail = new Ctrl_PenaltiesDetail();
        private string userAcc;
        public FPenalties(string username)
        {
            InitializeComponent();
            userAcc = username;
        }
        private void FPenaties_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            var penalties = ctrlPenaties.GetPenalties(userAcc);

            if (penalties.Count == 0)
            {
                MessageBox.Show("No penalties found for this user.");
            }

            dtgridPenalties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgridPenalties.DataSource = penalties;
            dtgridPenalties.Columns["Customer"].Visible = false;
            dtgridPenalties.Columns["Employee"].Visible = false;
            dtgridPenalties.Columns["PenaltyDetails"].Visible = false;
        }
        private void dtgridPenalties_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtgridPenalties.Rows[e.RowIndex];
            int idPenaty = Convert.ToInt32(row.Cells[0].Value);
            var User = ctrlUser.GetUserByUser(userAcc);
            PenaltyDetail penaltyDT = ctrlPenatiesDetail.getInforPenaty(idPenaty);
            if (penaltyDT == null)
            {
                MessageBox.Show("Thông tin chi tiết hình phạt không tồn tại!");
                return;
            }
            lblIDUser.DataBindings.Clear();
            lblIDUser.DataBindings.Add(new Binding("Text", User, "IDUser"));
            lblName.DataBindings.Clear();
            lblName.DataBindings.Add(new Binding("Text", User, "Name"));
            lblBirth.DataBindings.Clear();
            lblBirth.DataBindings.Add(new Binding("Text", User, "birth"));
            lblBirth.Text = ((DateTime)User.birth).ToString("dd/MM/yyyy");
            lblPhone.DataBindings.Clear();
            lblPhone.DataBindings.Add(new Binding("Text", User, "PhoneNumber"));
            lblReason.DataBindings.Clear();
            lblReason.DataBindings.Add(new Binding("Text", penaltyDT, "Reason"));
            lblPrice.DataBindings.Clear();
            lblPrice.DataBindings.Add(new Binding("Text", penaltyDT, "Price"));
        }

        private void dtgridVehicles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

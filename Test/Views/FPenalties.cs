using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
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
            btnCreateVisible();
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
            var penalties = ctrlPenaties.GetPenalties(userAcc);
            int idPenaty = Convert.ToInt32(row.Cells[0].Value);
            var User = ctrlUser.GetUserByUser(userAcc);
            PenaltyDetail penaltyDT = ctrlPenatiesDetail.getInforPenaty(idPenaty);
            if (penaltyDT == null)
            {
                MessageBox.Show("Thông tin chi tiết hình phạt không tồn tại!");
                return;
            }
            lblIDPen.DataBindings.Clear();
            lblIDPen.DataBindings.Add(new Binding("Text", penalties, "IDPenalty"));
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

       
        private void btnPayment_Click(object sender, EventArgs e)
        {
            var user = CUltils.db.Accounts.FirstOrDefault(a => a.Username == userAcc);
            var wallet = user.Wallets.FirstOrDefault();

            if (wallet == null)
            {
                MessageBox.Show("Wallet null!");
                return;
            }

            var money = wallet.Money;
            if (lblPrice != null && !string.IsNullOrEmpty(lblPrice.Text))
            {
                if (!decimal.TryParse(lblPrice.Text, out decimal penMoney))
                {
                    MessageBox.Show("Invalid penalty amount!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (money < penMoney)
                {
                    MessageBox.Show("Your wallet balance is insufficient!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                   
                    if (!string.IsNullOrEmpty(lblIDPen.Text) && decimal.TryParse(lblIDPen.Text, out decimal idpen))
                    {
                        // Tìm kiếm penalty từ cơ sở dữ liệu dựa trên IDPenalty
                        var penaltyToDelete = CUltils.db.Penalties.FirstOrDefault(p => p.IDPenalty == idpen);
                        MessageBox.Show("Payment successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        wallet.Money -= penMoney;
                        if (penaltyToDelete != null)
                        {
                            // Xóa penalty nếu tìm thấy
                            //CUltils.db.Penalties.Remove(penaltyToDelete);
                            //CUltils.db.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Penalty not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid penalty ID or penalty not selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Plese chose penalty you want to pay", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVehicles();
        }
        private void FilterVehicles()
        {
            string selectedPenaltyYear = cbYear.SelectedItem?.ToString();

            var penalties = ctrlPenaties.GetPenalties(userAcc);

            if (!string.IsNullOrEmpty(selectedPenaltyYear))
            {
                penalties = penalties.Where(t => t.PenaltyDate.Year.ToString() == selectedPenaltyYear).ToList();
            }

            UpdateVehicleList(penalties);
        }
        private void UpdateVehicleList(List<Penalty> penalties)
        {
            dtgridPenalties.DataSource = null; // Xóa dữ liệu cũ trước khi cập nhật.
            dtgridPenalties.DataSource = penalties;

            if (penalties == null || penalties.Count == 0)
            {
                MessageBox.Show("NULL.");
            }
            else
            {
                loadData();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
        }
        void btnCreateVisible()
        {
            //btnCreate.Visible = false;
            var user = ctrlUser.GetUserByUser(userAcc);
            if (user.UserType == "nhân viên")
            {
                btnCreate.Visible = true;
            }
        }
    }
}

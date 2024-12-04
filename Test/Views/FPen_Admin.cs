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

namespace Test.Views
{
    public partial class FPen_Admin : Form
    {
        Ctrl_Penalties ctrlPenaties = new Ctrl_Penalties();
        Ctrl_User ctrlUser = new Ctrl_User();
        Ctrl_PenaltiesDetail ctrlPenatiesDetail = new Ctrl_PenaltiesDetail();
        public FPen_Admin()
        {
            InitializeComponent();
            loadData();
        }

        private void dtgridPenalties_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtgridPenalties.Rows[e.RowIndex];
            int idPenalty = Convert.ToInt32(row.Cells[0].Value);
            var penalty = CUltils.db.Penalties.FirstOrDefault(p => p.IDPenalty == idPenalty);
            var customer = CUltils.db.Customers.FirstOrDefault(c => c.IDCustomer == penalty.IDCustomer);
            var user = CUltils.db.Users.FirstOrDefault(u => u.IDUser == customer.IDUser);
            var employee = CUltils.db.Employees.FirstOrDefault(u => u.IDEmployee == user.IDEmployee);
            var penaltyDetail = CUltils.db.PenaltyDetails.FirstOrDefault(pd => pd.IDPenalty == idPenalty);
            if (penalty == null)
            {
                MessageBox.Show("Hình phạt không tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (penaltyDetail == null)
            {
                MessageBox.Show("Thông tin chi tiết hình phạt không tồn tại!");
                return;
            }

            txtIDPen.DataBindings.Clear();
            txtIDPen.DataBindings.Add(new Binding("Text", penalty, "IDPenalty"));
            if (penalty.PenaltyDate != default(DateTime))
            {
                DTPen.Value = penalty.PenaltyDate;
            }
            else
            {
                DTPen.Value = DateTime.Now;
            }
            txtReason.Text = penaltyDetail.Reason ?? "Unknown";
            txtPrice.Text = penaltyDetail.price.ToString("C");
        }
        void loadData()
        {
            var penalties = ctrlPenaties.getlistPen();
            var emp = CUltils.db.Employees.ToList();
            if (penalties.Count == 0)
            {
                MessageBox.Show("No penalties found for this user.");
            }
            cbCust();
            cbEmp();

            dtgridPenalties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgridPenalties.DataSource = penalties;
            dtgridPenalties.Columns["Customer"].Visible = false;
            dtgridPenalties.Columns["Employee"].Visible = false;
            dtgridPenalties.Columns["PenaltyDetails"].Visible = false;
        }
        void cbEmp()
        {
            var employeeList = CUltils.db.Employees.Select(ep => ep.IDEmployee)
                .ToList();
            cbEmployee.DataSource = employeeList;
        }
        void cbCust()
        {
            var customerList = CUltils.db.Customers
           .Select(c => c.IDCustomer)
           .ToList();
            cbCustomer.DataSource = customerList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int idPenalty = Convert.ToInt32(txtIDPen.Text);
                var penalty = CUltils.db.Penalties.FirstOrDefault(p => p.IDPenalty == idPenalty);
                var penaltyDetail = CUltils.db.PenaltyDetails.FirstOrDefault(pd => pd.IDPenalty == idPenalty);
                if (penalty != null && penaltyDetail != null)
                {
                    CUltils.db.PenaltyDetails.Remove(penaltyDetail);
                    CUltils.db.Penalties.Remove(penalty);
                    CUltils.db.SaveChanges();

                    MessageBox.Show("Xóa thành công!");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hình phạt hoặc chi tiết hình phạt.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int idPenalty = Convert.ToInt32(txtIDPen.Text);
            var penalty = CUltils.db.Penalties.FirstOrDefault(p => p.IDPenalty == idPenalty);
            var customer = CUltils.db.Customers.FirstOrDefault(c => c.IDCustomer == penalty.IDCustomer);
            var user = CUltils.db.Users.FirstOrDefault(u => u.IDUser == customer.IDUser);
            var employee = CUltils.db.Employees.FirstOrDefault(u => u.IDEmployee == user.IDEmployee);
            var penaltyDetail = CUltils.db.PenaltyDetails.FirstOrDefault(pd => pd.IDPenalty == idPenalty);

            if (penalty != null && penaltyDetail != null)
            {
                penalty.PenaltyDate = DTPen.Value;
                penaltyDetail.Reason = txtReason.Text;
                penaltyDetail.price = Convert.ToDecimal(txtPrice.Text, System.Globalization.CultureInfo.InvariantCulture);

                CUltils.db.SaveChanges();
                MessageBox.Show("Cập nhật thành công!");
                loadData();
            }
            else
            {
                MessageBox.Show("Không tìm thấy hình phạt hoặc chi tiết hình phạt.");
            }
        }
    }
}

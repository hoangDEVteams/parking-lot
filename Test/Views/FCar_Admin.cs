using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Controller;

namespace Test.Views
{
    public partial class FCar_Admin : Form
    {
        CTrl_Vehicles ctrlVehicles = new CTrl_Vehicles();
        Ctrl_VehicleTypes ctrlVehicleType = new Ctrl_VehicleTypes();
        V_VehicleData viewVehicle = new V_VehicleData();
        Vehicle vehicle;
        public FCar_Admin()
        {
            InitializeComponent();
        }
        private void FCar_Admin_Load(object sender, EventArgs e)
        {
            LoadVehicleData();
            LoadColumnsToComboBox();
        }
        private void LoadVehicleData()
        {
            CTrl_Vehicles vehicleService = new CTrl_Vehicles();
            List<Vehicle> vehicles = vehicleService.getList();
            var vehicleData = vehicles.Select(v => new
            {
                v.LicensePlate,
                VehicleTypeName = v.VehicleType.VehicleTypeName,
                v.Color,
                v.Status,
                v.Description,
                v.IDEmployee,
                Manufacture = v.VehicleType.Manufacturer,
                ManufactureYear = v.VehicleType.ManufactureYear,
            }).ToList();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = vehicleData;
            dataGridView1.ScrollBars = ScrollBars.Horizontal;
        }

        private Bitmap StretchImage(Bitmap sourceImage, int targetWidth, int targetHeight)
        {
            Bitmap stretchedImage = new Bitmap(targetWidth, targetHeight);
            using (Graphics g = Graphics.FromImage(stretchedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourceImage, new Rectangle(0, 0, targetWidth, targetHeight));
            }
            return stretchedImage;
        }
        private string selectedImagePath;
        private void iconButton6_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                Bitmap sourceImage = new Bitmap(filePath);

                int targetWidth = 390;
                int targetHeight = 240;
                Bitmap stretchedImage = StretchImage(sourceImage, targetWidth, targetHeight);

                pictureBoxCar.Image = stretchedImage;
                selectedImagePath = filePath;  
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (pictureBoxCar.Image == null)
            {
                MessageBox.Show("Vui lòng tải ảnh lên trước.");
                return;
            }

            if (string.IsNullOrEmpty(txtLicense.Text))
            {
                MessageBox.Show("Vui lòng nhập biển số xe.");
                return;
            }

            try
            {
                string licensePlate = txtLicense.Text.Trim();
                string fileExtension = Path.GetExtension(openFileDialog1.FileName); 
                string imageName = licensePlate + fileExtension;

                string projectDirectory = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName;
                string imgDirectory = Path.Combine(projectDirectory, "Resource", "img");

                if (!Directory.Exists(imgDirectory))
                {
                    Directory.CreateDirectory(imgDirectory);
                }
                string imgPath = Path.Combine(imgDirectory, imageName);

                ImageFormat imageFormat;
                switch (fileExtension.ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        imageFormat = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;
                    case ".bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;
                    default:
                        imageFormat = ImageFormat.Jpeg; 
                        break;
                }
                pictureBoxCar.Image.Save(imgPath, imageFormat);

                CTrl_Vehicles vehicleService = new CTrl_Vehicles();
                VehicleType vehicleType = new VehicleType
                {
                    VehicleTypeName = cbxVTypes.Text,
                    Manufacturer = cbxManufac.Text,
                    ManufactureYear = dteManu.Value.Year 
                };

                var existingVehicleType = CUltils.db.VehicleTypes
                    .FirstOrDefault(vt => vt.VehicleTypeName == vehicleType.VehicleTypeName &&
                                           vt.Manufacturer == vehicleType.Manufacturer &&
                                           vt.ManufactureYear == vehicleType.ManufactureYear);

                if (existingVehicleType == null)
                {
                    CUltils.db.VehicleTypes.Add(vehicleType);
                    CUltils.db.SaveChanges();
                }
                else
                {
                    vehicleType = existingVehicleType;
                }

                Vehicle newVehicle = new Vehicle
                {
                    LicensePlate = licensePlate,
                    VehicleType = vehicleType,
                    Color = cbxColor.Text,
                    Status = cbxStatus.Text,
                    Description = txtDescription.Text,
                    IDEmployee = cbxEmployee.Text
                };

                vehicleService.add(newVehicle);

                LoadVehicleData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu ảnh hoặc thêm phương tiện: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FFillInforVehicle form = new FFillInforVehicle();
            form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtLicense.Text = row.Cells["LicensePlate"].Value.ToString();
                cbxVTypes.Text = row.Cells["VehicleTypeName"].Value.ToString();
                cbxColor.Text = row.Cells["Color"].Value.ToString();
                cbxStatus.Text = row.Cells["Status"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
                cbxEmployee.Text = row.Cells["IDEmployee"].Value.ToString();
                cbxManufac.Text = row.Cells["Manufacture"].Value.ToString();
                dteManu.Value = new DateTime(Convert.ToInt32(row.Cells["ManufactureYear"].Value), 1, 1);

                string licensePlate = row.Cells["LicensePlate"].Value.ToString();
                string projectDirectory = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName;
                string imgPath = Path.Combine(projectDirectory, "Resource", "img", licensePlate); 

                if (File.Exists(imgPath))
                {
                    pictureBox1.Image = Image.FromFile(imgPath);
                }
                else
                {
                    pictureBox1.Image = null; 
                }
            }
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string licensePlate = selectedRow.Cells["LicensePlate"].Value.ToString();
                try
                {
                    CTrl_Vehicles vehicleService = new CTrl_Vehicles();
                    Vehicle vehicleToDelete = CUltils.db.Vehicles.FirstOrDefault(v => v.LicensePlate == licensePlate);

                    if (vehicleToDelete != null)
                    {
                        pictureBox1.Image = null;
                        CUltils.db.Vehicles.Remove(vehicleToDelete);
                        CUltils.db.SaveChanges();
                        string projectDirectory = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName;
                        string imgPath = Path.Combine(projectDirectory, "Resource", "img", licensePlate);

                        if (File.Exists(imgPath))
                        {
                            File.Delete(imgPath); 
                        }

                        LoadVehicleData();

                        MessageBox.Show("Phương tiện đã được xóa thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phương tiện để xóa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa phương tiện: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phương tiện để xóa.");
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string licensePlate = selectedRow.Cells["LicensePlate"].Value.ToString();
                string currentStatus = selectedRow.Cells["Status"].Value.ToString();

                string newStatus = currentStatus == "Available" ? "Unavailable" : "Available";

                try
                {
                    CTrl_Vehicles vehicleService = new CTrl_Vehicles();
                    Vehicle vehicleToUpdate = CUltils.db.Vehicles.FirstOrDefault(v => v.LicensePlate == licensePlate);

                    if (vehicleToUpdate != null)
                    {
                        vehicleToUpdate.Status = newStatus;
                        CUltils.db.SaveChanges();

                        LoadVehicleData();

                        MessageBox.Show($"Trạng thái phương tiện {licensePlate} đã được cập nhật thành {newStatus}.");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phương tiện để cập nhật.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật trạng thái: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phương tiện để thay đổi trạng thái.");
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string licensePlate = selectedRow.Cells["LicensePlate"].Value.ToString();

                string newVehicleType = cbxVTypes.Text;
                string newColor = cbxColor.Text;
                string newStatus = cbxStatus.Text;
                string newDescription = txtDescription.Text;
                string newEmployee = cbxEmployee.Text;
                string newManufacturer = cbxManufac.Text;
                int newManufactureYear = dteManu.Value.Year;

                try
                {
                    CTrl_Vehicles vehicleService = new CTrl_Vehicles();
                    Vehicle vehicleToUpdate = CUltils.db.Vehicles.FirstOrDefault(v => v.LicensePlate == licensePlate);

                    if (vehicleToUpdate != null)
                    {
                        vehicleToUpdate.VehicleType.VehicleTypeName = newVehicleType;
                        vehicleToUpdate.Color = newColor;
                        vehicleToUpdate.Status = newStatus;
                        vehicleToUpdate.Description = newDescription;
                        vehicleToUpdate.IDEmployee = newEmployee;

                        VehicleType vehicleTypeToUpdate = CUltils.db.VehicleTypes
                            .FirstOrDefault(vt => vt.VehicleTypeName == newVehicleType &&
                                                  vt.Manufacturer == newManufacturer &&
                                                  vt.ManufactureYear == newManufactureYear);

                        if (vehicleTypeToUpdate == null)
                        {
                            vehicleTypeToUpdate = new VehicleType
                            {
                                VehicleTypeName = newVehicleType,
                                Manufacturer = newManufacturer,
                                ManufactureYear = newManufactureYear
                            };
                            CUltils.db.VehicleTypes.Add(vehicleTypeToUpdate);
                        }

                        vehicleToUpdate.VehicleType = vehicleTypeToUpdate;

                        CUltils.db.SaveChanges();

                        LoadVehicleData();

                        MessageBox.Show("Thông tin phương tiện đã được cập nhật thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phương tiện cần cập nhật.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật thông tin phương tiện: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phương tiện để cập nhật.");
            }
        }
        private void LoadColumnsToComboBox()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                cbxFilter.Items.Add(column.HeaderText);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string selectedColumn = cbxFilter.SelectedItem.ToString(); 
            string filterValue = txtSearch.Text.Trim(); 

            if (string.IsNullOrEmpty(selectedColumn) || string.IsNullOrEmpty(filterValue))
            {
                MessageBox.Show("Vui lòng chọn cột và nhập giá trị lọc.");
                return;
            }

            var filteredData = from row in dataGridView1.Rows.Cast<DataGridViewRow>()
                               where row.Cells[selectedColumn].Value != null &&
                                     row.Cells[selectedColumn].Value.ToString().Contains(filterValue)
                               select new
                               {
                                   LicensePlate = row.Cells["LicensePlate"].Value.ToString(),
                                   VehicleTypeName = row.Cells["VehicleTypeName"].Value.ToString(),
                                   Color = row.Cells["Color"].Value.ToString(),
                                   Status = row.Cells["Status"].Value.ToString(),
                                   Description = row.Cells["Description"].Value.ToString(),
                                   IDEmployee = row.Cells["IDEmployee"].Value.ToString(),
                                   Manufacture = row.Cells["Manufacture"].Value.ToString(),
                                   ManufactureYear = row.Cells["ManufactureYear"].Value.ToString()
                               };

            dataGridView1.DataSource = filteredData.ToList();
        }

        private void cbxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btxCancel_Click(object sender, EventArgs e)
        {
            cbxFilter.SelectedIndex = -1; 
            txtSearch.Clear();  

            LoadVehicleData();
        }
    }
}

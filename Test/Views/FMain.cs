using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using Test.ZaloPay;
using Test.Controller;
using Test.MomoPayment;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

namespace Test.Views
{
    public partial class FMain : Form
    {
        private string username;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private int maxWidth;
        private int maxHeight;
        private int maxMenu;
        public FMain(string username)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            maxWidth = Screen.PrimaryScreen.Bounds.Width;
            maxHeight = Screen.PrimaryScreen.Bounds.Height;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.username = username;
            button2.MouseEnter += button2_MouseEnter;
            button2.MouseLeave += button2_MouseLeave;

            panel2.MouseDown += panel2_MouseDown;
            panel2.MouseMove += panel2_MouseMove;
            panel2.MouseUp += panel2_MouseUp;
            panel2.DoubleClick += panel2_DoubleClick;
            iconButton1.Tag = "Thuê Xe";
            iconButton2.Tag = "Quản Lý Xe ";
            iconButton3.Tag = "Quản Lý Hóa Đơn";
            iconButton4.Tag = "Phiếu Phạt";
            iconButton6.Tag = "Sign Out";
            iconButton5.Tag = "Quản Lý Nhân Viên";
            iconButton8.Tag = "Quản Lý Khách Hàng";
            iconButton7.Tag = "Admin";
            iconButton10.Tag = "Nạp Tiền Cho Khách";
            iconButton11.Tag = "Báo Cáo";
            panel3.MouseDown += panel2_MouseDown;
            panel3.MouseMove += panel2_MouseMove;
            panel3.MouseUp += panel2_MouseUp;
            panel3.DoubleClick += panel2_DoubleClick;
        }

        private void FMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width < 800)
            {
                panelMenu.Width = 80; 
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = ""; 
                }
            }
            else
            {
                panelMenu.Width = 252;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "    " + menuButton.Tag; 
                }
            }
        }

        private MomoCallbackListener _callbackListener;

        private async void ActivateListerner()
        {
            try
            {
                _callbackListener = new MomoCallbackListener();
                await Task.Run(() => _callbackListener.StartListener());
                MessageBox.Show("Callback listener started!");

            }
            catch (Exception ex)
            {
            }
        }
        public static void Call(decimal balance)
        {
            LoadMoney(balance);
        }
        public static decimal PBalance { get; set; }

        private static void LoadMoney(decimal balance2)
        {
            PBalance = balance2;
        }
        private void Moneyy()
        {
            PBalance = Ctrl_Wallet.GetUserBalance(username);
            label2.Text = "$: " + PBalance.ToString("#,0", CultureInfo.InvariantCulture).Replace(',', '.') + " VND";
        }
        private void FMain_Load(object sender, EventArgs e)
        {
            label1.Text = "Hello, " + username;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            maxMenu = panelMenu.Width;
            Moneyy();
            MomoCallbackListener.GetUsername(username);
            ActivateListerner();
            string IDUser = Ctrl_Account.GetUserID(username);
            LoadFormIntoPanel(new FCar(IDUser));
            var accList = Ctrl_Account.GetAccountsByUS(username);

            if (accList != null && accList.Count > 0)
            {
                var acc = accList.First(); 

                if (acc.Role == "Admin")
                {
                    

                }
                else if (acc.Role == "Customer")
                {
                    iconButton7.Visible = false;
                    iconButton5.Visible = false;

                    iconButton2.Visible = false;
                    iconButton3.Visible = false;
                    iconButton8.Visible = false;
                    iconButton10.Visible = false;
                    iconButton11.Visible = false;

                }
                else if (acc.Role == "Employee")
                {
                    iconButton7.Visible = false;
                    iconButton5.Visible = false;


                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void cCirclePB1_Click(object sender, EventArgs e)
        {
            string UserID = Ctrl_Account.GetUserID(username);
            FCustomerInfo fCustomer = new FCustomerInfo(UserID);
            fCustomer.ShowDialog();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            FCar_Admin form = new FCar_Admin();
            LoadFormIntoPanel(form);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.Red;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Transparent;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ToggleFormSize();
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            ToggleFormSize();
        }

        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            ToggleFormSize();
        }
        private void ToggleFormSize()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = new Size((maxWidth / 2) + 100, (maxHeight / 2) + 100) ;
                this.Location = new Point(
                    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2 ,
                    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
                );
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            this.Close();
            FLogin loginForm = new FLogin();
            loginForm.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuAction();
        }
        private void MenuAction()
        {
            if (this.panelMenu.Width > 200)
            {
                groupBox1.Visible = false;
                panelMenu.Width = 80;
                btnTopUp.Visible = false;
                btnWithDraw.Visible = false;
                groupBox2.Visible = false;
                iconButton6.Width = 80;
                label1.Visible = false;
                iconButton9.Visible = false;
                decimal balance = Ctrl_Wallet.GetUserBalance(username);
                label2.Text = balance.ToString();
                cCirclePB1.Size = new Size(45, 45);
                cCirclePB1.Location = new Point(
                    (panelMenu.Width - cCirclePB1.Width) / 2,
                    btnMenu.Bottom + 10
                );
                iconButton1.Width = 60;
                iconButton2.Width = 60;
                iconButton3.Width = 60;
                iconButton4.Width = 60;
                iconButton5.Width = 60;
                iconButton8.Width = 60;
                iconButton7.Width = 60;
                iconButton10.Width = 60;
                iconButton11.Width = 60;
                btnMenu.Dock = DockStyle.Top;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
            else
            {
                groupBox1.Visible = true;
                groupBox2.Visible = true;

                iconButton9.Visible = true;
                iconButton9.Size = new Size(40, 30);
                iconButton9.ImageAlign = ContentAlignment.MiddleLeft;
                panelMenu.Width = maxMenu;
                label1.Visible = true;
                iconButton1.Width = 220;
                iconButton2.Width = 220;
                iconButton3.Width = 220;
                iconButton6.Width = 252;
                iconButton4.Width = 220;
                iconButton5.Width = 220;
                iconButton8.Width = 220;
                iconButton7.Width = 220;
                iconButton10.Width = 220;
                iconButton11.Width = 220;

                btnTopUp.Visible = true;
                btnWithDraw.Visible = true;
                btnTopUp.Text = "Top Up";
                btnWithDraw.Text = "Withdraw";
                cCirclePB1.Size = new Size(100, 100);
                cCirclePB1.Location = new Point(
                    16, 54
                );
                decimal balance = Ctrl_Wallet.GetUserBalance(username);
                Moneyy();
                label2.Location = new Point(10, 214);
                btnMenu.Dock = DockStyle.None;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    if (menuButton.Tag != null)
                    {
                        menuButton.Text = "    " + menuButton.Tag.ToString();
                    }
                    else
                    {
                        menuButton.Text = "";
                    }
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
            }

            this.Refresh();
        }

        private void cCirclePB2_Click(object sender, EventArgs e)
        {

        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            var accList = Ctrl_Account.GetAccountsByUS(username);

            if (accList != null && accList.Count > 0)
            {
                var acc = accList.First();

                if (acc.Role == "Admin")
                {
                    FPen_Admin form = new FPen_Admin();
                    LoadFormIntoPanel(form);

                }
                else if (acc.Role == "Customer")
                {

                    FPenalties form = new FPenalties(username);
                    LoadFormIntoPanel(form);
                }
                else if (acc.Role == "Employee")
                {

                    FPen_Admin form = new FPen_Admin();
                    LoadFormIntoPanel(form);

                }
            }
            
        }
        private void LoadFormIntoPanel(Form form) //LOad Form Ở đây, Giải thích thôi chứ không cần đụng vào đây !
        {
            if (panelContainer.Controls.Count > 0)
                panelContainer.Controls[0].Dispose(); // Câu lệnh này chỉ để cấm việc load quá nhiều form 1 lúc 

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;  

            panelContainer.Controls.Add(form);  // tham chiếu
            panelContainer.Tag = form;
            form.Show(); 
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void iconButton1_Click_1(object sender, EventArgs e) // Ơr đây là nút nhảy qua form của mấy ông
        {
            string IDUser = Ctrl_Account.GetUserID(username);
            LoadFormIntoPanel(new FCar(IDUser));
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button4_Click_1(object sender, EventArgs e)
        {
            FTopUp fTopUp = new FTopUp(username);
            fTopUp.ShowDialog();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            FRentals form = new FRentals(username); // Gọi form mấy ông muốn nhảy qua 
            LoadFormIntoPanel(form);
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            Moneyy();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            FEmpoyees form = new FEmpoyees();
            LoadFormIntoPanel(form);
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            FCustomer fCustomer = new FCustomer();
            LoadFormIntoPanel(fCustomer);
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            FAdmin form = new FAdmin();
            LoadFormIntoPanel(form);
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            FTopUpForCus fTopUpForCus = new FTopUpForCus();
            LoadFormIntoPanel(fTopUpForCus);
        }

        private void iconButton11_Click(object sender, EventArgs e)
        {
            FReport form = new FReport();
            LoadFormIntoPanel(form);
        }
    }

}
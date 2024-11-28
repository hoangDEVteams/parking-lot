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
            iconButton1.Tag = "Home";
            iconButton2.Tag = "Settings";
            iconButton3.Tag = "Profile";
            iconButton4.Tag = "Dashboard";
            iconButton6.Tag = "Sign Out";
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
        

        private void FMain_Load(object sender, EventArgs e)
        {
            label1.Text = "Hello, " + username;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            maxMenu = panelMenu.Width;
            decimal balance = GetUserBalance(username);
            label2.Text = "Balance: " + balance.ToString() + "VND";
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void cCirclePB1_Click(object sender, EventArgs e)
        {

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
                iconButton6.Width = 80;
                label1.Visible = false;
                decimal balance = GetUserBalance(username);
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
                panelMenu.Width = maxMenu;
                label1.Visible = true;
                iconButton1.Width = 212;
                iconButton2.Width = 212;
                iconButton3.Width = 212;
                iconButton6.Width = 252;
                iconButton4.Width = 212;
                btnTopUp.Visible = true;
                btnWithDraw.Visible = true;
                btnTopUp.Text = "Top Up";
                btnWithDraw.Text = "Withdraw";
                cCirclePB1.Size = new Size(100, 100);
                cCirclePB1.Location = new Point(
                    16, 54
                );
                decimal balance = GetUserBalance(username);
                label2.Text = "Balance: " + balance.ToString() + "VND";
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
            FCar form = new FCar(); // Gọi form mấy ông muốn nhảy qua 
            LoadFormIntoPanel(form);
        }
        private decimal GetUserBalance(string username)
        {
            var balance = (from acc in CUltils.db.Accounts
                           join wal in CUltils.db.Wallets on acc.IDAcc equals wal.IDAcc
                           where acc.Username == username
                           select wal.Money).FirstOrDefault();

            return balance;

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button4_Click_1(object sender, EventArgs e)
        {
            var paymentService = new CZLPayAPI();
            var result = await paymentService.CreateOrder();
            MessageBox.Show(result ?? "Payment failed.");
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            FRentals form = new FRentals(); // Gọi form mấy ông muốn nhảy qua 
            LoadFormIntoPanel(form);
        }
    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.Views
{
    public partial class FMain : Form
    {
        private string username;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public FMain(string username)
        {
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
            iconButton6.Tag = "Sign Out";
            panel3.MouseDown += panel2_MouseDown;
            panel3.MouseMove += panel2_MouseMove;
            panel3.MouseUp += panel2_MouseUp;
            panel3.DoubleClick += panel2_DoubleClick;

            this.LocationChanged += FMain_LocationChanged;

        }
        private void FMain_LocationChanged(object sender, EventArgs e)
        {
            var workingArea = Screen.PrimaryScreen.WorkingArea;

            if (this.Left <= workingArea.Left)
            {
                this.Width = workingArea.Width / 2;
                this.Height = workingArea.Height;
                this.Location = new Point(workingArea.Left, workingArea.Top);
            }
            else if (this.Right >= workingArea.Right)
            {
                this.Width = workingArea.Width / 2;
                this.Height = workingArea.Height;
                this.Location = new Point(workingArea.Right - this.Width, workingArea.Top);
                iconButton1.Tag = "Home";
                iconButton2.Tag = "Settings";
                iconButton3.Tag = "Profile";
                iconButton6.Tag = "Sign Out";

            }
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            label1.Text = "Hello, " + username;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
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
            if (this.Width == 1920 && this.Height == 1080)
            {
                this.Width = 960;
                this.Height = 540;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Location = new Point(
                    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
                );
            }
            else
            {
                this.Width = 1920;
                this.Height = 1080;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Location = new Point(
                    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2 + 20
                );
            }
        }
        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            if (this.Width == 1920 && this.Height == 1080)
            {
                this.Width = 960;
                this.Height = 540;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Location = new Point(
                    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
                );
            }
            else
            {
                this.Width = 1920;
                this.Height = 1080;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Location = new Point(
                    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2 + 20
                );
            }
        }
        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            if (this.Width == 1920 && this.Height == 1080)
            {
                this.Width = 960;
                this.Height = 540;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Location = new Point(
                    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
                );
            }
            else
            {
                this.Width = 1920;
                this.Height = 1080;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Location = new Point(
                    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2 + 20
                );
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
                panelMenu.Width = 80;
                iconButton6.Width = 80;
                label1.Visible = false;
                cCirclePB1.Size = new Size(45, 45);
                cCirclePB1.Location = new Point(
                    (panelMenu.Width - cCirclePB1.Width) / 2, 
                    btnMenu.Bottom + 10 
                );
                iconButton1.Width = 60;
                iconButton2.Width = 60;
                iconButton3.Width = 60;
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
                panelMenu.Width = 252;
                label1.Visible = true;
                iconButton1.Width = 212;
                iconButton2.Width = 212;
                iconButton3.Width = 212;
                iconButton6.Width = 252;
                cCirclePB1.Size = new Size(131, 131);
                cCirclePB1.Location = new Point(
                    (panelMenu.Width - cCirclePB1.Width) / 2, 
                    btnMenu.Bottom + 20 
                );

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
        }

    }

}


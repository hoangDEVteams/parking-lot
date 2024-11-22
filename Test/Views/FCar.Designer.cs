namespace Test.Views
{
    partial class FCar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCar));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.txtFind = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgridVehicles = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblFacturer = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.btnCarRental = new System.Windows.Forms.Button();
            this.btnForminfor = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.vVehicleDataBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.qLbaixeDataSet1 = new Test.QLbaixeDataSet1();
            this.v_VehicleDataTableAdapter1 = new Test.QLbaixeDataSet1TableAdapters.V_VehicleDataTableAdapter();
            this.qLbaixeDataSet = new Test.QLbaixeDataSet();
            this.v_VehicleDataTableAdapter = new Test.QLbaixeDataSetTableAdapters.V_VehicleDataTableAdapter();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Manufacturer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ManufactureYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LicensePlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridVehicles)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vVehicleDataBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLbaixeDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLbaixeDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.cbStatus);
            this.panel1.Controls.Add(this.cbColor);
            this.panel1.Controls.Add(this.txtFind);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtgridVehicles);
            this.panel1.Location = new System.Drawing.Point(12, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1187, 900);
            this.panel1.TabIndex = 0;
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "",
            "Available",
            "Unavailable"});
            this.cbStatus.Location = new System.Drawing.Point(507, 69);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(141, 24);
            this.cbStatus.TabIndex = 4;
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            // 
            // cbColor
            // 
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Items.AddRange(new object[] {
            "",
            "Red",
            "Blue",
            "White",
            "Yellow",
            "Black"});
            this.cbColor.Location = new System.Drawing.Point(159, 69);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(141, 24);
            this.cbColor.TabIndex = 3;
            this.cbColor.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(124, 17);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(847, 30);
            this.txtFind.TabIndex = 2;
            this.txtFind.Text = "";
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find";
            // 
            // dtgridVehicles
            // 
            this.dtgridVehicles.AllowUserToAddRows = false;
            this.dtgridVehicles.AutoGenerateColumns = false;
            this.dtgridVehicles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgridVehicles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.VehicleTypeName,
            this.Color,
            this.Status,
            this.Manufacturer,
            this.ManufactureYear,
            this.Description,
            this.LicensePlate});
            this.dtgridVehicles.DataSource = this.vVehicleDataBindingSource1;
            this.dtgridVehicles.Location = new System.Drawing.Point(0, 99);
            this.dtgridVehicles.Name = "dtgridVehicles";
            this.dtgridVehicles.RowHeadersWidth = 51;
            this.dtgridVehicles.RowTemplate.Height = 24;
            this.dtgridVehicles.Size = new System.Drawing.Size(1184, 800);
            this.dtgridVehicles.TabIndex = 0;
            this.dtgridVehicles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgridVehicles_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblYear);
            this.panel2.Controls.Add(this.lblFacturer);
            this.panel2.Controls.Add(this.lblName);
            this.panel2.Controls.Add(this.lblCode);
            this.panel2.Controls.Add(this.btnCarRental);
            this.panel2.Controls.Add(this.btnForminfor);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(1227, 203);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(440, 675);
            this.panel2.TabIndex = 1;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.Location = new System.Drawing.Point(188, 532);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(0, 20);
            this.lblYear.TabIndex = 16;
            // 
            // lblFacturer
            // 
            this.lblFacturer.AutoSize = true;
            this.lblFacturer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacturer.Location = new System.Drawing.Point(180, 466);
            this.lblFacturer.Name = "lblFacturer";
            this.lblFacturer.Size = new System.Drawing.Size(0, 20);
            this.lblFacturer.TabIndex = 15;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(180, 404);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 20);
            this.lblName.TabIndex = 14;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(171, 338);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(0, 20);
            this.lblCode.TabIndex = 13;
            // 
            // btnCarRental
            // 
            this.btnCarRental.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(78)))));
            this.btnCarRental.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarRental.ForeColor = System.Drawing.Color.White;
            this.btnCarRental.Location = new System.Drawing.Point(258, 588);
            this.btnCarRental.Name = "btnCarRental";
            this.btnCarRental.Size = new System.Drawing.Size(153, 48);
            this.btnCarRental.TabIndex = 12;
            this.btnCarRental.Text = "Car Rental";
            this.btnCarRental.UseVisualStyleBackColor = false;
            this.btnCarRental.Click += new System.EventHandler(this.btnCarRental_Click);
            // 
            // btnForminfor
            // 
            this.btnForminfor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(232)))), ((int)(((byte)(207)))));
            this.btnForminfor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForminfor.Location = new System.Drawing.Point(27, 588);
            this.btnForminfor.Name = "btnForminfor";
            this.btnForminfor.Size = new System.Drawing.Size(153, 48);
            this.btnForminfor.TabIndex = 11;
            this.btnForminfor.Text = "Information";
            this.btnForminfor.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(383, 251);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(54, 31);
            this.button2.TabIndex = 6;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 532);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "ManufactureYear:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 466);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Manufacturer:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 404);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Vehicle Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 338);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vehicle Code:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(440, 245);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // vVehicleDataBindingSource1
            // 
            this.vVehicleDataBindingSource1.DataMember = "V_VehicleData";
            this.vVehicleDataBindingSource1.DataSource = this.qLbaixeDataSet1;
            // 
            // qLbaixeDataSet1
            // 
            this.qLbaixeDataSet1.DataSetName = "QLbaixeDataSet1";
            this.qLbaixeDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // v_VehicleDataTableAdapter1
            // 
            this.v_VehicleDataTableAdapter1.ClearBeforeFill = true;
            // 
            // qLbaixeDataSet
            // 
            this.qLbaixeDataSet.DataSetName = "QLbaixeDataSet";
            this.qLbaixeDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // v_VehicleDataTableAdapter
            // 
            this.v_VehicleDataTableAdapter.ClearBeforeFill = true;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "IDVehicleType";
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // VehicleTypeName
            // 
            this.VehicleTypeName.DataPropertyName = "VehicleTypeName";
            this.VehicleTypeName.HeaderText = "VehicleTypeName";
            this.VehicleTypeName.MinimumWidth = 6;
            this.VehicleTypeName.Name = "VehicleTypeName";
            this.VehicleTypeName.ReadOnly = true;
            this.VehicleTypeName.Width = 125;
            // 
            // Color
            // 
            this.Color.DataPropertyName = "Color";
            this.Color.HeaderText = "Color";
            this.Color.MinimumWidth = 6;
            this.Color.Name = "Color";
            this.Color.ReadOnly = true;
            this.Color.Width = 125;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 125;
            // 
            // Manufacturer
            // 
            this.Manufacturer.DataPropertyName = "Manufacturer";
            this.Manufacturer.HeaderText = "Manufacturer";
            this.Manufacturer.MinimumWidth = 6;
            this.Manufacturer.Name = "Manufacturer";
            this.Manufacturer.ReadOnly = true;
            this.Manufacturer.Width = 125;
            // 
            // ManufactureYear
            // 
            this.ManufactureYear.DataPropertyName = "ManufactureYear";
            this.ManufactureYear.HeaderText = "ManufactureYear";
            this.ManufactureYear.MinimumWidth = 6;
            this.ManufactureYear.Name = "ManufactureYear";
            this.ManufactureYear.ReadOnly = true;
            this.ManufactureYear.Width = 125;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 125;
            // 
            // LicensePlate
            // 
            this.LicensePlate.DataPropertyName = "LicensePlate";
            this.LicensePlate.HeaderText = "LicensePlate";
            this.LicensePlate.MinimumWidth = 6;
            this.LicensePlate.Name = "LicensePlate";
            this.LicensePlate.ReadOnly = true;
            this.LicensePlate.Width = 125;
            // 
            // FCar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1715, 1043);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FCar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FCar";
            this.Load += new System.EventHandler(this.FCar_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridVehicles)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vVehicleDataBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLbaixeDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLbaixeDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.RichTextBox txtFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgridVehicles;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnCarRental;
        private System.Windows.Forms.Button btnForminfor;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblFacturer;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCode;
        private QLbaixeDataSet qLbaixeDataSet;
        private QLbaixeDataSetTableAdapters.V_VehicleDataTableAdapter v_VehicleDataTableAdapter;
        private QLbaixeDataSet1 qLbaixeDataSet1;
        private System.Windows.Forms.BindingSource vVehicleDataBindingSource1;
        private QLbaixeDataSet1TableAdapters.V_VehicleDataTableAdapter v_VehicleDataTableAdapter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Color;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Manufacturer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ManufactureYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicensePlate;
    }
}
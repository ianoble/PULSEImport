namespace PULSEImport
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.btnNext1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.grpAccounts = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAccountSearch = new System.Windows.Forms.TextBox();
            this.dtgAccounts = new System.Windows.Forms.DataGridView();
            this.AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.ddlEnvironment = new System.Windows.Forms.ComboBox();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.lblAccountName = new MaterialSkin.Controls.MaterialLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lnkDeviceUploadTemplate = new System.Windows.Forms.LinkLabel();
            this.lnkUploadDevices = new System.Windows.Forms.LinkLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.dtgDevices = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.lnkEquipmentUploadTemplate = new System.Windows.Forms.LinkLabel();
            this.lnkUploadEquipment = new System.Windows.Forms.LinkLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.dtgEquipment = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.lnkVehicleUploadTemplate = new System.Windows.Forms.LinkLabel();
            this.lnkUploadVehicles = new System.Windows.Forms.LinkLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.dtgVehicles = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lnkPeopleUploadTemplate = new System.Windows.Forms.LinkLabel();
            this.lnkUploadPeople = new System.Windows.Forms.LinkLabel();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.dtgPeople = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tab3 = new System.Windows.Forms.TabPage();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.ofdDevices = new System.Windows.Forms.OpenFileDialog();
            this.ofdEquipment = new System.Windows.Forms.OpenFileDialog();
            this.ofdVehicles = new System.Windows.Forms.OpenFileDialog();
            this.ofdPeople = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutPULSEImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceModel = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PartNumber = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DeviceManufacturer = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DeviceDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.VehicleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleVIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleModel = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.VehicleType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.VehicleDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhoneType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PeopleDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.EquipmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EquipmentSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EquipmentYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EquipmentNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EquipmentModel = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.EquipmentType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.EquipmentDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.materialTabControl1.SuspendLayout();
            this.tab1.SuspendLayout();
            this.grpAccounts.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAccounts)).BeginInit();
            this.tab2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDevices)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEquipment)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVehicles)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPeople)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(256, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tab1);
            this.materialTabControl1.Controls.Add(this.tab2);
            this.materialTabControl1.Controls.Add(this.tab3);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(16, 80);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(776, 472);
            this.materialTabControl1.TabIndex = 2;
            this.materialTabControl1.SelectedIndexChanged += new System.EventHandler(this.materialTabControl1_SelectedIndexChanged);
            this.materialTabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.materialTabControl1_Selecting);
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.btnNext1);
            this.tab1.Controls.Add(this.grpAccounts);
            this.tab1.Controls.Add(this.materialLabel1);
            this.tab1.Controls.Add(this.ddlEnvironment);
            this.tab1.Location = new System.Drawing.Point(4, 22);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(768, 446);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "Account Info";
            // 
            // btnNext1
            // 
            this.btnNext1.AutoSize = true;
            this.btnNext1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNext1.Depth = 0;
            this.btnNext1.Enabled = false;
            this.btnNext1.Icon = null;
            this.btnNext1.Location = new System.Drawing.Point(688, 400);
            this.btnNext1.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnNext1.Name = "btnNext1";
            this.btnNext1.Primary = true;
            this.btnNext1.Size = new System.Drawing.Size(66, 36);
            this.btnNext1.TabIndex = 9;
            this.btnNext1.Text = "Next >";
            this.btnNext1.UseVisualStyleBackColor = true;
            this.btnNext1.Click += new System.EventHandler(this.btnNext1_Click);
            // 
            // grpAccounts
            // 
            this.grpAccounts.Controls.Add(this.panel1);
            this.grpAccounts.Controls.Add(this.dtgAccounts);
            this.grpAccounts.Enabled = false;
            this.grpAccounts.Location = new System.Drawing.Point(0, 64);
            this.grpAccounts.Name = "grpAccounts";
            this.grpAccounts.Size = new System.Drawing.Size(768, 328);
            this.grpAccounts.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.txtAccountSearch);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 24);
            this.panel1.TabIndex = 5;
            // 
            // txtAccountSearch
            // 
            this.txtAccountSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAccountSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountSearch.Location = new System.Drawing.Point(2, 2);
            this.txtAccountSearch.MaximumSize = new System.Drawing.Size(280, 30);
            this.txtAccountSearch.Name = "txtAccountSearch";
            this.txtAccountSearch.Size = new System.Drawing.Size(254, 18);
            this.txtAccountSearch.TabIndex = 0;
            this.txtAccountSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAccountSearch_KeyPress);
            // 
            // dtgAccounts
            // 
            this.dtgAccounts.AllowUserToAddRows = false;
            this.dtgAccounts.AllowUserToDeleteRows = false;
            this.dtgAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtgAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AccountName,
            this.TID});
            this.dtgAccounts.Location = new System.Drawing.Point(8, 48);
            this.dtgAccounts.MultiSelect = false;
            this.dtgAccounts.Name = "dtgAccounts";
            this.dtgAccounts.ReadOnly = true;
            this.dtgAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAccounts.Size = new System.Drawing.Size(744, 272);
            this.dtgAccounts.TabIndex = 3;
            this.dtgAccounts.SelectionChanged += new System.EventHandler(this.dtgAccounts_SelectionChanged);
            this.dtgAccounts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgAccounts_KeyDown);
            // 
            // AccountName
            // 
            this.AccountName.DataPropertyName = "name";
            this.AccountName.Frozen = true;
            this.AccountName.HeaderText = "Account Name";
            this.AccountName.MinimumWidth = 200;
            this.AccountName.Name = "AccountName";
            this.AccountName.ReadOnly = true;
            this.AccountName.Width = 200;
            // 
            // TID
            // 
            this.TID.DataPropertyName = "tid";
            this.TID.HeaderText = "Account TID";
            this.TID.MinimumWidth = 150;
            this.TID.Name = "TID";
            this.TID.ReadOnly = true;
            this.TID.Width = 150;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(8, 8);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(93, 19);
            this.materialLabel1.TabIndex = 5;
            this.materialLabel1.Text = "Environment";
            // 
            // ddlEnvironment
            // 
            this.ddlEnvironment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlEnvironment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlEnvironment.DisplayMember = "Value";
            this.ddlEnvironment.Enabled = false;
            this.ddlEnvironment.FormattingEnabled = true;
            this.ddlEnvironment.Location = new System.Drawing.Point(8, 32);
            this.ddlEnvironment.Name = "ddlEnvironment";
            this.ddlEnvironment.Size = new System.Drawing.Size(280, 21);
            this.ddlEnvironment.TabIndex = 4;
            this.ddlEnvironment.ValueMember = "Value";
            this.ddlEnvironment.SelectedIndexChanged += new System.EventHandler(this.ddlEnvironment_SelectedIndexChanged);
            // 
            // tab2
            // 
            this.tab2.BackColor = System.Drawing.SystemColors.Control;
            this.tab2.Controls.Add(this.lblAccountName);
            this.tab2.Controls.Add(this.tabControl1);
            this.tab2.Controls.Add(this.materialRaisedButton1);
            this.tab2.Location = new System.Drawing.Point(4, 22);
            this.tab2.Name = "tab2";
            this.tab2.Size = new System.Drawing.Size(768, 446);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "Import Data";
            // 
            // lblAccountName
            // 
            this.lblAccountName.AutoSize = true;
            this.lblAccountName.Depth = 0;
            this.lblAccountName.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblAccountName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAccountName.Location = new System.Drawing.Point(8, 8);
            this.lblAccountName.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(117, 19);
            this.lblAccountName.TabIndex = 7;
            this.lblAccountName.Text = "[Account Name]";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(768, 352);
            this.tabControl1.TabIndex = 6;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lnkDeviceUploadTemplate);
            this.tabPage4.Controls.Add(this.lnkUploadDevices);
            this.tabPage4.Controls.Add(this.materialLabel2);
            this.tabPage4.Controls.Add(this.dtgDevices);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(760, 326);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Devices";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lnkDeviceUploadTemplate
            // 
            this.lnkDeviceUploadTemplate.AutoSize = true;
            this.lnkDeviceUploadTemplate.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDeviceUploadTemplate.Location = new System.Drawing.Point(512, 8);
            this.lnkDeviceUploadTemplate.Name = "lnkDeviceUploadTemplate";
            this.lnkDeviceUploadTemplate.Size = new System.Drawing.Size(160, 19);
            this.lnkDeviceUploadTemplate.TabIndex = 4;
            this.lnkDeviceUploadTemplate.TabStop = true;
            this.lnkDeviceUploadTemplate.Text = "Download Template";
            this.lnkDeviceUploadTemplate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDeviceUploadTemplate_LinkClicked);
            // 
            // lnkUploadDevices
            // 
            this.lnkUploadDevices.AutoSize = true;
            this.lnkUploadDevices.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUploadDevices.Location = new System.Drawing.Point(688, 8);
            this.lnkUploadDevices.Name = "lnkUploadDevices";
            this.lnkUploadDevices.Size = new System.Drawing.Size(62, 19);
            this.lnkUploadDevices.TabIndex = 3;
            this.lnkUploadDevices.TabStop = true;
            this.lnkUploadDevices.Text = "Upload";
            this.lnkUploadDevices.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUploadDevices_LinkClicked);
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(8, 8);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(62, 19);
            this.materialLabel2.TabIndex = 2;
            this.materialLabel2.Text = "Devices";
            // 
            // dtgDevices
            // 
            this.dtgDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDevices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SerialNumber,
            this.DeviceModel,
            this.PartNumber,
            this.DeviceManufacturer,
            this.DeviceDelete});
            this.dtgDevices.Location = new System.Drawing.Point(8, 32);
            this.dtgDevices.Name = "dtgDevices";
            this.dtgDevices.Size = new System.Drawing.Size(744, 288);
            this.dtgDevices.TabIndex = 0;
            this.dtgDevices.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgDevices_CellContentClick);
            this.dtgDevices.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgDevices_CellEndEdit);
            this.dtgDevices.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgDevices_CellEnter);
            this.dtgDevices.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgDevices_CellValueChanged);
            this.dtgDevices.CurrentCellDirtyStateChanged += new System.EventHandler(this.dtgDevices_CurrentCellDirtyStateChanged);
            this.dtgDevices.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dtgDevices_RowValidating);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lnkEquipmentUploadTemplate);
            this.tabPage5.Controls.Add(this.lnkUploadEquipment);
            this.tabPage5.Controls.Add(this.materialLabel3);
            this.tabPage5.Controls.Add(this.dtgEquipment);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(760, 326);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Equipment";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // lnkEquipmentUploadTemplate
            // 
            this.lnkEquipmentUploadTemplate.AutoSize = true;
            this.lnkEquipmentUploadTemplate.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkEquipmentUploadTemplate.Location = new System.Drawing.Point(512, 8);
            this.lnkEquipmentUploadTemplate.Name = "lnkEquipmentUploadTemplate";
            this.lnkEquipmentUploadTemplate.Size = new System.Drawing.Size(160, 19);
            this.lnkEquipmentUploadTemplate.TabIndex = 6;
            this.lnkEquipmentUploadTemplate.TabStop = true;
            this.lnkEquipmentUploadTemplate.Text = "Download Template";
            this.lnkEquipmentUploadTemplate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEquipmentUploadTemplate_LinkClicked);
            // 
            // lnkUploadEquipment
            // 
            this.lnkUploadEquipment.AutoSize = true;
            this.lnkUploadEquipment.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUploadEquipment.Location = new System.Drawing.Point(688, 8);
            this.lnkUploadEquipment.Name = "lnkUploadEquipment";
            this.lnkUploadEquipment.Size = new System.Drawing.Size(62, 19);
            this.lnkUploadEquipment.TabIndex = 5;
            this.lnkUploadEquipment.TabStop = true;
            this.lnkUploadEquipment.Text = "Upload";
            this.lnkUploadEquipment.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUploadEquipment_LinkClicked);
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(8, 8);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(81, 19);
            this.materialLabel3.TabIndex = 4;
            this.materialLabel3.Text = "Equipment";
            // 
            // dtgEquipment
            // 
            this.dtgEquipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgEquipment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EquipmentID,
            this.EquipmentSN,
            this.EquipmentYear,
            this.EquipmentNotes,
            this.EquipmentModel,
            this.EquipmentType,
            this.EquipmentDelete});
            this.dtgEquipment.Location = new System.Drawing.Point(8, 32);
            this.dtgEquipment.Name = "dtgEquipment";
            this.dtgEquipment.Size = new System.Drawing.Size(744, 288);
            this.dtgEquipment.TabIndex = 3;
            this.dtgEquipment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgEquipment_CellContentClick);
            this.dtgEquipment.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgEquipment_CellEndEdit);
            this.dtgEquipment.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgEquipment_CellEnter);
            this.dtgEquipment.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dtgEquipment_RowValidating);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.lnkVehicleUploadTemplate);
            this.tabPage6.Controls.Add(this.lnkUploadVehicles);
            this.tabPage6.Controls.Add(this.materialLabel4);
            this.tabPage6.Controls.Add(this.dtgVehicles);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(760, 326);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = "Vehicles";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // lnkVehicleUploadTemplate
            // 
            this.lnkVehicleUploadTemplate.AutoSize = true;
            this.lnkVehicleUploadTemplate.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkVehicleUploadTemplate.Location = new System.Drawing.Point(512, 8);
            this.lnkVehicleUploadTemplate.Name = "lnkVehicleUploadTemplate";
            this.lnkVehicleUploadTemplate.Size = new System.Drawing.Size(160, 19);
            this.lnkVehicleUploadTemplate.TabIndex = 7;
            this.lnkVehicleUploadTemplate.TabStop = true;
            this.lnkVehicleUploadTemplate.Text = "Download Template";
            this.lnkVehicleUploadTemplate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkVehicleUploadTemplate_LinkClicked);
            // 
            // lnkUploadVehicles
            // 
            this.lnkUploadVehicles.AutoSize = true;
            this.lnkUploadVehicles.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUploadVehicles.Location = new System.Drawing.Point(688, 8);
            this.lnkUploadVehicles.Name = "lnkUploadVehicles";
            this.lnkUploadVehicles.Size = new System.Drawing.Size(62, 19);
            this.lnkUploadVehicles.TabIndex = 6;
            this.lnkUploadVehicles.TabStop = true;
            this.lnkUploadVehicles.Text = "Upload";
            this.lnkUploadVehicles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUploadVehicles_LinkClicked);
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(8, 8);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(67, 19);
            this.materialLabel4.TabIndex = 5;
            this.materialLabel4.Text = "Vehicles";
            // 
            // dtgVehicles
            // 
            this.dtgVehicles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgVehicles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VehicleID,
            this.VehicleVIN,
            this.VehicleYear,
            this.VehicleNotes,
            this.VehicleModel,
            this.VehicleType,
            this.VehicleDelete});
            this.dtgVehicles.Location = new System.Drawing.Point(8, 32);
            this.dtgVehicles.Name = "dtgVehicles";
            this.dtgVehicles.Size = new System.Drawing.Size(744, 288);
            this.dtgVehicles.TabIndex = 0;
            this.dtgVehicles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgVehicles_CellContentClick);
            this.dtgVehicles.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgVehicles_CellEndEdit);
            this.dtgVehicles.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgVehicles_CellEnter);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lnkPeopleUploadTemplate);
            this.tabPage1.Controls.Add(this.lnkUploadPeople);
            this.tabPage1.Controls.Add(this.materialLabel5);
            this.tabPage1.Controls.Add(this.dtgPeople);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(760, 326);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "People";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lnkPeopleUploadTemplate
            // 
            this.lnkPeopleUploadTemplate.AutoSize = true;
            this.lnkPeopleUploadTemplate.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkPeopleUploadTemplate.Location = new System.Drawing.Point(512, 8);
            this.lnkPeopleUploadTemplate.Name = "lnkPeopleUploadTemplate";
            this.lnkPeopleUploadTemplate.Size = new System.Drawing.Size(160, 19);
            this.lnkPeopleUploadTemplate.TabIndex = 8;
            this.lnkPeopleUploadTemplate.TabStop = true;
            this.lnkPeopleUploadTemplate.Text = "Download Template";
            // 
            // lnkUploadPeople
            // 
            this.lnkUploadPeople.AutoSize = true;
            this.lnkUploadPeople.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUploadPeople.Location = new System.Drawing.Point(688, 8);
            this.lnkUploadPeople.Name = "lnkUploadPeople";
            this.lnkUploadPeople.Size = new System.Drawing.Size(62, 19);
            this.lnkUploadPeople.TabIndex = 7;
            this.lnkUploadPeople.TabStop = true;
            this.lnkUploadPeople.Text = "Upload";
            this.lnkUploadPeople.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUploadPeople_LinkClicked);
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(8, 8);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(55, 19);
            this.materialLabel5.TabIndex = 6;
            this.materialLabel5.Text = "People";
            // 
            // dtgPeople
            // 
            this.dtgPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgPeople.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FirstName,
            this.LastName,
            this.EmployeeID,
            this.EmailAddress,
            this.PhoneNumber,
            this.PhoneType,
            this.PeopleDelete});
            this.dtgPeople.Location = new System.Drawing.Point(8, 32);
            this.dtgPeople.Name = "dtgPeople";
            this.dtgPeople.Size = new System.Drawing.Size(744, 288);
            this.dtgPeople.TabIndex = 1;
            this.dtgPeople.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgPeople_CellContentClick);
            this.dtgPeople.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgPeople_CellEndEdit);
            this.dtgPeople.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgPeople_CellEnter);
            // 
            // tabPage2
            // 
            this.tabPage2.Cursor = System.Windows.Forms.Cursors.No;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(760, 326);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "Associations";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.AutoSize = true;
            this.materialRaisedButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Icon = null;
            this.materialRaisedButton1.Location = new System.Drawing.Point(683, 400);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(71, 36);
            this.materialRaisedButton1.TabIndex = 1;
            this.materialRaisedButton1.Text = "Submit";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // tab3
            // 
            this.tab3.Location = new System.Drawing.Point(4, 22);
            this.tab3.Name = "tab3";
            this.tab3.Padding = new System.Windows.Forms.Padding(3);
            this.tab3.Size = new System.Drawing.Size(768, 446);
            this.tab3.TabIndex = 2;
            this.tab3.Text = "Associations";
            this.tab3.UseVisualStyleBackColor = true;
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 24);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(800, 40);
            this.materialTabSelector1.TabIndex = 3;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.pgbStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 552);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // pgbStatus
            // 
            this.pgbStatus.Name = "pgbStatus";
            this.pgbStatus.Size = new System.Drawing.Size(100, 16);
            this.pgbStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgbStatus.Visible = false;
            // 
            // ofdDevices
            // 
            this.ofdDevices.FileName = "openFileDialog1";
            this.ofdDevices.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdDevices_FileOk);
            // 
            // ofdEquipment
            // 
            this.ofdEquipment.FileName = "openFileDialog1";
            this.ofdEquipment.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdEquipment_FileOk);
            // 
            // ofdVehicles
            // 
            this.ofdVehicles.FileName = "openFileDialog1";
            this.ofdVehicles.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdVehicles_FileOk);
            // 
            // ofdPeople
            // 
            this.ofdPeople.FileName = "openFileDialog1";
            this.ofdPeople.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdPeople_FileOk);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.switchUserToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // switchUserToolStripMenuItem
            // 
            this.switchUserToolStripMenuItem.Name = "switchUserToolStripMenuItem";
            this.switchUserToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.switchUserToolStripMenuItem.Text = "Switch User";
            this.switchUserToolStripMenuItem.Click += new System.EventHandler(this.switchUserToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(132, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutPULSEImportToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutPULSEImportToolStripMenuItem
            // 
            this.aboutPULSEImportToolStripMenuItem.Name = "aboutPULSEImportToolStripMenuItem";
            this.aboutPULSEImportToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.aboutPULSEImportToolStripMenuItem.Text = "About PULSE Import";
            // 
            // SerialNumber
            // 
            this.SerialNumber.HeaderText = "Serial Number*";
            this.SerialNumber.MinimumWidth = 100;
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.Width = 102;
            // 
            // DeviceModel
            // 
            this.DeviceModel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DeviceModel.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DeviceModel.HeaderText = "Device Model";
            this.DeviceModel.MinimumWidth = 80;
            this.DeviceModel.Name = "DeviceModel";
            this.DeviceModel.Width = 80;
            // 
            // PartNumber
            // 
            this.PartNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PartNumber.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.PartNumber.HeaderText = "Part Number";
            this.PartNumber.MinimumWidth = 100;
            this.PartNumber.Name = "PartNumber";
            // 
            // DeviceManufacturer
            // 
            dataGridViewCellStyle6.NullValue = "CalAmp";
            this.DeviceManufacturer.DefaultCellStyle = dataGridViewCellStyle6;
            this.DeviceManufacturer.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DeviceManufacturer.HeaderText = "Manufacturer";
            this.DeviceManufacturer.Items.AddRange(new object[] {
            "CalAmp"});
            this.DeviceManufacturer.Name = "DeviceManufacturer";
            // 
            // DeviceDelete
            // 
            this.DeviceDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DeviceDelete.HeaderText = "";
            this.DeviceDelete.MinimumWidth = 100;
            this.DeviceDelete.Name = "DeviceDelete";
            this.DeviceDelete.Text = "Delete";
            this.DeviceDelete.UseColumnTextForButtonValue = true;
            // 
            // VehicleID
            // 
            this.VehicleID.HeaderText = "Vehicle ID (Name)";
            this.VehicleID.MinimumWidth = 125;
            this.VehicleID.Name = "VehicleID";
            this.VehicleID.Width = 125;
            // 
            // VehicleVIN
            // 
            this.VehicleVIN.HeaderText = "VIN";
            this.VehicleVIN.Name = "VehicleVIN";
            // 
            // VehicleYear
            // 
            this.VehicleYear.HeaderText = "Year";
            this.VehicleYear.Name = "VehicleYear";
            // 
            // VehicleNotes
            // 
            this.VehicleNotes.HeaderText = "Notes";
            this.VehicleNotes.Name = "VehicleNotes";
            // 
            // VehicleModel
            // 
            this.VehicleModel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VehicleModel.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.VehicleModel.HeaderText = "Model";
            this.VehicleModel.MinimumWidth = 100;
            this.VehicleModel.Name = "VehicleModel";
            // 
            // VehicleType
            // 
            this.VehicleType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VehicleType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.VehicleType.HeaderText = "Type";
            this.VehicleType.MinimumWidth = 100;
            this.VehicleType.Name = "VehicleType";
            // 
            // VehicleDelete
            // 
            this.VehicleDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VehicleDelete.HeaderText = "";
            this.VehicleDelete.MinimumWidth = 100;
            this.VehicleDelete.Name = "VehicleDelete";
            this.VehicleDelete.Text = "Delete";
            this.VehicleDelete.UseColumnTextForButtonValue = true;
            // 
            // FirstName
            // 
            this.FirstName.HeaderText = "First Name";
            this.FirstName.Name = "FirstName";
            // 
            // LastName
            // 
            this.LastName.HeaderText = "Last Name";
            this.LastName.Name = "LastName";
            // 
            // EmployeeID
            // 
            this.EmployeeID.HeaderText = "Employee ID";
            this.EmployeeID.Name = "EmployeeID";
            // 
            // EmailAddress
            // 
            this.EmailAddress.HeaderText = "Email Address";
            this.EmailAddress.Name = "EmailAddress";
            // 
            // PhoneNumber
            // 
            this.PhoneNumber.HeaderText = "Phone Number";
            this.PhoneNumber.Name = "PhoneNumber";
            this.PhoneNumber.Width = 125;
            // 
            // PhoneType
            // 
            this.PhoneType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PhoneType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.PhoneType.HeaderText = "Type";
            this.PhoneType.Items.AddRange(new object[] {
            "Work",
            "Home",
            "Mobile",
            "Other"});
            this.PhoneType.MinimumWidth = 100;
            this.PhoneType.Name = "PhoneType";
            this.PhoneType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PhoneType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // PeopleDelete
            // 
            this.PeopleDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PeopleDelete.HeaderText = "";
            this.PeopleDelete.MinimumWidth = 100;
            this.PeopleDelete.Name = "PeopleDelete";
            this.PeopleDelete.Text = "Delete";
            this.PeopleDelete.UseColumnTextForButtonValue = true;
            // 
            // EquipmentID
            // 
            this.EquipmentID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EquipmentID.HeaderText = "Equipment ID (Name)";
            this.EquipmentID.MinimumWidth = 135;
            this.EquipmentID.Name = "EquipmentID";
            this.EquipmentID.Width = 135;
            // 
            // EquipmentSN
            // 
            this.EquipmentSN.HeaderText = "Serial Number";
            this.EquipmentSN.Name = "EquipmentSN";
            // 
            // EquipmentYear
            // 
            this.EquipmentYear.HeaderText = "Year";
            this.EquipmentYear.Name = "EquipmentYear";
            // 
            // EquipmentNotes
            // 
            this.EquipmentNotes.HeaderText = "Notes";
            this.EquipmentNotes.Name = "EquipmentNotes";
            // 
            // EquipmentModel
            // 
            this.EquipmentModel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EquipmentModel.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.EquipmentModel.HeaderText = "Model";
            this.EquipmentModel.MaxDropDownItems = 30;
            this.EquipmentModel.MinimumWidth = 100;
            this.EquipmentModel.Name = "EquipmentModel";
            // 
            // EquipmentType
            // 
            this.EquipmentType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EquipmentType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.EquipmentType.HeaderText = "Type";
            this.EquipmentType.MaxDropDownItems = 30;
            this.EquipmentType.MinimumWidth = 100;
            this.EquipmentType.Name = "EquipmentType";
            // 
            // EquipmentDelete
            // 
            this.EquipmentDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EquipmentDelete.HeaderText = "";
            this.EquipmentDelete.MinimumWidth = 100;
            this.EquipmentDelete.Name = "EquipmentDelete";
            this.EquipmentDelete.Text = "Delete";
            this.EquipmentDelete.UseColumnTextForButtonValue = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 574);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "PULSE Import";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.materialTabControl1.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.grpAccounts.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAccounts)).EndInit();
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDevices)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEquipment)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVehicles)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPeople)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tab1;
        private System.Windows.Forms.DataGridView dtgAccounts;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar pgbStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TID;
        private System.Windows.Forms.Panel grpAccounts;
        private System.Windows.Forms.TabPage tab2;
        private MaterialSkin.Controls.MaterialRaisedButton btnNext1;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private System.Windows.Forms.DataGridView dtgDevices;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private System.Windows.Forms.DataGridView dtgEquipment;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private System.Windows.Forms.DataGridView dtgVehicles;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage1;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private System.Windows.Forms.DataGridView dtgPeople;
        private MaterialSkin.Controls.MaterialLabel lblAccountName;
        private System.Windows.Forms.LinkLabel lnkUploadDevices;
        private System.Windows.Forms.LinkLabel lnkUploadEquipment;
        private System.Windows.Forms.LinkLabel lnkUploadVehicles;
        private System.Windows.Forms.LinkLabel lnkUploadPeople;
        private System.Windows.Forms.OpenFileDialog ofdDevices;
        private System.Windows.Forms.OpenFileDialog ofdEquipment;
        private System.Windows.Forms.OpenFileDialog ofdVehicles;
        private System.Windows.Forms.OpenFileDialog ofdPeople;
        private System.Windows.Forms.LinkLabel lnkDeviceUploadTemplate;
        private System.Windows.Forms.LinkLabel lnkEquipmentUploadTemplate;
        private System.Windows.Forms.LinkLabel lnkVehicleUploadTemplate;
        private System.Windows.Forms.LinkLabel lnkPeopleUploadTemplate;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tab3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutPULSEImportToolStripMenuItem;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        internal System.Windows.Forms.ComboBox ddlEnvironment;
        private System.Windows.Forms.ToolStripMenuItem switchUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAccountSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewComboBoxColumn DeviceModel;
        private System.Windows.Forms.DataGridViewComboBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewComboBoxColumn DeviceManufacturer;
        private System.Windows.Forms.DataGridViewButtonColumn DeviceDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleVIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleNotes;
        private System.Windows.Forms.DataGridViewComboBoxColumn VehicleModel;
        private System.Windows.Forms.DataGridViewComboBoxColumn VehicleType;
        private System.Windows.Forms.DataGridViewButtonColumn VehicleDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn EquipmentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EquipmentSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn EquipmentYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn EquipmentNotes;
        private System.Windows.Forms.DataGridViewComboBoxColumn EquipmentModel;
        private System.Windows.Forms.DataGridViewComboBoxColumn EquipmentType;
        private System.Windows.Forms.DataGridViewButtonColumn EquipmentDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhoneNumber;
        private System.Windows.Forms.DataGridViewComboBoxColumn PhoneType;
        private System.Windows.Forms.DataGridViewButtonColumn PeopleDelete;
    }
}


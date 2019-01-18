using log4net;
using Newtonsoft.Json;
using OculusAPI.Models;
using OculusAPI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PULSEImport
{
    public partial class Form1 : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //private string apiUrl = "";
        //private string clientId = "";
        //private string secret = "";

        //private string oculusClientId = "oLXcXnpbxq84WLIMKbAgRlHBPq0a";
        //private string oculusSecret = "PteTFNyWMpyog0RlHn5YkVfsVBoa";

        private ConfigData _configData;

        private string _environtmentName;
        private string _accountTId;

        private OculusRequest oculusRequest;

        private List<Model> DeviceModels = new List<Model>();

        private List<string> PartNumbers = new List<string>();
        //{
        //    "903-4330-000", "903-4430-000", "903-4242-000", "903-6030-000"
        //};

        private List<Model> EquipmentModels = new List<Model>();
        private List<OculusType> EquipmentTypes = new List<OculusType>();

        public List<Model> VehicleModels { get; set; }
        public List<OculusType> VehicleTypes { get; set; }

        public Form1()
        {
            InitializeComponent();

            //// Create a material theme manager and add the form to manage (this)
            //MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            //materialSkinManager.AddFormToManage(this);
            //materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            //// Configure color scheme
            //materialSkinManager.ColorScheme = new ColorScheme(
            //    Primary.Blue500, Primary.Blue600,
            //    Primary.Blue600, Accent.LightBlue200,
            //    TextShade.WHITE
            //);

            var assembly = typeof(Form1).Assembly.GetName();

            Log4NetHelper.LogDebug(Logger, MethodBase.GetCurrentMethod().Name);
            Log4NetHelper.LogDebug(Logger, assembly.FullName + " " + assembly.Version);
            Logger.Debug("Started!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var environments = new List<DropDownItem>();

            environments.Add(new DropDownItem() { Value = "Staging", Text = "Staging" });
            environments.Add(new DropDownItem() { Value = "Production", Text = "Production" });
            environments.Add(new DropDownItem() { Value = "Production (EU)", Text = "Production (EU)" });

            ddlEnvironment.Items.AddRange(environments.ToArray());

            //ddlEnvironment.Focus();

            this.AcceptButton = null;

            this.ActiveControl = txtAccountSearch;

            //ShowLoginDialog();
        }

        private void ShowLoginDialog()
        {
            Login loginDialog = new Login();

            loginDialog.ShowDialog(this);

            //// Show testDialog as a modal dialog and determine if DialogResult = OK.
            //if (loginDialog.ShowDialog(this) == DialogResult.OK)
            //{
            //    // Read the contents of testDialog's TextBox.
            //    Logger.Debug(loginDialog.txtEmail.Text);
            //}
            //else
            //{
            //    Logger.Debug("Cancelled");
            //}

            //loginDialog.Dispose();
        }

        /* -- Account Information Tab -- */
        private void ddlEnvironment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var environment = ((DropDownItem)ddlEnvironment.SelectedItem).Value;
            _environtmentName = ((DropDownItem)ddlEnvironment.SelectedItem).Text;

            //switch (environment)
            //{
            //    case "Staging":
            //        _configData = EnvironmentConfig.GetStagingConfig();
            //        break;
            //    case "Production":
            //        _configData = EnvironmentConfig.GetProductionConfig();
            //        break;
            //    case "Production (EU)":
            //        _configData = EnvironmentConfig.GetProductionEUConfig();
            //        break;
            //}

            _configData = EnvironmentConfig.ConfigData;

            //if (!string.IsNullOrEmpty(_configData.AccessToken))
            //{
            grpAccounts.Enabled = true;

            this.ActiveControl = txtAccountSearch;
            //}
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SearchAccounts(txtAccountSearch.Text);
        }

        private void SearchAccounts(string account)
        {
            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            UpdateStatusBar("Searching accounts...", true);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(_configData.ApiUrl + "accounts/v5/?name=" + account);

            var accounts = JsonConvert.DeserializeObject<OculusAPI.Models.Accounts>(jsonResponse.ToString());

            if (accounts != null && accounts.accounts != null)
            {
                //var bindingSource1 = new BindingSource();

                //foreach (var acct in accounts.accounts)
                //{
                //    bindingSource1.Add(new AccountDisplay(acct.name, acct.tId));
                //}

                dtgAccounts.AutoGenerateColumns = false;
                dtgAccounts.DataSource = accounts.accounts;

                UpdateStatusBar(
                    string.Format("{0} Account{1} found!", accounts.accounts.Count,
                        accounts.accounts.Count > 1 ? "s" : ""));

                dtgAccounts.Focus();

                //btnNext1.Enabled = true;
            }
            else
            {
                UpdateStatusBar("Error searching accounts", false);

                MessageBox.Show("Could not reach the Accounts API.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdateStatusBar(string message = null, bool showProgress = false)
        {
            //if (!string.IsNullOrEmpty(message))
            //{
            lblStatus.Text = message;
            //}

            if (showProgress)
            {
                pgbStatus.Enabled = true;
                pgbStatus.Visible = true;
                pgbStatus.MarqueeAnimationSpeed = 30;

                statusStrip1.Refresh();
            }
            else
            {
                pgbStatus.Enabled = false;
                pgbStatus.Visible = false;
                pgbStatus.Style = ProgressBarStyle.Continuous;
            }
        }

        private void txtAccountSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                SearchAccounts(txtAccountSearch.Text);
            }
        }

        private void dtgAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (dtgAccounts.SelectedRows.Count > 0)
            {
                btnNext1.Enabled = true;
            }
        }

        private void btnNext1_Click(object sender, EventArgs e)
        {
            UpdateStatusBar("Setting up grids...", true);

            _accountTId = dtgAccounts.SelectedRows[0].Cells[1].Value.ToString();
            lblAccountName.Text = dtgAccounts.SelectedRows[0].Cells[0].Value.ToString();

            SetupImportGrids();

            materialTabControl1.SelectTab(1);

            UpdateStatusBar();
        }

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtgAccounts.SelectedRows.Count == 0)
            {
                materialTabControl1.SelectTab(0);
            }
        }

        private void materialTabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (materialTabControl1.SelectedIndex >= 1)
            {
                e.Cancel = (dtgAccounts.SelectedRows.Count == 0);
            }
        }

        /* -- Import Data Tab --  */
        private void SetupImportGrids()
        {
            LoadDeviceModels();

            LoadEqiupmentModels();
            LoadEquipmentTypes();

            LoadVehicleModels();
            LoadVehicleTypes();

            ((Control)this.tabPage2).Enabled = false;
        }

        private void LoadDevicePartNumbers()
        {
            PartNumbers.AddRange(
                DeviceModels
                    .Where(dm => dm.typeData.trimblePartNumber != null)
                    .Select(dm => dm.typeData.trimblePartNumber));

            //foreach (var model in DeviceModels)
            //{
            //    if (!PartNumbers.Contains(model.typeData.trimblePartNumber))
            //    {
            //        PartNumbers.Add(model.typeData.trimblePartNumber);
            //    }
            //}

            PartNumber.DataSource = PartNumbers;
        }

        private void LoadDeviceModels()
        {
            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            var oculusTId = "58240ee5e4b01e7825f67da6";

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                //_configData.PULSEApiUrl +
                //string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}", oculusTId, "MODELS", "SENSORS"));
                string.Format("types/v1?type={0}&targetObjCat={1}", "MODELS", "SENSORS"));

            var models = JsonConvert.DeserializeObject<OculusAPI.Models.Models>(jsonResponse.ToString());

            try
            {
                var deviceModels = models.types;

                var modelList = deviceModels.Where(m => m.typeData.mdl != null);

                if (modelList != null && modelList.Any())
                {
                    DeviceModels = modelList.OrderBy(m => m.typeData.mdl).ToList();

                    DeviceModel.DataSource = DeviceModels;
                    DeviceModel.DisplayMember = "modelName";
                    DeviceModel.ValueMember = "tId";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Log4NetHelper.LogError(Logger, e);

                //throw;
            }

            LoadDevicePartNumbers();
        }

        private void LoadEqiupmentModels()
        {
            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}", _accountTId, "MODELS", "ASSETS"));

            var models = JsonConvert.DeserializeObject<OculusAPI.Models.Models>(jsonResponse.ToString());

            EquipmentModels = models.types;

            EquipmentModel.DataSource = EquipmentModels.OrderBy(m => m.name).ToList();
            EquipmentModel.DisplayMember = "name";
            EquipmentModel.ValueMember = "name"; //"tId";
        }

        private void LoadEquipmentTypes()
        {
            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}", _accountTId, "TYPES", "ASSETS"));

            var types = JsonConvert.DeserializeObject<OculusAPI.Models.OculusTypes>(jsonResponse.ToString());

            EquipmentTypes = types.types;

            EquipmentType.DataSource = EquipmentTypes.OrderBy(m => m.name).ToList();
            EquipmentType.DisplayMember = "name";
            EquipmentType.ValueMember = "name"; // "tId";
        }

        private void LoadVehicleModels()
        {
            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}", _accountTId, "MODELS", "VEHICLES"));

            var models = JsonConvert.DeserializeObject<OculusAPI.Models.Models>(jsonResponse.ToString());

            VehicleModels = models.types;

            VehicleModel.DataSource = VehicleModels.OrderBy(m => m.name).ToList();
            VehicleModel.DisplayMember = "name";
            VehicleModel.ValueMember = "name"; // "tId";
        }

        private void LoadVehicleTypes()
        {
            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}", _accountTId, "TYPES", "VEHICLES"));

            var types = JsonConvert.DeserializeObject<OculusAPI.Models.OculusTypes>(jsonResponse.ToString());

            VehicleTypes = types.types;

            VehicleType.DataSource = VehicleTypes.OrderBy(m => m.name).ToList();
            VehicleType.DisplayMember = "name";
            VehicleType.ValueMember = "name"; // "tId";
        }

        private void dtgAccounts_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData & Keys.KeyCode) == Keys.Enter)
                btnNext1.PerformClick();
            else
                base.OnKeyDown(e);
        }

        #region Upload Stuff

        private void lnkUploadDevices_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdDevices.ShowDialog();
        }

        private void lnkUploadEquipment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdEquipment.ShowDialog();
        }

        private void lnkUploadVehicles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdVehicles.ShowDialog();
        }

        private void lnkUploadPeople_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdPeople.ShowDialog();
        }

        private void ofdDevices_FileOk(object sender, CancelEventArgs e)
        {
            this.Activate();
            string[] files = ofdDevices.FileNames;

            // Open each file and display the image in pictureBox1.
            // Call Application.DoEvents to force a repaint after each
            // file is read.        
            foreach (string file in files)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

                //Read the contents of CSV file.  
                string csvData = File.ReadAllText(fileInfo.FullName);

                var rows = csvData.Split('\n');

                //Execute a loop over the rows.  
                for (int i = 0; i <= rows.Count() - 1; i++)
                {
                    // Skip header
                    if (i > 0)
                    {
                        var row = rows[i];

                        if (!string.IsNullOrEmpty(row))
                        {
                            var fields = new List<string>();

                            //Execute a loop over the columns.  
                            foreach (string cell in row.Split(','))
                            {
                                if (!string.IsNullOrEmpty(cell.Replace("\r", "")))
                                    fields.Add(cell);
                            }

                            try
                            {
                                var model = DeviceModels.SingleOrDefault(dm =>
                                    dm.typeData.trimblePartNumber == fields[2].ToString());

                                if (!string.IsNullOrEmpty(SafeType.SafeString(model)))
                                {
                                    fields.Add(model.tId);
                                }

                                dtgDevices.Rows.Add(fields.ToArray());
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show("Could not process import.");
                                Console.WriteLine(exception);
                                Log4NetHelper.LogError(Logger, exception);

                                break;
                            }
                        }
                    }

                }

                UpdateStatusBar(
                    string.Format("{0} row{1} imported!", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void ofdEquipment_FileOk(object sender, CancelEventArgs e)
        {
            this.Activate();
            string[] files = ofdEquipment.FileNames;

            // Open each file and display the image in pictureBox1.
            // Call Application.DoEvents to force a repaint after each
            // file is read.        
            foreach (string file in files)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

                //Read the contents of CSV file.  
                string csvData = File.ReadAllText(fileInfo.FullName);

                var rows = csvData.Split('\n');

                //Execute a loop over the rows.  
                for (int i = 0; i <= rows.Count() - 1; i++)
                {
                    // Skip header
                    if (i > 0)
                    {
                        var row = rows[i];

                        if (!string.IsNullOrEmpty(row))
                        {
                            var fields = new List<string>();

                            //Execute a loop over the columns.  
                            foreach (string cell in row.Split(','))
                            {
                                if (!string.IsNullOrEmpty(cell.Replace("\r", "")))
                                    fields.Add(cell);
                            }

                            try
                            {
                                dtgEquipment.Rows.Add(fields.ToArray());
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show("Could not process import.");
                                Console.WriteLine(exception);
                                Log4NetHelper.LogError(Logger, exception);

                                break;
                            }
                        }
                    }

                }

                UpdateStatusBar(
                    string.Format("{0} row{1} imported!", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void ofdVehicles_FileOk(object sender, CancelEventArgs e)
        {
            this.Activate();
            string[] files = ofdVehicles.FileNames;

            // Open each file and display the image in pictureBox1.
            // Call Application.DoEvents to force a repaint after each
            // file is read.        
            foreach (string file in files)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

                //Read the contents of CSV file.  
                string csvData = File.ReadAllText(fileInfo.FullName);

                var rows = csvData.Split('\n');

                //Execute a loop over the rows.  
                for (int i = 0; i <= rows.Count() - 1; i++)
                {
                    // Skip header
                    if (i > 0)
                    {
                        var row = rows[i];

                        if (!string.IsNullOrEmpty(row))
                        {
                            var fields = new List<string>();

                            //Execute a loop over the columns.  
                            foreach (string cell in row.Split(','))
                            {
                                if (!string.IsNullOrEmpty(cell.Replace("\r", "")))
                                    fields.Add(cell);
                            }

                            try
                            {
                                dtgVehicles.Rows.Add(fields.ToArray());
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show("Could not process import.");
                                Console.WriteLine(exception);
                                Log4NetHelper.LogError(Logger, exception);

                                break;
                            }
                        }
                    }

                }

                UpdateStatusBar(
                    string.Format("{0} row{1} imported!", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void ofdPeople_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void lnkDeviceUploadTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/file/d/1aAun6agJGRCOpGn4ibd9gyokUK4n6qqd");
            Process.Start(sInfo);
        }

        private void lnkEquipmentUploadTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/open?id=1fu2TIiyfFKG-vo1L1cT52PVi9kT2ma17");
            Process.Start(sInfo);
        }

        private void lnkVehicleUploadTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/open?id=1QRlHkkHcbjIG_LBlwxBBUGDkW9LgIaeZ");
            Process.Start(sInfo);
        }

        #endregion

        // Submit button
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to import these items?", "Confirm Import",
                    MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

                if (dtgDevices.Rows.Count > 0)
                    ProcessDevices();

                if (dtgEquipment.Rows.Count > 0)
                    ProcessEquipment();

                if (dtgVehicles.Rows.Count > 0)
                    ProcessVehicles();

                if (dtgPeople.Rows.Count > 0)
                    ProcessPeople();

            }
        }

        #region Device Grid Events

        private void dtgDevices_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("CellEnter");

            StartEditingRow(sender, e);
        }

        private void dtgDevices_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("CellEndEdit");

            if (e.RowIndex < dtgDevices.Rows.Count)
            {
                // Clear the row error in case the user presses ESC.   
                dtgDevices.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void dtgDevices_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            Console.WriteLine("RowValidating");

            if (dtgDevices.Rows[e.RowIndex] != null &&
                !dtgDevices.Rows[e.RowIndex].IsNewRow &&
                dtgDevices.IsCurrentRowDirty)
            {
                foreach (DataGridViewColumn col in dtgDevices.Columns)
                {
                    string headerName = col.Name;

                    // Skip validation if cell is not in the Serial Number column.
                    if (headerName.Equals("SerialNumber"))
                    {
                        // Confirm that the cell is not empty.
                        if (string.IsNullOrEmpty(dtgDevices.Rows[e.RowIndex].Cells[col.Index].FormattedValue
                            .ToString()))
                        {
                            Console.WriteLine("Error!");

                            dtgDevices.Rows[e.RowIndex].ErrorText = "Serial Number must not be empty";
                            //e.Cancel = true;
                        }
                        else
                        {
                            // Clear the row error in case the user presses ESC.   
                            dtgDevices.Rows[e.RowIndex].ErrorText = String.Empty;
                        }
                    }
                }
            }
            else
            {
                // Clear the row error in case the user presses ESC.   
                dtgDevices.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        #endregion

        #region Equipment Grid Events

        private void dtgEquipment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("CellEndEdit");

            if (e.RowIndex < dtgEquipment.Rows.Count)
            {
                // Clear the row error in case the user presses ESC.   
                dtgEquipment.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void dtgEquipment_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            StartEditingRow(sender, e);
        }

        private void dtgEquipment_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            Console.WriteLine("RowValidating");

            if (dtgEquipment.Rows[e.RowIndex] != null &&
                !dtgEquipment.Rows[e.RowIndex].IsNewRow &&
                dtgEquipment.IsCurrentRowDirty)
            {
                foreach (DataGridViewColumn col in dtgEquipment.Columns)
                {
                    string headerName = col.Name;

                    // Skip validation if cell is not in the Serial Number column.
                    if (headerName.Equals("VIN"))
                    {
                        // Confirm that the cell is not empty.
                        if (string.IsNullOrEmpty(dtgEquipment.Rows[e.RowIndex].Cells[col.Index].FormattedValue
                            .ToString()))
                        {
                            dtgEquipment.Rows[e.RowIndex].ErrorText =
                                "VIN must not be empty";
                            e.Cancel = true;
                        }
                        else
                        {
                            // Clear the row error in case the user presses ESC.   
                            dtgEquipment.Rows[e.RowIndex].ErrorText = String.Empty;
                        }
                    }
                }
            }
            else
            {
                // Clear the row error in case the user presses ESC.   
                dtgEquipment.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        #endregion

        #region Vehicle Grid Events

        private void dtgVehicles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("CellEndEdit");

            if (e.RowIndex < dtgVehicles.Rows.Count)
            {
                // Clear the row error in case the user presses ESC.   
                dtgVehicles.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void dtgVehicles_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            StartEditingRow(sender, e);
        }

        #endregion

        #region People Grid Events

        private void dtgPeople_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgPeople_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        #endregion

        private void StartEditingRow(object sender, DataGridViewCellEventArgs e)
        {
            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
            var datagridview = sender as DataGridView;

            // Check to make sure the cell clicked is the cell containing the combobox 
            if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
            {
                datagridview.BeginEdit(true);
                ((ComboBox)datagridview.EditingControl).DroppedDown = true;
            }
        }

        // Process DEVICES
        private void ProcessDevices()
        {
            var sensors = new List<sensors>();

            foreach (DataGridViewRow row in dtgDevices.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    try
                    {
                        DataGridViewComboBoxCell model = (DataGridViewComboBoxCell)row.Cells[1];

                        sensors.Add(new PULSEImport.sensors()
                        {
                            accountTId = _accountTId,
                            sn = SafeType.SafeString(row.Cells[0].Value),
                            //name = SafeType.SafeString(row.Cells[1].Value),
                            //pn = SafeType.SafeString(row.Cells[1].Value),
                            mnf = SafeType.SafeString(row.Cells[3].FormattedValue),
                            mdl = SafeType.SafeString(model.FormattedValue),
                            mdlTId = SafeType.SafeString(model.Value)
                        });
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Could not add row: " + row.Index + 1);
                        Console.WriteLine(exception);

                        Log4NetHelper.LogError(Logger, exception);

                        //throw;
                    }
                }
            }

            if (sensors.Any())
            {
                var body = "{ \"sensors\": " + JsonConvert.SerializeObject(sensors, Formatting.None,
                               new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore
                               }) + " }";

                try
                {
                    Log4NetHelper.LogDebug(Logger, body);

                    var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

                    UpdateStatusBar("Adding devices...", true);

                    var response = oculusApiRequest.ExecutePOST(_configData.ApiUrl + "sensors/v5", body);

                    var respObj = JsonConvert.DeserializeObject<OculusResponse>(response.ToString());

                    Log4NetHelper.LogDebug(Logger, response.ToString());

                    if (respObj.statusCode == "400")
                    {
                        MessageBox.Show(respObj.statusDescr, "Could not import devices!");
                    }
                    else
                    {
                        UpdateStatusBar(string.Format("{0} sensor{1} added!", sensors.Count,
                            sensors.Count > 1 ? "s" : ""));
                        dtgDevices.Rows.Clear();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    Log4NetHelper.LogError(Logger, exception);

                    //throw;
                }
            }
        }

        // Process EQUIPMENT
        private void ProcessEquipment()
        {
            var equipment = new List<equipment>();

            foreach (DataGridViewRow row in dtgEquipment.Rows)
            {
                // Only process if there is a Serial Number
                if (row.Cells[1].Value != null)
                {
                    try
                    {
                        Console.WriteLine(SafeType.SafeString(row.Cells[0].Value));

                        var name = SafeType.SafeString(row.Cells[0].Value);
                        var sn = SafeType.SafeString(row.Cells[1].Value);

                        equipment.Add(new PULSEImport.equipment()
                        {
                            accountTId = _accountTId,
                            name = string.IsNullOrEmpty(name) ? sn : name,
                            sn = sn,
                            mdlYr = SafeType.SafeString(row.Cells[2].Value),
                            descr = SafeType.SafeString(row.Cells[3].Value),
                            mdl = SafeType.SafeString(row.Cells[4].Value),
                            type = SafeType.SafeString(row.Cells[5].Value)
                        });
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Could not add row: " + row.Index + 1);
                        Console.WriteLine(exception);
                        Log4NetHelper.LogError(Logger, exception);

                        //throw;
                    }
                }
            }

            if (equipment.Any())
            {
                var body = "{ \"assets\": " + JsonConvert.SerializeObject(equipment, Formatting.None,
                               new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore
                               }) + " }";

                try
                {
                    var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

                    UpdateStatusBar("Adding equipment...", true);

                    oculusApiRequest.ExecutePOST(_configData.ApiUrl + "assets/v4", body);

                    UpdateStatusBar(string.Format("{0} equipment added!", equipment.Count));

                    dtgEquipment.Rows.Clear();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    UpdateStatusBar("Could not add equipment!");
                    Log4NetHelper.LogError(Logger, exception);

                    //throw;
                }
            }
        }

        // Process VEHICLES
        private void ProcessVehicles()
        {
            var vehicles = new List<vehicle>();

            foreach (DataGridViewRow row in dtgVehicles.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    try
                    {
                        Console.WriteLine(SafeType.SafeString(row.Cells[1].Value));

                        var name = SafeType.SafeString(row.Cells[0].Value);
                        var vin = SafeType.SafeString(row.Cells[1].Value);

                        vehicles.Add(new PULSEImport.vehicle()
                        {
                            accountTId = _accountTId,
                            name = string.IsNullOrEmpty(name) ? vin : name,
                            vin = vin,
                            mdlYr = SafeType.SafeString(row.Cells[2].Value),
                            descr = SafeType.SafeString(row.Cells[3].Value),
                            mdl = SafeType.SafeString(row.Cells[4].Value),
                            type = SafeType.SafeString(row.Cells[5].Value)
                        });
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Could not add row: " + row.Index + 1);
                        Console.WriteLine(exception);
                        Log4NetHelper.LogError(Logger, exception);

                        //throw;
                    }
                }
            }

            if (vehicles.Any())
            {
                var body = "{ \"vehicles\": " + JsonConvert.SerializeObject(vehicles, Formatting.None,
                               new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore
                               }) + " }";

                try
                {
                    var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

                    UpdateStatusBar("Adding vehicles...", true);

                    oculusApiRequest.ExecutePOST(_configData.ApiUrl + "vehicles/v5", body);

                    UpdateStatusBar(string.Format("{0} vehicle{1} added!", vehicles.Count,
                        vehicles.Count > 1 ? "s" : ""));

                    dtgVehicles.Rows.Clear();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    Log4NetHelper.LogError(Logger, exception);

                    //throw;
                }
            }

        }

        // Process PEOPLE
        private void ProcessPeople()
        {
            var people = new List<person>();

            foreach (DataGridViewRow row in dtgPeople.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    try
                    {
                        Console.WriteLine(SafeType.SafeString(row.Cells[0].Value) + " " +
                                          SafeType.SafeString(row.Cells[1].Value));

                        people.Add(new PULSEImport.person()
                        {
                            accountTId = _accountTId,
                            fName = SafeType.SafeString(row.Cells[0].Value),
                            lName = SafeType.SafeString(row.Cells[1].Value),
                            employeeId = SafeType.SafeString(row.Cells[2].Value),
                            email = SafeType.SafeString(row.Cells[3].Value),
                            phoneNumber = SafeType.SafeString(row.Cells[4].Value),
                            phoneType = SafeType.SafeString(row.Cells[5].Value)
                        });
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Could not add row: " + row.Index + 1);
                        Console.WriteLine(exception);
                        Log4NetHelper.LogError(Logger, exception);

                        //throw;
                    }
                }
            }

            if (people.Any())
            {
                var body = "{ \"people\": " + JsonConvert.SerializeObject(people, Formatting.None,
                               new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore
                               }) + " }";

                try
                {
                    var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

                    UpdateStatusBar("Adding people...", true);

                    oculusApiRequest.ExecutePOST(_configData.ApiUrl + "people/v5", body);

                    UpdateStatusBar(string.Format("{0} {1} added!", people.Count,
                        people.Count > 1 ? "people" : "person"));

                    dtgPeople.Rows.Clear();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    Log4NetHelper.LogError(Logger, exception);

                    //throw;
                }
            }
        }



        private void dtgDevices_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the value of the selected Device Model
                DataGridViewComboBoxCell cbDeviceModel =
                    (DataGridViewComboBoxCell)dtgDevices.Rows[e.RowIndex].Cells[1];
                DataGridViewComboBoxCell cbPartNumber = (DataGridViewComboBoxCell)dtgDevices.Rows[e.RowIndex].Cells[2];

                // Device Model
                if (e.ColumnIndex == 1)
                {
                    if (cbDeviceModel.Value != null)
                    {
                        var deviceModel = DeviceModels.SingleOrDefault(m => m.tId == (string)cbDeviceModel.Value);

                        if (deviceModel != null)
                        {
                            var devicePN = PartNumbers.SingleOrDefault(p => p == (string)cbPartNumber.Value);

                            if (devicePN != deviceModel.typeData.trimblePartNumber)
                            {
                                // Force selection of Part Number
                                dtgDevices.Rows[e.RowIndex].Cells[2].Value = deviceModel.typeData.trimblePartNumber;
                            }
                        }
                    }
                }
                // Part Number
                else if (e.ColumnIndex == 2)
                {
                    if (cbPartNumber.Value != null)
                    {
                        // New part number
                        var devicePN = PartNumbers.SingleOrDefault(p => p == (string)cbPartNumber.Value);

                        if (cbDeviceModel.Value != null)
                        {
                            var deviceModel = DeviceModels.SingleOrDefault(m => m.typeData.trimblePartNumber == devicePN);

                            if (deviceModel.tId != (string)cbDeviceModel.Value)
                            {
                                // Force selection of Device Model
                                dtgDevices.Rows[e.RowIndex].Cells[1].Value = deviceModel.tId;
                            }
                        }
                    }
                }

            }
        }

        private void dtgDevices_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dtgDevices.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dtgDevices.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPage2)
                e.Cancel = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.LoggedIn)
            {
                ShowLoginDialog();
            }
        }

        private void switchUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLoginDialog();

        }

        private void dtgDevices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void dtgEquipment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void dtgVehicles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void dtgPeople_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void HandleRowDelete(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (senderGrid.Rows[e.RowIndex].IsNewRow == false)
                {
                    senderGrid.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
    }

    internal class DropDownItem
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    internal class sensors
    {
        public string accountTId { get; set; }
        public string name { get; set; }
        public string mnf { get; set; }
        public string sn { get; set; }
        public string mdl { get; set; }
        public string mdlTId { get; set; }
        public string pn { get; set; }
    }

    internal class equipment
    {
        public string accountTId { get; set; }
        public string name { get; set; }
        public string sn { get; set; }
        public string mdlYr { get; set; }
        public string descr { get; set; }
        public string mdl { get; set; }
        public string type { get; set; }
    }

    internal class vehicle
    {
        public string accountTId { get; set; }
        public string name { get; set; }
        public string vin { get; set; }
        public string mdlYr { get; set; }
        public string descr { get; set; }
        public string mdl { get; set; }
        public string type { get; set; }
    }

    internal class person
    {
        public string accountTId { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string employeeId { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string phoneType { get; set; }
    }

    //internal class AccountDisplay
    //{
    //    public string name { get; set; }
    //    public string tid { get; set; }

    //    internal AccountDisplay(string name, string tid)
    //    {
    //        this.name = name;
    //        this.tid = tid;
    //    }
    //}
}

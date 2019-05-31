using log4net;
using Newtonsoft.Json;
using OculusAPI.Models;
using OculusAPI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        private string _accountName;

        private const string ExportDate = "yyyy-dd-MM HH-mm-ss";

        private OculusRequest oculusRequest;

        private List<Model> DeviceModels = new List<Model>();

        private List<string> PartNumbers = new List<string>();
        //{
        //    "903-4330-000", "903-4430-000", "903-4242-000", "903-6030-000"
        //};

        private IList<Model> EquipmentModels;
        private IList<OculusType> EquipmentTypes;

        private IList<Model> VehicleModels { get; set; }
        private IList<OculusType> VehicleTypes { get; set; }

        public List<Sensor> AccountDevices { get; set; }
        public List<Vehicle> AccountVehicles { get; set; }
        public List<Asset> AccountEquipment { get; set; }
        public List<Person> AccountPeople { get; set; }

        public List<DropDownItem> AccountAssets { get; set; }

        public List<Model> AccountModels { get; set; }

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

            environments.Add(new DropDownItem() { Value = "QA", Text = "QA" });
            environments.Add(new DropDownItem() { Value = "Staging", Text = "Staging" });
            environments.Add(new DropDownItem() { Value = "Production", Text = "Production" });
            environments.Add(new DropDownItem() { Value = "Production (EU)", Text = "Production (EU)" });

            ddlEnvironment.Items.AddRange(environments.ToArray());

            //ddlEnvironment.Focus();

            this.AcceptButton = null;

            this.ActiveControl = txtAccountSearch;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                lblVersion.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4);
            }
            else
            {
                lblVersion.Text = Application.ProductVersion;
            }

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

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            UpdateStatusBar("Searching accounts...", true);

            Accounts accounts = await Task.Run(() => SearchAccounts(txtAccountSearch.Text));

            ShowAccounts(accounts);

            //SearchAccounts(txtAccountSearch.Text);
        }

        private async void txtAccountSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                UpdateStatusBar("Searching accounts...", true);

                Accounts accounts = await Task.Run(() => SearchAccounts(txtAccountSearch.Text));

                ShowAccounts(accounts);

                //SearchAccounts(txtAccountSearch.Text);
            }
        }

        private Accounts SearchAccounts(string account)
        {
            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(_configData.ApiUrl + "accounts/v4/?name=" + account);

            var accounts = JsonConvert.DeserializeObject<OculusAPI.Models.Accounts>(jsonResponse.ToString());

            return accounts;
        }

        // Binds accounts to grid
        private void ShowAccounts(Accounts accounts)
        {
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
                statusStrip1.Refresh();

                pgbStatus.Visible = true;
                pgbStatus.Enabled = true;
                pgbStatus.MarqueeAnimationSpeed = 30;

                statusStrip1.Refresh();
            }
            else
            {
                statusStrip1.Refresh();

                pgbStatus.Visible = false;
                pgbStatus.Enabled = false;
                //pgbStatus.Style = ProgressBarStyle.Continuous;

                statusStrip1.Refresh();
            }

            Console.WriteLine("Statusbar Message: " + lblStatus.Text);
        }

        private void dtgAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (dtgAccounts.SelectedRows.Count > 0)
            {
                btnNext.Enabled = true;
            }
        }

        // An Account has been selected
        private async void btnNext_Click(object sender, EventArgs e)
        {
            _accountTId = dtgAccounts.SelectedRows[0].Cells[1].Value.ToString();
            _accountName = dtgAccounts.SelectedRows[0].Cells[0].Value.ToString();

            lblAccountName.Text = _accountName;
            lblAccountName2.Text = _accountName;

            foreach (TabPage tp in tabControl1.TabPages)
            {
                ((Control)tp).Enabled = false;

                foreach (Control ctrl in tp.Controls)
                {
                    ctrl.Enabled = false;
                }
            }

            materialTabSelector1.Enabled = false;

            // Move to Import tab
            materialTabControl1.SelectTab(1);

            UpdateStatusBar("Setting up grids...", true);

            //await Task.Run(() => LoadExistingDevices());
            //await Task.Run(() => LoadExistingVehicles());
            //await Task.Run(() => LoadExistingEquipment());
            //await Task.Run(() => LoadExistingPeople());

            await Task.Run(() => SetupImportGrids());

            //timer1.Enabled = true;
            UpdateStatusBar("Ready for imports!");

            foreach (TabPage tp in tabControl1.TabPages)
            {
                ((Control)tp).Enabled = true;

                foreach (Control ctrl in tp.Controls)
                {
                    ctrl.Enabled = true;
                }
            }

            materialTabSelector1.Enabled = true;
        }

        private async void materialTabControl1_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            if (dtgAccounts.SelectedRows.Count == 0)
            {
                materialTabControl1.SelectTab(0);
            }

            if (materialTabControl1.SelectedIndex == 2)
            {
                UpdateStatusBar("Loading data...", true);

                flpExportTiles.Enabled = false;

                lblEquipModels.Text = EquipmentModels.Count.ToString();
                lblEquipTypes.Text = EquipmentTypes.Count.ToString();
                lblVehicleModels.Text = VehicleModels.Count.ToString();
                lblVehicleTypes.Text = VehicleTypes.Count.ToString();

                await Task.Run(() => LoadExistingDevices());
                await Task.Run(() => LoadExistingVehicles());
                await Task.Run(() => LoadExistingEquipment());
                await Task.Run(() => LoadExistingPeople());

                lblDeviceCount.Text = AccountDevices.Count.ToString();
                lblEquipmentCount.Text = AccountEquipment.Count.ToString();
                lblVehicleCount.Text = AccountVehicles.Count.ToString();
                lblPeopleCount.Text = AccountPeople.Count.ToString();

                flpExportTiles.Enabled = true;

                UpdateStatusBar("Ready for exports!");
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
        private async void SetupImportGrids()
        {
            Console.WriteLine("SetupImportGrids");

            List<Model> models = LoadDeviceModels(); // await Task.Run(() => LoadDeviceModels());

            ShowDeviceModels(models);

            List<Model> equipModels = LoadEquipmentModels(); // await Task.Run(() => LoadEquipmentModels());
            List<OculusType> equipTypes = LoadEquipmentTypes(); //await Task.Run(() => LoadEquipmentTypes());

            List<Model> vehModels = LoadVehicleModels(); // await Task.Run(() => LoadVehicleModels());
            List<OculusType> vehTypes = LoadVehicleTypes(); // await Task.Run(() => LoadVehicleTypes());

            List<OculusType> placeTypes = LoadPlaceTypes();

            ShowEquipmentModels(new Models() { types = equipModels });
            ShowEquipmentTypes(new OculusTypes() { types = equipTypes });
            ShowVehicleModels(new Models() { types = vehModels });
            ShowVehicleTypes(new OculusTypes() { types = vehTypes });
            ShowPlaceTypes(new OculusTypes() { types = placeTypes });
        }


        private List<Model> LoadDeviceModels()
        {
            Console.WriteLine("LoadDeviceModels");

            var deviceModels = new List<Model>();

            try
            {
                var modelCount = 0;
                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var models = QuerySensorModels(pageIndex); // await Task.Run(() => QueryExistingSensors(pageIndex));

                    modelCount = models.metaInfo.totRsltSetCnt;

                    deviceModels.AddRange(models.types.ToList());

                } while (modelCount > deviceModels.Count);

                Console.WriteLine("Device Models: " + deviceModels.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return deviceModels;
        }

        private Models QuerySensorModels(int pageIndex = 1)
        {
            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            var oculusTId = _configData.OculusTId; // "58240ee5e4b01e7825f67da6";

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                //_configData.PULSEApiUrl +
                //string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}", oculusTId, "MODELS", "SENSORS"));
                string.Format("types/v1?type={0}&targetObjCat={1}&pageIndex={2}", "MODELS", "SENSORS", pageIndex));

            var models = JsonConvert.DeserializeObject<OculusAPI.Models.Models>(jsonResponse.ToString());

            return models;
        }

        private void ShowDeviceModels(List<Model> deviceModels)
        {
            try
            {
                //var deviceModels = models.types;

                var modelList = deviceModels.Where(m => m.typeData.mdl != null);

                if (modelList != null && modelList.Any())
                {
                    DeviceModels = modelList.OrderBy(m => m.typeData.mdl).ToList();

                    DeviceModel.DataSource = DeviceModels;
                    DeviceModel.DisplayMember = "name";
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

        private void LoadDevicePartNumbers()
        {
            if (DeviceModels != null && DeviceModels.Any())
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
        }

        private void ShowAssets()
        {
            try
            {
                //var deviceModels = models.types;

                var assetList = new List<DropDownItem>();

                foreach (var equip in AccountEquipment)
                {
                    assetList.Add(new DropDownItem()
                    {
                        Text = string.Format("E: {0} ({1})", equip.name, equip.mdl),
                        Value = equip.tId
                    });
                }

                foreach (var veh in AccountVehicles)
                {
                    assetList.Add(new DropDownItem()
                    {
                        Text = string.Format("V: {0} ({1} {2})", veh.name, veh.mnf, veh.mdl),
                        Value = veh.tId
                    });
                }

                if (assetList != null && assetList.Any())
                {
                    AccountAssets = assetList.OrderBy(a => a.Text).ToList();

                    DeviceAssociation.DataSource = AccountAssets;
                    DeviceAssociation.DisplayMember = "Text";
                    DeviceAssociation.ValueMember = "Value";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Log4NetHelper.LogError(Logger, e);

                // throw;
            }
        }

        private async void LoadExistingDevices()
        {
            Console.WriteLine("LoadExistingDevices");

            try
            {
                var deviceCount = 0;
                AccountDevices = new List<Sensor>();
                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var devices = QueryExistingSensors(pageIndex); // await Task.Run(() => QueryExistingSensors(pageIndex));

                    deviceCount = devices.metaInfo.totRsltSetCnt;

                    AccountDevices.AddRange(devices.sensors.ToList());

                } while (deviceCount > AccountDevices.Count);

                Console.WriteLine("Devices: " + AccountDevices.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }

        private Sensors QueryExistingSensors(int pageIndex = 1)
        {
            Console.WriteLine("QueryExistingSensors");

            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            var jsonResponse =
                oculusApiRequest.ExecuteGET(_configData.ApiUrl +
                                            string.Format("sensors/v5/?accountTId={0}&pageIndex={1}", _accountTId, pageIndex));

            var devices = JsonConvert.DeserializeObject<OculusAPI.Models.Sensors>(jsonResponse.ToString());

            return devices;
        }

        private async void LoadExistingVehicles()
        {
            Console.WriteLine("LoadExistingVehicles");

            try
            {
                var vehicleCount = 0;
                AccountVehicles = new List<Vehicle>();
                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var vehicles = QueryExistingVehicles(pageIndex); // await Task.Run(() => QueryExistingSensors(pageIndex));

                    vehicleCount = vehicles.MetaInfo.totRsltSetCnt;

                    AccountVehicles.AddRange(vehicles.vehicles.ToList());

                } while (vehicleCount > AccountDevices.Count);

                Console.WriteLine("Vehicles: " + AccountVehicles.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }

        private Vehicles QueryExistingVehicles(int pageIndex = 1)
        {
            Console.WriteLine("QueryExistingVehicles");

            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            var jsonResponse =
                oculusApiRequest.ExecuteGET(_configData.ApiUrl +
                                            string.Format("vehicles/v5/?accountTId={0}&pageIndex={1}", _accountTId, pageIndex));

            var vehicles = JsonConvert.DeserializeObject<Vehicles>(jsonResponse.ToString());

            return vehicles;
        }

        private async void LoadExistingEquipment()
        {
            Console.WriteLine("LoadExistingEquipment");

            try
            {
                var equipmentCount = 0;
                AccountEquipment = new List<Asset>();
                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var assets = QueryExistingEquipment(pageIndex);

                    AccountEquipment = new List<Asset>();

                    AccountEquipment.AddRange(assets.assets.ToList());

                } while (equipmentCount > AccountEquipment.Count);

                Console.WriteLine("Equipment: " + AccountEquipment.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }

        private Assets QueryExistingEquipment(int pageIndex = 1)
        {
            Console.WriteLine("QueryExistingEquipment");

            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            var jsonResponse =
                oculusApiRequest.ExecuteGET(_configData.ApiUrl +
                                            string.Format("assets/v4/?accountTId={0}&pageIndex={1}", _accountTId, pageIndex));

            var equipment = JsonConvert.DeserializeObject<Assets>(jsonResponse.ToString());

            return equipment;
        }

        private async void LoadExistingPeople()
        {
            Console.WriteLine("LoadExistingPeople");

            try
            {
                var persons = QueryExistingPeople();

                AccountPeople = new List<Person>();

                AccountPeople.AddRange(persons.persons.ToList());

                Console.WriteLine("People: " + AccountPeople.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }

        private Persons QueryExistingPeople(int pageIndex = 1)
        {
            Console.WriteLine("QueryExistingPeople");

            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            var jsonResponse =
                oculusApiRequest.ExecuteGET(_configData.ApiUrl +
                                            string.Format("persons/v5/?accountTId={0}&pageIndex={1}", _accountTId, pageIndex));

            var people = JsonConvert.DeserializeObject<Persons>(jsonResponse.ToString());

            return people;
        }

        #region Models & Types

        private List<Model> LoadEquipmentModels()
        {
            Console.WriteLine("LoadEquipmentModels");

            var equipModels = new List<Model>();

            try
            {
                int? modelCount = 0;
                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var models = QueryExistingEquipmentModels(pageIndex);

                    modelCount = models?.metaInfo?.totRsltSetCnt;

                    if (modelCount != null && modelCount > 0)
                        equipModels.AddRange(models.types);

                } while (modelCount > equipModels.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            Console.WriteLine("Equipment Models: " + equipModels.Count);

            return equipModels;
        }

        private Models QueryExistingEquipmentModels(int pageIndex)
        {
            Console.WriteLine("QueryExistingEquipmentModels");

            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AppAccessToken);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}&pageSize=1000&pageIndex={3}", _accountTId, "MODELS",
                    "ASSETS", pageIndex));

            var models = JsonConvert.DeserializeObject<OculusAPI.Models.Models>(jsonResponse.ToString());

            return models;
        }

        private void ShowEquipmentModels(Models models)
        {
            EquipmentModels = models.types;

            EquipmentModel.DataSource = EquipmentModels.OrderBy(m => m.name).ToList();
            EquipmentModel.DisplayMember = "name";
            EquipmentModel.ValueMember = "name"; // "tId";
        }

        private List<OculusType> LoadEquipmentTypes()
        {
            Console.WriteLine("LoadEquipmentTypes");

            var equipmentTypes = new List<OculusType>();

            try
            {
                int? typeCount = 0;
                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var types = QueryExistingEquipmentTypes(pageIndex);

                    typeCount = types?.metaInfo?.totRsltSetCnt;

                    if (typeCount != null && typeCount > 0)
                        equipmentTypes.AddRange(types.types.ToList());

                } while (typeCount > equipmentTypes.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return equipmentTypes;
        }

        private OculusTypes QueryExistingEquipmentTypes(int pageIndex = 1)
        {
            Console.WriteLine("QueryExistingEquipmentTypes");

            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AppAccessToken);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}&pageSize=1000", _accountTId, "TYPES", "ASSETS"));

            var types = JsonConvert.DeserializeObject<OculusAPI.Models.OculusTypes>(jsonResponse.ToString());

            return types;
        }

        private void ShowEquipmentTypes(OculusTypes types)
        {
            EquipmentTypes = types.types;

            EquipmentType.DataSource = EquipmentTypes.OrderBy(m => m.name).ToList();
            EquipmentType.DisplayMember = "name";
            EquipmentType.ValueMember = "name"; //"tId";
        }

        private List<Model> LoadVehicleModels()
        {
            Console.WriteLine("LoadVehicleModels");

            var vehicleModels = new List<Model>();

            try
            {
                int? modelCount = 0;
                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var models = QueryExistingVehicleModels(pageIndex);

                    modelCount = models?.metaInfo?.totRsltSetCnt;

                    if (modelCount != null && modelCount > 0)
                        vehicleModels.AddRange(models.types.ToList());

                } while (modelCount > vehicleModels.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return vehicleModels;
        }

        private Models QueryExistingVehicleModels(int pageIndex = 1)
        {
            Console.WriteLine("QueryExistingVehicleModels");

            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AppAccessToken);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}", _accountTId, "MODELS", "VEHICLES"));

            var models = JsonConvert.DeserializeObject<OculusAPI.Models.Models>(jsonResponse.ToString());

            return models;
        }

        private void ShowVehicleModels(Models models)
        {
            VehicleModels = models.types;

            VehicleModel.DataSource = VehicleModels.OrderBy(m => m.name).ToList();
            VehicleModel.DisplayMember = "name";
            VehicleModel.ValueMember = "name"; // "tId";
        }

        private List<OculusType> LoadVehicleTypes()
        {
            Console.WriteLine("LoadVehicleTypes");

            var vehicleTypes = new List<OculusType>();

            try
            {
                int? typeCount = 0;
                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var types = QueryExistingVehicleTypes(pageIndex);

                    typeCount = types?.metaInfo?.totRsltSetCnt;

                    if (typeCount != null && typeCount > 0)
                        vehicleTypes.AddRange(types.types.ToList());

                } while (typeCount > vehicleTypes.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return vehicleTypes;
        }

        private OculusTypes QueryExistingVehicleTypes(int pageIndex = 1)
        {
            Console.WriteLine("QueryExistingVehicleTypes");

            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AppAccessToken);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}", _accountTId, "TYPES", "VEHICLES"));

            var types = JsonConvert.DeserializeObject<OculusAPI.Models.OculusTypes>(jsonResponse.ToString());

            return types;
        }

        private void ShowVehicleTypes(OculusTypes types)
        {
            VehicleTypes = types.types;

            VehicleType.DataSource = VehicleTypes.OrderBy(m => m.name).ToList();
            VehicleType.DisplayMember = "name";
            VehicleType.ValueMember = "name"; // "tId";
        }

        private List<OculusType> LoadPlaceTypes()
        {
            Console.WriteLine("QueryPlaceTypes");

            var placeTypes = new List<OculusType>();

            try
            {
                int? typeCount = 0;
                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var types = QueryPlaceTypes(pageIndex);

                    typeCount = types?.metaInfo?.totRsltSetCnt;

                    if (typeCount != null && typeCount > 0)
                        placeTypes.AddRange(types.types.ToList());

                } while (typeCount > placeTypes.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return placeTypes;
        }

        private OculusTypes QueryPlaceTypes(int pageIndex = 1)
        {
            Console.WriteLine("LoadPlaceTypes");

            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AppAccessToken);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}", _accountTId, "TYPES", "PLACES"));

            var types = JsonConvert.DeserializeObject<OculusAPI.Models.OculusTypes>(jsonResponse.ToString());

            return types;
        }

        private void ShowPlaceTypes(OculusTypes types)
        {
            PlaceType.DataSource = types.types.OrderBy(t => t.name).ToList();
            PlaceType.DisplayMember = "name";
            PlaceType.ValueMember = "name";
        }

        #endregion

        private void dtgAccounts_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData & Keys.KeyCode) == Keys.Enter)
                btnNext.PerformClick();
            else
                base.OnKeyDown(e);
        }

        #region Upload Stuff

        private void lnkUploadDevices_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdDevices.Filter = "csv files (*.csv)|*.csv";
            ofdDevices.RestoreDirectory = true;

            ofdDevices.ShowDialog();
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

                            var sn = row.Split(',')[0];
                            var pn = row.Split(',')[1];

                            // Serial Number
                            if (!string.IsNullOrEmpty(sn))
                                fields.Add(sn);

                            // Part Number
                            if (!string.IsNullOrEmpty(pn))
                            {
                                // Model
                                try
                                {
                                    var model = DeviceModels.SingleOrDefault(dm =>
                                        dm.typeData.trimblePartNumber == pn.ToString());

                                    if (!string.IsNullOrEmpty(SafeType.SafeString(model)))
                                    {
                                        fields.Add(model.tId);
                                    }

                                }
                                catch (Exception exception)
                                {
                                    LogText(exception.Message);

                                    //MessageBox.Show("Could not process import.");
                                    Console.WriteLine(exception);
                                    Log4NetHelper.LogError(Logger, exception);

                                    break;
                                }

                                fields.Add(pn);
                            }

                            dtgDevices.Rows.Add(fields.ToArray());
                        }
                    }

                }

                UpdateStatusBar(
                    string.Format("{0} row{1} ready for import.", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void lnkDeviceUploadTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/file/d/1aAun6agJGRCOpGn4ibd9gyokUK4n6qqd");
            Process.Start(sInfo);
        }


        private void lnkUploadEquipment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdEquipment.Filter = "csv files (*.csv)|*.csv";
            ofdEquipment.RestoreDirectory = true;

            ofdEquipment.ShowDialog();
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

                            var cells = new List<string>();

                            cells.AddRange(row.Split(','));

                            fields.Add(cells[0]);
                            fields.Add(cells[1]);
                            fields.Add(cells[2]);
                            fields.Add(cells[3]);

                            var equipModel = cells[4];

                            var model = EquipmentModels.SingleOrDefault(em =>
                                em.name.Equals(equipModel.ToString(), StringComparison.InvariantCultureIgnoreCase));

                            if (model != null)
                            {
                                fields.Add(model.name); //.tId);
                            }
                            else
                            {
                                fields.Add("");
                            }

                            var equipType = cells[5].Replace("\r", "");

                            var type = EquipmentTypes.SingleOrDefault(et =>
                                et.name.Equals(equipType.ToString(), StringComparison.InvariantCultureIgnoreCase));

                            if (type != null)
                            {
                                fields.Add(type.name); //.tId);
                            }
                            else
                            {
                                fields.Add("");
                            }

                            ////Execute a loop over the columns.  
                            //foreach (string cell in row.Split(','))
                            //{


                            //    //if (!string.IsNullOrEmpty(cell.Replace("\r", "")))
                            //    fields.Add(cell.Replace("\r", ""));
                            //}

                            try
                            {
                                dtgEquipment.Rows.Add(fields.ToArray());
                            }
                            catch (Exception exception)
                            {
                                LogText(exception.Message);

                                //MessageBox.Show("Could not process import.");
                                Console.WriteLine(exception);
                                Log4NetHelper.LogError(Logger, exception);

                                break;
                            }
                        }
                    }

                }

                UpdateStatusBar(
                    string.Format("{0} row{1} ready for import.", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void lnkEquipmentUploadTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/open?id=1fu2TIiyfFKG-vo1L1cT52PVi9kT2ma17");
            Process.Start(sInfo);
        }


        private void lnkUploadVehicles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdVehicles.Filter = "csv files (*.csv)|*.csv";
            ofdVehicles.RestoreDirectory = true;

            ofdVehicles.ShowDialog();
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

                            var cells = new List<string>();

                            cells.AddRange(row.Split(','));

                            fields.Add(cells[0]);
                            fields.Add(cells[1]);
                            fields.Add(cells[2]);
                            fields.Add(cells[3]);

                            var vehicleModel = cells[4];

                            var model = VehicleModels.SingleOrDefault(vm =>
                                vm.name.Equals(vehicleModel.ToString(), StringComparison.InvariantCultureIgnoreCase));

                            if (model != null)
                            {
                                fields.Add(model.name); //.tId);
                            }
                            else
                            {
                                fields.Add("");
                            }

                            var vehicleType = cells[5].Replace("\r", "");

                            var type = VehicleTypes.SingleOrDefault(vt =>
                                vt.name.Equals(vehicleType.ToString(), StringComparison.InvariantCultureIgnoreCase));

                            if (type != null)
                            {
                                fields.Add(type.name); //.tId);
                            }
                            else
                            {
                                fields.Add("");
                            }

                            ////Execute a loop over the columns.  
                            //foreach (string cell in row.Split(','))
                            //{
                            //    //if (!string.IsNullOrEmpty(cell.Replace("\r", "")))
                            //    fields.Add(cell.Replace("\r", ""));
                            //}

                            try
                            {
                                dtgVehicles.Rows.Add(fields.ToArray());
                            }
                            catch (Exception exception)
                            {
                                LogText(exception.Message);

                                //MessageBox.Show("Could not process import.");
                                Console.WriteLine(exception);
                                Log4NetHelper.LogError(Logger, exception);

                                break;
                            }
                        }
                    }

                }

                UpdateStatusBar(
                    string.Format("{0} row{1} ready for import.", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void lnkVehicleUploadTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/open?id=1QRlHkkHcbjIG_LBlwxBBUGDkW9LgIaeZ");
            Process.Start(sInfo);
        }


        private void lnkUploadPeople_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdPeople.Filter = "csv files (*.csv)|*.csv";
            ofdPeople.RestoreDirectory = true;

            ofdPeople.ShowDialog();
        }

        private void ofdPeople_FileOk(object sender, CancelEventArgs e)
        {
            this.Activate();
            string[] files = ofdPeople.FileNames;

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
                                dtgPeople.Rows.Add(fields.ToArray());
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
                    string.Format("{0} row{1} ready for import.", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void lnkPeopleUploadTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/open?id=140Z5h4NBxgzZ-EAXmyLSvXkmAFDhYkM9");
            Process.Start(sInfo);
        }


        private void lnkEquipModelTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/file/d/12Pe-lB4aiE1L1hBce18s-J__g4ukyqhb/view?usp=sharing");
            Process.Start(sInfo);
        }

        private void ofdEModels_FileOk(object sender, CancelEventArgs e)
        {
            this.Activate();
            string[] files = ofdEModels.FileNames;

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

                            var name = row.Split(',')[0];
                            var manufacturer = row.Split(',')[1];
                            var description = row.Split(',')[2].Replace("\r", ""); ;

                            // Name
                            if (!string.IsNullOrEmpty(name))
                                fields.Add(name);

                            // Manufacturer
                            if (!string.IsNullOrEmpty(manufacturer))
                                fields.Add(manufacturer);

                            // Description
                            if (!string.IsNullOrEmpty(description))
                                fields.Add(description);

                            dtgEquipmentModels.Rows.Add(fields.ToArray());
                        }
                    }
                }

                UpdateStatusBar(
                    string.Format("{0} row{1} ready for import.", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void lnkEquipModelUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdEModels.Filter = "csv files (*.csv)|*.csv";
            ofdEModels.RestoreDirectory = true;

            ofdEModels.ShowDialog();
        }


        private void lnkVehicleModelTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/open?id=1qiSN1AIBPvkbMePmy4kF3rXMHqrzoqiI");
            Process.Start(sInfo);
        }

        private void ofdVModels_FileOk(object sender, CancelEventArgs e)
        {
            this.Activate();
            string[] files = ofdVModels.FileNames;

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

                            var name = row.Split(',')[0];
                            var manufacturer = row.Split(',')[1];
                            var description = row.Split(',')[2].Replace("\r", ""); ;

                            // Name
                            if (!string.IsNullOrEmpty(name))
                                fields.Add(name);

                            // Manufacturer
                            if (!string.IsNullOrEmpty(manufacturer))
                                fields.Add(manufacturer);

                            // Description
                            if (!string.IsNullOrEmpty(description))
                                fields.Add(description);

                            dtgVehicleModels.Rows.Add(fields.ToArray());
                        }
                    }
                }

                UpdateStatusBar(
                    string.Format("{0} row{1} ready for import.", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void lnkVehicleModelUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdVModels.Filter = "csv files (*.csv)|*.csv";
            ofdVModels.RestoreDirectory = true;

            ofdVModels.ShowDialog();
        }


        private void lnkEquipTypeTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/open?id=1vI366Xf179JAgP_urUrKghvI83AeZ-fs");
            Process.Start(sInfo);
        }

        private void ofdETypes_FileOk(object sender, CancelEventArgs e)
        {
            this.Activate();
            string[] files = ofdETypes.FileNames;

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

                            var name = row.Split(',')[0];
                            var description = row.Split(',')[1].Replace("\r", ""); ;

                            // Name
                            if (!string.IsNullOrEmpty(name))
                                fields.Add(name);

                            // Description
                            if (!string.IsNullOrEmpty(description))
                                fields.Add(description);

                            dtgEquipmentTypes.Rows.Add(fields.ToArray());
                        }
                    }
                }

                UpdateStatusBar(
                    string.Format("{0} row{1} ready for import.", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void lnkEquipTypeUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdETypes.Filter = "csv files (*.csv)|*.csv";
            ofdETypes.RestoreDirectory = true;

            ofdETypes.ShowDialog();
        }


        private void lnkVehicleTypeTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/open?id=1iY88F50mMVkjR4EVhaM2mNYWb2xcdUTS");
            Process.Start(sInfo);
        }

        private void ofdVTypes_FileOk(object sender, CancelEventArgs e)
        {
            this.Activate();
            string[] files = ofdVTypes.FileNames;

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

                            var name = row.Split(',')[0];
                            var description = row.Split(',')[1].Replace("\r", ""); ;

                            // Name
                            if (!string.IsNullOrEmpty(name))
                                fields.Add(name);

                            // Description
                            if (!string.IsNullOrEmpty(description))
                                fields.Add(description);

                            dtgVehicleTypes.Rows.Add(fields.ToArray());
                        }
                    }
                }

                UpdateStatusBar(
                    string.Format("{0} row{1} ready for import.", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void lnkVehicleTypeUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdVTypes.Filter = "csv files (*.csv)|*.csv";
            ofdVTypes.RestoreDirectory = true;

            ofdVTypes.ShowDialog();
        }


        private void lnkPlacesUploadTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo =
                new ProcessStartInfo("https://drive.google.com/open?id=1EMnfO28SYs6ksbzMEzwICWhgfENs_lAp");
            Process.Start(sInfo);
        }

        private void ofdPlaces_FileOk(object sender, CancelEventArgs e)
        {
            this.Activate();
            string[] files = ofdPlaces.FileNames;

            var placeTypes = LoadPlaceTypes();

            foreach (string file in files)
            {
                System.IO.FileInfo fileInfo = new FileInfo(file);

                string csvData = File.ReadAllText(fileInfo.FullName);

                var rows = csvData.Split('\n');

                for (int i = 0; i <= rows.Count() - 1; i++)
                {
                    if (i > 0)
                    {
                        var row = rows[i];

                        if (!string.IsNullOrEmpty(row))
                        {
                            var fields = new List<string>();

                            var columns = row.Split(',');

                            var name = columns[0];
                            var type = columns[1];

                            // Check for Name value
                            fields.Add(name);

                            // Place Type
                            if (!string.IsNullOrEmpty(type))
                            {
                                var placeType = placeTypes.SingleOrDefault(pt =>
                                    pt.name.Equals(type, StringComparison.InvariantCultureIgnoreCase));

                                if (placeType != null)
                                {
                                    fields.Add(placeType.tId);
                                }
                                else
                                {
                                    fields.Add(type);
                                }
                            }
                            else
                            {
                                fields.Add(type);
                            }

                            var columnCount = row.Split(',').Count();

                            for (var c = 2; c < columnCount; c++)
                            {
                                fields.Add(columns[c]);
                            }

                            if (dtgPlaces.Columns.Count < fields.Count)
                            {
                                // Adding 2 to account for the buttons
                                while (dtgPlaces.Columns.Count < (fields.Count + 2))
                                {
                                    AddPlacesColumns(dtgPlaces);
                                }
                            }

                            dtgPlaces.Rows.Add(fields.ToArray());
                        }
                    }
                }

                UpdateStatusBar(
                    string.Format("{0} row{1} ready for import.", rows.Count() - 1, rows.Count() - 1 > 1 ? "s" : ""));

                break;
            }
        }

        private void lnkUploadPlaces_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdPlaces.Filter = "csv files (*.csv)|*.csv";
            ofdPlaces.RestoreDirectory = true;

            ofdPlaces.ShowDialog();
        }

        #endregion

        // Submit button
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to import these items?", "Confirm Import",
                    MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (dtgDevices.Rows.Count > 0 && !dtgDevices.Rows[0].IsNewRow)
                    ProcessDevices();

                if (dtgEquipment.Rows.Count > 0 && !dtgEquipment.Rows[0].IsNewRow)
                    ProcessEquipment();

                if (dtgVehicles.Rows.Count > 0 && !dtgVehicles.Rows[0].IsNewRow)
                    ProcessVehicles();

                if (dtgPeople.Rows.Count > 0 && !dtgPeople.Rows[0].IsNewRow)
                    ProcessPeople();

                if (dtgEquipmentModels.Rows.Count > 0 && !dtgEquipmentModels.Rows[0].IsNewRow)
                    ProcessEquipmentModels();

                if (dtgVehicleModels.Rows.Count > 0 && !dtgVehicleModels.Rows[0].IsNewRow)
                    ProcessVehicleModels();

                if (dtgEquipmentTypes.Rows.Count > 0 && !dtgEquipmentTypes.Rows[0].IsNewRow)
                    ProcessEquipmentTypes();

                if (dtgVehicleTypes.Rows.Count > 0 && !dtgVehicleTypes.Rows[0].IsNewRow)
                    ProcessVehicleTypes();

                if (dtgPlaces.Rows.Count > 0 && !dtgPlaces.Rows[0].IsNewRow)
                    ProcessPlaces();

            }
        }

        #region Device Grid Events

        private void dtgDevices_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Console.WriteLine("CellEnter");

            StartEditingRow(sender, e);
        }

        private void dtgDevices_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Console.WriteLine("CellEndEdit");

            if (e.RowIndex < dtgDevices.Rows.Count)
            {
                // Clear the row error in case the user presses ESC.   
                dtgDevices.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void dtgDevices_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Console.WriteLine("RowValidating");

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
                            // Console.WriteLine("Error!");

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

        private void dtgDevices_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
                dtgDevices.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the SerialNumber column.
            if (!headerText.Equals("SerialNumber")) return;

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dtgDevices.Rows[e.RowIndex].ErrorText =
                    "Serial Number must not be empty";
                e.Cancel = true;
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

        private void dtgDevices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void dtgDevices_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion

        #region Equipment Grid Events

        private void dtgEquipment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Console.WriteLine("CellEndEdit");

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
            // Console.WriteLine("RowValidating");

            if (dtgEquipment.Rows[e.RowIndex] != null &&
                !dtgEquipment.Rows[e.RowIndex].IsNewRow &&
                dtgEquipment.IsCurrentRowDirty)
            {
                foreach (DataGridViewColumn col in dtgEquipment.Columns)
                {
                    string headerName = col.Name;

                    // Skip validation if cell is not in the Serial Number column.
                    if (headerName.Equals("EquipmentSN"))
                    {
                        // Confirm that the cell is not empty.
                        if (string.IsNullOrEmpty(dtgEquipment.Rows[e.RowIndex].Cells[col.Index].FormattedValue
                            .ToString()))
                        {
                            dtgEquipment.Rows[e.RowIndex].ErrorText =
                                "Serial Number must not be empty";
                            //e.Cancel = true;
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

        private void dtgEquipment_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
                dtgEquipment.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the CompanyName column.
            if (!headerText.Equals("EquipmentSN")) return;

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dtgEquipment.Rows[e.RowIndex].ErrorText =
                    "Serial Number must not be empty";
                e.Cancel = true;
            }
        }

        private void dtgEquipment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void dtgEquipment_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex != EquipmentModel.Index)
            {
                //EquipmentType.Items.Add("");
                //EquipmentType.DataSource
            }

            e.Cancel = true;
        }

        #endregion

        #region Vehicle Grid Events

        private void dtgVehicles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Console.WriteLine("CellEndEdit");

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

        private void dtgVehicles_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Console.WriteLine("RowValidating");

            if (dtgVehicles.Rows[e.RowIndex] != null &&
                !dtgVehicles.Rows[e.RowIndex].IsNewRow &&
                dtgVehicles.IsCurrentRowDirty)
            {
                foreach (DataGridViewColumn col in dtgVehicles.Columns)
                {
                    string headerName = col.Name;

                    // Skip validation if cell is not in the Serial Number column.
                    if (headerName.Equals("VIN"))
                    {
                        // Confirm that the cell is not empty.
                        if (string.IsNullOrEmpty(dtgVehicles.Rows[e.RowIndex].Cells[col.Index].FormattedValue
                            .ToString()))
                        {
                            dtgVehicles.Rows[e.RowIndex].ErrorText =
                                "VIN must not be empty";
                            //e.Cancel = true;
                        }
                        else
                        {
                            // Clear the row error in case the user presses ESC.   
                            dtgVehicles.Rows[e.RowIndex].ErrorText = String.Empty;
                        }
                    }
                }
            }
            else
            {
                // Clear the row error in case the user presses ESC.   
                dtgVehicles.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void dtgVehicles_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
                dtgVehicles.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the CompanyName column.
            if (!headerText.Equals("VIN")) return;

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dtgVehicles.Rows[e.RowIndex].ErrorText =
                    "VIN must not be empty";
                e.Cancel = true;
            }
        }

        private void dtgVehicles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void dtgVehicles_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion

        #region People Grid Events

        private void dtgPeople_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgPeople_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgPeople_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
                dtgPeople.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the CompanyName column.
            if (!headerText.Equals("LastName")) return;

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dtgPeople.Rows[e.RowIndex].ErrorText =
                    "Last Name must not be empty";
                e.Cancel = true;
            }
        }

        private void dtgPeople_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void dtgPeople_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion

        #region Equipment Model Events
        private void dtgEquipmentModels_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Console.WriteLine("CellEnter");

            StartEditingRow(sender, e);
        }

        private void dtgEquipmentModels_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Console.WriteLine("CellEndEdit");

            if (e.RowIndex < dtgDevices.Rows.Count)
            {
                // Clear the row error in case the user presses ESC.   
                dtgEquipmentModels.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void dtgEquipmentModels_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
                dtgEquipmentModels.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the Name column.
            if (!headerText.Equals("Name")) return;

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dtgEquipmentModels.Rows[e.RowIndex].ErrorText =
                    "Name must not be empty";
                e.Cancel = true;
            }
        }

        private void dtgEquipmentModels_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Nothing here, yet
        }

        private void dtgEquipmentModels_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dtgEquipmentModels.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dtgEquipmentModels.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dtgEquipmentModels_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void dtgEquipmentModels_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion

        #region Vehicle Model Events
        private void dtgVehicleModels_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            StartEditingRow(sender, e);
        }

        private void dtgVehicleModels_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dtgVehicleModels.Rows.Count)
            {
                // Clear the row error in case the user presses ESC.   
                dtgVehicleModels.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void dtgVehicleModels_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
                dtgVehicleModels.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the Name column.
            if (!headerText.Equals("Name")) return;

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dtgVehicleModels.Rows[e.RowIndex].ErrorText =
                    "Name must not be empty";
                e.Cancel = true;
            }
        }

        private void dtgVehicleModels_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Nothing here, yet
        }

        private void dtgVehicleModels_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dtgVehicleModels.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dtgVehicleModels.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dtgVehicleModels_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void dtgVehicleModels_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion

        #region Equipment Type Events
        private void dtgEquipmentTypes_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            StartEditingRow(sender, e);
        }

        private void dtgEquipmentTypes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dtgEquipmentTypes.Rows.Count)
            {
                // Clear the row error in case the user presses ESC.   
                dtgEquipmentTypes.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void dtgEquipmentTypes_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
                dtgEquipmentTypes.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the Name column.
            if (!headerText.Equals("Name")) return;

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dtgEquipmentTypes.Rows[e.RowIndex].ErrorText =
                    "Name must not be empty";
                e.Cancel = true;
            }
        }

        private void dtgEquipmentTypes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Nothing here, yet
        }

        private void dtgEquipmentTypes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dtgEquipmentTypes.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dtgEquipmentTypes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dtgEquipmentTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void dtgEquipmentTypes_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion

        #region Vehicle Type Events
        private void dtgVehicleTypes_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            StartEditingRow(sender, e);
        }

        private void dtgVehicleTypes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dtgVehicleTypes.Rows.Count)
            {
                // Clear the row error in case the user presses ESC.   
                dtgVehicleTypes.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void dtgVehicleTypes_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
                dtgVehicleTypes.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the Name column.
            if (!headerText.Equals("Name")) return;

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dtgVehicleTypes.Rows[e.RowIndex].ErrorText =
                    "Name must not be empty";
                e.Cancel = true;
            }
        }

        private void dtgVehicleTypes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Nothing here, yet
        }

        private void dtgVehicleTypes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dtgVehicleTypes.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dtgVehicleTypes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dtgVehicleTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowDelete(sender, e);
        }

        private void dtgVehicleTypes_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion

        #region Places Events
        private void dtgPlaces_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            StartEditingRow(sender, e);
        }

        private void dtgPlaces_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dtgPlaces.Rows.Count)
            {
                // Clear the row error in case the user presses ESC.   
                dtgPlaces.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void dtgPlaces_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
                dtgPlaces.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the Name column.
            if (!headerText.Equals("Name")) return;

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dtgPlaces.Rows[e.RowIndex].ErrorText =
                    "Name must not be empty";
                e.Cancel = true;
            }
        }

        private void dtgPlaces_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Nothing here yet
        }

        private void dtgPlaces_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dtgPlaces.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dtgPlaces.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dtgPlaces_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (senderGrid.Columns[e.ColumnIndex].Name == "Delete")
                {
                    HandleRowDelete(sender, e);
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name == "AddPoint")
                {
                    AddPlacesColumns(senderGrid);
                }
            }
        }

        private void AddPlacesColumns(DataGridView dataGrid)
        {
            var colCount = dataGrid.Columns.Count;

            var pointNumber = (colCount / 2) - 1;

            // Latitude
            var newLatColumn = new DataGridViewTextBoxColumn();

            newLatColumn.HeaderText = string.Format("Latitude {0}", pointNumber);
            newLatColumn.Name = string.Format("Latitude {0}", pointNumber);

            // Longitude
            var newLonColumn = new DataGridViewTextBoxColumn();

            newLonColumn.HeaderText = string.Format("Longitude {0}", pointNumber);
            newLonColumn.Name = string.Format("Longitude {0}", pointNumber);

            dataGrid.Columns.Insert((dataGrid.Columns.Count - 2), newLatColumn);
            dataGrid.Columns.Insert((dataGrid.Columns.Count - 2), newLonColumn);
        }

        private void dtgPlaces_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
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
            // Load devices to make sure there are no duplicates
            LoadExistingDevices();

            var validDevices = true;

            var sensors = new List<sensors>();

            foreach (DataGridViewRow row in dtgDevices.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    var sn = SafeType.SafeString(row.Cells[0].Value);

                    // Check if device is already in the system...
                    if (AccountDevices.Any(d =>
                        d.sn.Equals(sn, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        validDevices = false;

                        var errorText = string.Format("{0} is a duplicate Serial Number.", sn);

                        LogText(errorText);

                        row.ErrorText = errorText;
                        //row.Cells[0].ErrorText = errorText;
                    }

                    if (validDevices)
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
                            LogText("Could not add row: " + row.Index + 1);

                            Console.WriteLine("Could not add row: " + row.Index + 1);
                            Console.WriteLine(exception);

                            Log4NetHelper.LogError(Logger, exception);

                            //throw;
                        }
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

                if (SaveToOculus(body, "sensors", sensors.Count, "sensors/v5"))
                    dtgDevices.Rows.Clear();

            }
        }

        private void AssociateDevices(OculusResponse oculusResponse)
        {
            var associations = new associations();

            // How do we get the TId for the devices that were just added?
            // The POST response just lists TId but with not context to the device details...

            // "associations/v4"
            foreach (DataGridViewRow row in dtgDevices.Rows)
            {
                var sn = SafeType.SafeString(row.Cells[0].Value);

                DataGridViewComboBoxCell asset = (DataGridViewComboBoxCell)row.Cells[4];

                if (asset != null && !string.IsNullOrEmpty(SafeType.SafeString(asset.Value)))
                {
                    // Get the newly added sensor
                    var sensor = GetSensorTIdBySN(sn);

                    if (sensor?.tId != null && !string.IsNullOrEmpty(sensor.tId))
                    {
                        var assetTId = SafeType.SafeString(asset.Value);

                        associations.Associations.Add(new association()
                        {
                            accountTId = _accountTId,
                            type = "INSTALLED_IN",
                            name = "Sensor_Vehicle_Association",
                            descr = string.Format("{0} installed in {1}", sensor.tId, assetTId),
                            lifeState = "ACTV",
                            pmryTId = sensor.tId,
                            scdyTId = assetTId,
                            startDt = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-ddTHH:mm:ssZ")
                        });
                    }
                }
            }

            if (associations.Associations.Any())
            {
                var body = "{ \"associations\": " + JsonConvert.SerializeObject(associations, Formatting.None,
                               new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore
                               }) + " }";

                var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

                UpdateStatusBar("Associating devices...", true);

                var response = oculusApiRequest.ExecutePOST(_configData.ApiUrl + "associations/v5", body);

                if (!string.IsNullOrEmpty(SafeType.SafeString(response)))
                {
                    Log4NetHelper.LogDebug(Logger, response.ToString());

                    var associationResponse = JsonConvert.DeserializeObject<OculusResponse>(response.ToString());

                    if (associationResponse.statusCode != "201")
                    {
                        LogText(string.Format("{0}: {1}",
                            SafeType.SafeString(associationResponse.statusCode),
                            SafeType.SafeString(associationResponse.statusDescr)));

                        MessageBox.Show("There was a problem importing the data.");
                    }
                }
            }

        }

        private Sensor GetSensorTIdBySN(string sn)
        {
            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                string.Format("sensors/v5?accountTId={0}&sn={1}", _accountTId, sn));

            var sensors = JsonConvert.DeserializeObject<OculusAPI.Models.Sensor>(jsonResponse.ToString());

            return sensors;
        }

        // Process EQUIPMENT
        private void ProcessEquipment()
        {
            LoadExistingEquipment();

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

                        var model = EquipmentModels.SingleOrDefault(vm =>
                            vm.name == row.Cells[4].FormattedValue.ToString());

                        var type = EquipmentTypes.SingleOrDefault(vm =>
                            vm.name == row.Cells[5].FormattedValue.ToString());

                        equipment.Add(new PULSEImport.equipment()
                        {
                            accountTId = _accountTId,
                            name = string.IsNullOrEmpty(name) ? sn : name,
                            sn = sn,
                            mdlYr = SafeType.SafeString(row.Cells[2].Value),
                            descr = SafeType.SafeString(row.Cells[3].Value),
                            mdl = SafeType.SafeString(row.Cells[4].FormattedValue),
                            type = SafeType.SafeString(row.Cells[5].FormattedValue),
                            mnf = model?.typeData.mnf,
                            mdlTId = model?.tId,
                            typeTId = type?.tId
                        });
                    }
                    catch (Exception exception)
                    {
                        LogText("Could not add row: " + row.Index + 1);

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

                if (SaveToOculus(body, "assets", equipment.Count, "assets/v4"))
                    dtgEquipment.Rows.Clear();

            }
        }

        // Process VEHICLES
        private void ProcessVehicles()
        {
            LoadExistingVehicles();

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

                        var model = VehicleModels.SingleOrDefault(vm =>
                            vm.name == row.Cells[4].FormattedValue.ToString());

                        var type = VehicleTypes.SingleOrDefault(vm =>
                            vm.name == row.Cells[5].FormattedValue.ToString());

                        vehicles.Add(new PULSEImport.vehicle()
                        {
                            accountTId = _accountTId,
                            name = string.IsNullOrEmpty(name) ? vin : name,
                            sn = vin,
                            mdlYr = SafeType.SafeString(row.Cells[2].Value),
                            descr = SafeType.SafeString(row.Cells[3].Value),
                            mdl = SafeType.SafeString(row.Cells[4].FormattedValue),
                            type = SafeType.SafeString(row.Cells[5].FormattedValue),
                            mnf = model?.typeData.mnf,
                            mdlTId = model?.tId,
                            typeTId = type?.tId
                        });
                    }
                    catch (Exception exception)
                    {
                        LogText("Could not add row: " + row.Index + 1);

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

                if (SaveToOculus(body, "assets", vehicles.Count, "vehicles/v4"))
                    dtgVehicles.Rows.Clear();
            }
        }

        // Process PEOPLE
        private void ProcessPeople()
        {
            LoadExistingPeople();

            var people = new List<Person>();

            foreach (DataGridViewRow row in dtgPeople.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    try
                    {
                        Console.WriteLine(SafeType.SafeString(row.Cells[0].Value) + " " +
                                          SafeType.SafeString(row.Cells[1].Value));

                        people.Add(new Person()
                        {
                            accountTId = _accountTId,
                            givenNames = SafeType.SafeString(row.Cells[0].Value),
                            surname = SafeType.SafeString(row.Cells[1].Value),
                            contacts = new Contacts()
                            {
                                emails = new Emails()
                                {
                                    business = SafeType.SafeString(row.Cells[3].Value)
                                },
                                phones = new Phones()
                                {
                                    mobile = SafeType.SafeString(row.Cells[4].Value)
                                }
                            }
                            //employeeId = SafeType.SafeString(row.Cells[2].Value),
                            //email = SafeType.SafeString(row.Cells[3].Value),
                            //phoneNumber = SafeType.SafeString(row.Cells[4].Value),
                            //phoneType = SafeType.SafeString(row.Cells[5].Value)
                        });
                    }
                    catch (Exception exception)
                    {
                        LogText("Could not add row: " + row.Index + 1);

                        Console.WriteLine("Could not add row: " + row.Index + 1);
                        Console.WriteLine(exception);
                        Log4NetHelper.LogError(Logger, exception);

                        //throw;
                    }
                }
            }

            if (people.Any())
            {
                var body = "{ \"persons\": " + JsonConvert.SerializeObject(people, Formatting.None,
                               new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore
                               }) + " }";

                if (SaveToOculus(body, "people", people.Count, "persons/v5"))
                    dtgPeople.Rows.Clear();
            }
        }


        // Process Equipment Models
        private void ProcessEquipmentModels()
        {
            var validModels = true;
            var duplicateModels = new List<Model>();

            var eModels = new List<Model>();

            foreach (DataGridViewRow row in dtgEquipmentModels.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    var name = SafeType.SafeString(row.Cells[0].Value);

                    // Build the Model object
                    var model = new Model();

                    try
                    {
                        var manufacturer = SafeType.SafeString(row.Cells[1].Value);
                        var description = SafeType.SafeString(row.Cells[2].Value);

                        var typeData = new TypeData()
                        {
                            mnf = manufacturer,
                            mdl = name
                        };

                        model = new Model()
                        {
                            accountTId = _accountTId,
                            name = name,
                            descr = description,
                            //crtdDt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            //uptdDt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            type = "MODELS",
                            targetObjCat = "ASSETS",
                            objCat = "TYPES",
                            lifeState = "ACTV",
                            typeData = typeData
                        };

                    }
                    catch (Exception exception)
                    {
                        LogText("Could not add row: " + row.Index + 1);

                        Console.WriteLine("Could not add row: " + row.Index + 1);
                        Console.WriteLine(exception);

                        Log4NetHelper.LogError(Logger, exception);

                        //throw;
                    }

                    var validModel = true;

                    // Check if Model is already in the system...
                    if (EquipmentModels.ToList().Any(d =>
                        d.name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        validModel = false;
                        validModels = false;

                        var errorText = string.Format("{0} is a duplicate Model Name.", name);

                        LogText(errorText);

                        row.ErrorText = errorText;
                        //row.Cells[0].ErrorText = errorText;

                        duplicateModels.Add(model);
                    }

                    if (validModel)
                    {
                        eModels.Add(model);
                    }
                }
            }

            // Check if all Models are valid
            if (validModels && eModels.Any())
            {
                var eModelList = new Models();

                eModelList.types = eModels;

                var body = JsonConvert.SerializeObject(eModelList, Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                if (SaveToOculus(body, "equipment models", eModelList.types.Count, "types/v1"))
                    dtgEquipmentModels.Rows.Clear();

            }
            else
            {
                if (!validModels)
                {
                    var duplicateDialog = MessageBox.Show(
                        "Some Models already exist in PULSE. Do you want to remove those from the grid and import the models that are new?",
                        "Existing Models", MessageBoxButtons.YesNo);

                    if (duplicateDialog == DialogResult.Yes)
                    {
                        for (int n = dtgEquipmentModels.Rows.Count - 1; n >= 0; n--)
                        {
                            DataGridViewRow row = dtgEquipmentModels.Rows[n];

                            if (row.Cells[0].Value != null)
                            {
                                foreach (var model in duplicateModels)
                                {
                                    if (row.Cells[0].Value.Equals(model.name))
                                    {
                                        try
                                        {
                                            dtgEquipmentModels.Rows.RemoveAt(n);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e);
                                            //throw;
                                        }
                                    }
                                }
                            }
                        }

                        txtErrorLog.Text = "";

                        ProcessEquipmentModels();
                    }
                }
            }
        }

        // Process Equipment Types
        private void ProcessEquipmentTypes()
        {
            var validTypes = true;
            var duplicateTypes = new List<OculusType>();

            var eTypes = new List<OculusType>();

            foreach (DataGridViewRow row in dtgEquipmentTypes.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    var name = SafeType.SafeString(row.Cells[0].Value);

                    // Build the Model object
                    var type = new OculusType();

                    try
                    {
                        var description = SafeType.SafeString(row.Cells[1].Value);

                        type = new OculusType()
                        {
                            accountTId = _accountTId,
                            name = name,
                            descr = description,
                            //crtdDt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            //uptdDt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            type = "TYPES",
                            targetObjCat = "ASSETS",
                            objCat = "TYPES",
                            lifeState = "ACTV"
                        };

                    }
                    catch (Exception exception)
                    {
                        LogText("Could not add row: " + row.Index + 1);

                        Console.WriteLine("Could not add row: " + row.Index + 1);
                        Console.WriteLine(exception);

                        Log4NetHelper.LogError(Logger, exception);

                        //throw;
                    }

                    var validType = true;

                    // Check if Model is already in the system...
                    if (EquipmentTypes.ToList().Any(d =>
                        d.name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        validType = false;
                        validTypes = false;

                        var errorText = string.Format("{0} is a duplicate Type Name.", name);

                        LogText(errorText);

                        row.ErrorText = errorText;
                        //row.Cells[0].ErrorText = errorText;

                        duplicateTypes.Add(type);
                    }

                    if (validType)
                    {
                        eTypes.Add(type);
                    }
                }
            }

            // Check if all Models are valid
            if (validTypes && eTypes.Any())
            {
                var eTypeList = new OculusTypes();

                eTypeList.types = eTypes;

                var body = JsonConvert.SerializeObject(eTypeList, Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                if (SaveToOculus(body, "equipment types", eTypeList.types.Count, "types/v1"))
                    dtgEquipmentTypes.Rows.Clear();
            }
            else
            {
                if (!validTypes)
                {
                    var duplicateDialog = MessageBox.Show(
                        "Some Types already exist in PULSE. Do you want to remove those from the grid and import the types that are new?",
                        "Existing Types", MessageBoxButtons.YesNo);

                    if (duplicateDialog == DialogResult.Yes)
                    {
                        for (int n = dtgEquipmentTypes.Rows.Count - 1; n >= 0; n--)
                        {
                            DataGridViewRow row = dtgEquipmentTypes.Rows[n];

                            if (row.Cells[0].Value != null)
                            {
                                foreach (var type in duplicateTypes)
                                {
                                    if (row.Cells[0].Value.Equals(type.name))
                                    {
                                        try
                                        {
                                            dtgEquipmentTypes.Rows.RemoveAt(n);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e);
                                            //throw;
                                        }
                                    }
                                }
                            }
                        }

                        txtErrorLog.Text = "";

                        ProcessEquipmentTypes();
                    }
                }
            }
        }


        // Process Vehicle Models
        private void ProcessVehicleModels()
        {
            var validModels = true;
            var duplicateModels = new List<Model>();

            var eModels = new List<Model>();

            foreach (DataGridViewRow row in dtgVehicleModels.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    var name = SafeType.SafeString(row.Cells[0].Value);

                    // Build the Model object
                    var model = new Model();

                    try
                    {
                        var manufacturer = SafeType.SafeString(row.Cells[1].Value);
                        var description = SafeType.SafeString(row.Cells[2].Value);

                        var typeData = new TypeData()
                        {
                            mnf = manufacturer,
                            mdl = name
                        };

                        model = new Model()
                        {
                            accountTId = _accountTId,
                            name = name,
                            descr = description,
                            //crtdDt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            //uptdDt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            type = "MODELS",
                            targetObjCat = "VEHICLES",
                            objCat = "TYPES",
                            lifeState = "ACTV",
                            typeData = typeData
                        };

                    }
                    catch (Exception exception)
                    {
                        LogText("Could not add row: " + row.Index + 1);

                        Console.WriteLine("Could not add row: " + row.Index + 1);
                        Console.WriteLine(exception);

                        Log4NetHelper.LogError(Logger, exception);

                        //throw;
                    }

                    var validModel = true;

                    // Check if Model is already in the system...
                    if (VehicleModels.ToList().Any(d =>
                        d.name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        validModel = false;
                        validModels = false;

                        var errorText = string.Format("{0} is a duplicate Model Name.", name);

                        LogText(errorText);

                        row.ErrorText = errorText;
                        //row.Cells[0].ErrorText = errorText;

                        duplicateModels.Add(model);
                    }

                    if (validModel)
                    {
                        eModels.Add(model);
                    }
                }
            }

            // Check if all Models are valid
            if (validModels && eModels.Any())
            {
                var vModelList = new Models();

                vModelList.types = eModels;

                var body = JsonConvert.SerializeObject(vModelList, Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                if (SaveToOculus(body, "vehicle models", vModelList.types.Count, "types/v1"))
                    dtgVehicleModels.Rows.Clear();
            }
            else
            {
                if (!validModels)
                {
                    var duplicateDialog = MessageBox.Show(
                        "Some Models already exist in PULSE. Do you want to remove those from the grid and import the models that are new?",
                        "Existing Models", MessageBoxButtons.YesNo);

                    if (duplicateDialog == DialogResult.Yes)
                    {
                        for (int n = dtgVehicleModels.Rows.Count - 1; n >= 0; n--)
                        {
                            DataGridViewRow row = dtgVehicleModels.Rows[n];

                            if (row.Cells[0].Value != null)
                            {
                                foreach (var model in duplicateModels)
                                {
                                    if (row.Cells[0].Value.Equals(model.name))
                                    {
                                        try
                                        {
                                            dtgVehicleModels.Rows.RemoveAt(n);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e);
                                            //throw;
                                        }
                                    }
                                }
                            }
                        }

                        txtErrorLog.Text = "";

                        ProcessVehicleModels();
                    }
                }
            }
        }

        // Process Vehicle Types
        private void ProcessVehicleTypes()
        {
            var validTypes = true;
            var duplicateTypes = new List<OculusType>();

            var vTypes = new List<OculusType>();

            foreach (DataGridViewRow row in dtgVehicleTypes.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    var name = SafeType.SafeString(row.Cells[0].Value);

                    // Build the Model object
                    var type = new OculusType();

                    try
                    {
                        var description = SafeType.SafeString(row.Cells[1].Value);

                        type = new OculusType()
                        {
                            accountTId = _accountTId,
                            name = name,
                            descr = description,
                            //crtdDt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            //uptdDt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            type = "TYPES",
                            targetObjCat = "VEHICLES",
                            objCat = "TYPES",
                            lifeState = "ACTV"
                        };

                    }
                    catch (Exception exception)
                    {
                        LogText("Could not add row: " + row.Index + 1);

                        Console.WriteLine("Could not add row: " + row.Index + 1);
                        Console.WriteLine(exception);

                        Log4NetHelper.LogError(Logger, exception);

                        //throw;
                    }

                    var validType = true;

                    // Check if Model is already in the system...
                    if (VehicleTypes.ToList().Any(d =>
                        d.name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        validType = false;
                        validTypes = false;

                        var errorText = string.Format("{0} is a duplicate Type Name.", name);

                        LogText(errorText);

                        row.ErrorText = errorText;
                        //row.Cells[0].ErrorText = errorText;

                        duplicateTypes.Add(type);
                    }

                    if (validType)
                    {
                        vTypes.Add(type);
                    }
                }
            }

            // Check if all Models are valid
            if (validTypes && vTypes.Any())
            {
                var vTypeList = new OculusTypes();

                vTypeList.types = vTypes;

                var body = JsonConvert.SerializeObject(vTypeList, Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                if (SaveToOculus(body, "vehicle types", vTypeList.types.Count, "types/v1"))
                    dtgVehicleTypes.Rows.Clear();
            }
            else
            {
                if (!validTypes)
                {
                    var duplicateDialog = MessageBox.Show(
                        "Some Types already exist in PULSE. Do you want to remove those from the grid and import the types that are new?",
                        "Existing Types", MessageBoxButtons.YesNo);

                    if (duplicateDialog == DialogResult.Yes)
                    {
                        for (int n = dtgVehicleTypes.Rows.Count - 1; n >= 0; n--)
                        {
                            DataGridViewRow row = dtgVehicleTypes.Rows[n];

                            if (row.Cells[0].Value != null)
                            {
                                foreach (var type in duplicateTypes)
                                {
                                    if (row.Cells[0].Value.Equals(type.name))
                                    {
                                        try
                                        {
                                            dtgVehicleTypes.Rows.RemoveAt(n);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e);
                                            //throw;
                                        }
                                    }
                                }
                            }
                        }

                        txtErrorLog.Text = "";

                        ProcessVehicleTypes();
                    }
                }
            }
        }


        // Process Places
        private void ProcessPlaces()
        {
            // LoadExistingPlaces()

            var placeTypes = LoadPlaceTypes();

            var places = new List<Place>();

            foreach (DataGridViewRow row in dtgPlaces.Rows)
            {
                try
                {
                    // Ensure that the place has a name and at least 1 point
                    if (!row.IsNewRow && row.Cells[0].Value != null &&
                        row.Cells[2].Value != null && row.Cells[3].Value != null)
                    {
                        var type = placeTypes.SingleOrDefault(pt =>
                            pt.name == row.Cells[1].FormattedValue.ToString());

                        var place = new Place()
                        {
                            accountTId = _accountTId,
                            name = SafeType.SafeString(row.Cells[0].Value),
                            type = SafeType.SafeString(row.Cells[1].FormattedValue.ToString()),
                            typeTId = type?.tId
                        };

                        var vertices = new List<Vertex>();

                        foreach (DataGridViewColumn col in dtgPlaces.Columns)
                        {
                            if (col.Name.Contains("Latitude"))
                            {
                                var vertex = new Vertex();

                                var lonCol = dtgPlaces.Columns[col.Index + 1];

                                // Make sure both are populated
                                if (!string.IsNullOrEmpty(SafeType.SafeString(row.Cells[col.Index].Value)) &&
                                    !string.IsNullOrEmpty(SafeType.SafeString(row.Cells[lonCol.Index].Value)))
                                {
                                    vertex.lat = Convert.ToDouble(SafeType.SafeString(row.Cells[col.Index].Value));
                                    vertex.lon = Convert.ToDouble(SafeType.SafeString(row.Cells[lonCol.Index].Value));

                                    vertices.Add(vertex);
                                }
                            }
                        }

                        place.vertices = vertices;

                        if (place.vertices.Count == 1)
                        {
                            place.geometry = "POINT";
                        }
                        else
                        {
                            place.geometry = "POLYGON";
                        }

                        places.Add(place);
                    }
                }
                catch (Exception exception)
                {
                    LogText("Could not add row: " + row.Index + 1);

                    Console.WriteLine("Could not add row: " + row.Index + 1);
                    Console.WriteLine(exception);
                    Log4NetHelper.LogError(Logger, exception);

                    //throw;
                }
            }

            if (places.Any())
            {
                var body = "{ \"places\": " + JsonConvert.SerializeObject(places, Formatting.None,
                               new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore
                               }) + " }";

                if (SaveToOculus(body, "places", places.Count, "places/v5"))
                    dtgPlaces.Rows.Clear();
            }
        }

        private bool SaveToOculus(string body, string objectType, int objectCount, string apiSuffix)
        {
            try
            {
                var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

                UpdateStatusBar(string.Format("Adding {0}...", objectType), true);

                var response = oculusApiRequest.ExecutePOST(_configData.ApiUrl + apiSuffix, body);

                if (!string.IsNullOrEmpty(SafeType.SafeString(response)))
                {
                    var oculusResponse = JsonConvert.DeserializeObject<OculusResponse>(SafeType.SafeString(response));

                    if (oculusResponse.statusCode == "201")
                    {
                        var statusMessage = string.Format("{0} {1} added!", objectCount, objectType);

                        UpdateStatusBar(statusMessage);

                        MessageBox.Show(statusMessage, "Success!", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        return true;
                    }
                    else
                    {
                        LogText(string.Format("{0}: {1}",
                                                SafeType.SafeString(oculusResponse.statusCode),
                                                SafeType.SafeString(oculusResponse.statusDescr)));

                        MessageBox.Show("There was a problem importing the data.");

                        UpdateStatusBar("");
                    }
                }
                else
                {
                    MessageBox.Show("There was a problem importing the data.");

                    UpdateStatusBar("");
                }
            }
            catch (Exception exception)
            {
                LogText(exception.Message);

                MessageBox.Show(exception.Message);
                UpdateStatusBar("Could not import Equipment data!");
                Log4NetHelper.LogError(Logger, exception);

                //throw;
            }

            return false;
        }

        #region UI Logic
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
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

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            DialogResult msgboxConfirm = MessageBox.Show(
                "Are you sure you want clear the grid?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (msgboxConfirm == DialogResult.Yes)
            {
                // Devices
                if (tabControl1.SelectedIndex == 0)
                {
                    dtgDevices.Rows.Clear();
                }
                // Equipment
                else if (tabControl1.SelectedIndex == 1)
                {
                    dtgEquipment.Rows.Clear();
                }
                // Vehicles
                else if (tabControl1.SelectedIndex == 2)
                {
                    dtgVehicles.Rows.Clear();
                }
                // People
                else if (tabControl1.SelectedIndex == 3)
                {
                    dtgPeople.Rows.Clear();
                }
                else if (tabControl1.SelectedIndex == 4)
                {
                    if (tabControl2.SelectedIndex == 0)
                    {
                        dtgEquipmentModels.Rows.Clear();
                    }
                    else if (tabControl2.SelectedIndex == 1)
                    {
                        dtgVehicleModels.Rows.Clear();
                    }
                }
                else if (tabControl1.SelectedIndex == 5)
                {
                    if (tabControl3.SelectedIndex == 0)
                    {
                        dtgEquipmentTypes.Rows.Clear();
                    }
                    else if (tabControl3.SelectedIndex == 1)
                    {
                        dtgVehicleTypes.Rows.Clear();
                    }
                }
            }
        }

        // TODO: Under construction...
        private void InstallUpdateSyncWithInfo()
        {
            //System.Deployment.Application.ApplicationDeployment.CurrentDeployment.Update();

            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        DialogResult dr = MessageBox.Show(
                            "An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel);

                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show("This application has detected a mandatory update from your current " +
                            "version to version " + info.MinimumRequiredVersion.ToString() +
                            ". The application will now install the update and restart.",
                            "Update Available", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            MessageBox.Show("The application has been upgraded, and will now restart.");
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                            return;
                        }
                    }
                }
            }
        }

        private void LogText(string s)
        {
            if (txtErrorLog.Text.Length > 0)
                txtErrorLog.Text += Environment.NewLine;

            txtErrorLog.Text += s;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //InstallUpdateSyncWithInfo();
            LoadExistingDevices();
        }
        #endregion

        private void pnlDevices_Click(object sender, EventArgs e)
        {
            var deviceList = new List<string[]>();

            foreach (var device in AccountDevices)
            {
                // Part Number
                var partNumber = string.Empty;
                var deviceModel = DeviceModels.SingleOrDefault(m => m.tId == device.mdlTId);

                if (deviceModel != null)
                {
                    partNumber = deviceModel.typeData.trimblePartNumber;
                }

                var row = new string[]
                {
                    device.sn,
                    partNumber,
                    device.descr
                };

                deviceList.Add(row);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SerialNumber,PartNumber,Description");

            foreach (var row in deviceList)
            {
                sb.Append(row[0]);
                sb.Append("," + row[1]);
                sb.Append("," + row[2]);

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());

            string fileName = string.Format("DeviceExport-{0}-{1}.csv", _accountTId, DateTime.Now.ToString(ExportDate));

            ExportData(sb, fileName);
        }

        private void pnlEquipment_Click(object sender, EventArgs e)
        {
            var equipList = new List<string[]>();

            foreach (var equip in AccountEquipment)
            {
                // Model
                var equipModel = EquipmentModels.SingleOrDefault(m => m.tId == equip.mdlTId);

                var row = new string[]
                {
                    equip.name,
                    equip.sn,
                    equip.mdlYr,
                    equip.descr,
                    equipModel?.name,
                    equip.type
                };

                equipList.Add(row);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Equipment ID,Serial Number,Year,Notes,Model,Type");

            foreach (var row in equipList)
            {
                sb.Append(row[0]);
                sb.Append("," + row[1]);
                sb.Append("," + row[2]);
                sb.Append("," + row[3]);
                sb.Append("," + row[4]);
                sb.Append("," + row[5]);

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());

            string fileName = string.Format("EquipmentExport-{0}-{1}.csv", _accountTId, DateTime.Now.ToString(ExportDate));

            ExportData(sb, fileName);
        }

        private void pnlVehicles_Click(object sender, EventArgs e)
        {
            var vehList = new List<string[]>();

            foreach (var veh in AccountVehicles)
            {
                // Model
                var vehModel = VehicleModels.SingleOrDefault(m => m.tId == veh.mdlTId);

                var row = new string[]
                {
                    veh.name,
                    veh.sn,
                    veh.mdlYr.ToString(),
                    veh.descr,
                    vehModel?.name,
                    veh.type
                };

                vehList.Add(row);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Equipment ID,Serial Number,Year,Notes,Model,Type");

            foreach (var row in vehList)
            {
                sb.Append(row[0]);
                sb.Append("," + row[1]);
                sb.Append("," + row[2]);
                sb.Append("," + row[3]);
                sb.Append("," + row[4]);
                sb.Append("," + row[5]);

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());

            string fileName = string.Format("VehicleExport-{0}-{1}.csv", _accountTId, DateTime.Now.ToString(ExportDate));

            ExportData(sb, fileName);
        }

        private void pnlPeople_Click(object sender, EventArgs e)
        {
            var peopleList = new List<string[]>();

            foreach (var person in AccountPeople)
            {
                var email = person.contacts?.emails?.business;
                var phone = person.contacts?.phones?.mobile;

                var row = new string[]
                {
                    person.givenNames,
                    person.surname,
                    person.userId,
                    email,
                    phone
                };

                peopleList.Add(row);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("First Name,Last Name,Employee ID,Email Address,Phone Number");

            foreach (var row in peopleList)
            {
                sb.Append(row[0]);
                sb.Append("," + row[1]);
                sb.Append("," + row[2]);
                sb.Append("," + row[3]);
                sb.Append("," + row[4]);

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());

            string fileName = string.Format("PeopleExport-{0}-{1}.csv", _accountTId, DateTime.Now.ToString(ExportDate));

            ExportData(sb, fileName);
        }

        private void pnlEquipModels_Click(object sender, EventArgs e)
        {
            var typeList = new List<string[]>();

            foreach (var type in EquipmentModels)
            {
                var row = new string[]
                {
                    type.name,
                    type.typeData?.mnf,
                    type.descr
                };

                typeList.Add(row);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name,Manufacturer,Description");

            foreach (var row in typeList)
            {
                sb.Append(row[0]);
                sb.Append("," + row[1]);
                sb.Append("," + row[2]);

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());

            string fileName = string.Format("EquipmentModelExport-{0}-{1}.csv", _accountTId, DateTime.Now.ToString(ExportDate));

            ExportData(sb, fileName);
        }

        private void pnlEquipTypes_Click(object sender, EventArgs e)
        {
            var typeList = new List<string[]>();

            foreach (var type in EquipmentTypes)
            {
                var row = new string[]
                {
                    type.name,
                    type.descr
                };

                typeList.Add(row);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name,Description");

            foreach (var row in typeList)
            {
                sb.Append(row[0]);
                sb.Append("," + row[1]);

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());

            string fileName = string.Format("EquipmentTypesExport-{0}-{1}.csv", _accountTId, DateTime.Now.ToString(ExportDate));

            ExportData(sb, fileName);
        }

        private void pnlVehicleModels_Click(object sender, EventArgs e)
        {
            var typeList = new List<string[]>();

            foreach (var type in VehicleModels)
            {
                var row = new string[]
                {
                    type.name,
                    type.typeData?.mnf,
                    type.descr
                };

                typeList.Add(row);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name,Manufacturer,Description");

            foreach (var row in typeList)
            {
                sb.Append(row[0]);
                sb.Append("," + row[1]);
                sb.Append("," + row[2]);

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());

            string fileName = string.Format("VehicleModelExport-{0}-{1}.csv", _accountTId, DateTime.Now.ToString(ExportDate));

            ExportData(sb, fileName);
        }

        private void pnlVehicleTypes_Click(object sender, EventArgs e)
        {
            var typeList = new List<string[]>();

            foreach (var type in VehicleTypes)
            {
                var row = new string[]
                {
                    type.name,
                    type.descr
                };

                typeList.Add(row);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name,Description");

            foreach (var row in typeList)
            {
                sb.Append(row[0]);
                sb.Append("," + row[1]);

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());

            string fileName = string.Format("VehicleTypesExport-{0}-{1}.csv", _accountTId, DateTime.Now.ToString(ExportDate));

            ExportData(sb, fileName);
        }

        private void ExportData(StringBuilder sb, string fileName)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory + "csv/";

            if (fbdExportFolder.ShowDialog() == DialogResult.OK)
            {
                baseDir = fbdExportFolder.SelectedPath;
            }

            string filePath = System.IO.Path.Combine(baseDir, fileName);

            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            file.Directory.Create();

            System.IO.File.WriteAllText(file.FullName, sb.ToString());

            if (MessageBox.Show("Your file has been created. Would you like to open the export folder now?", "File Created", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(baseDir);
            }
        }

    }

    #region Classes
    public class DropDownItem
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

    public class equipment
    {
        public string tId { get; set; }
        public string accountTId { get; set; }
        public string name { get; set; }
        public string sn { get; set; }
        public string mdlYr { get; set; }
        public string descr { get; set; }
        public string mdl { get; set; }
        public string type { get; set; }
        public string mnf { get; set; }
        public string mdlTId { get; set; }
        public string typeTId { get; set; }
    }

    // I know...
    public class equipments
    {
        public MetaInfo MetaInfo { get; set; }
        public List<equipment> Assets { get; set; }
    }

    public class vehicle
    {
        public string tId { get; set; }
        public string accountTId { get; set; }
        public string name { get; set; }
        public string sn { get; set; }
        public string mdlYr { get; set; }
        public string descr { get; set; }
        public string mdl { get; set; }
        public string type { get; set; }
        public string mnf { get; set; }
        public string mdlTId { get; set; }
        public string typeTId { get; set; }
    }

    public class vehicles
    {
        public MetaInfo MetaInfo { get; set; }
        public List<vehicle> Vehicles { get; set; }
    }

    #region Person Classes
    //internal class person
    //{
    //    public string accountTId { get; set; }
    //    public string givenNames { get; set; }
    //    public string surname { get; set; }
    //    public contacts contacts { get; set; }
    //}

    //internal class contacts
    //{
    //    public phones phones { get; set; }
    //    public emails emails { get; set; }
    //}

    //internal class phones
    //{
    //    public string home { get; set; }
    //    public string work { get; set; }
    //    public string mobile { get; set; }
    //    public string other { get; set; }
    //}

    //internal class emails
    //{
    //    public string personal { get; set; }
    //    public string business { get; set; }
    //    public string other { get; set; }
    //}

    //internal class personData
    //{
    //    public string driverLic { get; set; }
    //    public string driverLicType { get; set; }
    //    public string driverLicIA { get; set; }
    //}
    #endregion

    public class association
    {
        public string tId { get; set; }
        public string accountTId { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string descr { get; set; }
        public string lifeState { get; set; }
        public string pmryTId { get; set; }
        public string scdyTId { get; set; }
        public string startDt { get; set; }
        public string endDt { get; set; }
    }

    public class associations
    {
        public MetaInfo MetaInfo { get; set; }
        public List<association> Associations { get; set; }

        public associations()
        {
            Associations = new List<association>();
        }
    }
    #endregion

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

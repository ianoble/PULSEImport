using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PULSEImport
{
    public partial class Login : Form
    {
        public bool _loggedIn { get; set; }

        private ConfigData _configData;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            var environments = new List<DropDownItem>();

            environments.Add(new DropDownItem() { Value = "Staging", Text = "Staging" });
            environments.Add(new DropDownItem() { Value = "Production", Text = "Production" });
            environments.Add(new DropDownItem() { Value = "Production (EU)", Text = "Production (EU)" });

            ddlEnvironment.Items.AddRange(environments.ToArray());

            this.ActiveControl = ddlEnvironment;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.Environment))
            {
                ddlEnvironment.SelectedIndex = ddlEnvironment.FindStringExact(Properties.Settings.Default.Environment);

                this.ActiveControl = txtEmail;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.Email))
            {
                txtEmail.Text = Properties.Settings.Default.Email;

                this.ActiveControl = txtPassword;
            }

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!_loggedIn)
            //{
            //    DialogResult msgboxConfirm = MessageBox.Show(
            //        "Are you sure you want to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (msgboxConfirm == DialogResult.No)
            //    {
            //        e.Cancel = true;
            //    }
            //    else
            //    {
            //        Environment.Exit(0);

            //        //Application.Exit();
            //    }
            //}
        }

        private string LoginUser()
        {
            var oculusApiAuth = new OculusAPI.Services.Authentication();

            var environment = ((DropDownItem)ddlEnvironment.SelectedItem).Value;

            switch (environment)
            {
                case "Staging":
                    _configData = EnvironmentConfig.GetStagingConfig();
                    break;
                case "Production":
                    _configData = EnvironmentConfig.GetProductionConfig();
                    break;
                case "Production (EU)":
                    _configData = EnvironmentConfig.GetProductionEUConfig();
                    break;
            }

            EnvironmentConfig.ConfigData = _configData;

            try
            {
                // Get Access token for the select environment
                var tokens =
                    oculusApiAuth.RequestAccessToken(
                        _configData.ApiUrl, _configData.ClientId, _configData.Secret, txtEmail.Text, txtPassword.Text);

                return tokens;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return null;
                //throw;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult msgboxConfirm = MessageBox.Show(
                "Are you sure you want to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (msgboxConfirm == DialogResult.Yes)
            {
                try
                {
                    Environment.Exit(0);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    //throw;
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(((DropDownItem)ddlEnvironment.SelectedItem).Value) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please enter an email and password!");
            }
            else
            {
                // Do login stuff...
                var tokens = LoginUser();

                if (!string.IsNullOrEmpty(tokens))
                {
                    var accessToken = tokens.Split('|')[0];
                    var refreshToken = tokens.Split('|')[1];

                    _configData.AccessToken = accessToken;
                    _configData.RefreshToken = refreshToken;

                    Properties.Settings.Default.Email = txtEmail.Text;
                    Properties.Settings.Default.Password = txtPassword.Text;

                    Properties.Settings.Default.AccessToken = _configData.AccessToken;
                    Properties.Settings.Default.RefreshKey = _configData.RefreshToken;
                    Properties.Settings.Default.Environment = ((DropDownItem)ddlEnvironment.SelectedItem).Value;

                    Properties.Settings.Default["LoggedIn"] = _loggedIn;
                    Properties.Settings.Default.Save(); // Saves settings in application configuration file

                    ComboBox environment = ((Form1)this.Owner).ddlEnvironment; // new Form1();
                    environment.SelectedIndex = environment.FindStringExact(Properties.Settings.Default.Environment);

                    //frm.Location = this.Location;
                    //frm.StartPosition = FormStartPosition.Manual;
                    //frm.FormClosing += delegate { this.Show(); };
                    //frm.Show();
                    //this.Hide();

                    _loggedIn = true;

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Could not log you in.", "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnLogin.Enabled = (txtPassword.Text.Length > 0);
        }
    }
}

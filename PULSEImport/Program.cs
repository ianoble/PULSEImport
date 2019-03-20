using System;
using System.Windows.Forms;

namespace PULSEImport
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //ShowLoginDialog();

            Application.Run(new Form1());
            //Application.Run(new Resize());
        }

        private static void ShowLoginDialog()
        {
            Login loginDialog = new Login();

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (loginDialog.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Form1());
                // Read the contents of testDialog's TextBox.
                //Logger.Debug(loginDialog.txtEmail.Text);
            }
            else
            {
                //Logger.Debug("Cancelled");
            }

            loginDialog.Dispose();
        }
    }
}

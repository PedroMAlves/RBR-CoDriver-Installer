using Microsoft.Win32;

namespace RBR_CoDriver_Installer
{
    public partial class Form1 : Form
    {
        private string rbrInstallPath = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            btnDetect_Click(null, null);
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            string keyPath = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Richard Burns Rally - RallySimFans.hu NB";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath))
            {
                if (key != null)
                {
                    object installLocation = key.GetValue("InstallLocation");
                    if (installLocation != null)
                    {
                        rbrInstallPath = installLocation.ToString();
                        ShowPathFound();
                        return;
                    }
                }
            }

            ShowPathNotFound();
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                rbrInstallPath = folderBrowserDialog.SelectedPath;
                ShowPathFound();
            }
        }

        private void btnChangePath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                rbrInstallPath = folderBrowserDialog.SelectedPath;
                ShowPathFound();
            }
        }

        private void ShowPathFound()
        {
            lblStatus.Text = "Pasta de instalação RBR RSF: " + rbrInstallPath;
            btnSelectPath.Visible = false;
            btnChangePath.Visible = true;
        }

        private void ShowPathNotFound()
        {
            lblStatus.Text = "Pasta de instalação do RBR RSF não encontrada. Seleciona manualmente.";
            btnSelectPath.Visible = true;
            btnChangePath.Visible = false;
        }
    }
}  
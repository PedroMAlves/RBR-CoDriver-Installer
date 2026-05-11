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
            string foundPath = FindRBRPath();
            if (!string.IsNullOrEmpty(foundPath))
            {
                rbrInstallPath = foundPath;
                ShowPathFound();
            }
            else
            {
                ShowPathNotFound();
            }
        }

        public string FindRBRPath()
        {
            try
            {
                // Procura na Registry no caminho específico do RallySimFans RBR
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Rallysimfans RBR"))
                {
                    if (key != null)
                    {
                        object installPath = key.GetValue("InstallPath");
                        if (installPath != null)
                        {
                            string path = installPath.ToString();
                            if (Directory.Exists(path))
                            {
                                return path;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Se falhar, tenta o método de busca por ficheiro
            }

            // Fallback: Procura por RSF_Launcher.exe em qualquer local do sistema
            try
            {
                string launcherPath = FindRSFLauncherInDrives();
                if (!string.IsNullOrEmpty(launcherPath))
                {
                    return launcherPath;
                }
            }
            catch
            {
                // Ignora erros durante a procura
            }

            return null; // Não encontrado
        }

        private string FindRSFLauncherInDrives()
        {
            // Procura em todas as unidades disponíveis
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady && (drive.DriveType == DriveType.Fixed || drive.DriveType == DriveType.Network))
                {
                    try
                    {
                        string result = SearchForRSFLauncherInDirectory(drive.RootDirectory.FullName);
                        if (!string.IsNullOrEmpty(result))
                        {
                            return result;
                        }
                    }
                    catch
                    {
                        // Continua procurando noutras unidades
                    }
                }
            }

            return null;
        }

        private string SearchForRSFLauncherInDirectory(string rootPath)
        {
            try
            {
                // Procura recursivamente por rsf_launcher\RSF_Launcher.exe
                DirectoryInfo rootDir = new DirectoryInfo(rootPath);

                // Limita a profundidade para evitar erros de acesso profundo
                return SearchDirectoryRecursive(rootDir, 0, 5);
            }
            catch
            {
                return null;
            }
        }

        private string SearchDirectoryRecursive(DirectoryInfo directory, int currentDepth, int maxDepth)
        {
            if (currentDepth > maxDepth)
            {
                return null;
            }

            try
            {
                // Procura por rsf_launcher pasta neste nível
                DirectoryInfo rsfLauncherDir = directory.GetDirectories("rsf_launcher", SearchOption.TopDirectoryOnly).FirstOrDefault();

                if (rsfLauncherDir != null)
                {
                    FileInfo launcherExe = rsfLauncherDir.GetFiles("RSF_Launcher.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
                    if (launcherExe != null)
                    {
                        // Retorna a pasta pai de rsf_launcher (a raiz da instalação do RBR)
                        return rsfLauncherDir.Parent.FullName;
                    }
                }

                // Procura recursivamente nas subpastas
                DirectoryInfo[] subdirectories = directory.GetDirectories();
                foreach (DirectoryInfo subdir in subdirectories)
                {
                    try
                    {
                        string result = SearchDirectoryRecursive(subdir, currentDepth + 1, maxDepth);
                        if (!string.IsNullOrEmpty(result))
                        {
                            return result;
                        }
                    }
                    catch
                    {
                        // Continua procurando
                    }
                }
            }
            catch
            {
                // Ignora erros de acesso
            }

            return null;
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
            lblBackupManagement.Visible = true;
            btnCreateBackup.Visible = true;
            btnRestoreCopilot.Visible = true;
        }

        private void ShowPathNotFound()
        {
            lblStatus.Text = "Pasta de instalação do RBR RSF não encontrada. Seleciona manualmente.";
            btnSelectPath.Visible = true;
            btnChangePath.Visible = false;
            lblBackupManagement.Visible = false;
            btnCreateBackup.Visible = false;
            btnRestoreCopilot.Visible = false;
        }

        private void btnCreateBackup_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade de criar backup a ser implementada.", "Criar Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRestoreCopilot_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade de restaurar co-piloto a ser implementada.", "Restaurar Co-Piloto", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}  
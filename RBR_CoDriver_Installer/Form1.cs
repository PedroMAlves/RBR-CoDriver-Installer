using Microsoft.Win32;

namespace RBR_CoDriver_Installer
{
    public partial class Form1 : Form
    {
        private string rbrInstallPath = string.Empty;
        private string audioPath = string.Empty;
        private string pluginsPath = string.Empty;
        private string audioBackup = string.Empty;
        private string pluginsBackup = string.Empty;
        private bool hasBackup = false;

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
                initializeState(foundPath);
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
                // Search Registry for installation path (64-bit)
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
                // If failure, try folder search as fallback
            }

            // Fallback: Search for RSF_Launcher.exe in the system
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
                // Ignore errors during search
            }

            return null;
        }

        private string FindRSFLauncherInDrives()
        {
            // Search all available drives for rsf_launcher\RSF_Launcher.exe
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
                        // Keep searching other drives if access is denied or any error occurs
                    }
                }
            }

            return null;
        }

        private string SearchForRSFLauncherInDirectory(string rootPath)
        {
            try
            {
                // Searches recursively rsf_launcher\RSF_Launcher.exe
                DirectoryInfo rootDir = new DirectoryInfo(rootPath);

                // Limits depth to avoid long searches, adjust as needed
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
                // Searches for rsf_launcher folder in the current directory
                DirectoryInfo rsfLauncherDir = directory.GetDirectories("rsf_launcher", SearchOption.TopDirectoryOnly).FirstOrDefault();

                if (rsfLauncherDir != null)
                {
                    FileInfo launcherExe = rsfLauncherDir.GetFiles("RSF_Launcher.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
                    if (launcherExe != null)
                    {
                        // Return the parent directory of rsf_launcher, which should be the RBR installation folder
                        return rsfLauncherDir.Parent.FullName;
                    }
                }

                // Search recursively in subdirectories
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
                        // Continue searching
                    }
                }
            }
            catch
            {
                // Ignore access errors and continue searching
            }

            return null;
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                initializeState(folderBrowserDialog.SelectedPath);
                ShowPathFound();
            }
        }

        private void btnChangePath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                initializeState(folderBrowserDialog.SelectedPath);
                ShowPathFound();
            }
        }

        private void initializeState(string installationFolder)
        {
            setPaths(installationFolder);
            setHasBackups();
            setBackupButtons();
        }

        private void setPaths(string installationFolder)
        {
            rbrInstallPath = installationFolder;
            audioPath = Path.Combine(installationFolder, "Audio");
            pluginsPath = Path.Combine(installationFolder, "Plugins");
            audioBackup = Path.Combine(installationFolder, "Audio - backup CDI");
            pluginsBackup = Path.Combine(installationFolder, "Plugins - backup CDI");
        }

        private void setHasBackups()
        {
            if (!string.IsNullOrEmpty(rbrInstallPath))
            {
                hasBackup = Directory.Exists(audioBackup) || Directory.Exists(pluginsBackup);
            }
        }

        private void setBackupButtons()
        {
            btnCreateBackup.Enabled = !hasBackup;
            btnRestoreCopilot.Enabled = hasBackup;
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

        private async void btnCreateBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rbrInstallPath))
            {
                MessageBox.Show(this, "Caminho de instalação não definido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pbStatus.Visible = true;
            lblLoadingText.Visible = true;
            btnCreateBackup.Enabled = false;

            try
            {
                await Task.Run(() =>
                {
                    if (Directory.Exists(audioPath))
                    {
                        CopyDirectory(audioPath, audioBackup);
                    }
                    if (Directory.Exists(pluginsPath))
                    {
                        CopyDirectory(pluginsPath, pluginsBackup);
                    }
                });
                MessageBox.Show(this, "Backup criado com sucesso.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erro ao criar backup: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pbStatus.Visible = false;
                lblLoadingText.Visible = false;
                setHasBackups();
                setBackupButtons();
            }
        }

        private void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }
            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, destSubDir);
            }
        }

        private async void btnRestoreCopilot_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rbrInstallPath))
            {
                MessageBox.Show(this, "Caminho de instalação não definido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pbStatus.Visible = true;
            lblLoadingText.Visible = true;
            btnRestoreCopilot.Enabled = false;

            bool restored = false;

            try
            {
               restored = await Task.Run(() => restoreBackups());

                if (restored)
                {
                    MessageBox.Show(this, "Backup restaurado com sucesso.", "Restauração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "Nenhum backup encontrado para restaurar.", "Restauração", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erro ao restaurar backup: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pbStatus.Visible = false;
                lblLoadingText.Visible = false;
                setHasBackups();
                setBackupButtons();
            }
        }

        private bool restoreBackups() {
            bool restored = false;

            if (Directory.Exists(audioBackup))
            {
                if (Directory.Exists(audioPath))
                {
                    Directory.Delete(audioPath, true);
                }
                Directory.Move(audioBackup, audioPath);
                restored = true;
            }

            if (Directory.Exists(pluginsBackup))
            {
                if (Directory.Exists(pluginsPath))
                {
                    Directory.Delete(pluginsPath, true);
                }
                Directory.Move(pluginsBackup, pluginsPath);
                restored = true;
            }

            return restored;
        }
    }
}  
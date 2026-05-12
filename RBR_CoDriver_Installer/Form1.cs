using Microsoft.Win32;
using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Common;
using System.Net;
using System.Security.Policy;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


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
        private string pacenotesBackup = string.Empty;
        private string pacenotesPath = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            comboBoxCodrivers.Enabled = true;
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
            try
            {
                var service = new CodriverService();
                List<Codriver> lista = await service.GetAvailableCodriversAsync();

                comboBoxCodrivers.DataSource = lista;
                comboBoxCodrivers.DisplayMember = "name";
                comboBoxCodrivers.ValueMember = "url";
                if (lista.Count > 0)
                {
                    lblDescription.Text = lista[0].description;
                    codriverName.Text = lista[0].codriver_name;
                    linkLabel1.Tag = lista[0].preview_url;
                    if (!string.IsNullOrEmpty(lista[0].image_url))
                    {
                        pbCodriverImage.LoadAsync(lista[0].image_url);
                    }
                    else
                    {
                        pbCodriverImage.Image = null; //
                    }

                    if (!string.IsNullOrEmpty(lista[0].scale_image))
                    {
                        object obj = Properties.Resources.ResourceManager.GetObject(lista[0].scale_image);
                        scaleImage.Image = (Image)obj;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Erro ao carregar co-pilotos: {ex.Message}",
                        "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                comboBoxCodrivers.Enabled = true;
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
            pluginsPath = Path.Combine(installationFolder, "Plugins", "Pacenote");
            audioBackup = Path.Combine(installationFolder, "Audio - backup CDI");
            pluginsBackup = Path.Combine(installationFolder, "Plugins", "Pacenote - backup CDI");
            pacenotesPath = Path.Combine(installationFolder, "Plugins", "NGPCarMenu", "MyPacenotes");
            pacenotesBackup = Path.Combine(installationFolder, "Plugins", "NGPCarMenu", "MyPacenotes - backup CDI");
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

            // Check if backups already exist
            if (Directory.Exists(audioBackup) && Directory.Exists(pluginsBackup))
            {
                var result = MessageBox.Show(this, "Os backups já existem. Deseja sobrescrever?", "Backups Existentes",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }
            ShowOverlay("A criar backup...");

            try
            {
                await Task.Run(() =>
                {
                    try
                    {
                        if (Directory.Exists(audioPath))
                        {
                            if (Directory.Exists(audioBackup))
                            {
                                Directory.Delete(audioBackup, true);
                            }
                            CopyDirectory(audioPath, audioBackup);
                        }

                        if (Directory.Exists(pluginsPath))
                        {
                            if (Directory.Exists(pluginsBackup))
                            {
                                Directory.Delete(pluginsBackup, true);
                            }
                            CopyDirectory(pluginsPath, pluginsBackup);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Erro durante a cópia: {ex.Message}", ex);
                    }
                });

                MessageBox.Show(this, "Backup criado com sucesso.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Erro ao criar backup: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HideOverlay();
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

            var confirm = MessageBox.Show(this, "Tem a certeza que deseja restaurar? A versão atual será substituída.",
                "Confirmar Recuperação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            ShowOverlay("A restaurar...");

            try
            {
                bool restored = await Task.Run(() => restoreBackups(audioPath, audioBackup));
                restored = await Task.Run(() => restoreBackups(pluginsPath, pluginsBackup)) || restored;

                if (restored)
                {
                    MessageBox.Show(this, "Backup restaurado com sucesso.", "Recuperação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "Nenhum backup encontrado para restaurar.", "Recuperação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Erro ao restaurar backup: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HideOverlay();
                setHasBackups();
                setBackupButtons();
            }
        }

        private bool restoreBackups(string originalPath, string backupPath)
        {
            bool restored = false;

            try
            {
                if (Directory.Exists(backupPath))
                {
                    if (Directory.Exists(originalPath))
                    {
                        Directory.Delete(originalPath, true);
                    }
                    Directory.Move(backupPath, originalPath);
                    restored = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao restaurar backup: {ex.Message}", ex);
            }

            return restored;
        }

        private async void buttonInstall_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rbrInstallPath))
            {
                MessageBox.Show(this, "Caminho de instalação não definido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selected = (Codriver)comboBoxCodrivers.SelectedItem;
            if (selected == null) return;

            var confirm = MessageBox.Show($"Desejas instalar {selected.name}?", "Confirmar",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                await IniciarInstalacao(selected.url, rbrInstallPath);
            }
        }

        private async Task IniciarInstalacao(string downloadUrl, string rbrPath)
        {
            string tempFile = Path.Combine(Path.GetTempPath(), "codriver_temp.7z");

            try
            {
                ShowOverlay("A descarregar...");

                using (var client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(new Uri(downloadUrl), tempFile);
                }

                lblLoadingText.Text = "A extrair ficheiros...";

                // Extract file
                await Task.Run(() =>
                {
                    using (var archive = SevenZipArchive.OpenArchive(tempFile))
                    {
                        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                        {
                            entry.WriteToDirectory(rbrPath, new ExtractionOptions
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }
                    }
                });

                MessageBox.Show("Co-piloto instalado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na instalação: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HideOverlay();
            }
        }

        private void comboBoxCodrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCodriver = (Codriver)comboBoxCodrivers.SelectedItem;

            if (selectedCodriver != null)
            {
                lblDescription.Text = selectedCodriver.description;
                codriverName.Text = selectedCodriver.codriver_name;
                linkLabel1.Tag = selectedCodriver.preview_url;
                if (!string.IsNullOrEmpty(selectedCodriver.image_url))
                {
                    pbCodriverImage.LoadAsync(selectedCodriver.image_url);
                }
                else
                {
                    pbCodriverImage.Image = null; //
                }
                if (!string.IsNullOrEmpty(selectedCodriver.scale_image))
                {
                    object obj = Properties.Resources.ResourceManager.GetObject(selectedCodriver.scale_image);
                    scaleImage.Image = (Image)obj;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AzorModsLogo.Image = Properties.Resources.Banner_RBRazorMODS;
        }

        private void previewURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel1.Tag == null) return;

            string url = linkLabel1.Tag.ToString();

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });

                linkLabel1.LinkVisited = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível abrir o link: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowOverlay(string mensagem)
        {
            panelOverlay.Size = this.ClientSize;
            panelOverlay.Location = new Point(0, 0);
            panelOverlay.BringToFront();

            lblLoadingText.Visible = true;
            lblLoadingText.Text = mensagem;
            pbStatus.Visible = true;

            panelOverlay.Visible = true;

            // Opcional: Centrar os controlos dentro do overlay
            lblLoadingText.Left = (panelOverlay.Width - lblLoadingText.Width) / 2;
            pbStatus.Left = (panelOverlay.Width - pbStatus.Width) / 2;
        }

        private void HideOverlay()
        {
            panelOverlay.Visible = false;
            lblLoadingText.Visible = false;
            pbStatus.Visible = false;
        }

        private void linkLuppisSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://luppisrbr.blogspot.com/p/how-to-install-pacenotes.html",
                    UseShellExecute = true
                });

                linkLuppisSite.LinkVisited = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível abrir o link: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnBackupPN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rbrInstallPath))
            {
                MessageBox.Show(this, "Caminho de instalação não definido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if backups already exist
            if (Directory.Exists(pacenotesBackup))
            {
                var result = MessageBox.Show(this, "O backup já existe. Deseja sobrescrever?", "Backup Existente",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }
            ShowOverlay("A criar backup...");

            try
            {
                await Task.Run(() =>
                {
                    try
                    {
                        if (Directory.Exists(pacenotesPath))
                        {
                            if (Directory.Exists(pacenotesBackup))
                            {
                                Directory.Delete(pacenotesBackup, true);
                            }
                            CopyDirectory(pacenotesPath, pacenotesBackup);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Erro durante a cópia: {ex.Message}", ex);
                    }
                });

                MessageBox.Show(this, "Backup criado com sucesso.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Erro ao criar backup: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HideOverlay();
            }
        }

        private async void btnRestorePN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rbrInstallPath))
            {
                MessageBox.Show(this, "Caminho de instalação não definido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show(this, "Tem a certeza que deseja restaurar? A versão atual será substituída.",
                "Confirmar Recuperação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            ShowOverlay("A restaurar...");

            try
            {
                bool restored = await Task.Run(() => restoreBackups(pacenotesPath, pacenotesBackup));

                if (restored)
                {
                    MessageBox.Show(this, "Backup restaurado com sucesso.", "Recuperação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "Nenhum backup encontrado para restaurar.", "Recuperação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Erro ao restaurar backup: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HideOverlay();
            }
        }
    }
}  
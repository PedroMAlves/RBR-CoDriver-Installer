namespace RBR_CoDriver_Installer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblStatus = new Label();
            btnSelectPath = new Button();
            btnChangePath = new Button();
            folderBrowserDialog = new FolderBrowserDialog();
            lblBackupManagement = new Label();
            btnCreateBackup = new Button();
            btnRestoreCopilot = new Button();
            pbStatus = new ProgressBar();
            lblLoadingText = new Label();
            comboBoxCodrivers = new ComboBox();
            selectCodriver = new Label();
            instalCodriver = new Button();
            lblDescription = new Label();
            imageList1 = new ImageList(components);
            imageList2 = new ImageList(components);
            pbCodriverImage = new PictureBox();
            scaleImage = new PictureBox();
            scaleLabel = new Label();
            codriverName = new Label();
            label1 = new Label();
            linkLabel1 = new LinkLabel();
            panelOverlay = new Panel();
            fileSystemWatcher1 = new FileSystemWatcher();
            panel1 = new Panel();
            panel2 = new Panel();
            lblAppVersion = new Label();
            AzorModsLogo = new PictureBox();
            lblLuppisV3 = new Label();
            lblLuppisSite = new Label();
            linkLuppisSite = new LinkLabel();
            btnBackupPN = new Button();
            btnRestorePN = new Button();
            btnLuppis = new Button();
            ((System.ComponentModel.ISupportInitialize)pbCodriverImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)scaleImage).BeginInit();
            panelOverlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AzorModsLogo).BeginInit();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 14);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(70, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "A detectar...";
            // 
            // btnSelectPath
            // 
            btnSelectPath.Location = new Point(12, 40);
            btnSelectPath.Name = "btnSelectPath";
            btnSelectPath.Size = new Size(150, 23);
            btnSelectPath.TabIndex = 1;
            btnSelectPath.Text = "Selecionar pasta";
            btnSelectPath.UseVisualStyleBackColor = true;
            btnSelectPath.Visible = false;
            btnSelectPath.Click += btnSelectPath_Click;
            // 
            // btnChangePath
            // 
            btnChangePath.Location = new Point(12, 40);
            btnChangePath.Name = "btnChangePath";
            btnChangePath.Size = new Size(150, 23);
            btnChangePath.TabIndex = 2;
            btnChangePath.Text = "Alterar pasta";
            btnChangePath.UseVisualStyleBackColor = true;
            btnChangePath.Visible = false;
            btnChangePath.Click += btnChangePath_Click;
            // 
            // folderBrowserDialog
            // 
            folderBrowserDialog.Description = "Seleciona a pasta de instalação do Richard Burns Rally RSF";
            // 
            // lblBackupManagement
            // 
            lblBackupManagement.AutoSize = true;
            lblBackupManagement.Location = new Point(12, 80);
            lblBackupManagement.Name = "lblBackupManagement";
            lblBackupManagement.Size = new Size(245, 15);
            lblBackupManagement.TabIndex = 3;
            lblBackupManagement.Text = "Gestão do backup do co-piloto original/atual";
            lblBackupManagement.Visible = false;
            // 
            // btnCreateBackup
            // 
            btnCreateBackup.Location = new Point(12, 110);
            btnCreateBackup.Name = "btnCreateBackup";
            btnCreateBackup.Size = new Size(150, 23);
            btnCreateBackup.TabIndex = 4;
            btnCreateBackup.Text = "Criar backup co-piloto";
            btnCreateBackup.UseVisualStyleBackColor = true;
            btnCreateBackup.Visible = false;
            btnCreateBackup.Click += btnCreateBackup_Click;
            // 
            // btnRestoreCopilot
            // 
            btnRestoreCopilot.Location = new Point(168, 110);
            btnRestoreCopilot.Name = "btnRestoreCopilot";
            btnRestoreCopilot.Size = new Size(150, 23);
            btnRestoreCopilot.TabIndex = 5;
            btnRestoreCopilot.Text = "Restaurar co-piloto";
            btnRestoreCopilot.UseVisualStyleBackColor = true;
            btnRestoreCopilot.Visible = false;
            btnRestoreCopilot.Click += btnRestoreCopilot_Click;
            // 
            // pbStatus
            // 
            pbStatus.Location = new Point(249, 347);
            pbStatus.Name = "pbStatus";
            pbStatus.Size = new Size(308, 27);
            pbStatus.Style = ProgressBarStyle.Marquee;
            pbStatus.TabIndex = 6;
            pbStatus.Visible = false;
            // 
            // lblLoadingText
            // 
            lblLoadingText.AutoSize = true;
            lblLoadingText.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLoadingText.Location = new Point(344, 305);
            lblLoadingText.Name = "lblLoadingText";
            lblLoadingText.Size = new Size(122, 25);
            lblLoadingText.TabIndex = 7;
            lblLoadingText.Text = "A processar...";
            lblLoadingText.Visible = false;
            // 
            // comboBoxCodrivers
            // 
            comboBoxCodrivers.FormattingEnabled = true;
            comboBoxCodrivers.Location = new Point(12, 276);
            comboBoxCodrivers.Name = "comboBoxCodrivers";
            comboBoxCodrivers.Size = new Size(313, 23);
            comboBoxCodrivers.TabIndex = 8;
            comboBoxCodrivers.SelectedIndexChanged += comboBoxCodrivers_SelectedIndexChanged;
            // 
            // selectCodriver
            // 
            selectCodriver.AutoSize = true;
            selectCodriver.Location = new Point(12, 249);
            selectCodriver.Name = "selectCodriver";
            selectCodriver.Size = new Size(231, 15);
            selectCodriver.TabIndex = 9;
            selectCodriver.Text = "Escolhe o co-piloto que pretendes instalar:";
            // 
            // instalCodriver
            // 
            instalCodriver.Location = new Point(331, 276);
            instalCodriver.Name = "instalCodriver";
            instalCodriver.Size = new Size(132, 23);
            instalCodriver.TabIndex = 10;
            instalCodriver.Text = "Instalar Co-piloto";
            instalCodriver.UseVisualStyleBackColor = true;
            instalCodriver.Click += buttonInstall_Click;
            // 
            // lblDescription
            // 
            lblDescription.BackColor = SystemColors.Control;
            lblDescription.ForeColor = SystemColors.ControlText;
            lblDescription.Location = new Point(15, 572);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(451, 188);
            lblDescription.TabIndex = 11;
            lblDescription.TextAlign = ContentAlignment.TopCenter;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // imageList2
            // 
            imageList2.ColorDepth = ColorDepth.Depth32Bit;
            imageList2.ImageSize = new Size(16, 16);
            imageList2.TransparentColor = Color.Transparent;
            // 
            // pbCodriverImage
            // 
            pbCodriverImage.BorderStyle = BorderStyle.FixedSingle;
            pbCodriverImage.Location = new Point(15, 316);
            pbCodriverImage.Name = "pbCodriverImage";
            pbCodriverImage.Size = new Size(225, 225);
            pbCodriverImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbCodriverImage.TabIndex = 12;
            pbCodriverImage.TabStop = false;
            // 
            // scaleImage
            // 
            scaleImage.BorderStyle = BorderStyle.FixedSingle;
            scaleImage.Location = new Point(517, 316);
            scaleImage.Name = "scaleImage";
            scaleImage.Size = new Size(202, 322);
            scaleImage.SizeMode = PictureBoxSizeMode.Zoom;
            scaleImage.TabIndex = 13;
            scaleImage.TabStop = false;
            // 
            // scaleLabel
            // 
            scaleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            scaleLabel.Location = new Point(530, 641);
            scaleLabel.Name = "scaleLabel";
            scaleLabel.Size = new Size(175, 23);
            scaleLabel.TabIndex = 14;
            scaleLabel.Text = "(Imagem de Janne Laahanen)";
            scaleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // codriverName
            // 
            codriverName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            codriverName.Location = new Point(256, 328);
            codriverName.Name = "codriverName";
            codriverName.Size = new Size(175, 15);
            codriverName.TabIndex = 15;
            codriverName.Text = "Nome";
            codriverName.UseWaitCursor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(256, 374);
            label1.Name = "label1";
            label1.Size = new Size(135, 15);
            label1.TabIndex = 16;
            label1.Text = "Vídeo de demonstração:";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(262, 398);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(59, 15);
            linkLabel1.TabIndex = 17;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Clica aqui";
            linkLabel1.LinkClicked += previewURL_LinkClicked;
            // 
            // panelOverlay
            // 
            panelOverlay.BackColor = Color.Transparent;
            panelOverlay.Controls.Add(pbStatus);
            panelOverlay.Controls.Add(lblLoadingText);
            panelOverlay.Location = new Point(440, 14);
            panelOverlay.Name = "panelOverlay";
            panelOverlay.Size = new Size(226, 129);
            panelOverlay.TabIndex = 18;
            panelOverlay.Visible = false;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(lblAppVersion);
            panel1.Controls.Add(AzorModsLogo);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 774);
            panel1.Name = "panel1";
            panel1.Size = new Size(734, 100);
            panel1.TabIndex = 19;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaption;
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(734, 1);
            panel2.TabIndex = 2;
            // 
            // lblAppVersion
            // 
            lblAppVersion.AutoSize = true;
            lblAppVersion.Location = new Point(62, 67);
            lblAppVersion.Name = "lblAppVersion";
            lblAppVersion.Size = new Size(239, 15);
            lblAppVersion.TabIndex = 1;
            lblAppVersion.Text = "Instalador de Co-Pilotos RBR RSF v1.0 - 2026";
            // 
            // AzorModsLogo
            // 
            AzorModsLogo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AzorModsLogo.Location = new Point(307, 3);
            AzorModsLogo.Name = "AzorModsLogo";
            AzorModsLogo.Size = new Size(425, 97);
            AzorModsLogo.SizeMode = PictureBoxSizeMode.Zoom;
            AzorModsLogo.TabIndex = 0;
            AzorModsLogo.TabStop = false;
            // 
            // lblLuppisV3
            // 
            lblLuppisV3.AutoSize = true;
            lblLuppisV3.Location = new Point(9, 155);
            lblLuppisV3.Name = "lblLuppisV3";
            lblLuppisV3.Size = new Size(492, 15);
            lblLuppisV3.TabIndex = 20;
            lblLuppisV3.Text = "ATENÇÃO: Estas mods de co-piloto são apenas compatíveis com o pack de notas Luppis V3.";
            // 
            // lblLuppisSite
            // 
            lblLuppisSite.AutoSize = true;
            lblLuppisSite.Location = new Point(9, 172);
            lblLuppisSite.Name = "lblLuppisSite";
            lblLuppisSite.RightToLeft = RightToLeft.No;
            lblLuppisSite.Size = new Size(222, 15);
            lblLuppisSite.TabIndex = 21;
            lblLuppisSite.Text = "Visita o site do criador para saberes mais:";
            // 
            // linkLuppisSite
            // 
            linkLuppisSite.AutoSize = true;
            linkLuppisSite.Location = new Point(237, 170);
            linkLuppisSite.Name = "linkLuppisSite";
            linkLuppisSite.Size = new Size(59, 15);
            linkLuppisSite.TabIndex = 22;
            linkLuppisSite.TabStop = true;
            linkLuppisSite.Text = "Clica aqui";
            linkLuppisSite.LinkClicked += linkLuppisSite_LinkClicked;
            // 
            // btnBackupPN
            // 
            btnBackupPN.Location = new Point(12, 205);
            btnBackupPN.Name = "btnBackupPN";
            btnBackupPN.Size = new Size(173, 23);
            btnBackupPN.TabIndex = 23;
            btnBackupPN.Text = "Criar backup pacenotes";
            btnBackupPN.UseVisualStyleBackColor = true;
            btnBackupPN.Click += btnBackupPN_Click;
            // 
            // btnRestorePN
            // 
            btnRestorePN.Location = new Point(191, 205);
            btnRestorePN.Name = "btnRestorePN";
            btnRestorePN.Size = new Size(206, 23);
            btnRestorePN.TabIndex = 24;
            btnRestorePN.Text = "Restaurar backup pacenotes";
            btnRestorePN.UseVisualStyleBackColor = true;
            btnRestorePN.Click += btnRestorePN_Click;
            // 
            // btnLuppis
            // 
            btnLuppis.Location = new Point(403, 205);
            btnLuppis.Name = "btnLuppis";
            btnLuppis.Size = new Size(207, 23);
            btnLuppis.TabIndex = 25;
            btnLuppis.Text = "Instalar pack Luppis Pacenotes V3";
            btnLuppis.UseVisualStyleBackColor = true;
            btnLuppis.Click += btnLuppis_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(734, 874);
            Controls.Add(btnLuppis);
            Controls.Add(btnRestorePN);
            Controls.Add(btnBackupPN);
            Controls.Add(linkLuppisSite);
            Controls.Add(lblLuppisSite);
            Controls.Add(lblLuppisV3);
            Controls.Add(panel1);
            Controls.Add(panelOverlay);
            Controls.Add(linkLabel1);
            Controls.Add(label1);
            Controls.Add(codriverName);
            Controls.Add(scaleLabel);
            Controls.Add(scaleImage);
            Controls.Add(pbCodriverImage);
            Controls.Add(lblDescription);
            Controls.Add(instalCodriver);
            Controls.Add(selectCodriver);
            Controls.Add(comboBoxCodrivers);
            Controls.Add(btnRestoreCopilot);
            Controls.Add(btnCreateBackup);
            Controls.Add(lblBackupManagement);
            Controls.Add(btnChangePath);
            Controls.Add(btnSelectPath);
            Controls.Add(lblStatus);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Instalador de Co-Pilotos RBR RSF";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pbCodriverImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)scaleImage).EndInit();
            panelOverlay.ResumeLayout(false);
            panelOverlay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AzorModsLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblStatus;
        private Button btnSelectPath;
        private Button btnChangePath;
        private Label lblBackupManagement;
        private Button btnCreateBackup;
        private Button btnRestoreCopilot;
        private FolderBrowserDialog folderBrowserDialog;

        #endregion

        private ProgressBar pbStatus;
        private Label lblLoadingText;
        private ComboBox comboBoxCodrivers;
        private Label selectCodriver;
        private Button instalCodriver;
        private Label lblDescription;
        private ImageList imageList1;
        private ImageList imageList2;
        private PictureBox pbCodriverImage;
        private PictureBox scaleImage;
        private Label scaleLabel;
        private Label codriverName;
        private Label label1;
        private LinkLabel linkLabel1;
        private Panel panelOverlay;
        private FileSystemWatcher fileSystemWatcher1;
        private Panel panel1;
        private PictureBox AzorModsLogo;
        private Panel panel2;
        private Label lblAppVersion;
        private Label lblLuppisV3;
        private Label lblLuppisSite;
        private LinkLabel linkLuppisSite;
        private Button btnLuppis;
        private Button btnRestorePN;
        private Button btnBackupPN;
    }
}

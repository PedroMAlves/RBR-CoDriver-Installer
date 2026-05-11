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
            lblStatus = new Label();
            btnSelectPath = new Button();
            btnChangePath = new Button();
            folderBrowserDialog = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 9);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(67, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "A detectar...";
            // 
            // btnSelectPath
            // 
            btnSelectPath.Location = new Point(12, 40);
            btnSelectPath.Name = "btnSelectPath";
            btnSelectPath.Size = new Size(150, 23);
            btnSelectPath.TabIndex = 1;
            btnSelectPath.Text = "Seleciona a pasta de instalação";
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
            btnChangePath.Text = "Alterar Pasta";
            btnChangePath.UseVisualStyleBackColor = true;
            btnChangePath.Visible = false;
            btnChangePath.Click += btnChangePath_Click;
            // 
            // folderBrowserDialog
            // 
            folderBrowserDialog.Description = "Seleciona a pasta de instalação do Richard Burns Rally RSF";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnChangePath);
            Controls.Add(btnSelectPath);
            Controls.Add(lblStatus);
            Name = "Form1";
            RightToLeft = RightToLeft.No;
            Text = "Instalador de Co-Pilotos RBR RSF";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblStatus;
        private Button btnSelectPath;
        private Button btnChangePath;
        private FolderBrowserDialog folderBrowserDialog;

        #endregion
    }
}

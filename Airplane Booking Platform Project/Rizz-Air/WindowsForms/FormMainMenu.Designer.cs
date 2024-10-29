namespace WindowsForms
{
    partial class FormMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.buttonUserManager = new System.Windows.Forms.Button();
            this.buttonAircraftManager = new System.Windows.Forms.Button();
            this.buttonLocationManager = new System.Windows.Forms.Button();
            this.buttonEmployeeManager = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelDesktopPanel = new System.Windows.Forms.Panel();
            this.buttonRequestManager = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panelMenu.Controls.Add(this.buttonRequestManager);
            this.panelMenu.Controls.Add(this.buttonUserManager);
            this.panelMenu.Controls.Add(this.buttonAircraftManager);
            this.panelMenu.Controls.Add(this.buttonLocationManager);
            this.panelMenu.Controls.Add(this.buttonEmployeeManager);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 521);
            this.panelMenu.TabIndex = 0;
            // 
            // buttonUserManager
            // 
            this.buttonUserManager.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonUserManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUserManager.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonUserManager.Location = new System.Drawing.Point(0, 320);
            this.buttonUserManager.Name = "buttonUserManager";
            this.buttonUserManager.Size = new System.Drawing.Size(220, 80);
            this.buttonUserManager.TabIndex = 4;
            this.buttonUserManager.Text = "User Manager";
            this.buttonUserManager.UseVisualStyleBackColor = true;
            this.buttonUserManager.Click += new System.EventHandler(this.buttonUserManager_Click);
            // 
            // buttonAircraftManager
            // 
            this.buttonAircraftManager.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonAircraftManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAircraftManager.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonAircraftManager.Location = new System.Drawing.Point(0, 240);
            this.buttonAircraftManager.Name = "buttonAircraftManager";
            this.buttonAircraftManager.Size = new System.Drawing.Size(220, 80);
            this.buttonAircraftManager.TabIndex = 3;
            this.buttonAircraftManager.Text = "Aircraft Manager";
            this.buttonAircraftManager.UseVisualStyleBackColor = true;
            this.buttonAircraftManager.Click += new System.EventHandler(this.buttonAircraftManager_Click);
            // 
            // buttonLocationManager
            // 
            this.buttonLocationManager.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonLocationManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLocationManager.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonLocationManager.Location = new System.Drawing.Point(0, 160);
            this.buttonLocationManager.Name = "buttonLocationManager";
            this.buttonLocationManager.Size = new System.Drawing.Size(220, 80);
            this.buttonLocationManager.TabIndex = 2;
            this.buttonLocationManager.Text = "Destination Manager";
            this.buttonLocationManager.UseVisualStyleBackColor = true;
            this.buttonLocationManager.Click += new System.EventHandler(this.buttonLocationManager_Click);
            // 
            // buttonEmployeeManager
            // 
            this.buttonEmployeeManager.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonEmployeeManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEmployeeManager.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonEmployeeManager.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonEmployeeManager.Location = new System.Drawing.Point(0, 80);
            this.buttonEmployeeManager.Name = "buttonEmployeeManager";
            this.buttonEmployeeManager.Size = new System.Drawing.Size(220, 80);
            this.buttonEmployeeManager.TabIndex = 1;
            this.buttonEmployeeManager.Text = "Employee Manager";
            this.buttonEmployeeManager.UseVisualStyleBackColor = true;
            this.buttonEmployeeManager.Click += new System.EventHandler(this.buttonEmployeeManager_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.panelLogo.Controls.Add(this.label1);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(220, 80);
            this.panelLogo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(54, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name Of User";
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(231)))), ((int)(((byte)(155)))));
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(220, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(980, 80);
            this.panelTitleBar.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Eras Medium ITC", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTitle.Location = new System.Drawing.Point(446, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(147, 49);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HOME";
            // 
            // panelDesktopPanel
            // 
            this.panelDesktopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktopPanel.Location = new System.Drawing.Point(220, 80);
            this.panelDesktopPanel.Name = "panelDesktopPanel";
            this.panelDesktopPanel.Size = new System.Drawing.Size(980, 441);
            this.panelDesktopPanel.TabIndex = 2;
            // 
            // buttonRequestManager
            // 
            this.buttonRequestManager.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonRequestManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRequestManager.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonRequestManager.Location = new System.Drawing.Point(0, 400);
            this.buttonRequestManager.Name = "buttonRequestManager";
            this.buttonRequestManager.Size = new System.Drawing.Size(220, 80);
            this.buttonRequestManager.TabIndex = 5;
            this.buttonRequestManager.Text = "Request Manager";
            this.buttonRequestManager.UseVisualStyleBackColor = true;
            this.buttonRequestManager.Click += new System.EventHandler(this.buttonRequestManager_Click);
            // 
            // FormMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 521);
            this.Controls.Add(this.panelDesktopPanel);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.Name = "FormMainMenu";
            this.Text = "FormMainMenu";
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelMenu;
        private Button buttonAircraftManager;
        private Button buttonLocationManager;
        private Button buttonEmployeeManager;
        private Panel panelLogo;
        private Panel panelTitleBar;
        private Label lblTitle;
        private Label label1;
        private Panel panelDesktopPanel;
        private Button buttonUserManager;
        private Button buttonRequestManager;
    }
}
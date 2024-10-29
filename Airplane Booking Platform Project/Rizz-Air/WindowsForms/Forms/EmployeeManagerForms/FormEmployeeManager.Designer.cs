namespace WindowsForms.Forms.EmployeeManagerForms
{
    partial class FormEmployeeManager
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
            this.listBoxEmployees = new System.Windows.Forms.ListBox();
            this.buttonAddManager = new System.Windows.Forms.Button();
            this.buttonPilot = new System.Windows.Forms.Button();
            this.buttonDeleteEmployee = new System.Windows.Forms.Button();
            this.buttonEditEmployee = new System.Windows.Forms.Button();
            this.buttonShowAllManagers = new System.Windows.Forms.Button();
            this.buttonShowPilots = new System.Windows.Forms.Button();
            this.buttonShowAllEmployees = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxEmployees
            // 
            this.listBoxEmployees.FormattingEnabled = true;
            this.listBoxEmployees.ItemHeight = 20;
            this.listBoxEmployees.Location = new System.Drawing.Point(36, 34);
            this.listBoxEmployees.Name = "listBoxEmployees";
            this.listBoxEmployees.Size = new System.Drawing.Size(428, 304);
            this.listBoxEmployees.TabIndex = 0;
            // 
            // buttonAddManager
            // 
            this.buttonAddManager.Location = new System.Drawing.Point(536, 64);
            this.buttonAddManager.Name = "buttonAddManager";
            this.buttonAddManager.Size = new System.Drawing.Size(128, 29);
            this.buttonAddManager.TabIndex = 1;
            this.buttonAddManager.Text = "Add Manager";
            this.buttonAddManager.UseVisualStyleBackColor = true;
            this.buttonAddManager.Click += new System.EventHandler(this.buttonAddManager_Click);
            // 
            // buttonPilot
            // 
            this.buttonPilot.Location = new System.Drawing.Point(536, 124);
            this.buttonPilot.Name = "buttonPilot";
            this.buttonPilot.Size = new System.Drawing.Size(128, 29);
            this.buttonPilot.TabIndex = 2;
            this.buttonPilot.Text = "Add Pilot";
            this.buttonPilot.UseVisualStyleBackColor = true;
            this.buttonPilot.Click += new System.EventHandler(this.buttonPilot_Click);
            // 
            // buttonDeleteEmployee
            // 
            this.buttonDeleteEmployee.Location = new System.Drawing.Point(554, 235);
            this.buttonDeleteEmployee.Name = "buttonDeleteEmployee";
            this.buttonDeleteEmployee.Size = new System.Drawing.Size(94, 29);
            this.buttonDeleteEmployee.TabIndex = 3;
            this.buttonDeleteEmployee.Text = "Delete";
            this.buttonDeleteEmployee.UseVisualStyleBackColor = true;
            this.buttonDeleteEmployee.Click += new System.EventHandler(this.buttonDeleteEmployee_Click);
            // 
            // buttonEditEmployee
            // 
            this.buttonEditEmployee.Location = new System.Drawing.Point(554, 285);
            this.buttonEditEmployee.Name = "buttonEditEmployee";
            this.buttonEditEmployee.Size = new System.Drawing.Size(94, 29);
            this.buttonEditEmployee.TabIndex = 4;
            this.buttonEditEmployee.Text = "Edit";
            this.buttonEditEmployee.UseVisualStyleBackColor = true;
            this.buttonEditEmployee.Click += new System.EventHandler(this.buttonEditEmployee_Click);
            // 
            // buttonShowAllManagers
            // 
            this.buttonShowAllManagers.Location = new System.Drawing.Point(306, 359);
            this.buttonShowAllManagers.Name = "buttonShowAllManagers";
            this.buttonShowAllManagers.Size = new System.Drawing.Size(130, 29);
            this.buttonShowAllManagers.TabIndex = 5;
            this.buttonShowAllManagers.Text = "All Managers";
            this.buttonShowAllManagers.UseVisualStyleBackColor = true;
            this.buttonShowAllManagers.Click += new System.EventHandler(this.buttonShowAllManagers_Click);
            // 
            // buttonShowPilots
            // 
            this.buttonShowPilots.Location = new System.Drawing.Point(177, 359);
            this.buttonShowPilots.Name = "buttonShowPilots";
            this.buttonShowPilots.Size = new System.Drawing.Size(103, 29);
            this.buttonShowPilots.TabIndex = 6;
            this.buttonShowPilots.Text = "All Pilots";
            this.buttonShowPilots.UseVisualStyleBackColor = true;
            this.buttonShowPilots.Click += new System.EventHandler(this.buttonShowPilots_Click);
            // 
            // buttonShowAllEmployees
            // 
            this.buttonShowAllEmployees.Location = new System.Drawing.Point(36, 359);
            this.buttonShowAllEmployees.Name = "buttonShowAllEmployees";
            this.buttonShowAllEmployees.Size = new System.Drawing.Size(118, 29);
            this.buttonShowAllEmployees.TabIndex = 7;
            this.buttonShowAllEmployees.Text = "All Employees";
            this.buttonShowAllEmployees.UseVisualStyleBackColor = true;
            this.buttonShowAllEmployees.Click += new System.EventHandler(this.buttonShowAllEmployees_Click);
            // 
            // FormEmployeeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 425);
            this.Controls.Add(this.buttonShowAllEmployees);
            this.Controls.Add(this.buttonShowPilots);
            this.Controls.Add(this.buttonShowAllManagers);
            this.Controls.Add(this.buttonEditEmployee);
            this.Controls.Add(this.buttonDeleteEmployee);
            this.Controls.Add(this.buttonPilot);
            this.Controls.Add(this.buttonAddManager);
            this.Controls.Add(this.listBoxEmployees);
            this.Name = "FormEmployeeManager";
            this.Text = "Employee Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listBoxEmployees;
        private Button buttonAddManager;
        private Button buttonPilot;
        private Button buttonDeleteEmployee;
        private Button buttonEditEmployee;
        private Button buttonShowAllManagers;
        private Button buttonShowPilots;
        private Button buttonShowAllEmployees;
    }
}
namespace WindowsForms
{
    partial class FormPilotMenu
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
            this.listBoxRents = new System.Windows.Forms.ListBox();
            this.buttonGetSalary = new System.Windows.Forms.Button();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxRents
            // 
            this.listBoxRents.FormattingEnabled = true;
            this.listBoxRents.ItemHeight = 20;
            this.listBoxRents.Location = new System.Drawing.Point(84, 53);
            this.listBoxRents.Name = "listBoxRents";
            this.listBoxRents.Size = new System.Drawing.Size(258, 184);
            this.listBoxRents.TabIndex = 0;
            // 
            // buttonGetSalary
            // 
            this.buttonGetSalary.Location = new System.Drawing.Point(162, 319);
            this.buttonGetSalary.Name = "buttonGetSalary";
            this.buttonGetSalary.Size = new System.Drawing.Size(94, 29);
            this.buttonGetSalary.TabIndex = 1;
            this.buttonGetSalary.Text = "Get Salary";
            this.buttonGetSalary.UseVisualStyleBackColor = true;
            this.buttonGetSalary.Click += new System.EventHandler(this.buttonGetSalary_Click);
            // 
            // buttonDetails
            // 
            this.buttonDetails.Location = new System.Drawing.Point(162, 262);
            this.buttonDetails.Name = "buttonDetails";
            this.buttonDetails.Size = new System.Drawing.Size(94, 29);
            this.buttonDetails.TabIndex = 2;
            this.buttonDetails.Text = "Details";
            this.buttonDetails.UseVisualStyleBackColor = true;
            this.buttonDetails.Click += new System.EventHandler(this.buttonDetails_Click);
            // 
            // FormPilotMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 390);
            this.Controls.Add(this.buttonDetails);
            this.Controls.Add(this.buttonGetSalary);
            this.Controls.Add(this.listBoxRents);
            this.Name = "FormPilotMenu";
            this.Text = "Pilot Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listBoxRents;
        private Button buttonGetSalary;
        private Button buttonDetails;
    }
}
namespace WindowsForms.Forms.RequestManagerForms
{
    partial class FormRentAndPilotRequests
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
            this.buttonUnaccept = new System.Windows.Forms.Button();
            this.listBoxRentsAndRequests = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonAllPilotRequests = new System.Windows.Forms.Button();
            this.buttonAllRents = new System.Windows.Forms.Button();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonUnaccept
            // 
            this.buttonUnaccept.Location = new System.Drawing.Point(428, 62);
            this.buttonUnaccept.Name = "buttonUnaccept";
            this.buttonUnaccept.Size = new System.Drawing.Size(94, 29);
            this.buttonUnaccept.TabIndex = 0;
            this.buttonUnaccept.Text = "Unaccept";
            this.buttonUnaccept.UseVisualStyleBackColor = true;
            this.buttonUnaccept.Click += new System.EventHandler(this.buttonUnaccept_Click);
            // 
            // listBoxRentsAndRequests
            // 
            this.listBoxRentsAndRequests.FormattingEnabled = true;
            this.listBoxRentsAndRequests.ItemHeight = 20;
            this.listBoxRentsAndRequests.Location = new System.Drawing.Point(98, 40);
            this.listBoxRentsAndRequests.Name = "listBoxRentsAndRequests";
            this.listBoxRentsAndRequests.Size = new System.Drawing.Size(288, 184);
            this.listBoxRentsAndRequests.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(428, 119);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 2;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonAllPilotRequests
            // 
            this.buttonAllPilotRequests.Location = new System.Drawing.Point(98, 248);
            this.buttonAllPilotRequests.Name = "buttonAllPilotRequests";
            this.buttonAllPilotRequests.Size = new System.Drawing.Size(150, 29);
            this.buttonAllPilotRequests.TabIndex = 3;
            this.buttonAllPilotRequests.Text = "All Pilot Requests";
            this.buttonAllPilotRequests.UseVisualStyleBackColor = true;
            this.buttonAllPilotRequests.Click += new System.EventHandler(this.buttonAllPilotRequests_Click);
            // 
            // buttonAllRents
            // 
            this.buttonAllRents.Location = new System.Drawing.Point(292, 248);
            this.buttonAllRents.Name = "buttonAllRents";
            this.buttonAllRents.Size = new System.Drawing.Size(94, 29);
            this.buttonAllRents.TabIndex = 4;
            this.buttonAllRents.Text = "All Rents";
            this.buttonAllRents.UseVisualStyleBackColor = true;
            this.buttonAllRents.Click += new System.EventHandler(this.buttonAllRents_Click);
            // 
            // buttonDetails
            // 
            this.buttonDetails.Location = new System.Drawing.Point(428, 182);
            this.buttonDetails.Name = "buttonDetails";
            this.buttonDetails.Size = new System.Drawing.Size(94, 29);
            this.buttonDetails.TabIndex = 5;
            this.buttonDetails.Text = "Details";
            this.buttonDetails.UseVisualStyleBackColor = true;
            this.buttonDetails.Click += new System.EventHandler(this.buttonDetails_Click);
            // 
            // FormRentAndPilotRequests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 331);
            this.Controls.Add(this.buttonDetails);
            this.Controls.Add(this.buttonAllRents);
            this.Controls.Add(this.buttonAllPilotRequests);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBoxRentsAndRequests);
            this.Controls.Add(this.buttonUnaccept);
            this.Name = "FormRentAndPilotRequests";
            this.Text = "FormRentAndPilotRequests";
            this.ResumeLayout(false);

        }

        #endregion

        private Button buttonUnaccept;
        private ListBox listBoxRentsAndRequests;
        private Button button2;
        private Button buttonAllPilotRequests;
        private Button buttonAllRents;
        private Button buttonDetails;
    }
}
namespace WindowsForms.Forms.RequestManagerForms
{
    partial class FormRequestManager
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
            this.buttonRequests = new System.Windows.Forms.Button();
            this.buttonRents = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRequests
            // 
            this.buttonRequests.Location = new System.Drawing.Point(167, 172);
            this.buttonRequests.Name = "buttonRequests";
            this.buttonRequests.Size = new System.Drawing.Size(168, 104);
            this.buttonRequests.TabIndex = 0;
            this.buttonRequests.Text = "Requests";
            this.buttonRequests.UseVisualStyleBackColor = true;
            this.buttonRequests.Click += new System.EventHandler(this.buttonRequests_Click);
            // 
            // buttonRents
            // 
            this.buttonRents.Location = new System.Drawing.Point(437, 172);
            this.buttonRents.Name = "buttonRents";
            this.buttonRents.Size = new System.Drawing.Size(168, 104);
            this.buttonRents.TabIndex = 1;
            this.buttonRents.Text = "Rents";
            this.buttonRents.UseVisualStyleBackColor = true;
            this.buttonRents.Click += new System.EventHandler(this.buttonRents_Click);
            // 
            // FormRequestManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 451);
            this.Controls.Add(this.buttonRents);
            this.Controls.Add(this.buttonRequests);
            this.Name = "FormRequestManager";
            this.Text = "FormRequestManager";
            this.ResumeLayout(false);

        }

        #endregion

        private Button buttonRequests;
        private Button buttonRents;
    }
}
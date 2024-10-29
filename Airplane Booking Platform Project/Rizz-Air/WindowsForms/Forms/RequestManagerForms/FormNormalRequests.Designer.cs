namespace WindowsForms.Forms.RequestManagerForms
{
    partial class FormNormalRequests
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
            this.buttonShowDetails = new System.Windows.Forms.Button();
            this.buttonCheapestAssign = new System.Windows.Forms.Button();
            this.buttonRandomAssign = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonManualConfirm = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonShowAll = new System.Windows.Forms.Button();
            this.listBoxRequests = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.buttonSearchByEmail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonShowDetails
            // 
            this.buttonShowDetails.Location = new System.Drawing.Point(459, 180);
            this.buttonShowDetails.Name = "buttonShowDetails";
            this.buttonShowDetails.Size = new System.Drawing.Size(186, 29);
            this.buttonShowDetails.TabIndex = 21;
            this.buttonShowDetails.Text = "Show Details for Request";
            this.buttonShowDetails.UseVisualStyleBackColor = true;
            this.buttonShowDetails.Click += new System.EventHandler(this.buttonShowDetails_Click);
            // 
            // buttonCheapestAssign
            // 
            this.buttonCheapestAssign.Location = new System.Drawing.Point(21, 239);
            this.buttonCheapestAssign.Name = "buttonCheapestAssign";
            this.buttonCheapestAssign.Size = new System.Drawing.Size(132, 29);
            this.buttonCheapestAssign.TabIndex = 20;
            this.buttonCheapestAssign.Text = "Cheapest Assign";
            this.buttonCheapestAssign.UseVisualStyleBackColor = true;
            this.buttonCheapestAssign.Click += new System.EventHandler(this.buttonCheapestAssign_Click);
            // 
            // buttonRandomAssign
            // 
            this.buttonRandomAssign.Location = new System.Drawing.Point(159, 239);
            this.buttonRandomAssign.Name = "buttonRandomAssign";
            this.buttonRandomAssign.Size = new System.Drawing.Size(122, 29);
            this.buttonRandomAssign.TabIndex = 19;
            this.buttonRandomAssign.Text = "Random Assign";
            this.buttonRandomAssign.UseVisualStyleBackColor = true;
            this.buttonRandomAssign.Click += new System.EventHandler(this.buttonRandomAssign_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(238, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(8, 8);
            this.button1.TabIndex = 18;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // buttonManualConfirm
            // 
            this.buttonManualConfirm.Location = new System.Drawing.Point(290, 239);
            this.buttonManualConfirm.Name = "buttonManualConfirm";
            this.buttonManualConfirm.Size = new System.Drawing.Size(122, 29);
            this.buttonManualConfirm.TabIndex = 16;
            this.buttonManualConfirm.Text = "Manual Assign";
            this.buttonManualConfirm.UseVisualStyleBackColor = true;
            this.buttonManualConfirm.Click += new System.EventHandler(this.buttonManualConfirm_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(488, 239);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(94, 29);
            this.buttonDelete.TabIndex = 14;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonShowAll
            // 
            this.buttonShowAll.Location = new System.Drawing.Point(459, 136);
            this.buttonShowAll.Name = "buttonShowAll";
            this.buttonShowAll.Size = new System.Drawing.Size(186, 29);
            this.buttonShowAll.TabIndex = 13;
            this.buttonShowAll.Text = "Show All Requests";
            this.buttonShowAll.UseVisualStyleBackColor = true;
            this.buttonShowAll.Click += new System.EventHandler(this.buttonShowAll_Click);
            // 
            // listBoxRequests
            // 
            this.listBoxRequests.FormattingEnabled = true;
            this.listBoxRequests.ItemHeight = 20;
            this.listBoxRequests.Location = new System.Drawing.Point(21, 35);
            this.listBoxRequests.Name = "listBoxRequests";
            this.listBoxRequests.Size = new System.Drawing.Size(391, 184);
            this.listBoxRequests.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(436, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Email";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(488, 35);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(144, 27);
            this.textBoxEmail.TabIndex = 12;
            // 
            // buttonSearchByEmail
            // 
            this.buttonSearchByEmail.Location = new System.Drawing.Point(459, 89);
            this.buttonSearchByEmail.Name = "buttonSearchByEmail";
            this.buttonSearchByEmail.Size = new System.Drawing.Size(186, 29);
            this.buttonSearchByEmail.TabIndex = 22;
            this.buttonSearchByEmail.Text = "Search By Email";
            this.buttonSearchByEmail.UseVisualStyleBackColor = true;
            this.buttonSearchByEmail.Click += new System.EventHandler(this.buttonSearchByEmail_Click);
            // 
            // FormNormalRequests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 306);
            this.Controls.Add(this.buttonSearchByEmail);
            this.Controls.Add(this.buttonShowDetails);
            this.Controls.Add(this.buttonCheapestAssign);
            this.Controls.Add(this.buttonRandomAssign);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonManualConfirm);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonShowAll);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.listBoxRequests);
            this.Name = "FormNormalRequests";
            this.Text = "Normal Requests";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonShowDetails;
        private Button buttonCheapestAssign;
        private Button buttonRandomAssign;
        private Button button1;
        private Button buttonManualConfirm;
        private Button buttonDelete;
        private Button buttonShowAll;
        private ListBox listBoxRequests;
        private Label label1;
        private TextBox textBoxEmail;
        private Button buttonSearchByEmail;
    }
}
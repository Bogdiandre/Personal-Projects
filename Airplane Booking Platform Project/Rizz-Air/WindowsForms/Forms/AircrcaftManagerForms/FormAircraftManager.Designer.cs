namespace WindowsForms.Forms.AircraftManagerForms
{
    partial class FormAircraftManager
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
            this.listBoxAircrafts = new System.Windows.Forms.ListBox();
            this.buttonAddHelicopter = new System.Windows.Forms.Button();
            this.buttonAddPrivateJet = new System.Windows.Forms.Button();
            this.buttonEditAircraft = new System.Windows.Forms.Button();
            this.buttonDeleteAircraft = new System.Windows.Forms.Button();
            this.buttonShowAllHelicopters = new System.Windows.Forms.Button();
            this.buttonShowAllPrivateJets = new System.Windows.Forms.Button();
            this.buttonShowAllAircrafts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxAircrafts
            // 
            this.listBoxAircrafts.FormattingEnabled = true;
            this.listBoxAircrafts.ItemHeight = 20;
            this.listBoxAircrafts.Location = new System.Drawing.Point(36, 35);
            this.listBoxAircrafts.Name = "listBoxAircrafts";
            this.listBoxAircrafts.Size = new System.Drawing.Size(480, 304);
            this.listBoxAircrafts.TabIndex = 0;
            // 
            // buttonAddHelicopter
            // 
            this.buttonAddHelicopter.Location = new System.Drawing.Point(543, 60);
            this.buttonAddHelicopter.Name = "buttonAddHelicopter";
            this.buttonAddHelicopter.Size = new System.Drawing.Size(140, 29);
            this.buttonAddHelicopter.TabIndex = 1;
            this.buttonAddHelicopter.Text = "Add Helicopter";
            this.buttonAddHelicopter.UseVisualStyleBackColor = true;
            this.buttonAddHelicopter.Click += new System.EventHandler(this.buttonAddHelicopter_Click);
            // 
            // buttonAddPrivateJet
            // 
            this.buttonAddPrivateJet.Location = new System.Drawing.Point(543, 118);
            this.buttonAddPrivateJet.Name = "buttonAddPrivateJet";
            this.buttonAddPrivateJet.Size = new System.Drawing.Size(140, 29);
            this.buttonAddPrivateJet.TabIndex = 2;
            this.buttonAddPrivateJet.Text = "Add Private Jet";
            this.buttonAddPrivateJet.UseVisualStyleBackColor = true;
            this.buttonAddPrivateJet.Click += new System.EventHandler(this.buttonAddPrivateJet_Click);
            // 
            // buttonEditAircraft
            // 
            this.buttonEditAircraft.Location = new System.Drawing.Point(566, 231);
            this.buttonEditAircraft.Name = "buttonEditAircraft";
            this.buttonEditAircraft.Size = new System.Drawing.Size(94, 29);
            this.buttonEditAircraft.TabIndex = 3;
            this.buttonEditAircraft.Text = "Edit";
            this.buttonEditAircraft.UseVisualStyleBackColor = true;
            this.buttonEditAircraft.Click += new System.EventHandler(this.buttonEditAircraft_Click);
            // 
            // buttonDeleteAircraft
            // 
            this.buttonDeleteAircraft.Location = new System.Drawing.Point(566, 292);
            this.buttonDeleteAircraft.Name = "buttonDeleteAircraft";
            this.buttonDeleteAircraft.Size = new System.Drawing.Size(94, 29);
            this.buttonDeleteAircraft.TabIndex = 4;
            this.buttonDeleteAircraft.Text = "Delete";
            this.buttonDeleteAircraft.UseVisualStyleBackColor = true;
            this.buttonDeleteAircraft.Click += new System.EventHandler(this.buttonDeleteAircraft_Click);
            // 
            // buttonShowAllHelicopters
            // 
            this.buttonShowAllHelicopters.Location = new System.Drawing.Point(348, 358);
            this.buttonShowAllHelicopters.Name = "buttonShowAllHelicopters";
            this.buttonShowAllHelicopters.Size = new System.Drawing.Size(140, 29);
            this.buttonShowAllHelicopters.TabIndex = 5;
            this.buttonShowAllHelicopters.Text = "All Helicopters";
            this.buttonShowAllHelicopters.UseVisualStyleBackColor = true;
            this.buttonShowAllHelicopters.Click += new System.EventHandler(this.buttonShowAllHelicopters_Click);
            // 
            // buttonShowAllPrivateJets
            // 
            this.buttonShowAllPrivateJets.Location = new System.Drawing.Point(190, 358);
            this.buttonShowAllPrivateJets.Name = "buttonShowAllPrivateJets";
            this.buttonShowAllPrivateJets.Size = new System.Drawing.Size(120, 29);
            this.buttonShowAllPrivateJets.TabIndex = 6;
            this.buttonShowAllPrivateJets.Text = "All Private Jets";
            this.buttonShowAllPrivateJets.UseVisualStyleBackColor = true;
            this.buttonShowAllPrivateJets.Click += new System.EventHandler(this.buttonShowAllPrivateJets_Click);
            // 
            // buttonShowAllAircrafts
            // 
            this.buttonShowAllAircrafts.Location = new System.Drawing.Point(54, 358);
            this.buttonShowAllAircrafts.Name = "buttonShowAllAircrafts";
            this.buttonShowAllAircrafts.Size = new System.Drawing.Size(94, 29);
            this.buttonShowAllAircrafts.TabIndex = 7;
            this.buttonShowAllAircrafts.Text = "All Aircrafts";
            this.buttonShowAllAircrafts.UseVisualStyleBackColor = true;
            this.buttonShowAllAircrafts.Click += new System.EventHandler(this.buttonShowAllAircrafts_Click);
            // 
            // FormAircraftManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 432);
            this.Controls.Add(this.buttonShowAllAircrafts);
            this.Controls.Add(this.buttonShowAllPrivateJets);
            this.Controls.Add(this.buttonShowAllHelicopters);
            this.Controls.Add(this.buttonDeleteAircraft);
            this.Controls.Add(this.buttonEditAircraft);
            this.Controls.Add(this.buttonAddPrivateJet);
            this.Controls.Add(this.buttonAddHelicopter);
            this.Controls.Add(this.listBoxAircrafts);
            this.Name = "FormAircraftManager";
            this.Text = "Aircraft Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listBoxAircrafts;
        protected Button buttonAddHelicopter;
        private Button buttonAddPrivateJet;
        private Button buttonEditAircraft;
        private Button buttonDeleteAircraft;
        private Button buttonShowAllHelicopters;
        private Button buttonShowAllPrivateJets;
        private Button buttonShowAllAircrafts;
    }
}
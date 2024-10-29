namespace WindowsForms.Forms.DestinationManagerForms
{
    partial class FormDestinationManager
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
            this.listBoxDestinaitons = new System.Windows.Forms.ListBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.numericUpDownDistance = new System.Windows.Forms.NumericUpDown();
            this.buttonCreateDestination = new System.Windows.Forms.Button();
            this.buttonEditDestination = new System.Windows.Forms.Button();
            this.buttonDeleteDestination = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxDestinaitons
            // 
            this.listBoxDestinaitons.FormattingEnabled = true;
            this.listBoxDestinaitons.ItemHeight = 20;
            this.listBoxDestinaitons.Location = new System.Drawing.Point(77, 21);
            this.listBoxDestinaitons.Name = "listBoxDestinaitons";
            this.listBoxDestinaitons.Size = new System.Drawing.Size(308, 264);
            this.listBoxDestinaitons.TabIndex = 0;
            this.listBoxDestinaitons.SelectedIndexChanged += new System.EventHandler(this.listBoxDestinaitons_SelectedIndexChanged);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(550, 35);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(125, 27);
            this.textBoxName.TabIndex = 1;
            // 
            // numericUpDownDistance
            // 
            this.numericUpDownDistance.Location = new System.Drawing.Point(550, 111);
            this.numericUpDownDistance.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numericUpDownDistance.Name = "numericUpDownDistance";
            this.numericUpDownDistance.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownDistance.TabIndex = 2;
            // 
            // buttonCreateDestination
            // 
            this.buttonCreateDestination.Location = new System.Drawing.Point(478, 161);
            this.buttonCreateDestination.Name = "buttonCreateDestination";
            this.buttonCreateDestination.Size = new System.Drawing.Size(113, 43);
            this.buttonCreateDestination.TabIndex = 3;
            this.buttonCreateDestination.Text = "Create";
            this.buttonCreateDestination.UseVisualStyleBackColor = true;
            this.buttonCreateDestination.Click += new System.EventHandler(this.buttonCreateDestination_Click);
            // 
            // buttonEditDestination
            // 
            this.buttonEditDestination.Location = new System.Drawing.Point(623, 161);
            this.buttonEditDestination.Name = "buttonEditDestination";
            this.buttonEditDestination.Size = new System.Drawing.Size(116, 43);
            this.buttonEditDestination.TabIndex = 4;
            this.buttonEditDestination.Text = "Edit";
            this.buttonEditDestination.UseVisualStyleBackColor = true;
            this.buttonEditDestination.Click += new System.EventHandler(this.buttonEditDestination_Click);
            // 
            // buttonDeleteDestination
            // 
            this.buttonDeleteDestination.Location = new System.Drawing.Point(623, 230);
            this.buttonDeleteDestination.Name = "buttonDeleteDestination";
            this.buttonDeleteDestination.Size = new System.Drawing.Size(116, 42);
            this.buttonDeleteDestination.TabIndex = 5;
            this.buttonDeleteDestination.Text = "Delete";
            this.buttonDeleteDestination.UseVisualStyleBackColor = true;
            this.buttonDeleteDestination.Click += new System.EventHandler(this.buttonDeleteDestination_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(495, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(478, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Distance";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(478, 230);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(113, 42);
            this.buttonReset.TabIndex = 8;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // FormDestinationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 332);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDeleteDestination);
            this.Controls.Add(this.buttonEditDestination);
            this.Controls.Add(this.buttonCreateDestination);
            this.Controls.Add(this.numericUpDownDistance);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.listBoxDestinaitons);
            this.Name = "FormDestinationManager";
            this.Text = "Destination Manager";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox listBoxDestinaitons;
        private TextBox textBoxName;
        private NumericUpDown numericUpDownDistance;
        private Button buttonCreateDestination;
        private Button buttonEditDestination;
        private Button buttonDeleteDestination;
        private Label label1;
        private Label label2;
        private Button buttonReset;
    }
}
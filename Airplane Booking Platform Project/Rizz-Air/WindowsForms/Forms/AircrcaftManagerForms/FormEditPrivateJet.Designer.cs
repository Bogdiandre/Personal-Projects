namespace WindowsForms.Forms.AircraftManagerForms
{
    partial class FormEditPrivateJet
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
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownSeatNumber = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAge = new System.Windows.Forms.NumericUpDown();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonUpdatePrivateJet = new System.Windows.Forms.Button();
            this.numericUpDownRange = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAverageSpeed = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxConsumption = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeatNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAverageSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(98, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(292, 46);
            this.label8.TabIndex = 54;
            this.label8.Text = "Update Private Jet";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 453);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 20);
            this.label7.TabIndex = 53;
            this.label7.Text = "Seat Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 397);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 20);
            this.label6.TabIndex = 52;
            this.label6.Text = "Consumption";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(134, 343);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 51;
            this.label5.Text = "Range";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 286);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 50;
            this.label4.Text = "Average Speed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 49;
            this.label3.Text = "Weight";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 20);
            this.label2.TabIndex = 48;
            this.label2.Text = "Age";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "Name";
            // 
            // numericUpDownSeatNumber
            // 
            this.numericUpDownSeatNumber.Location = new System.Drawing.Point(201, 451);
            this.numericUpDownSeatNumber.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSeatNumber.Name = "numericUpDownSeatNumber";
            this.numericUpDownSeatNumber.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownSeatNumber.TabIndex = 46;
            // 
            // numericUpDownAge
            // 
            this.numericUpDownAge.Location = new System.Drawing.Point(199, 176);
            this.numericUpDownAge.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownAge.Name = "numericUpDownAge";
            this.numericUpDownAge.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownAge.TabIndex = 45;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(312, 533);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(94, 29);
            this.buttonReset.TabIndex = 44;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(186, 533);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 43;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonUpdatePrivateJet
            // 
            this.buttonUpdatePrivateJet.Location = new System.Drawing.Point(60, 533);
            this.buttonUpdatePrivateJet.Name = "buttonUpdatePrivateJet";
            this.buttonUpdatePrivateJet.Size = new System.Drawing.Size(94, 29);
            this.buttonUpdatePrivateJet.TabIndex = 42;
            this.buttonUpdatePrivateJet.Text = "Update";
            this.buttonUpdatePrivateJet.UseVisualStyleBackColor = true;
            this.buttonUpdatePrivateJet.Click += new System.EventHandler(this.buttonUpdatePrivateJet_Click);
            // 
            // numericUpDownRange
            // 
            this.numericUpDownRange.Location = new System.Drawing.Point(199, 341);
            this.numericUpDownRange.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRange.Name = "numericUpDownRange";
            this.numericUpDownRange.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownRange.TabIndex = 40;
            // 
            // numericUpDownAverageSpeed
            // 
            this.numericUpDownAverageSpeed.Location = new System.Drawing.Point(201, 284);
            this.numericUpDownAverageSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownAverageSpeed.Name = "numericUpDownAverageSpeed";
            this.numericUpDownAverageSpeed.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownAverageSpeed.TabIndex = 39;
            // 
            // numericUpDownWeight
            // 
            this.numericUpDownWeight.Location = new System.Drawing.Point(199, 231);
            this.numericUpDownWeight.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numericUpDownWeight.Name = "numericUpDownWeight";
            this.numericUpDownWeight.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownWeight.TabIndex = 38;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(199, 120);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(152, 27);
            this.textBoxName.TabIndex = 37;
            // 
            // textBoxConsumption
            // 
            this.textBoxConsumption.Location = new System.Drawing.Point(201, 394);
            this.textBoxConsumption.Name = "textBoxConsumption";
            this.textBoxConsumption.Size = new System.Drawing.Size(152, 27);
            this.textBoxConsumption.TabIndex = 55;
            // 
            // FormEditPrivateJet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 628);
            this.Controls.Add(this.textBoxConsumption);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownSeatNumber);
            this.Controls.Add(this.numericUpDownAge);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonUpdatePrivateJet);
            this.Controls.Add(this.numericUpDownRange);
            this.Controls.Add(this.numericUpDownAverageSpeed);
            this.Controls.Add(this.numericUpDownWeight);
            this.Controls.Add(this.textBoxName);
            this.Name = "FormEditPrivateJet";
            this.Text = "FormEditPrivateJet";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeatNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAverageSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private NumericUpDown numericUpDownSeatNumber;
        private NumericUpDown numericUpDownAge;
        private Button buttonReset;
        private Button buttonCancel;
        private Button buttonUpdatePrivateJet;
        private NumericUpDown numericUpDownRange;
        private NumericUpDown numericUpDownAverageSpeed;
        private NumericUpDown numericUpDownWeight;
        private TextBox textBoxName;
        private TextBox textBoxConsumption;
    }
}
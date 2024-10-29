namespace WindowsForms.Forms.AircraftManagerForms
{
    partial class FormAddHelicopter
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAverageSpeed = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRange = new System.Windows.Forms.NumericUpDown();
            this.buttonCreateHelicopter = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.numericUpDownAge = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPricePerDay = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxConsumption = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAverageSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPricePerDay)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(225, 131);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(152, 27);
            this.textBoxName.TabIndex = 0;
            // 
            // numericUpDownWeight
            // 
            this.numericUpDownWeight.Location = new System.Drawing.Point(225, 242);
            this.numericUpDownWeight.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numericUpDownWeight.Name = "numericUpDownWeight";
            this.numericUpDownWeight.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownWeight.TabIndex = 2;
            // 
            // numericUpDownAverageSpeed
            // 
            this.numericUpDownAverageSpeed.Location = new System.Drawing.Point(227, 295);
            this.numericUpDownAverageSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownAverageSpeed.Name = "numericUpDownAverageSpeed";
            this.numericUpDownAverageSpeed.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownAverageSpeed.TabIndex = 3;
            // 
            // numericUpDownRange
            // 
            this.numericUpDownRange.Location = new System.Drawing.Point(225, 352);
            this.numericUpDownRange.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRange.Name = "numericUpDownRange";
            this.numericUpDownRange.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownRange.TabIndex = 4;
            // 
            // buttonCreateHelicopter
            // 
            this.buttonCreateHelicopter.Location = new System.Drawing.Point(86, 544);
            this.buttonCreateHelicopter.Name = "buttonCreateHelicopter";
            this.buttonCreateHelicopter.Size = new System.Drawing.Size(94, 29);
            this.buttonCreateHelicopter.TabIndex = 6;
            this.buttonCreateHelicopter.Text = "Create";
            this.buttonCreateHelicopter.UseVisualStyleBackColor = true;
            this.buttonCreateHelicopter.Click += new System.EventHandler(this.buttonCreateHelicopter_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(212, 544);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(338, 544);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(94, 29);
            this.buttonReset.TabIndex = 8;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // numericUpDownAge
            // 
            this.numericUpDownAge.Location = new System.Drawing.Point(225, 187);
            this.numericUpDownAge.Name = "numericUpDownAge";
            this.numericUpDownAge.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownAge.TabIndex = 9;
            // 
            // numericUpDownPricePerDay
            // 
            this.numericUpDownPricePerDay.Location = new System.Drawing.Point(227, 462);
            this.numericUpDownPricePerDay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownPricePerDay.Name = "numericUpDownPricePerDay";
            this.numericUpDownPricePerDay.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownPricePerDay.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Age";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Weight";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Average Speed";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 354);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Range";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(124, 408);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "Consumption";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(126, 464);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "Price Per Day";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(126, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(283, 46);
            this.label8.TabIndex = 18;
            this.label8.Text = "Create Helicopter";
            // 
            // textBoxConsumption
            // 
            this.textBoxConsumption.Location = new System.Drawing.Point(225, 405);
            this.textBoxConsumption.Name = "textBoxConsumption";
            this.textBoxConsumption.Size = new System.Drawing.Size(152, 27);
            this.textBoxConsumption.TabIndex = 19;
            // 
            // FormAddHelicopter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 614);
            this.Controls.Add(this.textBoxConsumption);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownPricePerDay);
            this.Controls.Add(this.numericUpDownAge);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonCreateHelicopter);
            this.Controls.Add(this.numericUpDownRange);
            this.Controls.Add(this.numericUpDownAverageSpeed);
            this.Controls.Add(this.numericUpDownWeight);
            this.Controls.Add(this.textBoxName);
            this.Name = "FormAddHelicopter";
            this.Text = "FormAddHelicopter";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAverageSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPricePerDay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxName;
        private NumericUpDown numericUpDownWeight;
        private NumericUpDown numericUpDownAverageSpeed;
        private NumericUpDown numericUpDownRange;
        private Button buttonCreateHelicopter;
        private Button buttonCancel;
        private Button buttonReset;
        private NumericUpDown numericUpDownAge;
        private NumericUpDown numericUpDownPricePerDay;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox textBoxConsumption;
    }
}
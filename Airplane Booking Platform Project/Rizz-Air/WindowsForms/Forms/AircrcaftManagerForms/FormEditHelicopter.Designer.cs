namespace WindowsForms.Forms.AircraftManagerForms
{
    partial class FormEditHelicopter
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
            this.numericUpDownPricePerDay = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAge = new System.Windows.Forms.NumericUpDown();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonUpdateHelicopter = new System.Windows.Forms.Button();
            this.numericUpDownRange = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAverageSpeed = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxConsumption = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPricePerDay)).BeginInit();
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
            this.label8.Location = new System.Drawing.Point(117, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(295, 46);
            this.label8.TabIndex = 36;
            this.label8.Text = "Update Helicopter";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(117, 439);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 20);
            this.label7.TabIndex = 35;
            this.label7.Text = "Price Per Day";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(115, 383);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 20);
            this.label6.TabIndex = 34;
            this.label6.Text = "Consumption";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 329);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 33;
            this.label5.Text = "Range";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(97, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 32;
            this.label4.Text = "Average Speed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 31;
            this.label3.Text = "Weight";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "Age";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 29;
            this.label1.Text = "Name";
            // 
            // numericUpDownPricePerDay
            // 
            this.numericUpDownPricePerDay.Location = new System.Drawing.Point(218, 437);
            this.numericUpDownPricePerDay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownPricePerDay.Name = "numericUpDownPricePerDay";
            this.numericUpDownPricePerDay.Size = new System.Drawing.Size(152, 27);
            this.numericUpDownPricePerDay.TabIndex = 28;
            // 
            // numericUpDownAge
            // 
            this.numericUpDownAge.Location = new System.Drawing.Point(216, 162);
            this.numericUpDownAge.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownAge.Name = "numericUpDownAge";
            this.numericUpDownAge.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownAge.TabIndex = 27;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(329, 519);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(94, 29);
            this.buttonReset.TabIndex = 26;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(203, 519);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 25;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonUpdateHelicopter
            // 
            this.buttonUpdateHelicopter.Location = new System.Drawing.Point(77, 519);
            this.buttonUpdateHelicopter.Name = "buttonUpdateHelicopter";
            this.buttonUpdateHelicopter.Size = new System.Drawing.Size(94, 29);
            this.buttonUpdateHelicopter.TabIndex = 24;
            this.buttonUpdateHelicopter.Text = "Update";
            this.buttonUpdateHelicopter.UseVisualStyleBackColor = true;
            this.buttonUpdateHelicopter.Click += new System.EventHandler(this.buttonUpdateHelicopter_Click);
            // 
            // numericUpDownRange
            // 
            this.numericUpDownRange.Location = new System.Drawing.Point(216, 327);
            this.numericUpDownRange.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRange.Name = "numericUpDownRange";
            this.numericUpDownRange.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownRange.TabIndex = 22;
            // 
            // numericUpDownAverageSpeed
            // 
            this.numericUpDownAverageSpeed.Location = new System.Drawing.Point(218, 270);
            this.numericUpDownAverageSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownAverageSpeed.Name = "numericUpDownAverageSpeed";
            this.numericUpDownAverageSpeed.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownAverageSpeed.TabIndex = 21;
            // 
            // numericUpDownWeight
            // 
            this.numericUpDownWeight.Location = new System.Drawing.Point(216, 217);
            this.numericUpDownWeight.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numericUpDownWeight.Name = "numericUpDownWeight";
            this.numericUpDownWeight.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownWeight.TabIndex = 20;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(216, 106);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(152, 27);
            this.textBoxName.TabIndex = 19;
            // 
            // textBoxConsumption
            // 
            this.textBoxConsumption.Location = new System.Drawing.Point(218, 380);
            this.textBoxConsumption.Name = "textBoxConsumption";
            this.textBoxConsumption.Size = new System.Drawing.Size(152, 27);
            this.textBoxConsumption.TabIndex = 37;
            // 
            // FormEditHelicopter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 600);
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
            this.Controls.Add(this.buttonUpdateHelicopter);
            this.Controls.Add(this.numericUpDownRange);
            this.Controls.Add(this.numericUpDownAverageSpeed);
            this.Controls.Add(this.numericUpDownWeight);
            this.Controls.Add(this.textBoxName);
            this.Name = "FormEditHelicopter";
            this.Text = "FormEditHelicopter";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPricePerDay)).EndInit();
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
        private NumericUpDown numericUpDownPricePerDay;
        private NumericUpDown numericUpDownAge;
        private Button buttonReset;
        private Button buttonCancel;
        private Button buttonUpdateHelicopter;
        private NumericUpDown numericUpDownRange;
        private NumericUpDown numericUpDownAverageSpeed;
        private NumericUpDown numericUpDownWeight;
        private TextBox textBoxName;
        private TextBox textBoxConsumption;
    }
}
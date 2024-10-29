using DomainLibrary.Domains;
using DomainLibrary.Exceptions;
using DomainLibrary.Validation;
using LogicLibrary.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.Forms.AircraftManagerForms
{
    public partial class FormAddPrivateJet : Form
    {
        private AircraftManager aircraftManager;
        private ValidateCredentials validateAircraft;

        public FormAddPrivateJet(AircraftManager am)
        {
            InitializeComponent();

            ResetEverything();

            this.aircraftManager = am;
            validateAircraft = new ValidateCredentials();
        }

        private void buttonCreatePrivateJet_Click(object sender, EventArgs e)
        {
            try { 

                string name = textBoxName.Text.ToString();
                int age = (int)numericUpDownAge.Value;
                int weight = (int)numericUpDownWeight.Value;
                int averageSpeed = (int)numericUpDownAverageSpeed.Value;
                int range = (int)numericUpDownRange.Value;
                double consumption = Convert.ToDouble(textBoxConsumption.Text);
                int seatNumber = (int)numericUpDownSeatNumber.Value;

                PrivateJet privateJet = new PrivateJet(name, age, weight, averageSpeed, range, consumption, seatNumber);



                string validationErrorMessage;
                if (validateAircraft.Validate(privateJet, out validationErrorMessage))
                {
                    aircraftManager.AddAircraft(privateJet);

                    MessageBox.Show($"Private Jet {name} created successfully!");

                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Validation Error:\n{validationErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        public void ResetEverything()
        {
            textBoxName.Text = null;
            numericUpDownAge.Value = 0;
            numericUpDownAverageSpeed.Value = 0;
            textBoxConsumption.Text = "0";
            numericUpDownSeatNumber.Value = 0;
            numericUpDownRange.Value = 0;
            numericUpDownWeight.Value = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetEverything();
        }
    }
}

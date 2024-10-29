using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using DomainLibrary.Domains;
using DomainLibrary.Enumerations;
using DomainLibrary.Validation;
using System.Security.Cryptography.X509Certificates;
using DomainLibrary.Exceptions;

namespace WindowsForms.Forms.AircraftManagerForms
{
    public partial class FormAddHelicopter : Form
    {
        private AircraftManager aircraftManager;
        private ValidateCredentials validateAircraft;

        public FormAddHelicopter(AircraftManager am)
        {
            InitializeComponent();

            ResetEverything();

            this.aircraftManager = am;
            validateAircraft = new ValidateCredentials();
        }

        private void buttonCreateHelicopter_Click(object sender, EventArgs e)
        {
            try
            {


                string name = textBoxName.Text.ToString();
                int age = (int)numericUpDownAge.Value;
                int weight = (int)numericUpDownWeight.Value;
                int averageSpeed = (int)numericUpDownAverageSpeed.Value;
                int range = (int)numericUpDownRange.Value;
                double consumption = Convert.ToDouble(textBoxConsumption.Text);
                int pricePerDay = (int)numericUpDownPricePerDay.Value;

                Helicopter helicopter = new Helicopter(name, age, weight, averageSpeed, range, consumption, pricePerDay);

                string validationErrorMessage;
                if (validateAircraft.Validate(helicopter, out validationErrorMessage))
                {
                    aircraftManager.AddAircraft(helicopter);

                    MessageBox.Show($"Helicopter {name} created successfully!");

                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Validation Error:\n{validationErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HelicopterException h) { MessageBox.Show(h.Message); }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); } 

        }

        public void ResetEverything()
        {
            textBoxName.Text = null;
            numericUpDownAge.Value = 0;
            numericUpDownAverageSpeed.Value = 0;
            textBoxConsumption.Text = "0";
            numericUpDownPricePerDay.Value = 0;
            numericUpDownRange.Value = 0;
            numericUpDownWeight.Value = 0;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetEverything();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

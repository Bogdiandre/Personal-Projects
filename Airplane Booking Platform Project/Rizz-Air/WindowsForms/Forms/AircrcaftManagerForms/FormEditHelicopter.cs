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
    public partial class FormEditHelicopter : Form
    {
        private AircraftManager aircraftManager;
        private Helicopter originalHelicopter;
        private NormalRequestManager normalRequestManager;
        private PilotRequestManager pilotRequestManager;
        private ValidateCredentials validateAircraft;

        public FormEditHelicopter(Helicopter hel, AircraftManager am, NormalRequestManager normalRequestManager, PilotRequestManager pilotRequestManager)
        {
            InitializeComponent();

            aircraftManager = am;
            originalHelicopter = hel;

            FillEverything(hel);
            this.normalRequestManager = normalRequestManager;
            this.pilotRequestManager = pilotRequestManager;
            validateAircraft = new ValidateCredentials();
        }

        private void buttonUpdateHelicopter_Click(object sender, EventArgs e)
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

                List<NormalRequest> normalRequestList = normalRequestManager.GetNormalRequestsByAircraftID(originalHelicopter.AircraftId);
                List<PilotRequest> pilotRequestList = pilotRequestManager.GetAllPilotRequestsByHelicopterID(originalHelicopter.AircraftId);

                Helicopter helicopter = new Helicopter(name, age, weight, averageSpeed, range, consumption, pricePerDay);

                string validationErrorMessage;
                if (validateAircraft.Validate(helicopter, out validationErrorMessage))
                {

                    if ((normalRequestList.Count > 0 && (averageSpeed != originalHelicopter.AverageSpeed || range != originalHelicopter.Range || consumption != originalHelicopter.Consumption)) || (pilotRequestList.Count > 0 && pricePerDay != originalHelicopter.PricePerDay))
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to update this aircraft in this way? If you do all the requests with this Aircraft will be deleted!", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            aircraftManager.DeleteEverythingForAircraft(originalHelicopter.AircraftId);



                            aircraftManager.UpdateAircraft(originalHelicopter.AircraftId, helicopter);

                            MessageBox.Show($"Helicopter {name} updated successfully!");

                            this.Close();
                        }
                    }

                    else
                    {


                        aircraftManager.UpdateAircraft(originalHelicopter.AircraftId, helicopter);

                        MessageBox.Show($"Helicopter {name} updated successfully!");

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show($"Validation Error:\n{validationErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HelicopterException h) { MessageBox.Show(h.Message); }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            FillEverything(originalHelicopter);
        }

        public void FillEverything(Helicopter helicopter)
        {
            textBoxName.Text = helicopter.Name;
            numericUpDownAge.Value = helicopter.Age;
            numericUpDownAverageSpeed.Value = helicopter.AverageSpeed;
            textBoxConsumption.Text = helicopter.Consumption.ToString();
            numericUpDownPricePerDay.Value = helicopter.PricePerDay;
            numericUpDownRange.Value = helicopter.Range;
            numericUpDownWeight.Value = helicopter.Weight;
        }
    }
}

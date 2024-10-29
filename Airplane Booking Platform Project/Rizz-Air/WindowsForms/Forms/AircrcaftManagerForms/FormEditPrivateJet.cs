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
    public partial class FormEditPrivateJet : Form
    {
        private AircraftManager aircraftManager;
        private PrivateJet originalPrivateJet;
        private NormalRequestManager normalRequestManager;
        private ValidateCredentials validateAircraft;
        public FormEditPrivateJet(PrivateJet prj, AircraftManager am, NormalRequestManager nrM)
        {
            InitializeComponent();

            aircraftManager = am;
            originalPrivateJet = prj;
            normalRequestManager = nrM;
            validateAircraft = new ValidateCredentials();

            FillEverything(originalPrivateJet);
        }

        private void buttonUpdatePrivateJet_Click(object sender, EventArgs e)
        {
            try
            {
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
                    List<NormalRequest> normalRequestList = normalRequestManager.GetNormalRequestsByAircraftID(originalPrivateJet.AircraftId);

                    if (normalRequestList.Count > 0 && (averageSpeed != originalPrivateJet.AverageSpeed || range != originalPrivateJet.Range || consumption != originalPrivateJet.Consumption))
                    {

                        DialogResult result = MessageBox.Show("Are you sure you want to update this aircraft in this way? If you do all the requests with this Aircraft will be deleted!", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            aircraftManager.DeleteEverythingForAircraft(originalPrivateJet.AircraftId);

                            aircraftManager.UpdateAircraft(originalPrivateJet.AircraftId, privateJet);

                            MessageBox.Show($"Private Jet {name} updated successfully!");

                            this.Close();
                        }

                    }

                    else
                    {


                        aircraftManager.UpdateAircraft(originalPrivateJet.AircraftId, privateJet);

                        MessageBox.Show($"Private Jet {name} updated successfully!");

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show($"Validation Error:\n{validationErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            FillEverything(originalPrivateJet);
        }

        public void FillEverything(PrivateJet privateJet)
        {
            textBoxName.Text = privateJet.Name;
            numericUpDownAge.Value = privateJet.Age;
            numericUpDownAverageSpeed.Value = privateJet.AverageSpeed;
            textBoxConsumption.Text = privateJet.Consumption.ToString();
            numericUpDownSeatNumber.Value = privateJet.SeatNumber;
            numericUpDownRange.Value = privateJet.Range;
            numericUpDownWeight.Value = privateJet.Weight;
        }

    }
}

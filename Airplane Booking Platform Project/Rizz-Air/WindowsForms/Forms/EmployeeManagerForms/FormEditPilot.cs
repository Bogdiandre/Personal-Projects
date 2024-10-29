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
using Interfaces;
using DALLibrary;
using LogicLibrary.Managers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DomainLibrary.Validation;
using DomainLibrary.Exceptions;

namespace WindowsForms.Forms.EmployeeManagerForms
{
    public partial class FormEditPilot : Form
    {
        private EmployeeManager employeeManager;
        private Pilot oldPilot;
        private RentManager rentManager;
        private ValidateCredentials validateEmplloyee;

        public FormEditPilot(Pilot p, EmployeeManager em, RentManager rentManager)
        {
            InitializeComponent();

            employeeManager = em;
            oldPilot = p;

            comboBoxLicense.DataSource = Enum.GetValues(typeof(AircraftType));

            dateTimeBirthday.Format = DateTimePickerFormat.Custom;
            dateTimeBirthday.CustomFormat = "dd-MM-yyyy";
            this.rentManager = rentManager;
            validateEmplloyee = new ValidateCredentials();


            //ResetEverything();
            FillEverything();
        }

        public void FillEverything()
        {
            textBoxFirstName.Text = oldPilot.FirstName.ToString().Trim();
            textBoxLastName.Text = oldPilot.LastName.ToString().Trim();
            textBoxEmail.Text = oldPilot.Email.ToString().Trim();
            //textBoxPassword.Text = oldPilot.Password.ToString().Trim();
            textBoxPhone.Text = oldPilot.PhoneNumber.ToString().Trim();
            textBoxSallary.Text = oldPilot.SallaryPerKm.ToString().Trim(); ;
            comboBoxLicense.SelectedItem =oldPilot.License ;
            dateTimeBirthday.Value = oldPilot.DateOfBirth;

        }
        public void ResetEverything()
        {
            textBoxFirstName = null;
            textBoxLastName = null;
            textBoxEmail = null;

            textBoxPhone = null;
            textBoxSallary = null;
            comboBoxLicense.SelectedIndex = 0;
        }

        private void buttonUpdatePilot_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = textBoxFirstName.Text.ToString();
                string lastName = textBoxLastName.Text.ToString();
                string email = textBoxEmail.Text.ToString();
                //string password = textBoxPassword.Text.ToString();
                string phone = textBoxPhone.Text.ToString();
                AircraftType license = (AircraftType)comboBoxLicense.SelectedIndex;
                double salary = 0;
                if (double.TryParse(textBoxSallary.Text, out double newSalary))
                {
                    salary = newSalary;
                }
                DateTime dateOfBirth = (DateTime)dateTimeBirthday.Value;

                Pilot pilot = new Pilot(firstName, lastName, email, oldPilot.PasswordHash, oldPilot.PasswordSalt, phone, license, salary, dateOfBirth);

                string validationErrorMessage;
                if (validateEmplloyee.Validate(pilot, out validationErrorMessage))
                {

                    List<Rent> rentList = rentManager.GetAllRentsByPilotID(oldPilot.EmployeeId);

                    if (rentList.Count > 0 && oldPilot.SallaryPerKm != salary)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to update this pilot in this way? If you do all the rents with this pilot will be deleted!", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {


                            rentManager.DeleteAllRentsForPilot(oldPilot.EmployeeId);

                            employeeManager.UpdateEmployee(oldPilot.EmployeeId, pilot);

                            MessageBox.Show($"Pilot {lastName} updated successfully!");

                            this.Close();
                        }
                    }
                    else
                    {
                        employeeManager.UpdateEmployee(oldPilot.EmployeeId, pilot);

                        MessageBox.Show($"Pilot {lastName} updated successfully!");

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show($"Validation Error:\n{validationErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (PilotException a) { MessageBox.Show(a.Message); }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            //ResetEverything();
            FillEverything();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

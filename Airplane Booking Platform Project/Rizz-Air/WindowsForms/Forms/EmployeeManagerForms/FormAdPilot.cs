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
    public partial class FormAdPilot : Form
    {
        private EmployeeManager employeeManager;
        private ValidateCredentials validateEmplloyee;
        public FormAdPilot(EmployeeManager em)
        {
            InitializeComponent();

            employeeManager = em;

            comboBoxLicense.DataSource = Enum.GetValues(typeof(AircraftType));

            dateTimeBirthday.Format = DateTimePickerFormat.Custom;
            dateTimeBirthday.CustomFormat = "dd-MM-yyyy";
            validateEmplloyee = new ValidateCredentials();

            //ResetEverything();
        }

        public void ResetEverything()
        {
            textBoxFirstName = null;
            textBoxLastName = null;
            textBoxEmail = null;
            textBoxPassword = null;
            textBoxPhone = null;
            textBoxSallary = null;
            comboBoxLicense.SelectedIndex = 0;
        }

        private void buttonCreatePilot_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = textBoxFirstName.Text.ToString();
                string lastName = textBoxLastName.Text.ToString();
                string email = textBoxEmail.Text.ToString();
                string password = textBoxPassword.Text.ToString();
                string phone = textBoxPhone.Text.ToString();
                AircraftType license = (AircraftType)comboBoxLicense.SelectedIndex;
                double salary = 0;
                if (double.TryParse(textBoxSallary.Text, out double newSalary))
                {
                    salary = newSalary;
                }
                DateTime dateOfBirth = (DateTime)dateTimeBirthday.Value;

                Pilot pilot = new Pilot(firstName, lastName, email, password, phone, license, salary, dateOfBirth);

                string validationErrorMessage;
                if (validateEmplloyee.Validate(pilot, out validationErrorMessage) && password.Length >= 5 && employeeManager.GetManagerByEmail(email) == null && employeeManager.GetPilotByEmail(email) == null)
                {

                    employeeManager.AddEmployee(pilot);

                    MessageBox.Show($"Pilot {lastName} created successfully!");

                    this.Close();
                }
                else
                {

                    if (password.Length < 5)
                    {
                        validationErrorMessage = validationErrorMessage + "\n Password must be at least 5 characters.";
                    }
                    if (employeeManager.GetManagerByEmail(email) != null || employeeManager.GetPilotByEmail(email) != null)
                    {
                        validationErrorMessage = validationErrorMessage + "\n You cannot use an already used email adress!";
                    }

                    MessageBox.Show($"Validation Error:\n{validationErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (PilotException a) { MessageBox.Show(a.Message); }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
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





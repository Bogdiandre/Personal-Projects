using DomainLibrary.Domains;
using DomainLibrary.Enumerations;
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

namespace WindowsForms.Forms.EmployeeManagerForms
{
    public partial class FormAddManager : Form
    {
        private EmployeeManager employeeManager;
        private ValidateCredentials validateEmplloyee;
        public FormAddManager(EmployeeManager em)
        {
            InitializeComponent();

            employeeManager = em;

            comboBoxType.DataSource = Enum.GetValues(typeof(ManagerType));

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
            comboBoxType.SelectedIndex = 0;
        }

        private void buttonCreateManager_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = textBoxFirstName.Text.ToString();
                string lastName = textBoxLastName.Text.ToString();
                string email = textBoxEmail.Text.ToString();
                string password = textBoxPassword.Text.ToString();
                string phone = textBoxPhone.Text.ToString();
                ManagerType type = (ManagerType)comboBoxType.SelectedIndex;
                DateTime dateOfBirth = (DateTime)dateTimeBirthday.Value;

                Manager manager = new Manager(firstName, lastName, email, password, phone, type, dateOfBirth);

                string validationErrorMessage;
                if (validateEmplloyee.Validate(manager, out validationErrorMessage) && password.Length >= 5 && employeeManager.GetManagerByEmail(email) == null && employeeManager.GetPilotByEmail(email) == null)
                {

                    employeeManager.AddEmployee(manager);

                    MessageBox.Show($"Manager {lastName} created successfully!");

                    this.Close();
                }

                else
                {
                    if(password.Length < 5)
                    {
                        validationErrorMessage = validationErrorMessage + "\n Password must be at least 5 characters.";
                    }
                    if(employeeManager.GetManagerByEmail(email) != null || employeeManager.GetPilotByEmail(email) != null)
                    {
                        validationErrorMessage = validationErrorMessage + "\n You cannot use an already used email adress!";
                    }

                    MessageBox.Show($"Validation Error:\n{validationErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

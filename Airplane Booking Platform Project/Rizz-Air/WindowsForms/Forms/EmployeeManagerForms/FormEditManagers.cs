using DomainLibrary.Enumerations;
using LogicLibrary.Managers;
using DomainLibrary.Domains;
using DomainLibrary.Enumerations;
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
using DomainLibrary.Validation;
using DomainLibrary.Exceptions;

namespace WindowsForms.Forms.EmployeeManagerForms
{
    public partial class FormEditManagers : Form
    {
        private EmployeeManager employeeManager;
        private Manager oldManager;
        private ValidateCredentials validateEmplloyee;

        public FormEditManagers(Manager m,EmployeeManager em)
        {
            InitializeComponent();

            employeeManager = em;
            oldManager = m;

            comboBoxType.DataSource = Enum.GetValues(typeof(ManagerType));

            dateTimeBirthday.Format = DateTimePickerFormat.Custom;
            dateTimeBirthday.CustomFormat = "dd-MM-yyyy";
            validateEmplloyee = new ValidateCredentials();

            //ResetEverything();
            FillEverything();
        }

        public void FillEverything()
        {
            textBoxFirstName.Text = oldManager.FirstName.ToString().Trim();
            textBoxLastName.Text = oldManager.LastName.ToString().Trim();
            textBoxEmail.Text = oldManager.Email.ToString().Trim();
            
            textBoxPhone.Text = oldManager.PhoneNumber.ToString().Trim();
            comboBoxType.SelectedItem = oldManager.Type;
            dateTimeBirthday.Value = oldManager.DateOfBirth;

        }

        private void buttonUpdateManager_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = textBoxFirstName.Text.ToString();
                string lastName = textBoxLastName.Text.ToString();
                string email = textBoxEmail.Text.ToString();
                //string password = textBoxPassword.Text.ToString();
                string phone = textBoxPhone.Text.ToString();
                ManagerType type = (ManagerType)comboBoxType.SelectedIndex;
                DateTime dateOfBirth = (DateTime)dateTimeBirthday.Value;

                Manager manager = new Manager(firstName, lastName, email, oldManager.PasswordHash, oldManager.PasswordSalt, phone, type, dateOfBirth);

                string validationErrorMessage;
                if (validateEmplloyee.Validate(manager, out validationErrorMessage))
                {

                    employeeManager.UpdateEmployee(oldManager.EmployeeId, manager);

                    MessageBox.Show($"Manager {lastName} updated successfully!");

                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Validation Error:\n{validationErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        public void ResetEverything()
        {
            textBoxFirstName = null;
            textBoxLastName = null;
            textBoxEmail = null;
            //textBoxPassword = null;
            textBoxPhone = null;
            comboBoxType.SelectedIndex = 0;
        }

        private void FormEditManagers_Load(object sender, EventArgs e)
        {

        }
    }
}

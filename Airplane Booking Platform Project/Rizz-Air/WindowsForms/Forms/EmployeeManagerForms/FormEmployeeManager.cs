using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DALLibrary;
using LogicLibrary.Managers;
using DomainLibrary.Domains;
using DomainLibrary.Enumerations;
using DomainLibrary.Exceptions;

namespace WindowsForms.Forms.EmployeeManagerForms
{
    public partial class FormEmployeeManager : Form
    {
        private EmployeeManager employeeManager;
        private RentManager rentManager;
        private Employee loggedEmployee;
        public FormEmployeeManager(EmployeeManager em, RentManager rM, Employee loggedEmployee)
        {
            InitializeComponent();

            employeeManager = em;
            rentManager = rM;

            this.loggedEmployee = loggedEmployee;
            UpdateListBox();
        }

        private void buttonAddManager_Click(object sender, EventArgs e)
        {
            FormAddManager addManager = new FormAddManager(employeeManager);
            addManager.ShowDialog();
        }

        private void buttonPilot_Click(object sender, EventArgs e)
        {
            FormAdPilot addPilot = new FormAdPilot(employeeManager);
            addPilot.ShowDialog();
        }

        private void buttonDeleteEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxEmployees.SelectedIndex != -1)
                {
                    Employee employee = (Employee)listBoxEmployees.SelectedItem;
                    if (listBoxEmployees.SelectedItem is Manager)
                    {

                        employeeManager.DeleteEmployee(employee.EmployeeId);

                        MessageBox.Show($"Employee {employee.LastName.Trim()} {employee.FirstName.Trim()} deleted successfully!");
                    }

                    else if (listBoxEmployees.SelectedItem is Pilot)
                    {

                        List<Rent> rentList = rentManager.GetAllRentsByPilotID(employee.EmployeeId);

                        if (rentList.Count > 0)
                        {
                            DialogResult result = MessageBox.Show("Are you sure you want to delete this pilot? If you do all the rents with this Pilot will be deleted!", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {

                                rentManager.DeleteAllRentsForPilot(employee.EmployeeId);

                                employeeManager.DeleteEmployee(employee.EmployeeId);

                                MessageBox.Show($"Employee {employee.LastName.Trim()} {employee.FirstName.Trim()} deleted successfully!");
                            }
                        }

                        else
                        {
                            employeeManager.DeleteEmployee(employee.EmployeeId);

                            MessageBox.Show($"Employee {employee.LastName.Trim()} {employee.FirstName.Trim()} deleted successfully!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please choose an employee from the list to delete!");
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        private void buttonEditEmployee_Click(object sender, EventArgs e)
        {
            if (listBoxEmployees.SelectedIndex != -1)
            {
                Employee employee = (Employee)listBoxEmployees.SelectedItem;

                if (listBoxEmployees.SelectedItem is Manager)
                {
                    Manager manager = employeeManager.GetManagerById(employee.EmployeeId);
                    FormEditManagers editManagers = new FormEditManagers(manager, employeeManager);
                    editManagers.ShowDialog();

                }
                else
                {
                    Pilot pilot = employeeManager.GetPilotById(employee.EmployeeId);
                    FormEditPilot editPilot = new FormEditPilot(pilot, employeeManager, rentManager);
                    editPilot.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please choose an employee to edit!");
            }
        }

        private void buttonShowAllManagers_Click(object sender, EventArgs e)
        {
            listBoxEmployees.Items.Clear();
            foreach (Manager manager in employeeManager.GetAllManagers())
            {
                if (manager.EmployeeId != loggedEmployee.EmployeeId)
                {
                    listBoxEmployees.Items.Add(manager);
                }
            }
        }

        private void buttonShowPilots_Click(object sender, EventArgs e)
        {
            listBoxEmployees.Items.Clear();
            foreach (Pilot pilot in employeeManager.GetAllPilots())
            {
                listBoxEmployees.Items.Add(pilot);
            }
        }

        private void buttonShowAllEmployees_Click(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        public void UpdateListBox()
        {
            try
            {
                listBoxEmployees.Items.Clear();
                foreach (Employee employee in employeeManager.GetAllEmployees())
                {
                    if (employee.EmployeeId != loggedEmployee.EmployeeId)
                    {
                        listBoxEmployees.Items.Add(employee);
                    }
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }
    }
}

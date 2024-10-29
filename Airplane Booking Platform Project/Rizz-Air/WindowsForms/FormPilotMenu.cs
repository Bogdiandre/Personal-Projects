using DALLibrary;
using DomainLibrary.Domains;
using LogicLibrary.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainLibrary.Enumerations;
using DomainLibrary.Exceptions;

namespace WindowsForms
{
    public partial class FormPilotMenu : Form
    {
        private Pilot curPilot;
        private EmployeeManager employeeManager;
        private RentManager rentManager;
        private List<Rent> rentList = new List<Rent>();
        public FormPilotMenu(Pilot cP, EmployeeManager eM)
        {
            InitializeComponent();

            curPilot = cP;
            employeeManager = eM;
            rentManager = new RentManager(new RentDAL());
            rentList = rentManager.GetAllRentsByPilotID(curPilot.EmployeeId);
            PopulateListBox(rentList);
        }

        public void PopulateListBox(List<Rent> rentList)
        {
            try
            {
                listBoxRents.Items.Clear();
                foreach (Rent rent in rentList)
                {
                    listBoxRents.Items.Add(rent);
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }

        }
        private void buttonGetSalary_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show($"Your salary is {curPilot.GetSalary(rentManager.GetAllRentsByPilotID(curPilot.EmployeeId))}");
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            if(listBoxRents.SelectedIndex != -1)
            {
                Rent rent = (Rent)listBoxRents.SelectedItem;

                MessageBox.Show(rent.GetInfoForPilot());
            }
            else
            {
                MessageBox.Show("Please select a rent to see the details of!");
            }
            
        }
    }
}

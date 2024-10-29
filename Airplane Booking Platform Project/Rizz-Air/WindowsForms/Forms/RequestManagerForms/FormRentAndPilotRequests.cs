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

namespace WindowsForms.Forms.RequestManagerForms
{
    public partial class FormRentAndPilotRequests : Form
    {
        private RentManager rentManager;
        private PilotRequestManager pilotRequestManager;

        public FormRentAndPilotRequests(PilotRequestManager pilotRequestManager, RentManager rentManager)
        {
            InitializeComponent();
            this.pilotRequestManager = pilotRequestManager;
            this.rentManager = rentManager;

        }

        public void FillWithRents()
        {   try
            {
                listBoxRentsAndRequests.Items.Clear();
                foreach (Rent rent in rentManager.GetAllRents())
                {
                    listBoxRentsAndRequests.Items.Add(rent);
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        public void FillWithPilotRequests()
        {   try
            {
                listBoxRentsAndRequests.Items.Clear();
                foreach (PilotRequest pilotRequest in pilotRequestManager.GetAllPilotRequests())
                {
                    listBoxRentsAndRequests.Items.Add(pilotRequest);
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        private void buttonAllPilotRequests_Click(object sender, EventArgs e)
        {
            FillWithPilotRequests();
        }

        private void buttonAllRents_Click(object sender, EventArgs e)
        {
            
            FillWithRents();
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxRentsAndRequests.SelectedIndex != -1)
                {
                    if (listBoxRentsAndRequests.SelectedItem is Rent)
                    {
                        Rent rent = (Rent)listBoxRentsAndRequests.SelectedItem;
                        MessageBox.Show(rent.GetInfo());
                    }
                    if (listBoxRentsAndRequests.SelectedItem is PilotRequest)
                    {
                        PilotRequest pilotRequest = (PilotRequest)listBoxRentsAndRequests.SelectedItem;
                        MessageBox.Show(pilotRequest.GetInfo());

                    }
                }
                else
                {
                    MessageBox.Show("Please choose an item from the list!");
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxRentsAndRequests.SelectedIndex != -1)
                {
                    if (listBoxRentsAndRequests.SelectedItem is Rent)
                    {
                        Rent rent = (Rent)listBoxRentsAndRequests.SelectedItem;
                        rentManager.DeleteRentWithNormalRequest(rent.RentID);

                        MessageBox.Show("Rent deleted successfully with the request!");
                        FillWithRents();
                    }
                    if (listBoxRentsAndRequests.SelectedItem is PilotRequest)
                    {
                        PilotRequest pilotRequest = (PilotRequest)listBoxRentsAndRequests.SelectedItem;
                        pilotRequestManager.DeletePilotRequest(pilotRequest.PilotRequestID);

                        MessageBox.Show("Pilot Request deleted successfully!");
                        FillWithPilotRequests();
                    }
                }
                else
                {
                    MessageBox.Show("Please choose an item from the list to delete!");
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        private void buttonUnaccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxRentsAndRequests.SelectedIndex != -1)
                {
                    if (listBoxRentsAndRequests.SelectedItem is Rent)
                    {
                        Rent rent = (Rent)listBoxRentsAndRequests.SelectedItem;
                        rentManager.DeleteRentAndUnacceptNormalRequest(rent.RentID);

                        MessageBox.Show("Rent unaccepted successfully!");
                        FillWithRents();
                    }
                    else
                    {
                        MessageBox.Show("Please choose a rent from the list to unaccept!");
                    }
                }
                else
                {
                    MessageBox.Show("Please choose an item from the list!");
                }
            }
            catch (PilotRequestException x)
            {
                MessageBox.Show(x.Message);
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }
    }
}

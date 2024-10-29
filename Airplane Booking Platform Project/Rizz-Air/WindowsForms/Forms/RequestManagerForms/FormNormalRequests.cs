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
    public partial class FormNormalRequests : Form
    {
        private NormalRequestManager normalRequestManager;
        private RentManager rentManager;
        private EmployeeManager employeeManager;
        public FormNormalRequests(NormalRequestManager nRM, RentManager rM, EmployeeManager eM)
        {
            InitializeComponent();
            normalRequestManager = nRM;
            this.rentManager = rM;
            this.employeeManager = eM;

            UpdateListBox(normalRequestManager.FilterUnaccptedNormalRequest(normalRequestManager.GetAllNormalRequests()));
            textBoxEmail.Hide();
            buttonSearchByEmail.Hide();
            label1.Hide();
        }

        public void UpdateListBox(List<NormalRequest> normalRequests)
        {
            listBoxRequests.Items.Clear();
            foreach (NormalRequest normalRequest in normalRequests)
            {
                listBoxRequests.Items.Add(normalRequest);
            }
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            UpdateListBox(normalRequestManager.FilterUnaccptedNormalRequest(normalRequestManager.GetAllNormalRequests()));
        }

       

        private void buttonCheapestAssign_Click(object sender, EventArgs e)
        {
            if (listBoxRequests.SelectedIndex != -1)
            {
                NormalRequest normalRequest = (NormalRequest)listBoxRequests.SelectedItem;
                Pilot chepeastPilot = employeeManager.GetCheapestPilotForNormalRequest(normalRequest);//get the cheapest pilot for the request

                if (chepeastPilot != null)
                {
                    Rent rent = normalRequest.AproveRequest(chepeastPilot);//make the rent
                    normalRequestManager.UpdateNormalRequest(normalRequest.NormalRequestID, normalRequest);//To make it accepted in the database

                    rentManager.AddRent(rent);//add the rent in the database

                    MessageBox.Show("Request accepted with the chepeast pilot successfully!");
                }
                else
                {
                    DialogResult result = MessageBox.Show("There are no pilots available for this request! \n Do you want to delete it?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        normalRequestManager.DeleteNormalRequest(normalRequest.NormalRequestID);
                        MessageBox.Show("NormalRequest deleted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("The request has not been deleted!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose a request to cheaply assign!");
            }
        }

        private void buttonRandomAssign_Click(object sender, EventArgs e)
        {
            if (listBoxRequests.SelectedIndex != -1)
            {
                NormalRequest normalRequest = (NormalRequest)listBoxRequests.SelectedItem;
                Pilot randomPilot = employeeManager.GetRandomPilotForNormalRequest(normalRequest);//get a random pilot for the request

                if (randomPilot != null)
                {
                    Rent rent = normalRequest.AproveRequest(randomPilot);
                    normalRequestManager.UpdateNormalRequest(normalRequest.NormalRequestID, normalRequest);//To make it accepted in the database

                    rentManager.AddRent(rent);//add the rent in the database

                    MessageBox.Show("Request accepted with a random pilot successfully!");
                }
                else
                {
                    DialogResult result = MessageBox.Show("There are no pilots available for this request! \n Do you want to delete it?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        normalRequestManager.DeleteNormalRequest(normalRequest.NormalRequestID);
                        MessageBox.Show("NormalRequest deleted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("The request has not been deleted!");
                    }
                }

            }
            else
            {
                MessageBox.Show("Please choose a request to randomly assign!");
            }
        }

        private void buttonManualConfirm_Click(object sender, EventArgs e)
        {
            if (listBoxRequests.SelectedIndex != -1)
            {
                NormalRequest normalRequest = (NormalRequest)listBoxRequests.SelectedItem;

                if (employeeManager.GetAvailablePilotsForNormalRequest(normalRequest).Count > 0)
                {
                    FormManualRequest manualRequest = new FormManualRequest(normalRequest, normalRequestManager, employeeManager, rentManager);
                    manualRequest.ShowDialog();
                }
                else
                {
                    DialogResult result = MessageBox.Show("There are no pilots available for this request! \n Do you want to delete it?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        normalRequestManager.DeleteNormalRequest(normalRequest.NormalRequestID);
                        MessageBox.Show("NormalRequest deleted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("The request has not been deleted!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose a request to manually confirm!");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxRequests.SelectedIndex != -1)
            {
                NormalRequest normalRequest = (NormalRequest)listBoxRequests.SelectedItem;
                normalRequestManager.DeleteNormalRequest(normalRequest.NormalRequestID);
                MessageBox.Show("NormalRequest deleted successfully!");

                //Idk what to show in the listbox it depends on what the filter is, finish later
            }
            else
            {
                MessageBox.Show("Please choose a request to delete!");
            }
        }

        private void buttonShowDetails_Click(object sender, EventArgs e)
        {
            if (listBoxRequests.SelectedIndex != -1)
            {
                NormalRequest normalRequest = (NormalRequest)listBoxRequests.SelectedItem;

                MessageBox.Show(normalRequest.GetInfo());
            }
            else
            {
                MessageBox.Show("Please choose a request to see the details of!");
            }
        }

        private void buttonSearchByEmail_Click(object sender, EventArgs e)
        {
            try
            {


                if (textBoxEmail != null)
                {
                    string Email = textBoxEmail.Text.ToString();
                    List<NormalRequest> listNormalRequest = normalRequestManager.GetAllNormalRequestsByEmail(Email);
                    List<NormalRequest> normalRequests = normalRequestManager.FilterUnaccptedNormalRequest(listNormalRequest);
                    if (normalRequests != null)
                    {
                        UpdateListBox(normalRequests);

                        MessageBox.Show($"List updated for {Email}!");
                    }
                    else
                    {
                        MessageBox.Show("There are no Requsts for this email!");
                    }
                }
                else
                {
                    MessageBox.Show("Please insert an email in the text box!");
                }
            }

            catch (NormalRequestException x)
            {
                MessageBox.Show(x.Message);
            }
            catch (DatabaseException ex){ MessageBox.Show(ex.Message); }
        }
    }
}

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
using DomainLibrary.Domains;
using DomainLibrary.Enumerations;
using DomainLibrary.Validation;
using System.Security.Cryptography.X509Certificates;
using DomainLibrary.Exceptions;

namespace WindowsForms.Forms.DestinationManagerForms
{
    public partial class FormDestinationManager : Form
    {
        private DestinationManager destinationManager;
        private NormalRequestManager normalRequestManager;
        private ValidateCredentials destinationValidation;
        public FormDestinationManager(DestinationManager dm, NormalRequestManager normalRequestManager)
        {
            InitializeComponent();

            destinationManager = dm;
            this.normalRequestManager = normalRequestManager;
            destinationValidation = new ValidateCredentials();
            RefreshList();
            
        }

        private void buttonCreateDestination_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxName.Text.ToString();
                int distance = (int)numericUpDownDistance.Value;

                Destination destination = new Destination(name, distance);


                string validationErrorMessage;
                if (destinationValidation.Validate(destination, out validationErrorMessage))
                {

                    destinationManager.AddDestination(destination);
                    RefreshList();
                    MessageBox.Show($"Destination {destination.Name} created successfully!");
                }
                else
                {

                    MessageBox.Show($"Validation Error:\n{validationErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        private void buttonEditDestination_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxDestinaitons.SelectedIndex != -1)
                {
                    Destination oldDestintation = (Destination)listBoxDestinaitons.SelectedItem;
                    List<NormalRequest> normalRequests = normalRequestManager.GetAllNormalRequestsByDestinationID(oldDestintation.DestinationId);

                    string name = textBoxName.Text.ToString();
                    int distance = (int)numericUpDownDistance.Value;

                    Destination updatedDestination = new Destination(name, distance);


                    string validationErrorMessage;
                    if (destinationValidation.Validate(updatedDestination, out validationErrorMessage))
                    {

                        if (normalRequests.Count != 0 && distance != oldDestintation.Distance)
                        {
                            DialogResult result = MessageBox.Show("Are you sure you want to update this destination? If you do all the requests with this Destination will be deleted!", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                destinationManager.DeleteEverythingForDestination(oldDestintation.DestinationId);


                                destinationManager.UpdateDestination(oldDestintation.DestinationId, updatedDestination);

                                RefreshList();

                                MessageBox.Show($"Destination {updatedDestination.Name} updated successfully!");


                            }

                        }
                        else
                        {

                            destinationManager.UpdateDestination(oldDestintation.DestinationId, updatedDestination);

                            RefreshList();

                            MessageBox.Show($"Destination {updatedDestination.Name} updated successfully!");

                        }
                    }
                    else
                    {

                        MessageBox.Show($"Validation Error:\n{validationErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please choose a destination from the list to edit!");
                }

            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }


        }

        private void buttonDeleteDestination_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxDestinaitons.SelectedIndex != -1)
                {
                    Destination destination = (Destination)listBoxDestinaitons.SelectedItem;
                    List<NormalRequest> normalRequests = normalRequestManager.GetAllNormalRequestsByDestinationID(destination.DestinationId);

                    if (normalRequests.Count != 0)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this destination? If you do all the requests with this Destination will be deleted!", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            destinationManager.DeleteEverythingForDestination(destination.DestinationId);

                            destinationManager.DeleteDestination(destination.DestinationId);

                            RefreshList();

                            MessageBox.Show($"Destination {destination.Name} deleted successfully!");
                        }

                    }
                    else
                    {
                        destinationManager.DeleteDestination(destination.DestinationId);

                        RefreshList();

                        MessageBox.Show($"Destination {destination.Name} deleted successfully!");
                    }
                }
                else
                {
                    MessageBox.Show("Please choose a destination from the list to delete!");
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        public void RefreshList()
        {
            try
            {
                listBoxDestinaitons.Items.Clear();

                foreach (Destination destination in destinationManager.GetAllDestinations())
                {
                    listBoxDestinaitons.Items.Add(destination);
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        private void listBoxDestinaitons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDestinaitons.SelectedIndex != -1)
            {
                textBoxName.Clear();
                numericUpDownDistance.Value = 0;

                Destination destination = (Destination)listBoxDestinaitons.SelectedItem;
                
                textBoxName.Text = destination.Name;
                numericUpDownDistance.Value = destination.Distance;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxName.Text = null;
            numericUpDownDistance.Value = 0;
            listBoxDestinaitons.SelectedIndex = -1;
        }
    }
}

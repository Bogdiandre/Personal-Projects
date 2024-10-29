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
using System.Linq.Expressions;
using DomainLibrary.Exceptions;

namespace WindowsForms.Forms.AircraftManagerForms
{
    public partial class FormAircraftManager : Form
    {
        private AircraftManager aircraftManager;
        private NormalRequestManager normalRequestManager;
        private PilotRequestManager pilotRequestManager;
        public FormAircraftManager(AircraftManager am, NormalRequestManager nrM, PilotRequestManager prM)
        {
            InitializeComponent();

            aircraftManager = am;
            normalRequestManager = nrM;
            pilotRequestManager = prM;

            UpdateListBox(aircraftManager.GetAllAircrafts());
        }

        private void buttonAddHelicopter_Click(object sender, EventArgs e)
        {
            FormAddHelicopter addHelicopter = new FormAddHelicopter(aircraftManager);
            addHelicopter.ShowDialog();
        }

        private void buttonAddPrivateJet_Click(object sender, EventArgs e)
        {
            FormAddPrivateJet addPrivateJet = new FormAddPrivateJet(aircraftManager);
            addPrivateJet.ShowDialog();
        }

        private void buttonEditAircraft_Click(object sender, EventArgs e)
        {
           
            if (listBoxAircrafts.SelectedIndex != -1)
            {
                Aircraft aircraft = (Aircraft)listBoxAircrafts.SelectedItem; 

                if (listBoxAircrafts.SelectedItem is PrivateJet)
                {

                    PrivateJet privateJet = aircraftManager.GetPrivateJetByID(aircraft.AircraftId);
                    FormEditPrivateJet editPrivateJet = new FormEditPrivateJet(privateJet, aircraftManager, normalRequestManager);
                    editPrivateJet.ShowDialog();
                }

                else
                {
                    Helicopter helicopter = aircraftManager.GetHelicopterByID(aircraft.AircraftId);
                    FormEditHelicopter editHelicopter = new FormEditHelicopter(helicopter, aircraftManager, normalRequestManager, pilotRequestManager);
                    editHelicopter.ShowDialog();
                }

            }

            else
            {
                MessageBox.Show("Please selected an aircraft to edit!");
            }
            
        }

        private void buttonShowAllAircrafts_Click(object sender, EventArgs e)
        {
            UpdateListBox(aircraftManager.GetAllAircrafts());
        }

        private void buttonShowAllPrivateJets_Click(object sender, EventArgs e)
        {
            try 
            { 
                listBoxAircrafts.Items.Clear();
                foreach (PrivateJet privateJet in aircraftManager.GetAllPrivateJets())
                {
                    listBoxAircrafts.Items.Add(privateJet);
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        private void buttonShowAllHelicopters_Click(object sender, EventArgs e)
        {
            try 
            { 
                listBoxAircrafts.Items.Clear();
                foreach (Helicopter helicopter in aircraftManager.GetAllHelicopters())
                {
                    listBoxAircrafts.Items.Add(helicopter);
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        private void buttonDeleteAircraft_Click(object sender, EventArgs e)
        {
            if (listBoxAircrafts.SelectedIndex != -1)
            {
                Aircraft aicraft = (Aircraft)listBoxAircrafts.SelectedItem;
                if (listBoxAircrafts.SelectedItem is Helicopter)
                {
                    List<PilotRequest> pilotRequestList = pilotRequestManager.GetAllPilotRequestsByHelicopterID(aicraft.AircraftId);
                }

                   
                List<NormalRequest> normalRequests = normalRequestManager.GetNormalRequestsByAircraftID(aicraft.AircraftId);

                if (normalRequests.Count != 0)
                   {
                     DialogResult result = MessageBox.Show("Are you sure you want to delete this aircraft? If you do all the requests with this Aircraft will be deleted!", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                     if (result == DialogResult.Yes)
                     {
                            
                        aircraftManager.DeleteEverythingForAircraft(aicraft.AircraftId);

                        aircraftManager.DeleteAircraft(aicraft.AircraftId);

                        MessageBox.Show($"Aircraft {aicraft.Name} deleted successfully!");
                     }

                }
                else
                {

                    aircraftManager.DeleteAircraft(aicraft.AircraftId);

                    MessageBox.Show($"Aircraft {aicraft.Name} deleted successfully!");
                }

            }
            else
            {
                MessageBox.Show("Please select an aircraft to delete!");
            }
        }

        public void UpdateListBox(List<Aircraft> aircrafts)
        {
            try
            {

                listBoxAircrafts.Items.Clear();
                foreach (Aircraft aircraft in aircrafts)
                {
                    listBoxAircrafts.Items.Add(aircraft);
                }
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }
    }
}

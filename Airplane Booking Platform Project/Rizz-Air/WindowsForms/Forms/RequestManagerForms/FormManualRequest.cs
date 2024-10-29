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
    public partial class FormManualRequest : Form
    {
        private NormalRequest normalRequest;
        private EmployeeManager employeeManager;
        private RentManager rentManager;
        private NormalRequestManager normalRequestManager;
        public FormManualRequest(NormalRequest nR, NormalRequestManager nRM, EmployeeManager eM, RentManager rM)
        {
            InitializeComponent();
            normalRequest = nR;
            employeeManager = eM;
            rentManager = rM;
            normalRequestManager = nRM; 
            PopulateListBox();
            
        }

        public void PopulateListBox()
        {
            listBoxAvailablePilots.Items.Clear();
            List<Pilot> listPilots = employeeManager.GetAvailablePilotsForNormalRequest(normalRequest);
            foreach(Pilot pilot in listPilots)
            {
                listBoxAvailablePilots.Items.Add(pilot);
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            try
            {


                if (listBoxAvailablePilots.SelectedIndex != -1)
                {
                    Pilot pilot = (Pilot)listBoxAvailablePilots.SelectedItem;
                    Rent rent = normalRequest.AproveRequest(pilot);

                    normalRequestManager.UpdateNormalRequest(normalRequest.NormalRequestID, normalRequest);
                    rentManager.AddRent(rent);

                    MessageBox.Show("Request accepted with a pilot successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select a pilot from the list!");
                }
            }
            catch (PilotException x)
            {
                MessageBox.Show(x.Message);
            }
            catch (DatabaseException a) { MessageBox.Show("Something went wrong in the database section!"); }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

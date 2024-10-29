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

namespace WindowsForms.Forms.RequestManagerForms
{
    public partial class FormRequestManager : Form
    {
        private NormalRequestManager normalRequestManager;
        private RentManager rentManager;
        private EmployeeManager employeeManager;
        private PilotRequestManager pilotRequestManager;
        public FormRequestManager(NormalRequestManager nRM, RentManager rM, EmployeeManager eM, PilotRequestManager prM)
        {
            
            InitializeComponent();
            normalRequestManager = nRM;
            rentManager = rM;
            employeeManager = eM;
            pilotRequestManager = prM;
        }

        private void buttonRequests_Click(object sender, EventArgs e)
        {
            FormNormalRequests RequestForm = new FormNormalRequests(normalRequestManager, rentManager, employeeManager);
            RequestForm.ShowDialog();
        }

        private void buttonRents_Click(object sender, EventArgs e)
        {
            FormRentAndPilotRequests RentsAndPilotRequests = new FormRentAndPilotRequests(pilotRequestManager, rentManager);
            RentsAndPilotRequests.ShowDialog();
        }
    }
}

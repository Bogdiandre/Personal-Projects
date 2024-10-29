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

namespace WindowsForms
{
    public partial class FormMainMenu : Form
    {
        private Button currentButton;
        // private Random random;

        string c = "#364F6B";
        private int tempIndex;
        private Form activeForm;

        //Employee curEmployee = new Employee("Bogdan","Andreescu","02bogdi18@gmail.com","password123","07223423");
        private Manager curEmployee;
        private EmployeeManager employeeManager;
        private UserManager userManager;
        private AircraftManager aircraftManager;
        private DestinationManager destinationManager;
        private NormalRequestManager normalRequestManager;
        private RentManager rentManager;
        private PilotRequestManager pilotRequestManager;


        public FormMainMenu(Manager emp, EmployeeManager em)
        {
            InitializeComponent();

            curEmployee = emp;
            employeeManager = em;
            //employeeManager = new EmployeeManager(new EmployeeDAL());
            userManager = new UserManager(new UserDAL());
            aircraftManager = new AircraftManager(new AircraftDAL());
            destinationManager = new DestinationManager(new DestinationDAL());
            normalRequestManager = new NormalRequestManager(new NormalRequestDAL());
            rentManager = new RentManager(new RentDAL());
            pilotRequestManager = new PilotRequestManager(new PilotRequestDAL());

            label1.Text = curEmployee.LastName;

        }

        private void ActivateButton(object btnSender)
        {
            if(btnSender != null)
            {
                if(currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = ColorTranslator.FromHtml(c);
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if(previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPanel.Controls.Add(childForm);
            this.panelDesktopPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;


        }

        private void buttonEmployeeManager_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (curEmployee.Type == ManagerType.Employee || curEmployee.Type == ManagerType.Admin)

            {
                OpenChildForm(new Forms.EmployeeManagerForms.FormEmployeeManager(employeeManager, rentManager, curEmployee), sender);
            }
            else
            {
                OpenChildForm(new Forms.FormNotAllowed(), sender);
            }
        }

        private void buttonLocationManager_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (curEmployee.Type == ManagerType.Destination || curEmployee.Type == ManagerType.Admin)

            {
                OpenChildForm(new Forms.DestinationManagerForms.FormDestinationManager(destinationManager,normalRequestManager), sender);
            }
            else
            {
                OpenChildForm(new Forms.FormNotAllowed(), sender);
            }
            
        }

        private void buttonAircraftManager_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (curEmployee.Type == ManagerType.Aircraft || curEmployee.Type == ManagerType.Admin)

            {
                OpenChildForm(new Forms.AircraftManagerForms.FormAircraftManager(aircraftManager, normalRequestManager, pilotRequestManager), sender);
            }
            else
            {
                OpenChildForm(new Forms.FormNotAllowed(), sender);
            }
            
        }

        private void buttonUserManager_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (curEmployee.Type == ManagerType.User || curEmployee.Type == ManagerType.Admin)

            {
                OpenChildForm(new Forms.UserManagerForms.FormUsers(), sender);
            }
            else
            {
                OpenChildForm(new Forms.FormNotAllowed(), sender);
            }
            
        }

        private void buttonRequestManager_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (curEmployee.Type == ManagerType.Request || curEmployee.Type == ManagerType.Admin)

            {
                OpenChildForm(new Forms.RequestManagerForms.FormRequestManager(normalRequestManager, rentManager, employeeManager, pilotRequestManager), sender);
            }
            else
            {
                OpenChildForm(new Forms.FormNotAllowed(), sender);
            }
        }
    }
}

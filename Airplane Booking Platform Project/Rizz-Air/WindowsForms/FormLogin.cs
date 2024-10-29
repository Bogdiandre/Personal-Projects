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
using DALLibrary;
using DomainLibrary.Exceptions;

namespace WindowsForms
{
    public partial class FormLogin : Form
    {
        private EmployeeManager employeeManager;
        
        public FormLogin()
        {
            InitializeComponent();

            this.BackColor = ColorTranslator.FromHtml("#FCE593");

            employeeManager = new EmployeeManager(new EmployeeDAL());

            this.BackColor = ColorTranslator.FromHtml("#022350");
            textBoxPassword.PasswordChar = '*';
            /*
            Manager manager = new Manager("Bogdan", "Andreescu", "02bogdi18@gmail.com", "12345", "0734294486", DomainLibrary.Enumerations.ManagerType.Admin, DateTime.Now, 1);
            employeeManager.AddEmployee(manager);*/
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {


                string email = textBoxEmail.Text.ToString();
                string password = textBoxPassword.Text.ToString();


                if (employeeManager.ManagerLogin(email, password) != null)
                {

                    Manager curManager = employeeManager.ManagerLogin(email, password);
                    FormMainMenu mainMenu = new FormMainMenu(curManager, employeeManager);
                    this.Hide();
                    mainMenu.ShowDialog();

                }
                else if (employeeManager.PilotLogin(email, password) != null)
                {
                    Pilot curPilot = employeeManager.PilotLogin(email, password);
                    FormPilotMenu mainMenu = new FormPilotMenu(curPilot, employeeManager);
                    this.Hide();
                    mainMenu.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Login unsuccessfully done! Please try again.");
                }
            }
            catch (DatabaseException ex) { MessageBox.Show("You are not connected to the database!"); }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btShow_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.PasswordChar == '\0')
            {
                textBoxPassword.PasswordChar = '*';
            }
            else if (textBoxPassword.PasswordChar == '*')
            {
                textBoxPassword.PasswordChar = '\0';
            }
        }
    }
}

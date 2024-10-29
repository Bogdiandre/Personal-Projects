using DALLibrary;
using DomainLibrary.Domains;
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

namespace WindowsForms.Forms.UserManagerForms
{
    public partial class FormUsers : Form
    {
        private UserManager userManager { get; set; }
        public FormUsers()
        {
            InitializeComponent();

            userManager = new UserManager(new UserDAL());

            UpdateListBox();
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public void UpdateListBox()
        {
            listBoxUsers.Items.Clear();
            foreach (User usr in userManager.GetAllUsers())
            {
                listBoxUsers.Items.Add(usr);
            }
        }
        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedItem != null)
            {
                User usr = (User)listBoxUsers.SelectedItem;
                userManager.DeleteUser(usr.UserId);
                UpdateListBox();
               

                MessageBox.Show($"User {usr.FirstName} {usr.LastName} deleted successfully!");
            }

            else
            {
                MessageBox.Show($"No user selected or another error accured!");
            }
        }
    }
}

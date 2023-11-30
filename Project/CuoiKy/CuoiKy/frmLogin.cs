using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CuoiKy
{
    public partial class frmLogin : Form
    {


        public frmLogin()
        {
            InitializeComponent();
        }
        internal string username;
        internal string usertype;
        

        private void btnOK_Click(object sender, EventArgs e)
        {

            this.username = txtUserName.Text;
            string password = txtPassword.Text;
            
            
            if (ValidateAdminCredentials(username, password))
            {
                this.usertype = "admin";
                DialogResult = DialogResult.OK;
                
            }

           
            else if (ValidateCustomerCredentials(username, password))
            {
                DialogResult = DialogResult.OK;
                this.usertype = "client";
                

            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateCustomerCredentials(string username, string password)
        {
            using (var db = new dbTourismDataContext())
            {
                var customer = db.Customers.FirstOrDefault(a =>
                    a.Phone == username && a.Password == password);

                return customer != null;
            }
        }

        private bool ValidateAdminCredentials(string username, string password)
        {


            using (var db = new dbTourismDataContext())
            {
                var admin = db.Admins.FirstOrDefault(a =>
                    a.Username == username && a.Password == password);

                return admin != null;
            }

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void radAdmin_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

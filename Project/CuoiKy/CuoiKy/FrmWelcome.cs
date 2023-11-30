using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuoiKy
{
    public partial class FrmWelcome : Form
    {
        public FrmWelcome()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            using (frmLogin loginForm = new frmLogin())
            {
                
                   
                    if (loginForm.ShowDialog()==DialogResult.OK && loginForm.usertype == "admin")
                        {
                        // Show the main form if login is successful
                        Form1 mainForm = new Form1();
                        mainForm.Show();

                        // Close the login form
                        loginForm.Close();
                    }

            }

        }

        private void btnExplore_Click(object sender, EventArgs e)
        {
            frmCustomer customer = new frmCustomer();
            customer.ShowDialog();
            
        }

        private void FrmWelcome_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

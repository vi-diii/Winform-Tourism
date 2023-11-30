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
    public partial class frmRating : Form
    {
        public frmRating()
        {
            InitializeComponent();
        }
        public int destinationID;
        public int customerID;
        public int rad =0;
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void frmRating_Load(object sender, EventArgs e)
        {
            dbTourismDataContext db = new dbTourismDataContext();

            var des = db.Destinations.FirstOrDefault(x => x.DestinationID == this.destinationID);

            if (des != null)
            {
                groupBox1.Text = "Feel free to share your experience at: " + des.DestinationName;
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (this.customerID <= 0)
            {
                MessageBox.Show("Please back to homepage to login first!");
            }
            else
            {
                dbTourismDataContext db = new dbTourismDataContext();

                var cus = db.Customers.FirstOrDefault(x => x.CustomerID == this.customerID);
                
                if (rad1.Checked) rad = 1;
                else if (rad2.Checked) rad = 2;
                else if (rad3.Checked) rad = 3;
                else if (rad4.Checked) rad = 4;
                else if (rad5.Checked) rad = 5;
               if(rad == 0) { MessageBox.Show("Please rating your reviews!"); }
                else
                {
                    DialogResult result = MessageBox.Show("Thank for your review, this would be so useful for us!", "Hi "+ cus.FirstName+"!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        Review re = new Review();
                        re.CustomerID = this.customerID;
                        re.DestinationID = this.destinationID;
                        re.ReviewText = txtComment.Text;
                        re.ReviewDate = DateTime.Now;
                        re.Rating = rad;
                        db.Reviews.InsertOnSubmit(re);
                        db.SubmitChanges();

                    }

                }

                
            }
        }
    }
}

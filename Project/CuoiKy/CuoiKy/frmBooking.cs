using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuoiKy
{
    public partial class frmBooking : Form
    {
        public frmBooking()
        {
            InitializeComponent();
        }

        public int DestinationID;
        public string userAccount;
        public float paid;

        private void frmBooking_Load(object sender, EventArgs e)
        {

            dbTourismDataContext db = new dbTourismDataContext();

            var des = db.Destinations.FirstOrDefault(x => x.DestinationID == this.DestinationID);

            if (des != null)
            {
                groupBox1.Text = des.DestinationName;
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            
            if (this.userAccount == null)
            {
                MessageBox.Show("Please back to homepage to login first!");
            }
            else
            {
                if (numAldult.Value == 0 && numChild.Value == 0)
                {
                    MessageBox.Show("Please choose the type and number of people for your booking!");
                }
                else
                {
                    dbTourismDataContext db = new dbTourismDataContext();

                    var cus = db.Customers.FirstOrDefault(x => x.CustomerID == int.Parse(this.userAccount));
                    DialogResult result = MessageBox.Show("Are you sure you want to make the payment?", "Confirm Payment: " + cus.FirstName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Booking booking = new Booking();
                        booking.DestinationID = this.DestinationID;
                        booking.CustomerID = int.Parse(this.userAccount);
                        dbTourismDataContext context = new dbTourismDataContext();
                        Destination des = context.Destinations.FirstOrDefault(x => x.DestinationID == this.DestinationID);
                        booking.PartnerID = (int)des.PartnerID;
                        booking.NumberOfAdults = (int)numAldult.Value;
                        booking.NumberOfChildren = (int)numChild.Value;
                        booking.BookingDate = DateTime.Now;
                        booking.BookingTime = DateTime.Now.TimeOfDay;
                        booking.TotalPrice = paid;
                        booking.PaymentStatus = "Paid";
                        db.Bookings.InsertOnSubmit(booking);
                        db.SubmitChanges();
                        MessageBox.Show("Hi" +cus.FirstName +" "+ cus.LastName +" !" +" the ticket for the destination has been booked successfully", "Thank you! 💕");

                    }
                }
               
            }
            

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (numAldult.Value == 0 && numChild.Value == 0)
            {
                MessageBox.Show("Please choose the type and number of people for your booking!");
            }
            else
            {
                dbTourismDataContext db = new dbTourismDataContext();

                var des = db.Destinations.FirstOrDefault(x => x.DestinationID == this.DestinationID);

                if (des != null)
                {
                    var total = ((float)numAldult.Value * des.BasePrice) + ((float)numChild.Value * des.PriceForChildren);
                    // The (decimal) cast is used to ensure correct calculations with decimal values.

                    // Display the total somewhere (e.g., a label or textbox)
                    lblTotal.Text = $"Total Cost: {total:C}";
                    float discount = 0;
                    if (des.Discount != null) { discount = (((float)des.Discount / 100) * (float)total); }
                    else discount = 0;

                    //else { float discount = (((float)(des.Discount) / 100) * (float)total); }
                    // lblDiscount.Text = $"Reduced Cost: - {discount: $}";
                    lblDiscount.Text = "Reduced Cost: -" + "   $" + discount.ToString();
                    float pay = ((float)total - (float)discount);
                    paid = pay;
                    lblPayment.ForeColor = Color.DarkRed;
                    lblPayment.Text = "Paid Amount: " + "$" + pay.ToString();
                }
                else
                {
                    MessageBox.Show("Destination not found.");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

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
    public partial class ShowPlace : UserControl
    {
        public ShowPlace()
        {
            InitializeComponent();
        }

        // Properties to set destination information


        // Method to set values of controls based on properties
        // Properties to hold destination information
       
        private void ShowPlace_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void picAvatar_Click(object sender, EventArgs e)
        {

        }

        // Properties to set destination information
        public string DestinationName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string BasePrice { get; set; }
        public string PriceForChildren { get; set; }
        public string Discount { get; set; }
        public string Images { get; set; }
        //public string ID { get; set; }
        public string DestinationID { get; set; }
        public string CustomerID { get; set; }
        public string AverageRating { get; set; }
        public string TotalReview { get; set; }
        public string Type { get; set; }

        // Call this method to update UI elements with destination information
        public void UpdateUI()
        {
            lblTitle.Text = DestinationName;
            lblLocation.Text = Location;
            lblDescription.Text = Description;
            lblPrice.Text = $"Base Price: {BasePrice:C}$";
            lblChild.Text = $"Child Price: {PriceForChildren:C}$";
            lblDiscount.Text = $"Discount: {Discount}%";
            if (!string.IsNullOrEmpty(Images))
            {
                picAvatar.Image = Image.FromFile(Images);
            }
            else picAvatar.Image = null;
            // lbltest.Text = CustomerID;



            // Load images, assuming you have a PictureBox named pbImages
            //if (!string.IsNullOrEmpty(Images))
            //{
            //    pbImages.Image = Image.FromFile(Images);
            //}
            lblRating.Text = AverageRating;
            lblReview.Text = TotalReview;
            lblType.Text = Type;
            
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            using (frmBooking bookingForm = new frmBooking())
            {

                dbTourismDataContext db = new dbTourismDataContext();

                var destinationid = this.DestinationID;
                bookingForm.DestinationID = int.Parse(destinationid);
                bookingForm.userAccount = this.CustomerID;
                bookingForm.ShowDialog();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblType_Click(object sender, EventArgs e)
        {

        }

        private void btnBook_Click_1(object sender, EventArgs e)
        {
            using (frmBooking bookingForm = new frmBooking())
            {

                dbTourismDataContext db = new dbTourismDataContext();

                var destinationid = this.DestinationID;
                bookingForm.DestinationID = int.Parse(destinationid);
                bookingForm.userAccount = this.CustomerID;
                bookingForm.ShowDialog();


            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (frmRating rating = new frmRating())
            {
                dbTourismDataContext db = new dbTourismDataContext();

                var destinationid = this.DestinationID;
                rating.destinationID = int.Parse(destinationid);
                if (this.CustomerID == null) rating.customerID = 0;
                else
                {
                    rating.customerID = int.Parse(this.CustomerID);
                }


                rating.ShowDialog();
            }
        }
    }
}

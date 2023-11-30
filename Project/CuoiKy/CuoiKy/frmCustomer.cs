using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CuoiKy
{
    public partial class frmCustomer : Form
    {
        internal int DestinationID;
        
        
        public frmCustomer()
        {
            InitializeComponent();
        }
        private string phone;
        internal string CustomerID;

        private void frmCustomer_Load(object sender, EventArgs e)

        {
            loginToolStripMenuItem.Visible = true;
            usenameToolStripMenuItem.Visible = false;
            LoadAllDestination();  
        }
        List<Control> controlsToRemove = new List<Control>();
        private void LoadAllDestination()
        {
            using (var dbContext = new dbTourismDataContext())
            {
                //var destinations = dbContext.Destinations.ToList();
                dbTourismDataContext db = new dbTourismDataContext();

                var destinations = db.Destinations
                .Where(destination => destination.DestinationName.ToLower().Contains(txtKeyword.Text.ToLower())
                          || destination.TourismType.ToLower().Contains(txtKeyword.Text.ToLower()))
                            .ToList();


                foreach (var destination in destinations)
                {
                    //this.DestinationID = destination.DestinationID;
                    ShowPlace destinationControl = new ShowPlace();

                    // Set properties using data from the LINQ query

                    destinationControl.DestinationName = destination.DestinationName;
                    destinationControl.Location = destination.Location;
                    destinationControl.Description = destination.Description;
                    destinationControl.BasePrice = destination.BasePrice.ToString();
                    destinationControl.PriceForChildren = destination.PriceForChildren.ToString();
                    destinationControl.Discount = destination.Discount.ToString();

                    destinationControl.Images = destination.Images;
                    destinationControl.Type = destination.TourismType;
                    destinationControl.DestinationID = destination.DestinationID.ToString();
                    destinationControl.CustomerID = this.CustomerID;
                    var reviewsForDestination = dbContext.Reviews.Where(review => review.DestinationID == destination.DestinationID);

                    string averageRating = "0"; // Default value
                    string reviewCount = "0"; // Default value

                    if (reviewsForDestination.Any())
                    {
                        averageRating =  Math.Round(reviewsForDestination.Average(review => (float)review.Rating),2).ToString();
                        reviewCount = reviewsForDestination.Count().ToString();
                    }

                    destinationControl.AverageRating = "Rating: "+averageRating;
                    destinationControl.TotalReview = "Total Review: " + reviewCount;
                    


                    //destinationControl.Images = destination.Images;

                    // Update the UserControl's UI with the data
                    destinationControl.UpdateUI();

                    // Add the UserControl to the FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(destinationControl);
                    controlsToRemove.Add(destinationControl);
                }
            }


        }

        private void loadCustomerInformation()
        {  
            if(phone != null)
            {
                loginToolStripMenuItem.Visible = false;
                usenameToolStripMenuItem.Visible = true;
                dbTourismDataContext context = new dbTourismDataContext();
                
                Customer cus = context.Customers.FirstOrDefault(x => x.Phone == phone);
                this.CustomerID = cus.CustomerID.ToString();
                usenameToolStripMenuItem.Text = cus.FirstName;
               

            }
            else
            {
                MessageBox.Show("no account");
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmLogin loginForm = new frmLogin())
            {

                if (loginForm.ShowDialog() == DialogResult.OK && loginForm.usertype == "client")
                {
                    dbTourismDataContext Context = new dbTourismDataContext();
                    this.phone = loginForm.username;
                    deleteAllInformation();
                    loadCustomerInformation();
                    LoadAllDestination();


                }

            }
                        
                        //frmBooking frmBooking = new frmBooking();
                        //using (frmBooking frm = new frmBooking())
                        //{
                        //    frm.userAccount = this.CustomerID;
                        //}
                        //using (frmRating rating = new frmRating())
                        //{
                        //    rating.customerID = int.Parse(this.CustomerID);
                        //}
            
        }

        private void deleteAllInformation()
        {
            foreach (ShowPlace control in controlsToRemove)
            {
                flowLayoutPanel1.Controls.Remove(control);
                control.Dispose(); // Optional: Dispose of the control if needed
            }

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            deleteAllInformation();
            LoadAllDestination();
        }

        private void exitToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuoiKy
{
    
    public partial class frmAddDestination : Form
    {
        public frmAddDestination()
        {
            InitializeComponent();
        }
        
        dbTourismDataContext db;
        private void frmAddDestination_Load(object sender, EventArgs e)
        {
            db = new dbTourismDataContext();
            // load đơn vị tính cho combobox
            cboType.DataSource = db.Partners.ToList();
            cboType.DisplayMember = "PartnerName";
            cboType.ValueMember = "PartnerID";
            cboType.SelectedIndex = -1; // ko chọn giá trị nào là mặc định cả

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please, Enter Destination Name field!");
                return;
            }
            if (cboType.SelectedIndex < 0)
            {
                MessageBox.Show("You didn't choose any partner!");
                return;
            }
            if (string.IsNullOrEmpty(txtBasePrice.Text))
            {
                MessageBox.Show("Please enter the base price of the destination");
            }
            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                MessageBox.Show("Please fill out the location field!");
            }
            // if (!string.IsNullOrEmpty(mahang)) ; // nếu cập nhật
            try
            {
                var price = int.Parse(txtBasePrice.Text);
                if (price < 0)
                {
                    MessageBox.Show("This is an unvalid price");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("This is an unvalid Price");
                return;
            }

                try
                {
                    Destination des = new Destination();
                   
                    des.DestinationName = txtName.Text;
                    des.BasePrice =  int.Parse((string)txtBasePrice.Text);
                    des.PartnerID = int.Parse(cboType.SelectedValue.ToString());
                    if (int.TryParse(txtChildrenPrice.Text, out int childrenPrice))
                    {
                        des.PriceForChildren = childrenPrice;
                    }
                    else
                    {
                        des.PriceForChildren = null;
                    }
                    if (string.IsNullOrEmpty(txtDiscount.Text))
                    {
                        des.Discount = 0;
                    }
                    else if (int.TryParse(txtDiscount.Text, out int discount))
                    {
                        des.Discount = discount;
                    }
                    else
                    {
                         des.Discount = 0;
                    }
           
                     des.Location = txtLocation.Text;
                    des.TourismType = txtType.Text;
      
          
                    des.Description = txtDescription.Text;
                   des.Images = selectedImagePath;


                db.Destinations.InsertOnSubmit(des);
                    db.SubmitChanges();
                    MessageBox.Show("Successfully add one destination!");
                    //this.Dispose();
                }
                catch (Exception ex)
                {
                   MessageBox.Show("Add the destination get the error with: " + ex.Message);
                }

        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {

        }
        private string selectedImagePath;
        private void btnChoosePic_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Update the PictureBox image
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);

                // Store the file path of the selected image
                selectedImagePath = openFileDialog1.FileName;
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

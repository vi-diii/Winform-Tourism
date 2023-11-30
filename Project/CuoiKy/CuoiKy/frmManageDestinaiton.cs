using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuoiKy
{
    public partial class frmManageDestinaiton : Form
    {
        public frmManageDestinaiton()
        {
            InitializeComponent();
        }
        private Destination destination;
        private dbTourismDataContext context;
        private int selectedDestinationId = -1;
        Destination des = null;

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the CellClick event occurred on a valid row (not header or empty row)
            if (e.RowIndex >= 0 && e.RowIndex < dgvData.Rows.Count)
            {
                // Get the selected destination ID from the first cell in the clicked row
                if (int.TryParse(dgvData.Rows[e.RowIndex].Cells["DestinationID"].Value.ToString(), out int destinationId))
                {
                    selectedDestinationId = destinationId;
                }
                dbTourismDataContext context = new dbTourismDataContext();
          
                        
                Destination des = context.Destinations.FirstOrDefault(x => x.DestinationID == selectedDestinationId);
                //lblid.Text = selectedDestinationId.ToString();
                txtName.Text = des.DestinationName;
                txtType.Text = des.TourismType;
                txtLocation.Text = des.Location;
                txtDiscount.Text = des.Discount.ToString();
                txtDescription.Text = des.Description;
                txtBasePrice.Text = des.BasePrice.ToString();
                txtChildrenPrice.Text = des.PriceForChildren.ToString();
                var IdPartner = des.PartnerID;
                Partner pa = context.Partners.FirstOrDefault(x => x.PartnerID == IdPartner);
                cboType.DataSource = context.Partners.ToList();
                cboType.DisplayMember = "PartnerName";
                cboType.ValueMember = "PartnerID";
                cboType.SelectedValue = IdPartner; //
                lblContactPerson.Text = pa.Email.ToString();
                if (!string.IsNullOrEmpty(des.Images))
                {
                    pictureBox1.Image = Image.FromFile(des.Images);
                }
                else pictureBox1.Image = null;

            }

        }


        private void frmManageDestinaiton_Load(object sender, EventArgs e)
        {
            LoadDestination();
            // đặt lại tên cột hiển thị
            dgvData.Columns["DestinationID"].HeaderText = "ID";
            dgvData.Columns["DestinationName"].HeaderText = "Name";
            
            dgvData.Columns["Location"].HeaderText = "Location";
            dgvData.Columns["BasePrice"].HeaderText = "Base Price";
           
            // Căn chỉnh cột
            dgvData.Columns["DestinationName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvData.Columns["Location"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            // format kiểu hàng nghìn cho đơn giá bán
            dgvData.Columns["BasePrice"].DefaultCellStyle.Format = "N0";

            // set độ rộng của column
           dgvData.Columns["DestinationID"].Width = 30;
           dgvData.Columns["BasePrice"].Width = 90;
           dgvData.Columns["DestinationName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvData.CellFormatting += dgvData_CellFormatting;

        }
        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvData.Columns["BasePrice"].Index)
            {
                if (e.Value != null && float.TryParse(e.Value.ToString(), out float price))
                {
                    e.Value = price.ToString("C"); // "C" format specifier will display the value as currency with the appropriate currency symbol
                    e.FormattingApplied = true;
                }
            }
        }

        private void LoadDestination()
        {
            
            dbTourismDataContext db = new dbTourismDataContext();

            var rs = from d in db.Destinations //join p in db.Partners on d.PartnerID equals p.PartnerID
                    where d.DestinationName.ToLower().Contains(txtKeyWord.Text.ToLower()) //|| p.PartnerName.ToLower().Contains(txtKeyWord.Text.ToLower())
                     select new
                     { d.DestinationID,
                         d.DestinationName,
                         d.Location,
                         d.BasePrice 
                         };

                dgvData.DataSource = rs.ToList();
            
        }

        private void btnLoadmh_Click(object sender, EventArgs e)
        {
            LoadDestination();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddDestination frm = new frmAddDestination();
            frm.ShowDialog(this);
        }

        private void bntDelete_Click(object sender, EventArgs e)
        {


                DialogResult result = MessageBox.Show("Do you really want to delete it?", "Confirmation",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Check the user's choice
                if (result == DialogResult.Yes)
                {

                if (selectedDestinationId != -1)
                {
                    // Perform the deletion from the database using LINQ
                    using (dbTourismDataContext db = new dbTourismDataContext()) // Replace YourDataContext with your actual DataContext
                    {
                        // Find the destination with the selectedDestinationId
                        var destination = db.Destinations.FirstOrDefault(d => d.DestinationID == selectedDestinationId);

                        if (destination != null)
                        {
                            // Remove the destination from the database
                            db.Destinations.DeleteOnSubmit(destination);
                            db.SubmitChanges();
                            MessageBox.Show("Destination deleted successfully!");
                            LoadDestination();
                        }
                        else
                        {
                            MessageBox.Show("Destination not found!");
                        }
                    }

                    // Clear the selectedDestinationId after the deletion
                    selectedDestinationId = -1;
                }
                else
                {
                    MessageBox.Show("Please select a destination to delete.");
                }

            }     
                
        }

        static private string selectedImagePath = "";
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

        private void btnSave_Click(object sender, EventArgs e)
        {
         
            try
            {
            if (dgvData.SelectedRows.Count > 0)
               {
                dbTourismDataContext db = new dbTourismDataContext();
                // dgvData.DataSource = db.Destinations;


                // Find the destination in the destinationList collection based on the destinationId
                
                    var des= db.Destinations.FirstOrDefault(d => d.DestinationID == selectedDestinationId);
                if (des != null)
                {

                    des.DestinationID = selectedDestinationId;
                    
                    des.DestinationName = txtName.Text;
                    des.TourismType = txtType.Text;
                    des.Location = txtLocation.Text;

                    decimal discount;
                    if (decimal.TryParse(txtDiscount.Text, out discount))
                    {
                        des.Discount = discount;
                    }
                    else
                    {
                        des.Discount = null;
                    }

                    des.Description = txtDescription.Text;

                    float basePrice;
                    if (float.TryParse(txtBasePrice.Text, out basePrice))
                    {
                        des.BasePrice = basePrice;
                    }
                    else
                    {
                        MessageBox.Show("Fail to save the base price!");
                    }

                    float childrenPrice;
                    if (float.TryParse(txtChildrenPrice.Text, out childrenPrice))
                    {
                        des.PriceForChildren = childrenPrice;
                    }
                    else
                    {
                        des.PriceForChildren = null;
                    }

                    // Update the PartnerID based on the selected value in the ComboBox
                    if (cboType.SelectedValue != null && int.TryParse(cboType.SelectedValue.ToString(), out int partnerId))
                    {
                        des.PartnerID = partnerId;
                    }
                    else
                    {
                        MessageBox.Show("Fail to save the partner!");
                    }

                    des.Images = selectedImagePath;

                    // Save changes to the database
                    db.SubmitChanges();
                                  
                }
                LoadDestination();
                // Refresh the DataGridView to reflect the updated information
                dgvData.Refresh();

            }
                else
                {
                    MessageBox.Show("No destination selected in the DataGridView, Please, choose one!");
                }

           }      
           catch(Exception ex)
            {
                MessageBox.Show("Saving change get the error: " +ex.Message);
            }

           
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    
}

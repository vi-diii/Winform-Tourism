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
using System.Xml.Linq;

namespace CuoiKy
{
    public partial class frmManagePartner : Form
    {
        public frmManagePartner()
        {
            InitializeComponent();
        }
        
        dbTourismDataContext db;
        Partner pa = null;
        private string partnername;
       
        
        private int selectedPartnerId = -1;
        

        private void frmManagePartner_Load(object sender, EventArgs e)
        {
            LoadPartners();
            dgvData.Columns["PartnerID"].HeaderText = "ID";
            dgvData.Columns["PartnerName"].HeaderText = "Name";

            dgvData.Columns["ContactPerson"].HeaderText = "Contact Person";
            dgvData.Columns["Email"].HeaderText = "Email";
            dgvData.Columns["Phone"].HeaderText = "Phone";


            // Căn chỉnh cột
            dgvData.Columns["ContactPerson"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvData.Columns["Email"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvData.Columns["PartnerID"].Width = 35;


        }

        private void LoadPartners()
        {
            dbTourismDataContext db = new dbTourismDataContext();

            var rs = from d in db.Partners 
            where d.PartnerName.ToLower().Contains(txtKeyWord.Text.ToLower()) //|| p.PartnerName.ToLower().Contains(txtKeyWord.Text.ToLower())
                     select new
                     {
                         d.PartnerID,
                         d.PartnerName,
                         d.ContactPerson,
                         d.Email,
                         d.Phone,
                     };

            dgvData.DataSource = rs.ToList();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the CellClick event occurred on a valid row (not header or empty row)
            if (e.RowIndex >= 0 && e.RowIndex < dgvData.Rows.Count)

            {
                
                // Get the selected partner ID from the first cell in the clicked row
                if (int.TryParse(dgvData.Rows[e.RowIndex].Cells["PartnerID"].Value.ToString(), out int partnerId))
                {
                    dbTourismDataContext context = new dbTourismDataContext();
                    List<Destination> DestinationList = context.Destinations.Where(x => x.PartnerID == partnerId)
                        .ToList();
                    this.selectedPartnerId = partnerId;

                    // Clear the ListBox and add the destinations belonging to the selected partner
                    lstListDestination.Items.Clear();
                    foreach (var list in DestinationList)
                    {
                        lstListDestination.Items.Add(list.DestinationName);
                    }
                    Partner pa = context.Partners.FirstOrDefault(x => x.PartnerID == partnerId);
                    txtName.Text = pa.PartnerName;
                    txtEmail.Text = pa.Email;
                    txtPhone.Text = pa.Phone;
                    txtPerson.Text = pa.ContactPerson;
                   
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                dbTourismDataContext db = new dbTourismDataContext();
                if (dgvData.SelectedRows.Count > 0)
                {
                   
                    // dgvData.DataSource = db.Destinations;


                    // Find the destination in the destinationList collection based on the destinationId

                    var pa = db.Partners.FirstOrDefault(d => d.PartnerID == selectedPartnerId);

                    if (string.IsNullOrEmpty(txtName.Text))
                    {
                        MessageBox.Show("Please fill out partner name ");
                        return;
                    }
                    if (string.IsNullOrEmpty(txtEmail.Text))
                    {
                        MessageBox.Show("Please fill out partner email ");
                    }
                    if (string.IsNullOrEmpty(txtPerson.Text))
                    {
                        MessageBox.Show("Please fill out partner person contact ");
                    }
                    if (string.IsNullOrEmpty(txtPhone.Text))
                    {
                        MessageBox.Show("Please fill out phone number");
                    }
                    if (this.selectedPartnerId > -1)
                    {
                        try
                        {
                            pa.PartnerName = txtName.Text;
                            pa.Email = txtEmail.Text;
                            pa.ContactPerson = txtPerson.Text;
                            pa.Phone = txtPhone.Text;

                            db.SubmitChanges();
                            MessageBox.Show("Update the partner sucessfully!");
                            

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Get update error: " + ex.Message);
                        }


                    }
                   

                }
                else
                {

                    try
                    {
                        Partner pa = new Partner();
                        pa.PartnerName = txtName.Text;
                        pa.Email = txtEmail.Text;
                        pa.ContactPerson = txtPerson.Text;
                        pa.Phone = txtPhone.Text;
                        db.Partners.InsertOnSubmit(pa);
                        db.SubmitChanges();

                        MessageBox.Show("Sucessfully add one Partner");
                        

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("The create get error with " + ex.Message);
                    }

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Saving get error with " + ex.Message);

            }
            LoadPartners();



        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPerson.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            dgvData.ClearSelection();
            // if(pa == null) // thêm mới một mặt hàng


        }

        private void btnLoadmh_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void bntDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete it?", "Confirmation",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's choice
            try
            {
                if (result == DialogResult.Yes)
                {

                    if (selectedPartnerId != -1)
                    {
                        // Perform the deletion from the database using LINQ
                        using (dbTourismDataContext db = new dbTourismDataContext()) // Replace YourDataContext with your actual DataContext
                        {
                            // Find the destination with the selectedDestinationId
                            var partener = db.Partners.FirstOrDefault(d => d.PartnerID == selectedPartnerId);

                            if (partener != null)
                            {
                                // Remove the destination from the database
                                db.Partners.DeleteOnSubmit(partener);
                                db.SubmitChanges();
                                MessageBox.Show("Partner deleted successfully!");
                                LoadPartners();
                            }
                            else
                            {
                                MessageBox.Show("Partner not found!");
                            }
                            LoadPartners();
                        }

                        // Clear the selectedDestinationId after the deletion
                        selectedPartnerId = -1;
                    }
                    else
                    {
                        MessageBox.Show("Please select a destination to delete.");
                    }

                }
                LoadPartners();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Got error because of Partner have Destionation, so you can't delete it: ");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPartners();
        }
    }
}

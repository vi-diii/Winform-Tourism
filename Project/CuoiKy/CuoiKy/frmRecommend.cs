using MLsentiment;
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
    public partial class frmRecommend : Form
    {
        public frmRecommend()
        {
            InitializeComponent();
        }
        private dbTourismDataContext context;

        private void frmRecommend_Load(object sender, EventArgs e)
        {
            context = new dbTourismDataContext(); // Initialize your LINQ to SQL context
            LoadCustomerNames();

        }

        private void LoadCustomerNames()
        {
            var customerNames = from customer in context.Customers
                                select customer.FirstName;

            cboCustomer.DataSource = customerNames.ToList();
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstDestination.Items.Clear();

            string selectedCustomerName = cboCustomer.SelectedItem.ToString();
            int customerId = GetCustomerIdByName(selectedCustomerName);
            this.customerid = customerId;

            if (customerId != -1)
            {
                // Load recommended destinations based on customer ID
                LoadRecommendedDestinations(customerId);
            }
            else
            {
                MessageBox.Show("Selected customer not found.");
            }
        }

        private void LoadRecommendedDestinations(int customerId)
        {
            // Assuming you have a method to retrieve customer data
            using (dbTourismDataContext context = new dbTourismDataContext())
            {
                var recommendations = (from re in context.Recommends
                                       where re.CustomerID == customerId
                                       select re).ToList();

                foreach (var data in recommendations)
                {
                    MLRecommendation.ModelInput input = new MLRecommendation.ModelInput
                    {
                        CustomerID = (float)data.CustomerID,
                        DestinationID = (float)data.DestinationID,
                        RoundedValue = (float)data.Score,
                        Key = data.key
                    };

                    try
                    {
                        // Predict recommendations using the ML.NET model
                        MLRecommendation.ModelOutput recommendationPrediction = MLRecommendation.Predict(input);

                        if (recommendationPrediction.Score > 0.5)
                        {
                            lstDestination.Items.Add(data.DestinationID);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error predicting recommendations: {ex.Message}");
                        // Handle the exception as needed
                    }
                }
            }
        }
        public int customerid;


        private int GetCustomerIdByName(string selectedCustomerName)
        {

            var customer = context.Customers.FirstOrDefault(c => c.FirstName == selectedCustomerName);

            return customer != null ? customer.CustomerID : -1;
        }
    }
}

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
    public partial class frmSentimentAnalysis : Form
    {
        public frmSentimentAnalysis()
        {
            InitializeComponent();
        }
        string folder = "Models";
        dbTourismDataContext context = new dbTourismDataContext();
        private void frmSentimentAnalysis_Load(object sender, EventArgs e)
        {
            dbTourismDataContext context = new dbTourismDataContext();
            var destination = (from des in context.Destinations
                               join pa in context.Partners on des.PartnerID equals pa.PartnerID

                               select new
                               {
                                   des.DestinationID,
                                   des.DestinationName,
                                   pa.PartnerName
                               }).ToList();

            destination.ForEach(x =>
            {
                ListViewItem lvi = new ListViewItem(x.DestinationID + "");
                lvi.SubItems.Add(x.DestinationName);
                lvi.SubItems.Add(x.PartnerName);
                lvi.Tag = x.DestinationID;


                listView1.Items.Add(lvi);

            });
        }
        public class ReviewData
        {
            public int CustomerID { get; set; }
            public int DestinationID { get; set; }
            public double Rating { get; set; }
            public string ReviewText { get; set; }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView3.Items.Clear();
                int destinationId = (int)listView1.SelectedItems[0].Tag;
                var reviews = (from re in context.Reviews
                               join cus in context.Customers on re.CustomerID equals cus.CustomerID
                               where re.DestinationID == destinationId
                               select new
                               {
                                   re.Rating,
                                   re.ReviewText,
                                   cus.FirstName,
                                   cus.LastName,
                                   re.ReviewID,
                                   re.DestinationID,
                                   re.CustomerID,
                               })
                    .ToList();

                foreach (var review in reviews)
                {
                    ListViewItem lvi = new ListViewItem(review.LastName + " " + review.FirstName);
                    lvi.SubItems.Add($"{review.Rating}");
                    lvi.SubItems.Add(review.ReviewText.ToString());

                    // Prepare input for sentiment prediction
                    MLSentiment.ModelInput input = new MLSentiment.ModelInput
                    {
                        Col0 = review.ReviewText.ToString(),
                        Col1 = (float)review.Rating
                    };

                    //try
                    //{
                    // Predict sentiment using the ML.NET model
                    MLSentiment.ModelOutput sentimentPrediction = MLSentiment.Predict(input);

                    double percentPositive = Math.Round(sentimentPrediction.Score[1] * 100, 2);
                    double percentNegative = Math.Round(sentimentPrediction.Score[0] * 100, 2);
                    var score = (percentNegative + review.Rating * 10 * 2) / 2;



                    if (sentimentPrediction.PredictedLabel == 1)
                    {
                        lvi.SubItems.Add(percentNegative.ToString());
                        lvi.ForeColor = Color.Green;
                        picGood.Visible = true;
                    }
                    else
                    {
                        lvi.SubItems.Add(percentNegative.ToString());
                        lvi.ForeColor = Color.Red;
                        picBad.Visible = true;
                    }
                    // lvi.SubItems.Add(percentPositive + "%");
                    lvi.SubItems.Add(score + "%");
                    // Insert the data into the database
                    using (var dbContext = new dbTourismDataContext()) // Replace with your actual DbContext class
                    {
                        var newReviewData = new Recommend
                        {
                            CustomerID = review.CustomerID,
                            DestinationID = review.DestinationID,
                            Score = score
                        };

                        dbContext.Recommends.InsertOnSubmit(newReviewData);
                        dbContext.SubmitChanges();
                    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show($"Error predicting sentiment: {ex.Message}");
                    //    // Handle the exception as needed
                    //}

                    listView3.Items.Add(lvi);
                }

            }

        }
    }
}

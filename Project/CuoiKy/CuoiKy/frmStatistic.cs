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
    public partial class frmStatistic : Form
    {
        public frmStatistic()
        {
            InitializeComponent();
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(radDestination.Checked)
            {
                loadDestinationData();
                dgvData.Columns["DestinationName"].HeaderText = "Destination Name";
                dgvData.Columns["BasePrice"].HeaderText = "Base Price";

                dgvData.Columns["NumberOfBookings"].HeaderText = "Bookings Count";
                dgvData.Columns["TotalAmount"].HeaderText = "Total Amount";

                // Căn chỉnh cột
                dgvData.Columns["BasePrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvData.Columns["NumberOfBookings"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                // format kiểu hàng nghìn cho đơn giá bán
                dgvData.Columns["NumberOfBookings"].Width = 90;
                dgvData.Columns["DestinationName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvData.Columns["DestinationName"].Width = 200;
                dgvData.CellFormatting += dgvData_CellFormatting;


            }
            if (radPartner.Checked)
            {
                LoadPartnerData();
                dgvData.Columns["PartnerName"].HeaderText = "Partner Name";

                dgvData.Columns["NumberOfBookings"].HeaderText = "Bookings Count";
                dgvData.Columns["TotalAmount"].HeaderText = "Total Amount";

                // Căn chỉnh cột
      
                dgvData.Columns["NumberOfBookings"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // format kiểu hàng nghìn cho đơn giá bán
                dgvData.Columns["NumberOfBookings"].Width = 100;
                dgvData.Columns["PartnerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvData.Columns["PartnerName"].Width = 150;
                dgvData.CellFormatting += dgvData_CellFormatting;
            }
            if (radSale.Checked)
            {
                LoadByBookings();
                dgvData.CellFormatting += dgvData_CellFormatting;
            }
            

        }

        private void LoadByBookings()
        {
            dbTourismDataContext db = new dbTourismDataContext();
            var startDateTime = dateTimePicker1.Value.Date;
            var endDateTime =  dateTimePicker2.Value.Date;

            var statistics = (from booking in db.Bookings
                              join customer in db.Customers on booking.CustomerID equals customer.CustomerID
                              where booking.BookingDate >= startDateTime && booking.BookingDate < endDateTime 
                              && booking.PaymentStatus =="Paid"
                              select new
                              {
                                  booking.BookingID,
                                  booking.CustomerID,
                                  customer.FirstName,
                                  booking.BookingDate,
                                  booking.BookingTime,
    
                                 TotalAmount = booking.TotalPrice
                                  
                              }).ToList();

            var paidStatistics = statistics.ToList();

            dgvData.DataSource = paidStatistics;
            int distinctCustomerCount = statistics.Select(s => s.CustomerID).Distinct().Count();
            lbltitle.Text = $"Total Distinct Customers: {distinctCustomerCount}";

            // Calculate total amount and display it in the Label
            var totalAmount = paidStatistics.Sum(s => s.TotalAmount);
            var bookingCount = dgvData.Rows.Count;
            lblTotal.Text = $"Booking Count: {bookingCount}";
            lblReceive.Text = $"Total Amount: {totalAmount:C}";
            
        }

        private void LoadPartnerData()
        {
            dbTourismDataContext db = new dbTourismDataContext();
            var startDateTime = dateTimePicker1.Value.Date;
            var endDateTime = dateTimePicker2.Value.Date;

            var statistics = (from partner in db.Partners
                              join booking in db.Bookings on partner.PartnerID equals booking.PartnerID
                              where booking.BookingDate >= startDateTime && booking.BookingDate < endDateTime
                              && booking.PaymentStatus == "Paid"
                              group booking by new
                              {
                                  partner.PartnerID,
                                  partner.PartnerName
                              } into grouped
                              select new
                              {
                                  PartnerName = grouped.Key.PartnerName,
                                  NumberOfBookings = grouped.Count(),
                                  TotalAmount = grouped.Sum(b =>  b.TotalPrice)
                              }).ToList();

            int numberOfPartners = statistics.Count;
            lbltitle.Text = $"Total Partners: {numberOfPartners}";
            var totalAmount = statistics.Sum(s => s.TotalAmount);
            var BookingCount = statistics.Sum(s => s.NumberOfBookings);
            lblTotal.Text = $"Booking Count: {BookingCount}";
            lblReceive.Text = $"Total Amount: {totalAmount:C}";

            dgvData.DataSource = statistics;
        }

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvData.Columns["TotalAmount"].Index)
            {
                if (e.Value != null && float.TryParse(e.Value.ToString(), out float price))
                {
                    e.Value = price.ToString("C"); // "C" format specifier will display the value as currency with the appropriate currency symbol
                    e.FormattingApplied = true;
                }
            }
        }

        private void loadDestinationData()

        {
            dbTourismDataContext db = new dbTourismDataContext();
            var startDateTime = dateTimePicker1.Value.Date;
            var endDateTime =dateTimePicker2.Value.Date;

            var statistics = (from destination in db.Destinations
                              join booking in db.Bookings on destination.DestinationID equals booking.DestinationID
                              where booking.BookingDate >= startDateTime && booking.BookingDate < endDateTime
                              && booking.PaymentStatus == "Paid"
                              group booking by new
                              {
                                  destination.DestinationID,
                                  destination.DestinationName,
                                  destination.BasePrice
                              } into grouped
                              select new
                              {
                                  DestinationName = grouped.Key.DestinationName,
                                  BasePrice = grouped.Key.BasePrice,
                                  NumberOfBookings = grouped.Count(),
                                  TotalAmount = grouped.Sum(b =>  b.TotalPrice)
                              }).ToList();
            int numberOfDestination = statistics.Count;
            lbltitle.Text = $"Total Destination: {numberOfDestination}";


            dgvData.DataSource = statistics;
            var totalAmount = statistics.Sum(s => s.TotalAmount);
            var BookingCount = statistics.Sum(s => s.NumberOfBookings);
            lblTotal.Text = $"Booking Count: {BookingCount}";
            lblReceive.Text = $"Total Amount: {totalAmount:C}";
        }

        private void frmStatistic_Load(object sender, EventArgs e)
        {

        }

        private void radPartner_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

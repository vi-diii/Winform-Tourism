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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void managementToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private List<Control> controls = new List<Control>();

        public SplitContainer splitContainer { get; set; }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            frmStatistic f = new frmStatistic();
            this.splitContainer1.Panel2.Controls.Clear();

            f.TopLevel = false;
            f.AutoScroll = true;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            this.Text = f.Text;
            this.splitContainer1.Panel2.Controls.Add(f);
            f.Show();
        }

        private void destinationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDestinaiton f = new frmManageDestinaiton();
            this.splitContainer1.Panel2.Controls.Clear();

            f.TopLevel = false;
            f.AutoScroll = true;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            this.Text = f.Text;
            this.splitContainer1.Panel2.Controls.Add(f);
            f.Show();
        }

        private void partnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePartner f = new frmManagePartner();
            this.splitContainer1.Panel2.Controls.Clear();

            f.TopLevel = false;
            f.AutoScroll = true;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            this.Text = f.Text;
            this.splitContainer1.Panel2.Controls.Add(f);
            f.Show();

        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStatistic f = new frmStatistic();
            this.splitContainer1.Panel2.Controls.Clear();

            f.TopLevel = false;
            f.AutoScroll = true;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            this.Text = f.Text;
            this.splitContainer1.Panel2.Controls.Add(f);
            f.Show();

        }

        private void sentimentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSentimentAnalysis f = new frmSentimentAnalysis();
            this.splitContainer1.Panel2.Controls.Clear();

            f.TopLevel = false;
            f.AutoScroll = true;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            this.Text = f.Text;
            this.splitContainer1.Panel2.Controls.Add(f);
            f.Show();

        }

        private void recommendationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRecommend f = new frmRecommend();
            this.splitContainer1.Panel2.Controls.Clear();

            f.TopLevel = false;
            f.AutoScroll = true;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            this.Text = f.Text;
            this.splitContainer1.Panel2.Controls.Add(f);
            f.Show();

        }
    }
}

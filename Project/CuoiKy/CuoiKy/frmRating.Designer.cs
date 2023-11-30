namespace CuoiKy
{
    partial class frmRating
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRating));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.rad4 = new System.Windows.Forms.RadioButton();
            this.rad3 = new System.Windows.Forms.RadioButton();
            this.rad5 = new System.Windows.Forms.RadioButton();
            this.rad2 = new System.Windows.Forms.RadioButton();
            this.rad1 = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox2.BackgroundImage")));
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.rad4);
            this.groupBox2.Controls.Add(this.rad3);
            this.groupBox2.Controls.Add(this.rad5);
            this.groupBox2.Controls.Add(this.rad2);
            this.groupBox2.Controls.Add(this.rad1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(796, 386);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rate your feel!";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.DarkOrange;
            this.btnSend.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(724, 338);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(64, 47);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "✔";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.txtComment);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 225);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(785, 106);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comment:";
            // 
            // txtComment
            // 
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtComment.Location = new System.Drawing.Point(4, 26);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(777, 76);
            this.txtComment.TabIndex = 0;
            // 
            // rad4
            // 
            this.rad4.AutoSize = true;
            this.rad4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rad4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad4.Location = new System.Drawing.Point(151, 140);
            this.rad4.Margin = new System.Windows.Forms.Padding(4);
            this.rad4.Name = "rad4";
            this.rad4.Size = new System.Drawing.Size(113, 24);
            this.rad4.TabIndex = 11;
            this.rad4.TabStop = true;
            this.rad4.Text = "4.Satisfied ";
            this.rad4.UseVisualStyleBackColor = false;
            // 
            // rad3
            // 
            this.rad3.AutoSize = true;
            this.rad3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rad3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad3.Location = new System.Drawing.Point(151, 108);
            this.rad3.Margin = new System.Windows.Forms.Padding(4);
            this.rad3.Name = "rad3";
            this.rad3.Size = new System.Drawing.Size(97, 24);
            this.rad3.TabIndex = 10;
            this.rad3.TabStop = true;
            this.rad3.Text = "3.Neutral";
            this.rad3.UseVisualStyleBackColor = false;
            // 
            // rad5
            // 
            this.rad5.AutoSize = true;
            this.rad5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rad5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad5.Location = new System.Drawing.Point(151, 172);
            this.rad5.Margin = new System.Windows.Forms.Padding(4);
            this.rad5.Name = "rad5";
            this.rad5.Size = new System.Drawing.Size(147, 24);
            this.rad5.TabIndex = 9;
            this.rad5.TabStop = true;
            this.rad5.Text = "5.Very Satisfied";
            this.rad5.UseVisualStyleBackColor = false;
            this.rad5.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged_1);
            // 
            // rad2
            // 
            this.rad2.AutoSize = true;
            this.rad2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rad2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad2.Location = new System.Drawing.Point(151, 76);
            this.rad2.Margin = new System.Windows.Forms.Padding(4);
            this.rad2.Name = "rad2";
            this.rad2.Size = new System.Drawing.Size(137, 24);
            this.rad2.TabIndex = 8;
            this.rad2.TabStop = true;
            this.rad2.Text = "2. Dissatisfied";
            this.rad2.UseVisualStyleBackColor = false;
            // 
            // rad1
            // 
            this.rad1.AutoSize = true;
            this.rad1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rad1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad1.Location = new System.Drawing.Point(151, 44);
            this.rad1.Margin = new System.Windows.Forms.Padding(4);
            this.rad1.Name = "rad1";
            this.rad1.Size = new System.Drawing.Size(171, 24);
            this.rad1.TabIndex = 7;
            this.rad1.TabStop = true;
            this.rad1.Text = "1.Very Dissatisfied";
            this.rad1.UseVisualStyleBackColor = false;
            // 
            // frmRating
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(820, 410);
            this.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmRating";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rating";
            this.Load += new System.EventHandler(this.frmRating_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.RadioButton rad4;
        private System.Windows.Forms.RadioButton rad3;
        private System.Windows.Forms.RadioButton rad5;
        private System.Windows.Forms.RadioButton rad2;
        private System.Windows.Forms.RadioButton rad1;
    }
}
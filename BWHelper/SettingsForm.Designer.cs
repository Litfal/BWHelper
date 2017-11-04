namespace BWHelper
{
    partial class SettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cb_negaviteColor = new System.Windows.Forms.CheckBox();
            this.cb_NegativeColorOffWithColorPage = new System.Windows.Forms.CheckBox();
            this.cb_NegativeColorOffWithPictureArea = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.cb_ReduceLight = new System.Windows.Forms.CheckBox();
            this.cb_ReduceLightOffWithColorPage = new System.Windows.Forms.CheckBox();
            this.cb_ReduceLightOffWithPictureArea = new System.Windows.Forms.CheckBox();
            this.tb_ReduceLightRed = new BWHelper.TrackBarPlus();
            this.tb_ReduceLightGreen = new BWHelper.TrackBarPlus();
            this.tb_ReduceLightBlue = new BWHelper.TrackBarPlus();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.link_email = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(18, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(18, 18, 18, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(416, 242);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "負片";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.cb_negaviteColor);
            this.flowLayoutPanel1.Controls.Add(this.cb_NegativeColorOffWithColorPage);
            this.flowLayoutPanel1.Controls.Add(this.cb_NegativeColorOffWithPictureArea);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(9, 32);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(399, 180);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // cb_negaviteColor
            // 
            this.cb_negaviteColor.AutoSize = true;
            this.cb_negaviteColor.Location = new System.Drawing.Point(4, 4);
            this.cb_negaviteColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_negaviteColor.Name = "cb_negaviteColor";
            this.cb_negaviteColor.Padding = new System.Windows.Forms.Padding(15, 15, 15, 15);
            this.cb_negaviteColor.Size = new System.Drawing.Size(172, 52);
            this.cb_negaviteColor.TabIndex = 0;
            this.cb_negaviteColor.Text = "啟動負片功能";
            this.cb_negaviteColor.UseVisualStyleBackColor = true;
            // 
            // cb_NegativeColorOffWithColorPage
            // 
            this.cb_NegativeColorOffWithColorPage.AutoSize = true;
            this.cb_NegativeColorOffWithColorPage.Location = new System.Drawing.Point(4, 64);
            this.cb_NegativeColorOffWithColorPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_NegativeColorOffWithColorPage.Name = "cb_NegativeColorOffWithColorPage";
            this.cb_NegativeColorOffWithColorPage.Padding = new System.Windows.Forms.Padding(90, 15, 15, 15);
            this.cb_NegativeColorOffWithColorPage.Size = new System.Drawing.Size(391, 52);
            this.cb_NegativeColorOffWithColorPage.TabIndex = 1;
            this.cb_NegativeColorOffWithColorPage.Text = "偵測到彩圖時，全頁不使用負片";
            this.cb_NegativeColorOffWithColorPage.UseVisualStyleBackColor = true;
            // 
            // cb_NegativeColorOffWithPictureArea
            // 
            this.cb_NegativeColorOffWithPictureArea.AutoSize = true;
            this.cb_NegativeColorOffWithPictureArea.Location = new System.Drawing.Point(4, 124);
            this.cb_NegativeColorOffWithPictureArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_NegativeColorOffWithPictureArea.Name = "cb_NegativeColorOffWithPictureArea";
            this.cb_NegativeColorOffWithPictureArea.Padding = new System.Windows.Forms.Padding(90, 15, 15, 15);
            this.cb_NegativeColorOffWithPictureArea.Size = new System.Drawing.Size(301, 52);
            this.cb_NegativeColorOffWithPictureArea.TabIndex = 2;
            this.cb_NegativeColorOffWithPictureArea.Text = "頁內圖片不使用負片";
            this.cb_NegativeColorOffWithPictureArea.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(18, 296);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(18, 18, 18, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(431, 530);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "調色";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.cb_ReduceLight);
            this.flowLayoutPanel2.Controls.Add(this.cb_ReduceLightOffWithColorPage);
            this.flowLayoutPanel2.Controls.Add(this.cb_ReduceLightOffWithPictureArea);
            this.flowLayoutPanel2.Controls.Add(this.tb_ReduceLightRed);
            this.flowLayoutPanel2.Controls.Add(this.tb_ReduceLightGreen);
            this.flowLayoutPanel2.Controls.Add(this.tb_ReduceLightBlue);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(9, 32);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(414, 468);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // cb_ReduceLight
            // 
            this.cb_ReduceLight.AutoSize = true;
            this.cb_ReduceLight.Location = new System.Drawing.Point(4, 4);
            this.cb_ReduceLight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_ReduceLight.Name = "cb_ReduceLight";
            this.cb_ReduceLight.Padding = new System.Windows.Forms.Padding(15, 15, 15, 15);
            this.cb_ReduceLight.Size = new System.Drawing.Size(172, 52);
            this.cb_ReduceLight.TabIndex = 0;
            this.cb_ReduceLight.Text = "啟動調光功能";
            this.cb_ReduceLight.UseVisualStyleBackColor = true;
            // 
            // cb_ReduceLightOffWithColorPage
            // 
            this.cb_ReduceLightOffWithColorPage.AutoSize = true;
            this.cb_ReduceLightOffWithColorPage.Location = new System.Drawing.Point(4, 64);
            this.cb_ReduceLightOffWithColorPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_ReduceLightOffWithColorPage.Name = "cb_ReduceLightOffWithColorPage";
            this.cb_ReduceLightOffWithColorPage.Padding = new System.Windows.Forms.Padding(90, 15, 15, 15);
            this.cb_ReduceLightOffWithColorPage.Size = new System.Drawing.Size(391, 52);
            this.cb_ReduceLightOffWithColorPage.TabIndex = 1;
            this.cb_ReduceLightOffWithColorPage.Text = "偵測到彩圖時，全頁不使用調光";
            this.cb_ReduceLightOffWithColorPage.UseVisualStyleBackColor = true;
            // 
            // cb_ReduceLightOffWithPictureArea
            // 
            this.cb_ReduceLightOffWithPictureArea.AutoSize = true;
            this.cb_ReduceLightOffWithPictureArea.Location = new System.Drawing.Point(4, 124);
            this.cb_ReduceLightOffWithPictureArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_ReduceLightOffWithPictureArea.Name = "cb_ReduceLightOffWithPictureArea";
            this.cb_ReduceLightOffWithPictureArea.Padding = new System.Windows.Forms.Padding(90, 15, 15, 15);
            this.cb_ReduceLightOffWithPictureArea.Size = new System.Drawing.Size(301, 52);
            this.cb_ReduceLightOffWithPictureArea.TabIndex = 2;
            this.cb_ReduceLightOffWithPictureArea.Text = "頁內圖片不使用調光";
            this.cb_ReduceLightOffWithPictureArea.UseVisualStyleBackColor = true;
            // 
            // tb_ReduceLightRed
            // 
            this.tb_ReduceLightRed.AutoSize = true;
            this.tb_ReduceLightRed.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tb_ReduceLightRed.LargeChange = 10;
            this.tb_ReduceLightRed.Location = new System.Drawing.Point(6, 186);
            this.tb_ReduceLightRed.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tb_ReduceLightRed.Maximum = 100;
            this.tb_ReduceLightRed.Name = "tb_ReduceLightRed";
            this.tb_ReduceLightRed.Padding = new System.Windows.Forms.Padding(90, 0, 0, 0);
            this.tb_ReduceLightRed.Size = new System.Drawing.Size(402, 84);
            this.tb_ReduceLightRed.TabIndex = 5;
            this.tb_ReduceLightRed.Text = "紅";
            this.tb_ReduceLightRed.TickFrequency = 25;
            this.tb_ReduceLightRed.Value = 5;
            this.tb_ReduceLightRed.ValueFormat = "{0} %";
            // 
            // tb_ReduceLightGreen
            // 
            this.tb_ReduceLightGreen.AutoSize = true;
            this.tb_ReduceLightGreen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tb_ReduceLightGreen.LargeChange = 10;
            this.tb_ReduceLightGreen.Location = new System.Drawing.Point(6, 282);
            this.tb_ReduceLightGreen.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tb_ReduceLightGreen.Maximum = 100;
            this.tb_ReduceLightGreen.Name = "tb_ReduceLightGreen";
            this.tb_ReduceLightGreen.Padding = new System.Windows.Forms.Padding(90, 0, 0, 0);
            this.tb_ReduceLightGreen.Size = new System.Drawing.Size(402, 84);
            this.tb_ReduceLightGreen.TabIndex = 6;
            this.tb_ReduceLightGreen.Text = "綠";
            this.tb_ReduceLightGreen.TickFrequency = 25;
            this.tb_ReduceLightGreen.Value = 5;
            this.tb_ReduceLightGreen.ValueFormat = "{0} %";
            // 
            // tb_ReduceLightBlue
            // 
            this.tb_ReduceLightBlue.AutoSize = true;
            this.tb_ReduceLightBlue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tb_ReduceLightBlue.LargeChange = 10;
            this.tb_ReduceLightBlue.Location = new System.Drawing.Point(6, 378);
            this.tb_ReduceLightBlue.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tb_ReduceLightBlue.Maximum = 100;
            this.tb_ReduceLightBlue.Name = "tb_ReduceLightBlue";
            this.tb_ReduceLightBlue.Padding = new System.Windows.Forms.Padding(90, 0, 0, 0);
            this.tb_ReduceLightBlue.Size = new System.Drawing.Size(402, 84);
            this.tb_ReduceLightBlue.TabIndex = 7;
            this.tb_ReduceLightBlue.Text = "藍";
            this.tb_ReduceLightBlue.TickFrequency = 25;
            this.tb_ReduceLightBlue.Value = 5;
            this.tb_ReduceLightBlue.ValueFormat = "{0} %";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Location = new System.Drawing.Point(4, 980);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(474, 66);
            this.panel1.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(312, 16);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 34);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(69, 16);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 34);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(190, 16);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(94, 34);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.groupBox1);
            this.flowLayoutPanel3.Controls.Add(this.groupBox2);
            this.flowLayoutPanel3.Controls.Add(this.groupBox3);
            this.flowLayoutPanel3.Controls.Add(this.panel1);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(482, 1050);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.flowLayoutPanel4);
            this.groupBox3.Location = new System.Drawing.Point(18, 862);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(18, 18, 18, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(446, 96);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "問題回報";
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this.label1);
            this.flowLayoutPanel4.Controls.Add(this.link_email);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(9, 32);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(170, 36);
            this.flowLayoutPanel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "擷圖並寄至 ";
            // 
            // link_email
            // 
            this.link_email.AutoSize = true;
            this.link_email.Location = new System.Drawing.Point(4, 18);
            this.link_email.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.link_email.Name = "link_email";
            this.link_email.Size = new System.Drawing.Size(162, 18);
            this.link_email.TabIndex = 4;
            this.link_email.TabStop = true;
            this.link_email.Text = "litfal1265@gmail.com";
            this.link_email.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_email_LinkClicked);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(879, 1020);
            this.Controls.Add(this.flowLayoutPanel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox cb_negaviteColor;
        private System.Windows.Forms.CheckBox cb_NegativeColorOffWithColorPage;
        private System.Windows.Forms.CheckBox cb_NegativeColorOffWithPictureArea;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox cb_ReduceLight;
        private System.Windows.Forms.CheckBox cb_ReduceLightOffWithColorPage;
        private System.Windows.Forms.CheckBox cb_ReduceLightOffWithPictureArea;
        private TrackBarPlus tb_ReduceLightRed;
        private TrackBarPlus tb_ReduceLightGreen;
        private TrackBarPlus tb_ReduceLightBlue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel link_email;
    }
}
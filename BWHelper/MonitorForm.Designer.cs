namespace BWHelper
{
    partial class MonitorForm
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
            this.pl_waitApp = new System.Windows.Forms.Panel();
            this.btn_run = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pl_waitView = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pl_waitApp.SuspendLayout();
            this.pl_waitView.SuspendLayout();
            this.SuspendLayout();
            // 
            // pl_waitApp
            // 
            this.pl_waitApp.Controls.Add(this.btn_run);
            this.pl_waitApp.Controls.Add(this.label1);
            this.pl_waitApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_waitApp.Location = new System.Drawing.Point(30, 30);
            this.pl_waitApp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pl_waitApp.Name = "pl_waitApp";
            this.pl_waitApp.Size = new System.Drawing.Size(412, 234);
            this.pl_waitApp.TabIndex = 0;
            // 
            // btn_run
            // 
            this.btn_run.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_run.Location = new System.Drawing.Point(0, 153);
            this.btn_run.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(412, 81);
            this.btn_run.TabIndex = 1;
            this.btn_run.Text = "啟動BOOK☆WALKER";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("新細明體", 24F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 153);
            this.label1.TabIndex = 0;
            this.label1.Text = "偵測不到執行中的BOOK☆WALKER";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pl_waitView
            // 
            this.pl_waitView.Controls.Add(this.label2);
            this.pl_waitView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_waitView.Location = new System.Drawing.Point(30, 30);
            this.pl_waitView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pl_waitView.Name = "pl_waitView";
            this.pl_waitView.Size = new System.Drawing.Size(412, 234);
            this.pl_waitView.TabIndex = 1;
            this.pl_waitView.Visible = false;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("新細明體", 24F);
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(412, 234);
            this.label2.TabIndex = 1;
            this.label2.Text = "等待開啟電子書";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(472, 294);
            this.Controls.Add(this.pl_waitApp);
            this.Controls.Add(this.pl_waitView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "MonitorForm";
            this.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.Text = "BW Helper";
            this.Load += new System.EventHandler(this.MonitorForm_Load);
            this.pl_waitApp.ResumeLayout(false);
            this.pl_waitView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pl_waitApp;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pl_waitView;
        private System.Windows.Forms.Label label2;
    }
}
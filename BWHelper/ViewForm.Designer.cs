namespace BWHelper
{
    partial class ViewForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Pb_Main = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cb_negativeColor = new System.Windows.Forms.ToolStripMenuItem();
            this.cb_lightReduce = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_openSettings = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_Main)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pb_Main
            // 
            this.Pb_Main.ContextMenuStrip = this.contextMenuStrip1;
            this.Pb_Main.Location = new System.Drawing.Point(0, 0);
            this.Pb_Main.Margin = new System.Windows.Forms.Padding(0);
            this.Pb_Main.Name = "Pb_Main";
            this.Pb_Main.Size = new System.Drawing.Size(100, 50);
            this.Pb_Main.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Pb_Main.TabIndex = 0;
            this.Pb_Main.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cb_negativeColor,
            this.cb_lightReduce,
            this.toolStripSeparator1,
            this.btn_openSettings});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 76);
            // 
            // cb_negativeColor
            // 
            this.cb_negativeColor.CheckOnClick = true;
            this.cb_negativeColor.Name = "cb_negativeColor";
            this.cb_negativeColor.Size = new System.Drawing.Size(98, 22);
            this.cb_negativeColor.Text = "負片";
            this.cb_negativeColor.CheckedChanged += new System.EventHandler(this.cb_negativeColor_CheckedChanged);
            // 
            // cb_lightReduce
            // 
            this.cb_lightReduce.CheckOnClick = true;
            this.cb_lightReduce.Name = "cb_lightReduce";
            this.cb_lightReduce.Size = new System.Drawing.Size(98, 22);
            this.cb_lightReduce.Text = "調光";
            this.cb_lightReduce.Click += new System.EventHandler(this.cb_lightReduce_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(95, 6);
            // 
            // btn_openSettings
            // 
            this.btn_openSettings.Name = "btn_openSettings";
            this.btn_openSettings.Size = new System.Drawing.Size(98, 22);
            this.btn_openSettings.Text = "設定";
            this.btn_openSettings.Click += new System.EventHandler(this.btn_openSettings_Click);
            // 
            // ViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(188, 131);
            this.Controls.Add(this.Pb_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "BW Helper";
            ((System.ComponentModel.ISupportInitialize)(this.Pb_Main)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Pb_Main;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cb_negativeColor;
        private System.Windows.Forms.ToolStripMenuItem cb_lightReduce;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btn_openSettings;
    }
}


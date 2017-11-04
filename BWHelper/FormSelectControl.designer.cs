namespace WindowCaptureAndShow
{
    partial class FormSelectControl
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
            this.components = new System.ComponentModel.Container();
            this.txt_Handle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timerGetHandle = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lb_info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_Handle
            // 
            this.txt_Handle.Location = new System.Drawing.Point(32, 88);
            this.txt_Handle.Name = "txt_Handle";
            this.txt_Handle.Size = new System.Drawing.Size(100, 22);
            this.txt_Handle.TabIndex = 0;
            this.txt_Handle.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Handle";
            this.label1.Visible = false;
            // 
            // timerGetHandle
            // 
            this.timerGetHandle.Tick += new System.EventHandler(this.timerGetHandle_Tick);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(32, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(113, 116);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // lb_info
            // 
            this.lb_info.BackColor = System.Drawing.Color.White;
            this.lb_info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_info.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_info.Location = new System.Drawing.Point(0, 0);
            this.lb_info.Name = "lb_info";
            this.lb_info.Size = new System.Drawing.Size(248, 151);
            this.lb_info.TabIndex = 4;
            this.lb_info.Text = "Move cursor on the window \r\nyou want resampling\r\n\r\nand click F4";
            this.lb_info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 151);
            this.Controls.Add(this.lb_info);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Handle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSelectControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Handle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerGetHandle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lb_info;
    }
}
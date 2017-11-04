using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowCaptureAndShow
{
    public partial class FormFakeControlView : Form
    {
        Pen borderPen;
        float borderWidth = 6f;
        float borderWidthHalf = 6f / 2;

        public FormFakeControlView()
        {
            InitializeComponent();

            borderPen = new Pen(Color.Red, borderWidth);
        }

        private void FormFakeControlView_Paint(object sender, PaintEventArgs e)
        {
            
            e.Graphics.DrawRectangle(borderPen, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);

            // e.Graphics.DrawRectangle(borderPen, 0, 0, this.ClientRectangle.Width - borderWidthHalf, this.ClientRectangle.Height - borderWidthHalf);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RectangleF rect = this.DisplayRectangle;
            rect.Inflate(-borderWidthHalf,-borderWidthHalf);

            Region region = new Region(this.DisplayRectangle);
            region.Exclude(rect);
            this.Region = region;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BWHelper
{
    public partial class TrackBarPlus : UserControl
    {
        [Bindable(true)]
        [DefaultValue(0)]
        public int Value { get { return trackBar.Value; } set { trackBar.Value = value; } }

        //
        // 傳回:
        //     The text associated with this control.
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text { get { return lb_Text.Text; } set { lb_Text.Text = value; } }


        //
        // 摘要:
        //     Gets or sets a value that specifies the delta between ticks drawn on the control.
        //
        // 傳回:
        //     The numeric value representing the delta between ticks. The default is 1.
        [DefaultValue(1)]
        public int TickFrequency { get { return trackBar.TickFrequency; } set { trackBar.TickFrequency = value; } }
        //
        // 摘要:
        //     Gets or sets a value indicating how to display the tick marks on the track bar.
        //
        // 傳回:
        //     One of the System.Windows.Forms.TickStyle values. The default is System.Windows.Forms.TickStyle.BottomRight.
        //
        // 例外狀況:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     The assigned value is not a valid System.Windows.Forms.TickStyle.
        [DefaultValue(TickStyle.BottomRight)]
        public TickStyle TickStyle { get { return trackBar.TickStyle; } set { trackBar.TickStyle = value; } }
        //
        // 摘要:
        //     Gets or sets the value added to or subtracted from the System.Windows.Forms.TrackBar.Value
        //     property when the scroll box is moved a small distance.
        //
        // 傳回:
        //     A numeric value. The default value is 1.
        //
        // 例外狀況:
        //   T:System.ArgumentException:
        //     The assigned value is less than 0.
        [DefaultValue(1)]
        public int SmallChange { get { return trackBar.SmallChange; } set { trackBar.SmallChange = value; } }

        //
        // 摘要:
        //     Gets or sets a value to be added to or subtracted from the System.Windows.Forms.TrackBar.Value
        //     property when the scroll box is moved a large distance.
        //
        // 傳回:
        //     A numeric value. The default is 5.
        //
        // 例外狀況:
        //   T:System.ArgumentException:
        //     The assigned value is less than 0.
        [DefaultValue(5)]
        public int LargeChange { get { return trackBar.LargeChange; } set { trackBar.LargeChange = value; } }

        //
        // 摘要:
        //     Gets or sets the lower limit of the range this System.Windows.Forms.TrackBar
        //     is working with.
        //
        // 傳回:
        //     The minimum value for the System.Windows.Forms.TrackBar. The default is 0.
        [DefaultValue(0)]
        [RefreshProperties(RefreshProperties.All)]
        public int Minimum { get { return trackBar.Minimum; } set { trackBar.Minimum = value; } }
        //
        // 摘要:
        //     Gets or sets the upper limit of the range this System.Windows.Forms.TrackBar
        //     is working with.
        //
        // 傳回:
        //     The maximum value for the System.Windows.Forms.TrackBar. The default is 10.
        [DefaultValue(10)]
        [RefreshProperties(RefreshProperties.All)]
        public int Maximum { get { return trackBar.Maximum; } set { trackBar.Maximum = value; } }


        //
        // 摘要:
        //     Occurs when the System.Windows.Forms.TrackBar.Value property of a track bar changes,
        //     either by movement of the scroll box or by manipulation in code.
        public event EventHandler ValueChanged;

        private string valueFormat = "{0}";
        [DefaultValue("{0}")]
        [RefreshProperties(RefreshProperties.All)]
        public string ValueFormat { get { return valueFormat; } set
            {
                valueFormat = value;
                refreshValueText();
            }
        }


        public TrackBarPlus()
        {
            InitializeComponent();

            trackBar.ValueChanged += (s, e) =>
            {
                refreshValueText();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            };

            lb_Text.Text = Text;
            refreshValueText();
        }

        private void refreshValueText()
        {
            lb_value.Text = string.Format(valueFormat, Value);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            lb_Text.Text = Text;
        }



    }
}

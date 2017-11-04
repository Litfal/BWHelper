using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowCaptureAndShow
{
    public partial class FormSelectControl : Form
    {
        FormFakeControlView _fakeControlForm = new FormFakeControlView();
        KeyboardHook _keyboardHook;
        IntPtr hwnd = IntPtr.Zero;

           
        const string _infoFormat = "Move cursor on the window\nyou want resampling\n\nand click {0}\nOr click {1} to exit";

        public Keys KeyToSelect { get; set; } = Keys.F4;
        public ModifierKeys ModifierToSelect { get; set; } = 0;

        public Keys KeyToExit { get; set; } = Keys.Escape;
        public ModifierKeys ModifierToExit { get; set; } = 0;

        public IntPtr SelectHandle
        {
            get { return hwnd; }
        }

        public FormSelectControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _keyboardHook = new KeyboardHook();
            _keyboardHook.KeyPressed += keyboardHook_KeyPressed;

            _keyboardHook.RegisterHotKey(ModifierToSelect, KeyToSelect);
            _keyboardHook.RegisterHotKey(ModifierToExit, KeyToExit);

            lb_info.Text = string.Format(_infoFormat,
                getKeyName(ModifierToSelect, KeyToSelect), getKeyName(ModifierToExit, KeyToExit));


            timerGetHandle.Start();
        }

        string getKeyName(ModifierKeys modifier, Keys key)
        {
            if (modifier == 0) return key.ToString();
            return string.Format("{0}+{1}", modifier, key);
        }

        void keyboardHook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            timerGetHandle.Stop();
            _fakeControlForm.Dispose();
            _fakeControlForm = null;

            if (hwnd == IntPtr.Zero ||
               (e.Modifier == ModifierToExit &&
               e.Key == KeyToExit))
            {
                // Exit
                hwnd = IntPtr.Zero;
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            }
            else if (e.Modifier == ModifierToSelect &&
                e.Key == KeyToSelect)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                throw new NotImplementedException();

            this.Hide();
        }


        private void timerGetHandle_Tick(object sender, EventArgs e)
        {
            hwnd = Litfal.CommonWinAPI.WindowControl.GetWindowUnderCursor();
            txt_Handle.Text = hwnd.ToString();
            if (hwnd != _fakeControlForm.Handle)
            {
                Litfal.WinAPI.RECT rect;
                Litfal.WinAPI.GetWindowRect(hwnd, out rect);
                var rect2 = rect.ToRectangle();
                _fakeControlForm.SetDesktopBounds(rect2.X, rect2.Y, rect2.Width, rect2.Height);
                _fakeControlForm.Show();
                _fakeControlForm.TopMost = true;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _keyboardHook.Dispose();
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace BWHelper
{
    public partial class ViewForm : Form
    {

        // ImageBox imageBox;


        //IScreenCapturer<Screenshot> capturer;
        //Converter<Screenshot, Stream> converter = ScreenshotConverter.GetConverter();
        GDIScreenCapturer capturer;

        ProcessSettings settings = new ProcessSettings();


        private System.Threading.Thread thDraw;

        private bool thDrawIsWork;
        private IntPtr targetHandle = IntPtr.Zero;
        private IntPtr sendMessageHwnd2;

        int drawMode = 0;

        EBPage page;



        Plugins.PageControllerByMouseWheel _pageControllerByMouseWheel = new Plugins.PageControllerByMouseWheel();
        List<Plugins.Input.BaseController> _pluginControllers = new List<Plugins.Input.BaseController>();

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x100:
                case 0x101:
                case 0x102:
                    if (targetHandle != IntPtr.Zero)
                    {
                        Litfal.WinAPI.SendMessage(targetHandle, m.Msg, m.WParam, m.LParam);
                    }
                    break;
                case 0x20A:
                    if (sendMessageHwnd2 != IntPtr.Zero)
                    {
                        Litfal.WinAPI.SendMessage(sendMessageHwnd2, m.Msg, m.WParam, m.LParam);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        public ViewForm()
        {
            InitializeComponent();

            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.MouseMove += PictureBox1_MouseMove;

            this.Load += ViewForm_Load;

            page = new EBPage(settings);

            settings.Changed += (s, e) => page.Refresh();

            this.Text = "BW Helper " + Application.ProductVersion;



            loadPlugins();
        }

        private void loadPlugins()
        {
            string pluginsDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),"Plugins");
            if (!Directory.Exists(pluginsDirectory)) return;
            string[] dllFiles = Directory.GetFiles(pluginsDirectory, "BWHelper.Plugins.*.dll");
            try
            {
                foreach (var dllFile in dllFiles)
                {
                    Assembly assembly = Assembly.LoadFrom(dllFile);
                    foreach(var type in assembly.ExportedTypes)
                    {
                        if (typeof(Plugins.Input.BaseController).IsAssignableFrom(type))
                        {
                            var controller = (Plugins.Input.BaseController)Activator.CreateInstance(type);
                            _pluginControllers.Add(controller);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            var settings = Properties.Settings.Default;
            WindowState = settings.Maximised ? FormWindowState.Maximized : FormWindowState.Normal;
            Left = Math.Max(-20, settings.Left);
            Top = Math.Max(-20, settings.Top);
            syncConfigUI();
            LocationChanged += ViewForm_LocationChanged;
            SizeChanged += ViewForm_SizeChanged;
        }

        private void ViewForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) return;
            switch (WindowState)
            {
                case FormWindowState.Maximized:
                    Properties.Settings.Default.Maximised = true;
                    break;
                case FormWindowState.Normal:
                    Properties.Settings.Default.Maximised = false;
                    pictureBox1.Image = pictureBox1.Image;  // autosize 
                    break;
            }
        }

        private void ViewForm_LocationChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.Left = Left;
                Properties.Settings.Default.Top = Top;
            }
        }

        public void StartCapture(IntPtr targetHandle)
        {
            this.targetHandle = targetHandle;
            sendMessageHwnd2 = Litfal.WinAPI.GetParent(targetHandle);
            _pageControllerByMouseWheel.WindowHandle = sendMessageHwnd2;

            startCapture();
        }

        private void startCapture()
        {
            var gdiCapturer = new GDIScreenCapturer()
            {
                FPSLimit = Properties.Settings.Default.CaptionFPSLimit,
                HighAccurateFPSLimit = false,
            };
            capturer = gdiCapturer;

            capturer.SetWindowHandle(targetHandle);

            if (drawMode == 1)
            {
                gdiCapturer.Captured += WindowCaptured;
            }
            else
            {
                thDrawIsWork = true;
                thDraw = new System.Threading.Thread(new System.Threading.ThreadStart(draw_proc_loop));
                thDraw.Start();
            }

            capturer.Start();
            pluginsStart();
        }

        public void StopTasks()
        {
            pluginsStop();
            if (thDraw != null)
            {
                thDrawIsWork = false;
                if (!thDraw.Join(2000))
                {
                    thDraw.Abort();
                }
                thDraw = null;
            }

            if (capturer != null)
            {
                capturer.Dispose();
                capturer = null;
            }
            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            pluginsDispose();
            try
            {
                if (thDraw != null)
                    thDraw.Abort();
                if (capturer != null)
                    capturer.Dispose();
            }
            finally
            {
                capturer = null;
                thDraw = null;
            }
            base.OnFormClosing(e);
        }

        private void pluginsStart()
        {
            _pluginControllers.ForEach(c => c.Start(_pageControllerByMouseWheel));
        }
        private void pluginsStop()
        {
            _pluginControllers.ForEach(c => c.Stop());
        }
        private void pluginsDispose()
        {
            _pluginControllers.ForEach(c =>
            {
                try
                {
                    c.Dispose();
                }
                catch (Exception) { }
            });
            _pluginControllers.Clear();
        }


        private void draw_proc_loop()
        {
            IScreenCaptureData<Bitmap> usedData = null;


            while (thDrawIsWork)
            {
                // Bitmap bmp = surfaceBuffer.Dequeue();
                var _capt = (IScreenCapturer<Bitmap>)capturer;
                var scdata = _capt.DequeueBuffer();
                var data = scdata.GetData();

                page.Process(data);
                if (!page.IsSamePage)
                {
                    if (usedData != null)
                        _capt.EequeueBuffer(usedData);
                    usedData = scdata;
                    onGetCaptureData(data);
                }
                else
                    _capt.EequeueBuffer(scdata);

            }
        }


        private void WindowCaptured(object sender, CapturedEventArgs<Bitmap> e)
        {
            onGetCaptureData(e.Data.GetData());
        }


        private void onGetCaptureData(Bitmap bmp)
        {
            Invoke(new Action(() => pictureBox1.Image = bmp));
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);


        #region Keyboard / Mouse Relay

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Litfal.CommonWinAPI.WinForm.MouseControl.MouseMove(targetHandle, e.X, e.Y);
        }


        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Litfal.CommonWinAPI.WinForm.MouseControl.MouseDown(targetHandle, e.Button, e.X, e.Y);
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Litfal.CommonWinAPI.WinForm.MouseControl.MouseUp(targetHandle, e.Button, e.X, e.Y);
        }

        #endregion

        private void syncConfigUI()
        {
            cb_negativeColor.Checked = settings.NegativeColor;
            cb_lightReduce.Checked = settings.ReduceLight;
        }

        private void cb_negativeColor_CheckedChanged(object sender, EventArgs e)
        {
            settings.NegativeColor = cb_negativeColor.Checked;
        }

        private void cb_lightReduce_CheckedChanged(object sender, EventArgs e)
        {
            settings.ReduceLight = cb_lightReduce.Checked;
        }

        SettingsForm settingsForm;



        private void btn_openSettings_Click(object sender, EventArgs e)
        {
            if (settingsForm == null || settingsForm.IsDisposed)
                settingsForm = new SettingsForm(settings);
            settingsForm.Show();
            settingsForm.Activate();
        }
    }
}

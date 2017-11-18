using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Management;

namespace BWHelper
{
    public partial class MonitorForm : Form
    {
        private bool thMonitorIsWork;
        private System.Threading.Thread thMonitor;
        private int waitToBwInterval = 500;
        private int listenToBwClose = 200;
        private bool bwIsFound = false;

        ViewForm _viewForm;

        public MonitorForm()
        {
            InitializeComponent();
            this.Text = "BW Helper " + Application.ProductVersion;
        }

        private void MonitorForm_Load(object sender, EventArgs e)
        {
            cb_autorunApp.Checked = Properties.Settings.Default.AutorunBookWalker;
            var findResult = findBookWalker();
            if (!findResult.ProcessRunning &&
                Properties.Settings.Default.AutorunBookWalker &&
                !string.IsNullOrEmpty(Properties.Settings.Default.BWPath) &&
                File.Exists(Properties.Settings.Default.BWPath))
            {
                startBookWalker(Properties.Settings.Default.BWPath);
            }

            thMonitorIsWork = true;
            thMonitor = new System.Threading.Thread(monitorLoop);
            thMonitor.Start();
        }

        private void monitorLoop()
        {
            IntPtr bwHwnd = IntPtr.Zero;
            while (thMonitorIsWork)
            {
                if (!bwIsFound)
                {
                    var findResult = findBookWalker();
                    if (pl_waitApp.Visible == findResult.ProcessRunning)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            pl_waitApp.Visible = !findResult.ProcessRunning;
                            pl_waitView.Visible = findResult.ProcessRunning;
                        }));
                    }
                    if (findResult.ViewerFound)
                    {
                        if (Properties.Settings.Default.BWPath != findResult.ProcessPath)
                        {
                            Properties.Settings.Default.BWPath = findResult.ProcessPath;
                            Properties.Settings.Default.Save();
                        }
                        bwHwnd = findResult.ViewerHandle;
                        bwIsFound = true;
                        openViewForm(bwHwnd);
                    }
                    System.Threading.Thread.Sleep(waitToBwInterval);
                }
                else
                {
                    if (!Litfal.WinAPI.IsWindow(bwHwnd))
                    {
                        closeViewForm();
                        bwIsFound = false;
                    }
                    System.Threading.Thread.Sleep(listenToBwClose);
                }
            }
        }

        private string getProcessPath(int pId)
        {

            var wmiQueryString = "SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process WHERE ProcessId=" + pId;
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            using (var results = searcher.Get())
            {
                foreach (var item in results)
                {
                    string path = item["ExecutablePath"]?.ToString();
                    // Do what you want with the Process, Path, and CommandLine
                }
            }
            return null;
        }

        private void closeViewForm()
        {
            _viewForm?.StopTasks();
            BeginInvoke(new Action(() =>
            {
                _viewForm?.Hide();
                this.Show();
            }));
        }

        private void openViewForm(IntPtr bwHwnd)
        {
            BeginInvoke(new Action(() =>
            {
                if (_viewForm == null)
                {
                    _viewForm = new ViewForm();
                    _viewForm.FormClosed += (s, e) => Close();
                }
                _viewForm.Show(this);
                _viewForm.StartCapture(bwHwnd);
                _viewForm.Activate();
                this.Hide();
            }));
        }

        private FindBookWalkerResult findBookWalker()
        {
            // get main process ID
            int bwMainProcId = 0;
            string bwPath = string.Empty;
            foreach (var bwProc in Process.GetProcessesByName("bookwalker"))
            {
                if (bwProc.MainWindowHandle != IntPtr.Zero)
                {
                    bwMainProcId = bwProc.Id;
                }
                try
                {
                    bwPath = bwProc.MainModule.FileName;
                }
                catch (Exception)
                {
                }
            }

            // get all handle of window that is under PID
            List<IntPtr> windowHandles = new List<IntPtr>();

            Litfal.WinAPI.EnumWindows((hWnd, lParam) =>
            {
                int pId;
                int tId = Litfal.WinAPI.GetWindowThreadProcessId(hWnd, out pId);
                if (pId == bwMainProcId)
                    windowHandles.Add(hWnd);
                return true;
            }, IntPtr.Zero);

            return new FindBookWalkerResult()
            {
                ProcessRunning = bwMainProcId != 0,
                PId = bwMainProcId,
                ProcessPath = bwPath,
                // find chromeHost
                ViewerHandle = windowHandles  
                    .Select(h => findChromeHost(h))
                    .FirstOrDefault(h => h != IntPtr.Zero)
            };
        }

        class FindBookWalkerResult
        {
            public bool ProcessRunning { get; set; }
            public string ProcessPath { get; internal set; }
            public int PId { get; set; }
            public bool ViewerFound { get { return ViewerHandle != IntPtr.Zero; } } 
            public IntPtr ViewerHandle { get; set; } = IntPtr.Zero;
        }

        private IntPtr findChromeHost(IntPtr mainWindowHandle)
        {
            IntPtr subWin = IntPtr.Zero;
            do
            {
                subWin = Litfal.WinAPI.FindWindowEx(mainWindowHandle, subWin, IntPtr.Zero, IntPtr.Zero);
                if (subWin == IntPtr.Zero) break;
                IntPtr browserWindow = Litfal.WinAPI.FindWindowEx(subWin, IntPtr.Zero, "CefBrowserWindow", IntPtr.Zero);
                if (browserWindow == IntPtr.Zero) continue;
                IntPtr chromeWidget = Litfal.WinAPI.FindWindowEx(browserWindow, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                if (chromeWidget == IntPtr.Zero) break;
                IntPtr chromeHost = Litfal.WinAPI.FindWindowEx(chromeWidget, IntPtr.Zero, "Chrome_RenderWidgetHostHWND", IntPtr.Zero);
                if (chromeHost != IntPtr.Zero) return chromeHost;

            } while (subWin != IntPtr.Zero);
            return IntPtr.Zero;
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            thMonitorIsWork = false;
            try
            {
                thMonitor?.Join(2000);
                thMonitor = null;
                _viewForm?.Dispose();
                _viewForm = null;
            }
            catch (Exception)
            {
            }
            base.OnFormClosing(e);
        }


        private void btn_run_Click(object sender, EventArgs e)
        {
            string path = Properties.Settings.Default.BWPath;
            if (string.IsNullOrEmpty(path) ||
                !File.Exists(path))
            {
                string dirName = string.Empty;
                if (File.Exists(@"C:\Program Files (x86)\BOOKWALKER\BOOKWALKER for Windows\bookwalker.exe"))
                    dirName = @"C:\Program Files (x86)\BOOKWALKER\BOOKWALKER for Windows";
                else if (File.Exists(@"C:\Program Files\BOOKWALKER\BOOKWALKER for Windows\bookwalker.exe"))
                    dirName = @"C:\Program Files\BOOKWALKER\BOOKWALKER for Windows";

                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "執行檔|*.exe";
                    ofd.CheckPathExists = true;
                    ofd.FileName = "bookwalker.exe";
                    ofd.InitialDirectory = dirName;
                    if (ofd.ShowDialog(this) == DialogResult.OK)
                    {
                        startBookWalker(ofd.FileName);
                    }
                }
            }
            else
                startBookWalker(path);
        }

        private void startBookWalker(string path)
        {
            Process.Start(path);
            Properties.Settings.Default.AutorunBookWalker = cb_autorunApp.Checked;
        }
    }
}

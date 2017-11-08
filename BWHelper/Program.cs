using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BWHelper
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (System.Environment.OSVersion.Version.Major >= 6) { Litfal.WinAPI.SetProcessDPIAware(); }
            var settings = Properties.Settings.Default;
            if (settings.SettingsVersion != Application.ProductVersion)
            {
                settings.Upgrade();
                settings.SettingsVersion = Application.ProductVersion;
                settings.Save();
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MonitorForm());

        }
    }
}

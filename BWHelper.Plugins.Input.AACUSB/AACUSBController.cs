using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AACUSB;

namespace BWHelper.Plugins.Input.AACUSB
{
    public class AACUSBController : BWHelper.Plugins.Input.BaseController
    {
        int[] supportPIDs = new int[] { 20 };

        private List<SwitchDongle> _switchDongles;

        protected override void OnStart()
        {
            base.OnStart();
            _switchDongles = new List<SwitchDongle>();
            foreach (var pid in supportPIDs)
            {
                var switchDongle = new SwitchDongle();
                switchDongle.ProductID = pid;
                switchDongle.KeyDown += switchDongle_KeyDown;
                switchDongle.Start();
                _switchDongles.Add(switchDongle);
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            _switchDongles.ForEach(sd => sd.Stop());
        }

        private void switchDongle_KeyDown(object sender, SwitchKeyEventArgs e)
        {
            PageController.PageDown();
        }

        protected override void OnDisposing()
        {
            base.OnDisposing();
            _switchDongles.ForEach(sd => sd.Dispose());
        }
    }
}

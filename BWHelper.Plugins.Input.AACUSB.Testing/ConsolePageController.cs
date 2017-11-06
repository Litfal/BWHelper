using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BWHelper.Plugins.Input.AACUSB.Testing
{

    class ConsolePageController : IPageController
    {
        public void PageDown()
        {
            Console.WriteLine("PageDown");
        }

        public void PageUp()
        {
            Console.WriteLine("PageUp");
        }
    }
}
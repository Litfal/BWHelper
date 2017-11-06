using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWHelper.Plugins.Input.AACUSB.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            AACUSBController testController = new AACUSBController();
            testController.Start(new ConsolePageController());
            Console.ReadKey();
        }
    }
}

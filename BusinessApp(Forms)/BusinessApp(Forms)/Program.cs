using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessApp_Forms_.UI;
namespace BusinessApp_Forms_
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ObjectHandler.GetDeviceDL().LoadDeviceData();
            ObjectHandler.GetUserDL().LoadUsers();
            Application.Run(new Sign());
        }
    }
}

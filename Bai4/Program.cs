
using System;
using System.Windows.Forms;

namespace Bai4
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new frmRevenueAnalytics());
        }
    }
}

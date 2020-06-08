using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OZIRSALIYE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (System.Diagnostics.Process.GetProcessesByName("Özügüçer İrsaliye").Length > 1)
            {
                MessageBox.Show("Çalışan bir program bulunmakta.");
                Application.Exit();
            }


            CultureInfo culture = new CultureInfo("tr-TR", true);
            culture.NumberFormat.CurrencySymbol = "₺";
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            DevExpress.Utils.FormatInfo.AlwaysUseThreadFormat = true;



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}

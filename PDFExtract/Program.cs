using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFExtract
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Finanzreport fr = new Finanzreport();
            fr.TestDoWork();
            Environment.Exit(1);
            //DoWork.TestDoWork(@"Wertpapierabrechnung_Verkauf_33_St._WKN_A1C6L0(GAM_MULT.-ASIA_FOCE_USD_B)_vom_11.12.2017584885.pdf");
            DoWork.TestDoWork();
            Environment.Exit(1);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}

using OZIRSALIYE.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace OZIRSALIYE.OzClass
{
    class ExcelYazdir : ExcelLib
    {
        BaglantiDataContext dc;

        public ExcelYazdir()
        {
            dc = new BaglantiDataContext();
        }


        public void PrintMyExcelFile(int irsaliyeID)
        {

            string path = getPath(irsaliyeID);



            Excel.Application excelApp = new Excel.Application();

            // Open the Workbook:
            Excel.Workbook wb = excelApp.Workbooks.Open(
                path,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Get the first worksheet.
            // (Excel uses base 1 indexing, not base 0.)
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];

            // Print out 1 copy to the default printer:
            ws.PrintOut(
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Cleanup:
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.FinalReleaseComObject(ws);

            wb.Close(false, Type.Missing, Type.Missing);
            Marshal.FinalReleaseComObject(wb);

            excelApp.Quit();
            Marshal.FinalReleaseComObject(excelApp);
        }
    }
}

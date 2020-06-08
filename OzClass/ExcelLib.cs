using OZIRSALIYE.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace OZIRSALIYE.OzClass
{
    class ExcelLib
    {
        BaglantiDataContext dc = new BaglantiDataContext();

        Excel.Application xlap;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        Excel.Range userRange;

        public ExcelLib()
        {
            try
            {
                xlap = new Excel.Application();
                // xlWorkBook = xlap.Workbooks.Open(@"C:\Users\Ozan\Desktop\faturaexcel\OrijinalKopya\irsaliye.xlsx");
               
                if (File.Exists(@"C:\OZUGUCER\OzIrsaliye\irsaliye.xlsx")==false)
                {
                    xlWorkBook = xlap.Workbooks.Open(@"C:\OZUGUCER\OzIrsaliye\IrsaliyeYedek\irsaliye.xlsx");
                }

                xlWorkBook = xlap.Workbooks.Open(@"C:\OZUGUCER\OzIrsaliye\irsaliye.xlsx");
                xlWorkSheet = xlap.ActiveSheet as Excel.Worksheet;
                userRange = xlWorkSheet.UsedRange;
            }
            
            catch(FileNotFoundException e)
            {
                MessageBox.Show("Dosya Bulunamadı.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        public string getPath(int irsaliyeID)
        {
            IQueryable<IrsaliyeTablo> pathQuery;
            string query = "";
            string[] pathValues = new string[4];

            if (irsaliyeID == -1) //yeni irsaliye kayıdından gelen
                pathQuery = from getir in dc.IrsaliyeTablos where getir.Sil == 1 orderby getir.IrsaliyeID descending select getir;
            else // seçili irsaliye
                pathQuery = from getir in dc.IrsaliyeTablos where getir.IrsaliyeID == irsaliyeID && getir.Sil == 1 select getir;

            foreach (var item in pathQuery)
            {
                pathValues[0] = item.IrsaliyeID.ToString();
                pathValues[1] = item.Tarih.ToString().Remove(10);
                pathValues[2] = item.PlakaNo;
                pathValues[3] = item.Soforler.AdiSoyadi;
                break;

            }
            query = pathValues[0] + "_" + pathValues[1] + "_" + pathValues[2] + "_" + pathValues[3];
            //string mySheet = @"C:\Users\Ozan\Desktop\faturaexcel\irsaliyeler\" + query + ".xlsx";
            string mySheet = @"C:\OZUGUCER\OzIrsaliye\Irsaliyeler\" + query + ".xlsx";

            return mySheet;


        }
        public void excelAc()
        {
            
            try
            {
                if (File.Exists(getPath(-1)))
                {
                    var excelApp = new Excel.Application();
                    excelApp.Visible = true;

                    Excel.Workbooks books = excelApp.Workbooks;
                    Excel.Workbook sheets = books.Open(getPath(-1));
                }
                else
                {
                    MessageBox.Show("Excel dosyası bulunamadı. Silinmiş ve ya zarar görmüş olabilir.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(FileNotFoundException e)
            {
                MessageBox.Show("Hata ile karşılaşıldı. Hata: " + e.ToString(), "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }


        public void excelAc(int irsaliyeID)
        {
            try
            {
                if(File.Exists(getPath(irsaliyeID))) // EXCEL DOSYASI VAR MI YOK MU?
                {
                    var excelApp = new Excel.Application();
                    excelApp.Visible = true;

                    Excel.Workbooks books = excelApp.Workbooks;
                    Excel.Workbook sheets = books.Open(getPath(irsaliyeID));

                    xlWorkBook.Close(true, Type.Missing, Type.Missing);
                }
                else
                {
                    MessageBox.Show("Excel dosyası bulunamadı. Silinmiş ve ya zarar görmüş olabilir.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(FileNotFoundException e)
            {
                MessageBox.Show("Hata ile karşılaşıldı. Hata: " + e.ToString(), "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        public string olustur(string[,] satirlar, int rowCount, int irsaliyeNo, int kontrol,string farkliKaydet) //kontrol 0-> irsaliyeolustur kontrol 1-> düzenle
        {


            int sayac = 0;
            string excelDosyaAdi = "";
            rowCount = rowCount + 11;
            //EXCEL DOSYASINDA İRSALİYE DETAY SATIR VE SUTUNLARI "A11"DEN BASLIYOR.
            //excelDosyaAdi.
            var tarihGetir = from getir in dc.IrsaliyeTablos where getir.IrsaliyeID == irsaliyeNo && getir.Sil == 1 select getir;
            //
            foreach (var item in tarihGetir)
            {
                excelDosyaAdi = item.IrsaliyeID.ToString() + "_" + item.Tarih.ToString().Remove(10) + "_" + item.PlakaNo + "_" + item.Soforler.AdiSoyadi;

                if (kontrol == 1)
                {
                    if (System.IO.File.Exists(@"C:\OZUGUCER\OzIrsaliye\Irsaliyeler\" + excelDosyaAdi + ".xlsx"))
                    {
                        //System.IO.File.Delete(@"C:\Users\Ozan\Desktop\faturaexcel\irsaliyeler\" + excelDosyaAdi + ".xlsx");
                        System.IO.File.Delete(@"C:\OZUGUCER\OzIrsaliye\Irsaliyeler\" + excelDosyaAdi + ".xlsx");
                    }
                }

                xlWorkSheet.Range["F2"].Value = "Adı Soyadı : " + item.Soforler.AdiSoyadi;
                xlWorkSheet.Range["F3"].Value = "Plaka No.  : " + item.PlakaNo;
                if (item.Soforler.HesapNo == "-Yok-" && item.Soforler.V_D == "-Yok-")
                {
                    xlWorkSheet.Range["F4"].Value = "V.D :                " + " Hes. No :           ";
                }
                else
                {
                    xlWorkSheet.Range["F4"].Value = "V.D : " + item.Soforler.V_D + " Hes. No : " + item.Soforler.HesapNo;
                }

                xlWorkSheet.Range["F5"].Value = "Tarih: " + item.Tarih.ToString().Remove(10); // SAAT DETAYI CIKARTILIYOR.
                xlWorkSheet.Range["F8"].Value = "NO: " + irsaliyeNo;

            }


        
                string[] sutun = { "A", "B", "C", "D", "E", "F", "G" };

                for (int i = 11; i < rowCount; i++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        /*if(k==0 && satirlar[sayac, k] == "0")
                         {
                             xlWorkSheet.Range[sutun[k] + i.ToString()].Value = null; // HAL NO BOS GECİLEMEZ.
                         }*/
                        if (k == 6)
                        {
                            decimal deger = Convert.ToDecimal(satirlar[sayac, k]);
                            xlWorkSheet.Range[sutun[k] + i.ToString()].Value = String.Format("{0:C}", deger);
                        }
                        else
                        {
                            xlWorkSheet.Range[sutun[k] + i.ToString()].Value = satirlar[sayac, k];
                        }

                    }
                    sayac++;
                }

            if(farkliKaydet!=null)
            {
               xlWorkBook.SaveAs(@""+farkliKaydet);
            }
            else
            {

                //xlWorkBook.SaveAs(@"C:\Users\Ozan\Desktop\faturaexcel\irsaliyeler\" + excelDosyaAdi + ".xlsx");
                xlWorkBook.SaveAs(@"C:\OZUGUCER\OzIrsaliye\Irsaliyeler\" + excelDosyaAdi + ".xlsx");
            }
                
                xlWorkBook.Close(true, Type.Missing, Type.Missing);
                xlap.Quit();



                return "Excel dosyası oluşturuldu.";
            }
        }
    }



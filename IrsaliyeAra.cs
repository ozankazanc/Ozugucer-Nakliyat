using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using OZIRSALIYE.DB;
using OZIRSALIYE.OzClass;
using System.Globalization;

namespace OZIRSALIYE
{
    public partial class IrsaliyeAra : DevExpress.XtraEditors.XtraUserControl
    {
        
        private static IrsaliyeAra _instance;
        private int click_irsaliyeID=0;
        
        public IrsaliyeAra()
        {
            InitializeComponent();
            Kontrol.ListeleriGuncelle();
        }
        public static IrsaliyeAra Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IrsaliyeAra();

                return _instance;
            }

         }
        public void Guncelle(int id)
        {
            
            Instance.BringToFront();
            dgC_Arama.DataSource = from getir in Kontrol.IrsaliyeTablo
                                   orderby getir.IrsaliyeID descending
                                   where getir.IrsaliyeID==id
                                   select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi };
            DgBilgiDoldur(id);

        }
        private void DgBilgiDoldur(int click_irsaliyeID)
        {

            var sonucDetay = from getir in Kontrol.IrsaliyeDetay
                             where getir.IrsaliyeID == click_irsaliyeID && getir.Sil == 1
                             select new { getir.HalNo, getir.Gonderen, getir.Kilogram, getir.Adet, getir.Cinsi, getir.SandıkNevi, getir.Fiyat };

            var sonucTablo = Kontrol.IrsaliyeTablo.Where(t => t.IrsaliyeID == click_irsaliyeID).Select(t => t);

            decimal sum = (decimal)Kontrol.IrsaliyeDetay.Where(t => t.IrsaliyeID == click_irsaliyeID && t.Sil == 1).Select(t => t.Fiyat).Sum();
            int count = Kontrol.IrsaliyeDetay.Where(t => t.IrsaliyeID == click_irsaliyeID && t.Sil == 1).Select(t => t).Count();

            foreach (var item in sonucTablo)
            {
                lblNo.Text = item.IrsaliyeID.ToString();
                lblTarih.Text = item.Tarih.ToString();
                lblSurucu.Text = item.Soforler.AdiSoyadi;
            }
            var hatirlatma = Kontrol.IrsaliyeDetay.Where(t => t.IrsaliyeID == click_irsaliyeID && t.Hatirlatma != null && t.Sil == 1).Select(t => t).Distinct();

            if (hatirlatma.Count() != 0)
            {
                foreach (var item in hatirlatma)
                    lblHatirlatma.Text = item.Hatirlatma;
            }
            else
            {
                lblHatirlatma.Text = "Girilmemiş";
            }




            lblUrunT.Text = count.ToString();

            lblFiyatT.Text = String.Format("{0:C}", sum).ToString();
            dgC_İrsaliyeBilgi.DataSource = sonucDetay;

        }

     

        private void btnGizle_Click(object sender, EventArgs e)
        {

            btnGoster.Visible = true;
            gpArama.Location = new Point(71, 12);
            gpArama.Width = 869;
            dgC_Arama.Width = 859;
            gpAramaSecenek.Visible = false;


        }

        private void btnGoster_Click(object sender, EventArgs e)
        {
            gpAramaSecenek.Visible = true;
            btnGoster.Visible = false;
            gpArama.Location = new Point(451, 12); 
            dgC_Arama.Width = 479;
            gpArama.Width = 489;
            

        }

        private void btnGizle2_Click(object sender, EventArgs e)
        {
            gpBilgi.Visible = false;
            gpSecenek.Visible = false;
            btnGoster2.Visible = true;
            dgC_İrsaliyeBilgi.Location = new Point(5, 45);
            dgC_İrsaliyeBilgi.Height = 490;


        }

        private void btnGoster2_Click(object sender, EventArgs e)
        {
            gpBilgi.Visible = true;
            gpSecenek.Visible = true;
            btnGoster2.Visible = false;
            dgC_İrsaliyeBilgi.Location = new Point(5, 166);
            dgC_İrsaliyeBilgi.Height = 381;
        }

        private void barDonem_EditValueChanged(object sender, EventArgs e)
        {
            int Months = 1 - DateTime.Now.Day;

            if (barDonem.Value == 0)
            {
                DateTime startDateTime = DateTime.Today; //Today at 00:00:00
                DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59

                dgC_Arama.DataSource = (from getir in Kontrol.IrsaliyeTablo.OrderByDescending(n => n.Tarih)
                                        where (getir.Tarih >= startDateTime && getir.Tarih <= endDateTime)
                                        select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi });

            }
            else if (barDonem.Value == 1)
            {
                dgC_Arama.DataSource = from getir in Kontrol.IrsaliyeTablo
                                       orderby getir.IrsaliyeID descending
                                       where DateTime.Now.AddDays(-7) < getir.Tarih && getir.Sil == 1
                                       select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi };
            }
            else if (barDonem.Value == 2)
            {
                dgC_Arama.DataSource = from getir in Kontrol.IrsaliyeTablo
                                       orderby getir.IrsaliyeID descending
                                       where DateTime.Now.AddDays(Months) < getir.Tarih && getir.Sil == 1
                                       select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi };

            }
            else
            {
                dgC_Arama.DataSource = from getir in Kontrol.IrsaliyeTablo
                                       orderby getir.IrsaliyeID descending
                                       where DateTime.Now.AddYears(-1) < getir.Tarih && getir.Tarih < DateTime.Now.AddYears(1) && getir.Sil == 1
                                       select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi };

            }
        }

        DateTime baslangic;
        private void dtBaslangic_EditValueChanged(object sender, EventArgs e)
        {
            baslangic = dtBaslangic.DateTime;
            dgC_Arama.DataSource = from getir in Kontrol.IrsaliyeTablo
                                   orderby getir.IrsaliyeID descending
                                   where baslangic < getir.Tarih && getir.Sil == 1
                                   select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi };


        }

        private void dtBitis_EditValueChanged(object sender, EventArgs e)
        {
            if (baslangic == null)
            {

            }
            else
            {
                dgC_Arama.DataSource = from getir in Kontrol.IrsaliyeTablo
                                       orderby getir.IrsaliyeID descending
                                       where baslangic < getir.Tarih && getir.Tarih < dtBitis.DateTime && getir.Sil == 1
                                       select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi };


            }
        }

        private void txIrsaliyeNoAra_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txIrsaliyeNoAra.Text == "")
                {
                    var goster = from getir in Kontrol.IrsaliyeTablo
                                 where getir.Sil == 1
                                 orderby getir.IrsaliyeID descending
                                 select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi };

                    dgC_Arama.DataSource = goster;
                }
                else
                {
                    int no = Convert.ToInt32(txIrsaliyeNoAra.Text);



                    var getirNo = from getir in Kontrol.IrsaliyeTablo
                                  orderby getir.IrsaliyeID descending
                                  where getir.IrsaliyeID == no && getir.Sil == 1
                                  select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi };

                    dgC_Arama.DataSource = getirNo;
                    
                    DgBilgiDoldur(no);
                }
            }
            catch (Exception)
            { }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            string ara = txAra.Text;

            if (cbDonemler.SelectedIndex == 0)
            {//GONDEREN
                var goster = (from getir in Kontrol.IrsaliyeDetay
                              join getir2 in Kontrol.IrsaliyeTablo
                              on getir.IrsaliyeID equals getir2.IrsaliyeID
                              where getir.Gonderen.Contains(ara) && getir.Sil == 1
                              select new { getir2.IrsaliyeID, getir2.PlakaNo, getir2.Tarih, getir2.Soforler.AdiSoyadi }).Distinct();

                if (goster.Count() == 0)
                { MessageBox.Show("Sonuç bulunamamıştır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                dgC_Arama.DataSource = goster;
            }
            else if (cbDonemler.SelectedIndex == 1)
            { //CİNSİ
                var goster = (from getir in Kontrol.IrsaliyeDetay
                              join getir2 in Kontrol.IrsaliyeTablo
                              on getir.IrsaliyeID equals getir2.IrsaliyeID
                              where getir.Cinsi.Contains(ara) && getir.Sil == 1 && getir2.Sil == 1
                              select new { getir2.IrsaliyeID, getir2.PlakaNo, getir2.Tarih, getir2.Soforler.AdiSoyadi }).Distinct();

                if (goster.Count() == 0)
                { MessageBox.Show("Sonuç bulunamamıştır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                dgC_Arama.DataSource = goster;

            }
            else if (cbDonemler.SelectedIndex == 2)
            {//SANDIKNEVİ
                var goster = (from getir in Kontrol.IrsaliyeDetay
                              join getir2 in Kontrol.IrsaliyeTablo
                              on getir.IrsaliyeID equals getir2.IrsaliyeID
                              where getir.SandıkNevi.Contains(ara) && getir.Sil == 1 && getir2.Sil == 1
                              select new { getir2.IrsaliyeID, getir2.PlakaNo, getir2.Tarih, getir2.Soforler.AdiSoyadi }).Distinct();

                if (goster.Count() == 0)
                { MessageBox.Show("Sonuç bulunamamıştır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                dgC_Arama.DataSource = goster;
            }
            else
            {//SÜRÜCÜADISOYADI
                var goster = (from getir in Kontrol.Suruculer
                              join getir2 in Kontrol.IrsaliyeTablo
                              on getir.SoforID equals getir2.SoforID
                              where getir.AdiSoyadi.Contains(ara) && getir.Sil == 1 && getir2.Sil == 1
                              select new { getir2.IrsaliyeID, getir2.PlakaNo, getir2.Tarih, getir2.Soforler.AdiSoyadi }).Distinct();

                if (goster.Count() == 0)
                { MessageBox.Show("Sonuç bulunamamıştır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                dgC_Arama.DataSource = goster;
            }

        }

        private void btnHepsiniGoster_Click(object sender, EventArgs e)
        {
            dgC_Arama.DataSource = from getir in Kontrol.IrsaliyeTablo
                                   orderby getir.IrsaliyeID descending
                                   where getir.Sil==1
                                   select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi };
        }

        

        private void btnExcelAc_Click(object sender, EventArgs e)
        {
            if (dgArama.RowCount != 0)
            {
                ExcelLib excelTest = new ExcelLib();
                if (click_irsaliyeID != 0)
                {
                    excelTest.excelAc(Convert.ToInt32(click_irsaliyeID.ToString()));
                }
                else
                {
                    MessageBox.Show("İrsaliye Seçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Listeden irsaliye seçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

           

            
        }

        private void btnYazdır_Click(object sender, EventArgs e)
        {
            if (dgArama.RowCount != 0)
            {
                ExcelYazdir yazdir = new ExcelYazdir();
                if (click_irsaliyeID != 0)
                {
                    yazdir.PrintMyExcelFile(Convert.ToInt32(click_irsaliyeID.ToString()));
                }
                else
                {
                    MessageBox.Show("İrsaliye Seçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("Listeden irsaliye seçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


           
           
        }

        private void dgArama_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            //Gridte tıklanan satırın id'si çekiliyor.
            click_irsaliyeID = Convert.ToInt32(dgArama.GetRowCellValue(e.FocusedRowHandle, colIrsaliyeNo));
            DgBilgiDoldur(click_irsaliyeID);


        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if(dgArama.RowCount!=0)
            {
                Kontrol.gonderID = click_irsaliyeID;
                IrsaliyeDuzenle.Instance.BringToFront();
                IrsaliyeDuzenle.Instance.GridDoldur(Kontrol.gonderID);
            }
            else
            {
                MessageBox.Show("Listeden irsaliye seçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                
         }

        private void btnFarkliKaydet_Click(object sender, EventArgs e)
        {
            if(click_irsaliyeID==0)
            {
                MessageBox.Show("Listeden irsaliye seçiniz","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            }
            else
            {
                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFileDialog1.Title = "Excel Dosyasını Kaydet";
                saveFileDialog1.DefaultExt = "xlsx";
                saveFileDialog1.Filter = "Excel |*.xlsx";
                saveFileDialog1.FileName = click_irsaliyeID.ToString();



                DialogResult result = saveFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {


                    int rowCount = dgSonucGoster.RowCount;
                    string[,] satirlar = new string[rowCount, 7];


                    // DATAGRID ICERİSİNDEKİ HÜCRELERİ matrise ATMA
                    for (int i = 0; i < rowCount; i++)
                    {

                        if (dgSonucGoster.GetRowCellValue(i, colHalNo) == null) { satirlar[i, 0] = null; ; } // HAL NO BOŞ GEÇİLEMEZ
                        else { satirlar[i, 0] = dgSonucGoster.GetRowCellValue(i, colHalNo).ToString().ToUpper(); }
                        if (dgSonucGoster.GetRowCellValue(i, colGonderen) == null) { satirlar[i, 1] = null; }
                        else { satirlar[i, 1] = dgSonucGoster.GetRowCellValue(i, colGonderen).ToString().ToUpper(); }
                        if (dgSonucGoster.GetRowCellValue(i, colKilogram) == null) { satirlar[i, 2] = null; }
                        else { satirlar[i, 2] = dgSonucGoster.GetRowCellValue(i, colKilogram).ToString().ToUpper(); }
                        if (dgSonucGoster.GetRowCellValue(i, colAdet) == null) { satirlar[i, 3] = null; }
                        else { satirlar[i, 3] = dgSonucGoster.GetRowCellValue(i, colAdet).ToString().ToUpper(); }
                        if (dgSonucGoster.GetRowCellValue(i, colCinsi) == null) { satirlar[i, 4] = null; }
                        else { satirlar[i, 4] = dgSonucGoster.GetRowCellValue(i, colCinsi).ToString().ToUpper(); }
                        if (dgSonucGoster.GetRowCellValue(i, colSandikNevi) == null) { satirlar[i, 5] = null; }
                        else { satirlar[i, 5] = dgSonucGoster.GetRowCellValue(i, colSandikNevi).ToString().ToUpper(); }
                        if (dgSonucGoster.GetRowCellValue(i, colFiyat) == null) { satirlar[i, 6] = null; }
                        else { satirlar[i, 6] = dgSonucGoster.GetRowCellValue(i, colFiyat).ToString().ToUpper(); }

                    }
                    ExcelLib excelOlustur = new ExcelLib();
                    excelOlustur.olustur(satirlar, rowCount, click_irsaliyeID, 1, saveFileDialog1.FileName);
                }

            }
            
            
           
            }

        }
    }
    
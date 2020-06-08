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

namespace OZIRSALIYE
{
    public partial class IrsaliyeOlustur : DevExpress.XtraEditors.XtraUserControl
    {
        private static IrsaliyeOlustur _instance;
        BaglantiDataContext dc;
        
       

        public int irsaliyeNo;
        
        public IrsaliyeOlustur()
        {
            dc = new BaglantiDataContext();
            InitializeComponent();
            bindingSource1.DataSource = typeof(IrsaliyeModel);
            this.gridControl1.DataSource = bindingSource1;
        }
        public static IrsaliyeOlustur Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IrsaliyeOlustur();


                return _instance;
            }
        }

        class IrsaliyeModel
        {
            public string HalNo { get; set; }
            public string Gonderen { get; set; }
            public int Kilogram { get; set; }
            public int Adet { get; set; }
            public string Cinsi { get; set; }
            public string SandikNevi { get; set; }
            public decimal Fiyat { get; set; }

        }

        private void IrsaliyeOlustur_Load(object sender, EventArgs e)
        {
            Kontrol obj = new Kontrol();
            irsaliyeNo = obj.GetIrsaliyeID_Son();
            lblIrsaliyeNo.Text = irsaliyeNo.ToString();
            dtTarih.EditValue = DateTime.Now;
        }

       
        private void IrsaliyeTemizle(int control)
        {
            DialogResult onay = new DialogResult();

          

            if (control == 0)
            {
                for (int i = 0; i < dgIrsaliyeOlustur.RowCount + 1; i++)
                {
                    dgIrsaliyeOlustur.DeleteRow(0);
                }

                txAdiSoyadi.Text = "";
                txHesapNo.Text = "";
                txPlakaNo.Text = "";
                txVD.Text = "";
                txHatirlatma.Text = "İrsaliye not bırak";
                dtTarih.EditValue = DateTime.Now;
            }
            else
            {
                onay = MessageBox.Show("Sayfanın tamamı temizlenecek, onaylıyor musunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (onay == DialogResult.Yes)
                {

                    int test = dgIrsaliyeOlustur.RowCount;
                    for (int i = 0; i <= test; i++)
                    {
                        dgIrsaliyeOlustur.DeleteRow(0);
                    }


                    txAdiSoyadi.Text = "";
                    txHesapNo.Text = "";
                    txPlakaNo.Text = "";
                    txVD.Text = "";
                    txHatirlatma.Text = "İrsaliye not bırak";
                    dtTarih.EditValue = dtTarih.DateTime;
                }
            }

        }
        private void IrsaliyeKaydet(int control)
        {
            bool kontrol1 = false, kontrol2 = false ;
            int rowCount = dgIrsaliyeOlustur.RowCount - 1;
            string[,] satirlar = new string[rowCount, 7];


            // DATAGRID ICERİSİNDEKİ HÜCRELERİ matrise ATMA
            for (int i = 0; i < rowCount; i++)
            {
                kontrol2 = true; // İÇERİYE HİÇ GİRMEZ İSE YANİ GRİDE HİÇ VERİ GİRİLMEZ İSE.
                if (dgIrsaliyeOlustur.GetRowCellValue(i, colHalNo) == null){ kontrol1 = true; } // HAL NO BOŞ GEÇİLEMEZ
                else { satirlar[i, 0] = dgIrsaliyeOlustur.GetRowCellValue(i, colHalNo).ToString().ToUpper(); }
                if (dgIrsaliyeOlustur.GetRowCellValue(i, colGonderen) == null) { satirlar[i, 1] = null; }
                else { satirlar[i, 1] = dgIrsaliyeOlustur.GetRowCellValue(i, colGonderen).ToString().ToUpper(); }
                if (dgIrsaliyeOlustur.GetRowCellValue(i, colKilogram) == null) { satirlar[i, 2] = null; }
                else { satirlar[i, 2] = dgIrsaliyeOlustur.GetRowCellValue(i, colKilogram).ToString().ToUpper(); }
                if (dgIrsaliyeOlustur.GetRowCellValue(i, colAdet) == null) { satirlar[i, 3] = null; }
                else { satirlar[i, 3] = dgIrsaliyeOlustur.GetRowCellValue(i, colAdet).ToString().ToUpper(); }
                if (dgIrsaliyeOlustur.GetRowCellValue(i, colCinsi) == null) { satirlar[i, 4] = null; }
                else { satirlar[i, 4] = dgIrsaliyeOlustur.GetRowCellValue(i, colCinsi).ToString().ToUpper(); }
                if (dgIrsaliyeOlustur.GetRowCellValue(i, colSandikNevi) == null) { satirlar[i, 5] = null; }
                else { satirlar[i, 5] = dgIrsaliyeOlustur.GetRowCellValue(i, colSandikNevi).ToString().ToUpper(); }
                if (dgIrsaliyeOlustur.GetRowCellValue(i, colFiyat) == null) { satirlar[i, 6] = null; }
                else { satirlar[i, 6] = dgIrsaliyeOlustur.GetRowCellValue(i, colFiyat).ToString().ToUpper(); }

                if(kontrol1==true)
                { i = rowCount + 1; }

            }

            if(txAdiSoyadi.Text=="" || txAdiSoyadi.Text == " " || txPlakaNo.Text == "" || txPlakaNo.Text == " " || kontrol1 == true || kontrol2==false)
            {
                MessageBox.Show("[Adı Soyadı], [Plaka No] ve [Hal No] alanları boş geçilemez.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Kontrol obj = new Kontrol();
                int soforID = obj.soforKontrol(txAdiSoyadi.Text);

                IrsaliyeTablo irsaliye = new IrsaliyeTablo()
                {
                    PlakaNo = txPlakaNo.Text,
                    SoforID = soforID,
                    Tarih = dtTarih.DateTime,
                    Sil = 1
                };
                dc.IrsaliyeTablos.InsertOnSubmit(irsaliye);
                dc.SubmitChanges();

                for (int i = 0; i < rowCount; i++)
                {

                    IrsaliyeDetay detay = new IrsaliyeDetay();
                    detay.IrsaliyeID = irsaliyeNo;

                    if (satirlar[i, 0] == null) { detay.HalNo = null; } else { detay.HalNo = Convert.ToInt32(satirlar[i, 0].ToString()); }
                    if (satirlar[i, 1] == null) { detay.Gonderen = null; } else { detay.Gonderen = satirlar[i, 1]; }
                    if (satirlar[i, 2] == null) { detay.Kilogram = null; } else { detay.Kilogram = Convert.ToInt32(satirlar[i, 2].ToString()); }
                    if (satirlar[i, 3] == null) { detay.Adet = null; } else { detay.Adet = Convert.ToInt32(satirlar[i, 3].ToString()); }
                    if (satirlar[i, 4] == null) { detay.Cinsi = null; } else { detay.Cinsi = satirlar[i, 4]; }
                    if (satirlar[i, 5] == null) { detay.SandıkNevi = null; } else { detay.SandıkNevi = satirlar[i, 5]; }
                    if (satirlar[i, 6] == null) { detay.Fiyat = null; } else { detay.Fiyat = Convert.ToDecimal(satirlar[i, 6].ToString()); }

                    if (txHatirlatma.Text == "İrsaliyeye not bırak") { detay.Hatirlatma = null; } else { detay.Hatirlatma = txHatirlatma.Text; }

                    detay.Sil = 1;

                    dc.IrsaliyeDetays.InsertOnSubmit(detay);
                    dc.SubmitChanges();
                }

                Kontrol.ListeleriGuncelle();

                ExcelLib excelYaz = new ExcelLib();
                MessageBox.Show(excelYaz.olustur(satirlar, rowCount, irsaliyeNo,0,null),"Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);

                lblIrsaliyeNo.Text = obj.GetIrsaliyeID_Son().ToString();
                irsaliyeNo = obj.GetIrsaliyeID_Son();
                IrsaliyeTemizle(0);

                if (control == 0)
                {
                    //Sadece Kaydet
                }
                else if (control == 1)
                {
                    excelYaz.excelAc();
                }
                else
                {
                    var result = Kontrol.IrsaliyeDetay.OrderByDescending(x => x.IrsaliyeID).Where(x => x.Sil == 1).First();
                    ExcelYazdir yazdir = new ExcelYazdir();
                    yazdir.PrintMyExcelFile((int)result.IrsaliyeID);
                }
            }

           
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            IrsaliyeKaydet(0);
        }
        private void btnYazdır_Click(object sender, EventArgs e)
        {
            IrsaliyeKaydet(1);
        }
        private void btnExcelAc_Click(object sender, EventArgs e)
        {
            IrsaliyeKaydet(2);
        }

        
        int sayac = 0;
        private void btnSurucuDegistir_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (sayac < Kontrol.SurucuListesi.Count)
                {

                    txAdiSoyadi.Text = Kontrol.SurucuListesi[sayac];
                    sayac++;
                }
                else
                {
                    sayac = 0;
                    txAdiSoyadi.Text = Kontrol.SurucuListesi[sayac];
                    sayac++;
                }
            }
            catch (Exception)
            {
            }
        }
       
        int sayac2 = 0;
        private void btnPlakaDegistir_Click(object sender, EventArgs e)
        {
            try
            {
                if (sayac2 < Kontrol.PlakaListesi.Count)
                {
                    txPlakaNo.Text = Kontrol.PlakaListesi[sayac2];
                    sayac2++;
                }
                else
                {
                    sayac2 = 0;
                    txPlakaNo.Text = Kontrol.PlakaListesi[sayac2];
                    sayac2++;
                }
            }
            catch (Exception)
            { }
        }

        private void txHatirlatma_Click(object sender, EventArgs e)
        {
            txHatirlatma.Text = "";
        }

        private void txHatirlatma_Leave(object sender, EventArgs e)
        {
            if(txHatirlatma.Text=="")
            {
                txHatirlatma.Text = "İrsaliyeye Not Bırak";
            }
        }

        private void btnSatirTemizle_Click(object sender, EventArgs e)
        {
            dgIrsaliyeOlustur.DeleteSelectedRows();
        }

        private void btnIrsaliyeTemizle_Click(object sender, EventArgs e)
        {
           IrsaliyeTemizle(1);
        }

        private void dgIrsaliyeOlustur_RowCountChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgIrsaliyeOlustur.RowCount == 26)
                {
                    MessageBox.Show("Bir irsaliyede en fazla 25 adet kayıt girelibilirsiniz.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgIrsaliyeOlustur.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                }
                else
                {
                    dgIrsaliyeOlustur.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
                }


            }
            catch(Exception)
            { }
        }

       
    }
}

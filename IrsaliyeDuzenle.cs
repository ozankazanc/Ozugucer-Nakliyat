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
    public partial class IrsaliyeDuzenle : DevExpress.XtraEditors.XtraUserControl
    {


        private static BaglantiDataContext dc = new BaglantiDataContext();
        private static IrsaliyeDuzenle _instance;




        List<int> kayitID = new List<int>();
        List<int> index = new List<int>();
        List<string> value = new List<string>();
        List<int> silID = new List<int>();
        List<int> ekleID = new List<int>();
        private string Hatirlatma="";
        public static IrsaliyeDuzenle Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IrsaliyeDuzenle();

                return _instance;
            }

        }

        private void GridBilgiAlveYazdir()
        {
            int rowCount = dgSonucGoster.RowCount - 1;
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
            excelOlustur.olustur(satirlar, rowCount, Kontrol.gonderID, 1, null);
        }


        public void TooltipDoldur()
        {
            toolTip1.Active = true;
            toolTip1.SetToolTip(cbSuruculer, "Sürücü eğer listede yok ise [Sürücüler]  başlığından ekleyiniz.");
        }
        public IrsaliyeDuzenle()
        {
            InitializeComponent();

            gpSecenekler.Visible = true;
            gpDetay.Visible = false;
        }
        private void DetayDoldur(int id)
        {

            var sonucTablo = Kontrol.IrsaliyeTablo.Where(t => t.IrsaliyeID == id).Select(t => t);

            decimal sum = (decimal)Kontrol.IrsaliyeDetay.Where(t => t.IrsaliyeID == id).Select(t => t.Fiyat).Sum();
            int count = Kontrol.IrsaliyeDetay.Where(t => t.IrsaliyeID == id).Select(t => t).Count();
            
            cbSuruculer.Properties.Items.Clear();
            foreach (var item in Kontrol.SurucuListesi)
            {
                cbSuruculer.Properties.Items.Add(item);
            }


            foreach (var item in sonucTablo)
            {
                lblNo.Text = item.IrsaliyeID.ToString();
                lblTarih.Text = item.Tarih.ToString();
                lblSurucu.Text = item.Soforler.AdiSoyadi;
                cbSuruculer.SelectedItem = item.Soforler.AdiSoyadi;
                txPlaka.Text = item.PlakaNo;

            }
            var hatirlatma = Kontrol.IrsaliyeDetay.Where(t => t.IrsaliyeID == id && t.Hatirlatma != null).Select(t => t).Distinct();

            if (hatirlatma.Count() != 0)
            {
                foreach (var item in hatirlatma)
                {
                    txHatirlatma.Text = item.Hatirlatma;
                    Hatirlatma = item.Hatirlatma;
                }
                    
            }
            else
            {
                txHatirlatma.Text = "";
            }


            lblUrunT.Text = count.ToString();
            lblFiyatT.Text = String.Format("{0:C}", sum).ToString();
        }
        public void GridDoldur(int id)
        {
            if (Kontrol.gonderID == -1)
            {
                gpSecenekler.Visible = true;
                gpDetay.Visible = false;
            }
            else
            {
                gpSecenekler.Visible = false;
                gpDetay.Visible = true;

                bindingSource1.DataSource = (from getir in Kontrol.IrsaliyeDetay where getir.IrsaliyeID == id select getir).ToList();
                dgC_İrsaliyeBilgi.DataSource = bindingSource1;
                DetayDoldur(Kontrol.gonderID);

            }
        }
        private void KayitlariSil()
        {
            while (dgSonucGoster.RowCount != 0)
            {
                dgSonucGoster.SelectAll();
                dgSonucGoster.DeleteSelectedRows();
            }
        }
        private void Sifirla()
        {
            kayitID.Clear(); index.Clear(); value.Clear(); silID.Clear(); ekleID.Clear();
            KayitlariSil();
            controlID = 0;
            Kontrol.gonderID = -1;
            gpSecenekler.Visible = true;
            gpDetay.Visible = false;
        }
        private void btnDuzenle_Click(object sender, EventArgs e)
        {

            
            try
            {
              
                if(silID.Count==0 && ekleID.Count==0 && index.Count==0)
                {
                    MessageBox.Show("İrsaliye üzerinde değişiklik yapılmadı.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (txPlaka.Text != null)
                    {

                        ekleID = ekleID.Distinct().ToList();

                        if (silID.Count != 0)//SİLME
                        {

                            for (int i = 0; i < ekleID.Count; i++) //DATAGRIDTE YENİ KAYIT OLUŞTURULMUŞ VE VERİTABANINA GİTMEDEN SİLİNMİŞSE.
                            {
                                for (int j = 0; j < silID.Count; j++)
                                {
                                    if (ekleID[i] == silID[j])
                                    {
                                        ekleID.RemoveAt(i);
                                    }
                                }
                            }

                            for (int i = 0; i < silID.Count; i++)
                            {
                                IrsaliyeDetay sil = dc.IrsaliyeDetays.FirstOrDefault(x => x.Sil == 1 && x.KayitID == silID[i]);
                                if (sil != null)
                                {
                                    sil.Sil = 0;
                                }
                                dc.SubmitChanges();
                            }

                        } //SİLME


                        if (index.Count != 0)//GÜNCELLEME
                        {
                            for (int i = 0; i < index.Count; i++)
                            {
                                IrsaliyeDetay duzenle = dc.IrsaliyeDetays.First(x => x.KayitID == kayitID[i]);

                                if (index[i] == 1) { duzenle.HalNo = Convert.ToInt32(value[i].ToString()); }
                                else if (index[i] == 2) { duzenle.Gonderen = value[i].ToUpper(); }
                                else if (index[i] == 3) { duzenle.Kilogram = Convert.ToInt32(value[i].ToString()); }
                                else if (index[i] == 4) { duzenle.Adet = Convert.ToInt32(value[i].ToString()); }
                                else if (index[i] == 5) { duzenle.Cinsi = value[i].ToUpper(); }
                                else if (index[i] == 6) { duzenle.SandıkNevi = value[i].ToUpper(); }
                                else if (index[i] == 7) { duzenle.Fiyat = Convert.ToDecimal(value[i].ToString()); }
                                dc.SubmitChanges();

                            }
                        } // GÜNCELLEME

                        IrsaliyeTablo guncelleGenel = dc.IrsaliyeTablos.FirstOrDefault(x => x.IrsaliyeID == Kontrol.gonderID);
                        guncelleGenel.PlakaNo = txPlaka.Text.ToUpper();

                        var suruculer = from getir in Kontrol.Suruculer select getir;
                        int surucuid = 0;
                        foreach (var item in suruculer)
                        {
                            if (item.AdiSoyadi == cbSuruculer.SelectedItem.ToString())
                                surucuid = item.SoforID;

                        }
                        guncelleGenel.SoforID = surucuid;

                        dc.SubmitChanges();

                        if (ekleID.Count != 0)
                        {
                            int sayac = 0;

                            string[,] satirlar = new string[ekleID.Count, 7];

                            for (int i = (dgSonucGoster.RowCount - 1); i >= 0; i--)
                            {
                                int test = Convert.ToInt32(dgSonucGoster.GetRowCellValue(i, colKayitNo));
                                if (ekleID.Contains(test))
                                {
                                    if (dgSonucGoster.GetRowCellValue(i, colHalNo) == null) { satirlar[sayac, 0] = null; }
                                    else { satirlar[sayac, 0] = dgSonucGoster.GetRowCellValue(i, colHalNo).ToString().ToUpper(); }
                                    if (dgSonucGoster.GetRowCellValue(i, colGonderen) == null) { satirlar[sayac, 1] = null; }
                                    else { satirlar[sayac, 1] = dgSonucGoster.GetRowCellValue(i, colGonderen).ToString().ToUpper(); }
                                    if (dgSonucGoster.GetRowCellValue(i, colKilogram) == null) { satirlar[sayac, 2] = null; }
                                    else { satirlar[sayac, 2] = dgSonucGoster.GetRowCellValue(i, colKilogram).ToString().ToUpper(); }
                                    if (dgSonucGoster.GetRowCellValue(i, colAdet) == null) { satirlar[sayac, 3] = null; }
                                    else { satirlar[sayac, 3] = dgSonucGoster.GetRowCellValue(i, colAdet).ToString().ToUpper(); }
                                    if (dgSonucGoster.GetRowCellValue(i, colCinsi) == null) { satirlar[sayac, 4] = null; }
                                    else { satirlar[sayac, 4] = dgSonucGoster.GetRowCellValue(i, colCinsi).ToString().ToUpper(); }
                                    if (dgSonucGoster.GetRowCellValue(i, colSandikNevi) == null) { satirlar[sayac, 5] = null; }
                                    else { satirlar[sayac, 5] = dgSonucGoster.GetRowCellValue(i, colSandikNevi).ToString().ToUpper(); }
                                    if (dgSonucGoster.GetRowCellValue(i, colFiyat) == null) { satirlar[sayac, 6] = null; }
                                    else { satirlar[sayac, 6] = dgSonucGoster.GetRowCellValue(i, colFiyat).ToString().ToUpper(); }
                                    sayac++;
                                }

                                if (ekleID.Count == sayac)
                                {
                                    i = -1;
                                }

                            }

                            for (int i = (ekleID.Count - 1); i >= 0; i--)
                            {


                                IrsaliyeDetay detay = new IrsaliyeDetay();
                                detay.IrsaliyeID = Kontrol.gonderID;

                                if (satirlar[i, 0] == null) { detay.HalNo = null; } else { detay.HalNo = Convert.ToInt32(satirlar[i, 0].ToString()); }
                                if (satirlar[i, 1] == null) { detay.Gonderen = null; } else { detay.Gonderen = satirlar[i, 1].ToUpper(); }
                                if (satirlar[i, 2] == null) { detay.Kilogram = null; } else { detay.Kilogram = Convert.ToInt32(satirlar[i, 2].ToString()); }
                                if (satirlar[i, 3] == null) { detay.Adet = null; } else { detay.Adet = Convert.ToInt32(satirlar[i, 3].ToString()); }
                                if (satirlar[i, 4] == null) { detay.Cinsi = null; } else { detay.Cinsi = satirlar[i, 4].ToUpper(); }
                                if (satirlar[i, 5] == null) { detay.SandıkNevi = null; } else { detay.SandıkNevi = satirlar[i, 5].ToUpper(); }
                                if (satirlar[i, 6] == null) { detay.Fiyat = null; } else { detay.Fiyat = Convert.ToDecimal(satirlar[i, 6].ToString()); }

                                detay.Hatirlatma = txHatirlatma.Text;

                                detay.Sil = 1;

                                dc.IrsaliyeDetays.InsertOnSubmit(detay);
                                dc.SubmitChanges();
                            }
                        } //EKLEME


                        var goster = from getir in dc.IrsaliyeDetays where getir.IrsaliyeID == Kontrol.gonderID && getir.Sil == 1 select getir;

                        foreach (var item in goster)
                        {
                            IrsaliyeDetay guncelle = dc.IrsaliyeDetays.FirstOrDefault(x => x.KayitID == item.KayitID);
                            guncelle.Hatirlatma = txHatirlatma.Text;
                            dc.SubmitChanges();
                        }


                        GridBilgiAlveYazdir();

                        MessageBox.Show("Değişiklikler kaydedilmiştir.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Kontrol.ListeleriGuncelle();
                        IrsaliyeAra.Instance.Guncelle(Kontrol.gonderID);
                        Sifirla();

                    }
                    else
                    {
                        MessageBox.Show("Plaka alanı boş geçilemez.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Kritik bir hata ile karşılaşıldı.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        int controlID = 0;
        private void dgSonucGoster_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e) //UPDATE
        {
            int id = Convert.ToInt32(dgSonucGoster.GetRowCellValue(e.RowHandle, colKayitNo));

            if (id == 0)
            {

                dgSonucGoster.SetRowCellValue(e.RowHandle, colKayitNo, controlID);

            }
            else
            {
                if (id > 0) // 0 numaralı kayitID'ler yeni eklenen kayıtlar
                {

                    int ind = e.Column.VisibleIndex;
                    string val = e.Value.ToString();

                    kayitID.Add(id);
                    index.Add(ind);
                    value.Add(val);
                }
                else
                {
                    ekleID.Add(controlID);
                }
            }
            //  MessageBox.Show(kayitID[i].ToString() + " + " + index[i].ToString() + " + " + value[i].ToString());
        }
        private void btnKayitEkle_Click(object sender, EventArgs e)
        {

            int kontrol = dgSonucGoster.RowCount - 1;

            if (dgSonucGoster.GetRowCellValue(kontrol, colHalNo) == null)
            {
                MessageBox.Show("Hal numarası alanı boş geçilemez.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(dgSonucGoster.RowCount==25)
            {
                MessageBox.Show("Tek irsaliyede en fazla 25 adet kayıt oluşturulabilir.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dgSonucGoster.AddNewRow();
                controlID--;
            }

        }
        private void btnKayitSil_Click(object sender, EventArgs e)
        {
            int kontrol = (int)dgSonucGoster.GetRowCellValue(dgSonucGoster.FocusedRowHandle, colKayitNo);





            DialogResult dr = new DialogResult();

            if (dgSonucGoster.RowCount > 1)
            {
                silID.Add(kontrol);
                dgSonucGoster.DeleteSelectedRows();

            }
            else
            {
                dr = MessageBox.Show("Bütün kayıtları silmek üzeresiniz. İrsaliyenin tamamen silinmesini onaylıyor musunuz?", "ÖNEMLİ UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    silID.Add(kontrol);

                    var sil = dc.IrsaliyeDetays.Where(x => x.IrsaliyeID == Kontrol.gonderID).Select(x => x);

                    foreach (var item in sil)
                    {
                        item.Sil = 0;
                    }
                    dc.SubmitChanges();

                    IrsaliyeTablo siltablo = dc.IrsaliyeTablos.FirstOrDefault(x => x.IrsaliyeID == Kontrol.gonderID);
                    siltablo.Sil = 0;
                    dc.SubmitChanges();

                    Kontrol.ListeleriGuncelle();
                    IrsaliyeAra.Instance.Guncelle(Kontrol.IrsaliyeID_Son - 1);
                    Sifirla();

                }
            }

        }
        private void btnGit_Click_1(object sender, EventArgs e)
        {
            IrsaliyeAra.Instance.BringToFront();
        }
        private void btnNoGetir_Click_1(object sender, EventArgs e)
        {
            try
            {
                Kontrol.gonderID = Convert.ToInt32(txNo.Text);

                int sonuc = Kontrol.IrsaliyeDetay.Where(t => t.IrsaliyeID == Kontrol.gonderID).Select(t => t).Count();

                if (sonuc == 0)
                {
                    MessageBox.Show("Girdiğiniz numaraya ait irsaliye bulunamamıştır.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    GridDoldur(Kontrol.gonderID);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doğru değeri girdiğinizden emin olun.", "UYARI",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void btnDuzenleiptal_Click(object sender, EventArgs e)
        {
            kayitID.Clear(); index.Clear(); value.Clear(); silID.Clear(); ekleID.Clear();
            KayitlariSil();
            controlID = 0;
            GridDoldur(Kontrol.gonderID);
        }

        private void btnIrsaliyeSil_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();

            dr = MessageBox.Show("İrsaliyenin tamamen silinmesini onaylıyor musunuz?", "ÖNEMLİ UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {

                var sil = dc.IrsaliyeDetays.Where(x => x.IrsaliyeID == Kontrol.gonderID).Select(x => x);

                foreach (var item in sil)
                {
                    item.Sil = 0;
                }
                dc.SubmitChanges();

                IrsaliyeTablo siltablo = dc.IrsaliyeTablos.FirstOrDefault(x => x.IrsaliyeID == Kontrol.gonderID);
                siltablo.Sil = 0;
                dc.SubmitChanges();

                Kontrol.ListeleriGuncelle();
                IrsaliyeAra.Instance.Guncelle(Kontrol.IrsaliyeID_Son - 1);
                Sifirla();
            }
        }

        private void btnGetir2_Click(object sender, EventArgs e)
        {
            try
            {
                Kontrol.gonderID = Convert.ToInt32(txNo2.Text);

                int sonuc = Kontrol.IrsaliyeDetay.Where(t => t.IrsaliyeID == Kontrol.gonderID).Select(t => t).Count();

                if (sonuc == 0)
                {
                    MessageBox.Show("Girdiğiniz numaraya ait irsaliye bulunamamıştır.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    GridDoldur(Kontrol.gonderID);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doğru değeri girdiğinizden emin olun.", "UYARI",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        
    }
}

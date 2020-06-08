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
    public partial class SoforBilgileri : DevExpress.XtraEditors.XtraUserControl
    {
        private static SoforBilgileri _instance;
       
        private static BaglantiDataContext dc = new BaglantiDataContext();
        private int surucuID;
        public SoforBilgileri()
        {
            InitializeComponent();
            Guncelle();
            
        }
        public void Guncelle()
        {
            Kontrol.ListeleriGuncelle();
            dgC_Suruculer.DataSource = from getir in Kontrol.Suruculer select new { getir.AdiSoyadi, getir.HesapNo, getir.V_D, getir.SoforID };
        }
        public static SoforBilgileri Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SoforBilgileri();

                return _instance;
            }

        }

        private void txgAdiSoyadi_Click(object sender, EventArgs e)
        {
            if (txgAdiSoyadi.Text == "Sürücüyü listeden seçin")
                txgAdiSoyadi.Text = "";
        }

        private void txAra_EditValueChanged(object sender, EventArgs e)
        {
            if (txAra.Text != "Aramak istediğiniz sürücüyü yazın")
            {

                dgC_Suruculer.DataSource = from getir in Kontrol.Suruculer where getir.AdiSoyadi.Contains(txAra.Text) select new { getir.AdiSoyadi, getir.HesapNo, getir.V_D, getir.SoforID };


            }
            else if (txAra.Text == "")
            {
                dgC_Suruculer.DataSource = from getir in Kontrol.Suruculer select new { getir.AdiSoyadi, getir.HesapNo, getir.V_D, getir.SoforID };
            }
            else
                txgAdiSoyadi.Text = "";
        }
        private void txAra_Click(object sender, EventArgs e)
        {
            if (txAra.Text == "Aramak istediğiniz sürücüyü yazın")
            {
                txAra.Text = "";
            }
        }

        private void dgSuruculer_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            surucuID = Convert.ToInt32(dgSuruculer.GetRowCellValue(e.FocusedRowHandle, colSoforID));

            var surucu = from getir in Kontrol.Suruculer where getir.SoforID == surucuID select getir;

            foreach (var item in surucu)
            {
                txgAdiSoyadi.Text = item.AdiSoyadi;
                txgHesapNo.Text = item.HesapNo;
                txgVergiNo.Text = item.V_D;
                lbAdSoyad.Text = item.AdiSoyadi;
                lbHesapNo.Text = item.HesapNo;
                lbVergiNo.Text = item.V_D;
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string soforAdi = txeAdiSoyadi.Text, hesapNo = txeHesapNo.Text, vdNo = txeVergiNo.Text;

            if (soforAdi == "")
            { MessageBox.Show("Şöförün 'Adı ve Soyadı' alanı boş geçilemez"); }
            else
            {
                if (hesapNo == "")
                    hesapNo = "-Yok-";
                if (vdNo == "")
                    vdNo = "-Yok-";

                //vergi numarasına göre aynı söför veritabanında var mı?
                var kontrol = from getir in Kontrol.Suruculer where getir.V_D == vdNo && getir.V_D != "-Yok-" select getir;

                if (kontrol.Count() == 0)
                {
                    Soforler sofor = new Soforler()
                    {
                        AdiSoyadi = soforAdi,
                        HesapNo = hesapNo,
                        V_D = vdNo,
                        Sil = 1

                    };
                    dc.Soforlers.InsertOnSubmit(sofor);
                    dc.SubmitChanges();

                    MessageBox.Show("[ " + soforAdi + " ]" + "sürücüsü sisteme kaydedilmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txeAdiSoyadi.Text = "";
                    txeHesapNo.Text = "";
                    txeVergiNo.Text = "";

                    Guncelle();
                    dgC_Suruculer.DataSource = from getir in Kontrol.Suruculer select new { getir.AdiSoyadi, getir.HesapNo, getir.V_D, getir.SoforID };


                }
                else
                {
                    MessageBox.Show("Bu sürücü sistemde kayıtlıdır.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                Soforler sofor = dc.Soforlers.First(x => x.SoforID == surucuID);
                sofor.AdiSoyadi = txgAdiSoyadi.Text;
                sofor.HesapNo = txgHesapNo.Text;
                sofor.V_D = txgVergiNo.Text;
                dc.SubmitChanges();
                MessageBox.Show("Güncelleme gerçekleştirildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Guncelle();
            }
            catch (Exception)
            {
               
            }


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("[ " + lbAdSoyad.Text + " ] " + "sürücüsünü sistemden silmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == dr)
            {
                Soforler sofor = dc.Soforlers.First(x => x.SoforID == surucuID);
                sofor.Sil = 0;
                dc.SubmitChanges();
                Guncelle();
            }

        }

        private void btnGoster_Click(object sender, EventArgs e)
        {

            try
            {   //Sürücünün kaydedilip irsaliye işlenmemiş olabilir.
                int kontrol = Kontrol.IrsaliyeTablo.Where(x => x.SoforID == surucuID).Select(x => x).Count();
                if(kontrol==0)
                {
                    MessageBox.Show("Sürücünün bulunduğu irsaliye bulunamamıştır.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    IrsaliyeAra.Instance.BringToFront();
                    IrsaliyeAra.dgC_Arama.DataSource = from getir in Kontrol.IrsaliyeTablo
                                                       where getir.SoforID == surucuID
                                                       select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi };
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Hata ile karşılaşıldı. Alternatif çözüm olarak İrsaliye Ara tuşuna basıp işlemi tekrardan gerçekleştirin.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        private void txAra_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txAra.Text == "")
                {
                    var goster = from getir in Kontrol.Suruculer
                                 select new { getir.AdiSoyadi, getir.HesapNo, getir.V_D, getir.SoforID };

                    dgC_Suruculer.DataSource = goster;
                }
                else
                {
                   var getirNo = from getir in Kontrol.Suruculer
                                  where getir.AdiSoyadi.Contains(txAra.Text)
                                  select new { getir.AdiSoyadi, getir.HesapNo, getir.V_D, getir.SoforID };

                    dgC_Suruculer.DataSource = getirNo;
                }
            }
            catch (Exception)
            { }
        }
    }
}

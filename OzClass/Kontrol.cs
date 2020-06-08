using OZIRSALIYE.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OZIRSALIYE.OzClass
{
    class Kontrol
    {
        private static BaglantiDataContext dc;
        public static List<IrsaliyeTablo> IrsaliyeTablo { get; set; }
        public static List<IrsaliyeDetay> IrsaliyeDetay { get; set; }
        public static List<Soforler> Suruculer { get; set; }
        public static List<string> SurucuListesi { get; set; }
        public static List<string> PlakaListesi { get; set; }
        public static int IrsaliyeID_Son { get; set; }
        public static int gonderID { get; set; }
        public static int KayitID_Son { get; set; }
        
       
        public Kontrol()
        {
            ListeleriGuncelle();
            gonderID = -1;
        }

        public static void ListeleriGuncelle()
        {
            dc= new BaglantiDataContext();
            IrsaliyeTablo = (from getir in dc.IrsaliyeTablos where getir.Sil == 1 select getir).ToList();
            IrsaliyeDetay = (from getir in dc.IrsaliyeDetays where getir.Sil == 1 select getir).ToList();
            Suruculer = (from getir in dc.Soforlers where getir.Sil == 1 select getir).ToList();
            SurucuListesi = txSurucuDoldur();
            PlakaListesi = txPlakadoldur();
            IrsaliyeID_Son = GetIrsaliyeID();
            KayitID_Son = KayitID();
        }
        
        
        public int soforKontrol(string soforAdiSoyadi)
        {
            BaglantiDataContext dc = new BaglantiDataContext();
            
            int soforID=0;
           
            var soforKontrol = from getir in Suruculer where getir.AdiSoyadi == soforAdiSoyadi select getir.AdiSoyadi;

            if (soforKontrol.Count() == 0) //sofor sisteme kayıtlı değil ise
            {
                Soforler sofor = new Soforler()
                {
                    AdiSoyadi = soforAdiSoyadi,
                    V_D = "-Yok-",
                    HesapNo = "-Yok-",
                    Sil = 1

                };

                dc.Soforlers.InsertOnSubmit(sofor);
                dc.SubmitChanges();

                MessageBox.Show(soforAdiSoyadi + " sürücüsü sisteme kaydedilmiştir.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

            ListeleriGuncelle();
            SoforBilgileri.Instance.Guncelle();

            var yeniSoforID = from getir in Suruculer where getir.AdiSoyadi == soforAdiSoyadi select getir.SoforID;
            foreach (var item in yeniSoforID)
            {
                soforID = item;
            }
            return soforID;
        }
        private static int GetIrsaliyeID() //irsaliye no çekme
        {
            int irsaliyeNo = 0;
            var irsaliyeID = from getir in IrsaliyeTablo where getir.Sil == 1 orderby getir.IrsaliyeID descending select getir;

            foreach (var getir in irsaliyeID)
            {
                irsaliyeNo = getir.IrsaliyeID + 1;
                break;
            }

            return irsaliyeNo;
        }
        public int GetIrsaliyeID_Son()
        {

            int irsaliyeNo = 0;
            var irsaliyeID = from getir in IrsaliyeTablo where getir.Sil == 1 orderby getir.IrsaliyeID descending select getir;

            foreach (var getir in irsaliyeID)
            {
                irsaliyeNo = getir.IrsaliyeID + 1;
                break;
            }

            return irsaliyeNo;
        }
        private static List<string> txSurucuDoldur()
        {
            var surucuListesi = (from getir in Suruculer select getir.AdiSoyadi).ToList();

            return surucuListesi;
        }
        private static List<string> txPlakadoldur()
        {
            var plakaListesi = (from getir in IrsaliyeTablo select getir.PlakaNo).Distinct().ToList();
            
            return plakaListesi;
        }

        private static int KayitID()
        {
            int kayitID = 0;
            var irsaliyeID = from getir in dc.IrsaliyeDetays orderby getir.KayitID descending select getir;

            foreach (var getir in irsaliyeID)
            {
                kayitID = getir.KayitID;
                break;
            }

            return (kayitID);

        }

    }
}

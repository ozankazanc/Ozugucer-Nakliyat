using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OZIRSALIYE.DB;
using OZIRSALIYE.OzClass;

namespace OZIRSALIYE
{
    public partial class FormMain : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        DB.BaglantiDataContext dc = new DB.BaglantiDataContext();
        Kontrol kontrol = new Kontrol();
        public FormMain()
        {
            InitializeComponent();
            addInstance();
        
        }
        
        private void addInstance()
        {
            Container.Controls.Add(IrsaliyeOlustur.Instance);
            Container.Controls.Add(IrsaliyeAra.Instance);
            Container.Controls.Add(SoforBilgileri.Instance);
            Container.Controls.Add(IrsaliyeDuzenle.Instance);
        }

       

        private void accIrsaliyeOlustur_Click(object sender, EventArgs e)
        {
            if (!Container.Controls.Contains(IrsaliyeOlustur.Instance))
            {
                //Container.Controls.Add(IrsaliyeOlustur.Instance);
                IrsaliyeOlustur.Instance.Dock = DockStyle.Fill;
                IrsaliyeOlustur.Instance.BringToFront();

            }
            IrsaliyeOlustur.Instance.BringToFront();
        }

        private void accIrsaliyeAra_Click(object sender, EventArgs e)
        {

            if (!Container.Controls.Contains(IrsaliyeAra.Instance))
            {
                //Container.Controls.Add(IrsaliyeAra.Instance);
                IrsaliyeAra.Instance.Dock = DockStyle.Fill;
                IrsaliyeAra.Instance.BringToFront();

            }
            IrsaliyeAra.Instance.BringToFront();
        }

        private bool state=true;
        private void accordionControl1_StateChanged(object sender, EventArgs e)
        {
            if(state==true)
            {
                FormMain.ActiveForm.Width = 1050;
                state = false;
            }
            else
            {
                FormMain.ActiveForm.Width = 1250;
                state = true;
            }
        }

        private void accSoforBilgileri_Click(object sender, EventArgs e)
        {
            if (!Container.Controls.Contains(SoforBilgileri.Instance))
            {
               // Container.Controls.Add(SoforBilgileri.Instance);
                SoforBilgileri.Instance.Dock = DockStyle.Fill;
                SoforBilgileri.Instance.BringToFront();

            }
            SoforBilgileri.Instance.BringToFront();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (IrsaliyeAra.Instance == null)
                {
                    MessageBox.Show("İlk olarak İrsaliye Ara tuşuna basın", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    

                    IrsaliyeAra.Instance.BringToFront();
                    IrsaliyeAra.dgC_Arama.DataSource = from getir in dc.IrsaliyeTablos where getir.IrsaliyeID == (Kontrol.IrsaliyeID_Son-1) select new { getir.IrsaliyeID, getir.PlakaNo, getir.Tarih, getir.Soforler.AdiSoyadi };
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata ile karşılaşıldı. Alternatif çözüm olarak İrsaliye Ara tuşuna basıp işlemi tekrardan gerçekleştirin.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void accIrsaliyeDuzenle_Click(object sender, EventArgs e)
        {
            if (!Container.Controls.Contains(IrsaliyeDuzenle.Instance))
            {
                //Container.Controls.Add(IrsaliyeDuzenle.Instance);
                IrsaliyeDuzenle.Instance.Dock = DockStyle.Fill;
                IrsaliyeDuzenle.Instance.BringToFront();

            }
            IrsaliyeDuzenle.Instance.BringToFront();
        }
    }
}

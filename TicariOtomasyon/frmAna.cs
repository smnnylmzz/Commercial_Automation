using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyon
{
    public partial class frmAna : Form
    {
        public frmAna()
        {
            InitializeComponent();
        }


        frmanasayfa anasayfa;
        private void btnanasayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(anasayfa == null)
            {
                anasayfa = new frmanasayfa();
                anasayfa.MdiParent = this;
                anasayfa.Show();
            }
        }
        frmUrunler fr;
        private void btnurun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr == null)
            {
                fr = new frmUrunler();
                fr.MdiParent = this;
                fr.Show();
            }

        }
        frmMusteriler frm;
        private void btnmusteri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frm == null)
            {
                frm = new frmMusteriler();
                frm.MdiParent = this;
                frm.Show();
            }
        }
        frmFirma frmm;
        private void btnfirma_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmm == null)
            {
                frmm = new frmFirma();
                frmm.MdiParent = this;
                frmm.Show();
            }
        }
        FrmPersonel fr2;
        private void btnpersonel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr2 == null)
            {
                fr2 = new FrmPersonel();
                fr2.MdiParent = this;
                fr2.Show();
            }
        }
        frmRehber frm2;
        private void btnrehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frm2 == null)
            {
                frm2 = new frmRehber();
                frm2.MdiParent = this;
                frm2.Show();
            }

        }
        frmGiderler gider;
        private void btngider_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gider == null)
            {
                gider = new frmGiderler();
                gider.MdiParent = this;
                gider.Show();
            }
           
        }
        frmBanka banka;
        private void btnbanka_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (banka == null)
            {
                banka = new frmBanka();
                banka.MdiParent = this;
                banka.Show();
            }
        }
        frmFatura fatura;
        private void btnfatura_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            if (fatura == null)
            {
                fatura = new frmFatura();
                fatura.MdiParent = this;
                fatura.Show();
            }
        }
        frmnot not;
        private void btnnot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(not == null)
            {
                not = new frmnot();
                not.MdiParent = this;
                not.Show();
            }
        }
        frmhareket hareket;
        private void btnhareket_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(hareket == null)
            {
                hareket = new frmhareket();
                hareket.MdiParent = this;
                hareket.Show();
            }
        }
        frmrapor rapor;

        private void btnrapor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (rapor == null)
            {
                rapor = new frmrapor();
                rapor.MdiParent = this;
                rapor.Show();
            }
        }

        frrmstok stok;
        private void btnstok_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(stok == null)
            {
                stok = new frrmstok();
                stok.MdiParent = this;
                stok.Show();
            }
        }
        frmayarlar ayarlar;
        private void btnayar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

                ayarlar = new frmayarlar();
                ayarlar.Show();
            
        }
        frmkasa kasa;
        private void btnkasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(kasa == null)
            {
                kasa = new frmkasa();
                kasa.MdiParent = this;
                kasa.Show();
            }
        }

        private void frmAna_Load(object sender, EventArgs e)
        {
            if (anasayfa == null)
            {
                anasayfa = new frmanasayfa();
                anasayfa.MdiParent = this;
                anasayfa.Show();
            }
        }
    }
}

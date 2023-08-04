using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TicariOtomasyon
{
    public partial class frmRehber : Form
    {
        public frmRehber()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        private void frmRehber_Load(object sender, EventArgs e)
        {
            //Müşteri Bilgileri
            SqlDataAdapter da = new SqlDataAdapter("Select AD,SOYAD,TELEFON,TELEFON2,MAIL from TBL_Musteri",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //Firma Bilgileri
            SqlDataAdapter da1 = new SqlDataAdapter("Select AD as 'Firma Adı', YETKILISTATU as 'Personel Ünvanı',YETKILIADSOYAD as 'Yetkilinin Adı ve Soyadı',TELEFON1 as 'Telefon-1',TELEFON2 as 'Telefon-2', FAX, MAIL as 'Mail' from TBL_Firmalar",bgl.baglanti());
            DataTable dt2 = new DataTable();
            da1.Fill(dt2);
            gridControl2.DataSource = dt2;

        }



        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            frmMail frm = new frmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();

        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            frmMail frm = new frmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();

        }
    }
}

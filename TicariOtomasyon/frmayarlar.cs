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
    public partial class frmayarlar : Form
    {
        public frmayarlar()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_Giris",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void frmayarlar_Load(object sender, EventArgs e)
        {
            listele();
            txtad.Text = "";
            txtsifre.Text = "";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seçilen kullanıcıyı silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlCommand komut2 = new SqlCommand("delete from TBL_Giris where KullaniciAd=@i1", bgl.baglanti());
                komut2.Parameters.AddWithValue("@i1",txtad.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Seçilen kullanıcı silinmiştir..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(btnkaydet.Text == "KAYDET")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_Giris values (@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtad.Text);
                komut.Parameters.AddWithValue("p2", txtsifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni kullanıcı sisteme kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if(btnkaydet.Text == "Güncelle")
            {
                SqlCommand komut2 = new SqlCommand("update TBL_Giris set Sifre = @a2 where KullaniciAd=@a1",bgl.baglanti());
                komut2.Parameters.AddWithValue("@a1",txtad.Text);
                komut2.Parameters.AddWithValue("a2",txtsifre.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kullanıcı bilgileri güncellendi");
                listele();
            }

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtad.Text = dr["KullaniciAd"].ToString();
                txtsifre.Text = dr["Sifre"].ToString();

            }
        }

        private void txtad_EditValueChanged(object sender, EventArgs e)
        {
            if (txtad.Text != "")
            {
                btnkaydet.Text = "Güncelle";
            }
            else
            {
                btnkaydet.Text = "KAYDET";
            }
        }
    }
}

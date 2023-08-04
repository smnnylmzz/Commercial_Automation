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
    public partial class frmnot : Form
    {
        public frmnot()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_Notlar",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        } 
        void temizle()
        {
            txtid.Text = "";
            txtbaslik.Text = "";
            msksaat.Text = "";
            msktarih.Text = "";
            txtdetay.Text = "";
            txtolusturan.Text = "";
            txthitap.Text = "";
            msktarih.Focus();
        }
        private void frmnot_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand kaydet = new SqlCommand("INSERT into TBL_Notlar (TARIH,SAAT,BASLIK,DETAY,OLUSTURAN,HITAP) VALUES (@P2,@P3,@P4,@P5,@P6,@P7)",bgl.baglanti());
            kaydet.Parameters.AddWithValue("@P2", msktarih.Text);
            kaydet.Parameters.AddWithValue("@P3", msksaat.Text);
            kaydet.Parameters.AddWithValue("@P4", txtbaslik.Text);
            kaydet.Parameters.AddWithValue("@P5", txtdetay.Text);
            kaydet.Parameters.AddWithValue("@P6", txtolusturan.Text);
            kaydet.Parameters.AddWithValue("@P7", txthitap.Text);
            kaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Notunuz kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
            listele();

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncelle = new SqlCommand("update TBL_Notlar set TARIH=@N2, SAAT=@N3, BASLIK=@N4, DETAY=@N5, OLUSTURAN=@N6, HITAP=@N7 WHERE ID=@N1",bgl.baglanti());
            guncelle.Parameters.AddWithValue("@N1", txtid.Text);
            guncelle.Parameters.AddWithValue("@N2", msktarih.Text);
            guncelle.Parameters.AddWithValue("@N3", msksaat.Text);
            guncelle.Parameters.AddWithValue("@N4", txtbaslik.Text);
            guncelle.Parameters.AddWithValue("@N5", txtdetay.Text);
            guncelle.Parameters.AddWithValue("@N6", txtolusturan.Text);
            guncelle.Parameters.AddWithValue("@N7", txthitap.Text);
            guncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Notunuz güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seçilen notu silmek istiyor musunuz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Silme işlemi için SQL komutunu oluşturun
                SqlCommand sil = new SqlCommand("DELETE FROM TBL_Notlar WHERE ID=@i1", bgl.baglanti());
                sil.Parameters.AddWithValue("@i1", txtid.Text);

                // Komutu çalıştırın
                sil.ExecuteNonQuery();

                // Bağlantıyı kapatın
                bgl.baglanti().Close();

                MessageBox.Show("Seçilen not silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Listeleyin ve alanları temizleyin
                listele();
                temizle();
            }
        }


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                msktarih.Text = dr["TARIH"].ToString();
                msksaat.Text = dr["SAAT"].ToString();
                txtbaslik.Text = dr["BASLIK"].ToString();
                txtdetay.Text = dr["DETAY"].ToString();
                txtolusturan.Text = dr["OLUSTURAN"].ToString();
                txthitap.Text = dr["HITAP"].ToString();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            frmnotdetay fr = new frmnotdetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.metin= dr["DETAY"].ToString();
            }
            fr.Show();

        }
    }
}

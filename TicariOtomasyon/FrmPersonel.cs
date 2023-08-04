using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraBars;

namespace TicariOtomasyon
{
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR from iller", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_Personel", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void Temizle()
        {
            txtid.Text = "";
            txtad.Text = "";
            txtSOYAD.Text = "";
            msktel1.Text = "";
            txtmail.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            rchadres.Text = "";
            txtgorev.Text = "";
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            sehirlistesi();
            Temizle();
            listele();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("INSERT INTO TBL_Personel (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) VALUES(@F1,@F2,@F3,@F4,@F5,@F6,@F7,@F8,@F9)", bgl.baglanti());
            komut1.Parameters.AddWithValue("@F1",txtad.Text);
            komut1.Parameters.AddWithValue("@F2",txtSOYAD.Text);
            komut1.Parameters.AddWithValue("@F3",msktel1.Text);
            komut1.Parameters.AddWithValue("@F4", msktc.Text);
            komut1.Parameters.AddWithValue("@F5", txtmail.Text);
            komut1.Parameters.AddWithValue("@F6", cmbil.Text);
            komut1.Parameters.AddWithValue("@F7", cmbilce.Text);
            komut1.Parameters.AddWithValue("@F8", rchadres.Text);
            komut1.Parameters.AddWithValue("@F9", txtgorev.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Kayıt edilmiştir..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtad.Text = dr["AD"].ToString();
                txtSOYAD.Text = dr["SOYAD"].ToString();
                msktel1.Text = dr["TELEFON"].ToString();
                msktc.Text = dr["TC"].ToString();
                txtmail.Text = dr["MAIL"].ToString();
                cmbil.Text = dr["IL"].ToString();
                cmbilce.Text = dr["ILCE"].ToString();
                rchadres.Text = dr["ADRES"].ToString();
                txtgorev.Text = dr["GOREV"].ToString();
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seçilen personeli silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlCommand komut2 = new SqlCommand("delete from TBL_Personel where ID=@i1", bgl.baglanti());
                komut2.Parameters.AddWithValue("@i1", txtid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Personel kaydı silinmiştir..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("UPDATE TBL_Personel SET AD=@g1, SOYAD=@g2, TELEFON=@g3, TC=@g4, MAIL=@g5, IL=@g6, ILCE=@g7, ADRES=@g8, GOREV=@g9 WHERE ID=@g10", bgl.baglanti());
            komut1.Parameters.AddWithValue("@g1", txtad.Text);
            komut1.Parameters.AddWithValue("@g2", txtSOYAD.Text);
            komut1.Parameters.AddWithValue("@g3", msktel1.Text);
            komut1.Parameters.AddWithValue("@g4", msktc.Text);
            komut1.Parameters.AddWithValue("@g5",txtmail.Text);
            komut1.Parameters.AddWithValue("@g6", cmbil.Text);
            komut1.Parameters.AddWithValue("@g7", cmbilce.Text);
            komut1.Parameters.AddWithValue("@g8", rchadres.Text);
            komut1.Parameters.AddWithValue("@g9", txtgorev.Text);
            komut1.Parameters.AddWithValue("@g10", txtid.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgiler güncellendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            SqlCommand komut1 = new SqlCommand("Select ILCE from ilceler where SEHIR = @k1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@k1", cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
    }
}

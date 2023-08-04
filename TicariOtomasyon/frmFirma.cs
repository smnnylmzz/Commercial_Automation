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
    public partial class frmFirma : Form
    {
        public frmFirma()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select ID as 'Ürün İd', AD as 'Firma Adı', YETKILISTATU as 'Personel Ünvanı',YETKILIADSOYAD as 'Yetkilinin Adı ve Soyadı',YETKILITC as 'Görevli T.C. No', SEKTOR as 'Sektör',TELEFON1 as 'Telefon-1',TELEFON2 as 'Telefon-2',TELEFON3 as 'Telefon-3',MAIL as 'Mail',FAX,IL as 'İl',ILCE as 'İlçe',VERGIDAIRESI as 'Vergi Dairesi',ADRES as 'Adres Bilgisi',OZELKOD1 as 'Özel Kod-1',OZELKOD2 as 'Özel Kod-2',OZELKOD3 as 'Özel Kod-3' from TBL_Firmalar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void carikodaciklamalar()
        {
            SqlCommand cari = new SqlCommand("Select FIRMAKOD1 from TBL_Kodlar",bgl.baglanti());
            SqlDataReader dr = cari.ExecuteReader();
            while(dr.Read())
            {
                rchkod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }
        void Temizle()
        {
            txtid.Text = "";
            txtad.Text = "";
            txtgorevli.Text = "";
            txtkod.Text = "";
            txtkod2.Text = "";
            txtkod3.Text = "";
            txtmail.Text = "";
            txtSektor.Text = "";
            txtvergi.Text = "";
            txtyetki.Text = "";
            mskfax.Text = "";
            msktc.Text = "";
            msktel1.Text = "";
            msktel2.Text = "";
            msktel3.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            txtad.Focus();
        }
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

        private void frmFirma_Load(object sender, EventArgs e)
        {
            listele();
            carikodaciklamalar();
            Temizle();
            sehirlistesi();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                txtid.Text = dr["Ürün İd"].ToString();
                txtad.Text = dr["Firma Adı"].ToString();
                txtgorevli.Text = dr["Personel Ünvanı"].ToString();
                txtyetki.Text = dr["Yetkilinin Adı ve Soyadı"].ToString();
                msktc.Text = dr["Görevli T.C. No"].ToString();
                txtSektor.Text = dr["Sektör"].ToString();
                msktel1.Text = dr["Telefon-1"].ToString();
                msktel2.Text = dr["Telefon-2"].ToString();
                msktel3.Text = dr["Telefon-3"].ToString();
                txtmail.Text = dr["Mail"].ToString();
                mskfax.Text = dr["FAX"].ToString();
                cmbil.Text = dr["İl"].ToString();
                cmbilce.Text = dr["İlçe"].ToString();
                txtvergi.Text = dr["Vergi Dairesi"].ToString();
                rchadres.Text = dr["Adres Bilgisi"].ToString();
                txtkod.Text = dr["Özel Kod-1"].ToString();
                txtkod2.Text = dr["Özel Kod-2"].ToString();
                txtkod3.Text = dr["Özel Kod-3"].ToString();
            }  
        }



        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seçilen ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlCommand komut2 = new SqlCommand("delete from TBL_Firmalar where ID=@i1", bgl.baglanti());
                komut2.Parameters.AddWithValue("@i1", bgl.baglanti());
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Seçilen ürün silinmiştir..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("update TBL_Firmalar set AD=@c1, YETKILISTATU=@c2, YETKILIADSOYAD=@c3, YETKILITC=@c4, SEKTOR=@c5, TELEFON1=@c6, TELEFON2=@c7, TELEFON3=@c8, MAIL=@c9, FAX=@c10, IL=@c11, ILCE=@c12, VERGIDAIRESI=@c13, ADRES=@c14, OZELKOD1=@c15, OZELKOD2=@c16, OZELKOD3=@c17  where ID=@c18", bgl.baglanti());

            komut3.Parameters.AddWithValue("@c18", txtid.Text);
            komut3.Parameters.AddWithValue("@c1", txtad.Text);
            komut3.Parameters.AddWithValue("@c2", txtgorevli.Text);
            komut3.Parameters.AddWithValue("@c3", txtyetki.Text);
            komut3.Parameters.AddWithValue("@c4", msktc.Text);
            komut3.Parameters.AddWithValue("@c5", txtSektor.Text);
            komut3.Parameters.AddWithValue("@c6", msktel1.Text);
            komut3.Parameters.AddWithValue("@c7", msktel2.Text);
            komut3.Parameters.AddWithValue("@c8", msktel3.Text); 
            komut3.Parameters.AddWithValue("@c9", txtmail.Text);
            komut3.Parameters.AddWithValue("@c10", mskfax.Text);
            komut3.Parameters.AddWithValue("@c11", cmbil.Text);
            komut3.Parameters.AddWithValue("@c12", cmbilce.Text);
            komut3.Parameters.AddWithValue("@c13", txtvergi.Text);
            komut3.Parameters.AddWithValue("@c14", rchadres.Text);
            komut3.Parameters.AddWithValue("@c15", txtkod.Text);
            komut3.Parameters.AddWithValue("@c16", txtkod2.Text);
            komut3.Parameters.AddWithValue("@c17", txtkod3.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Güncelleme işlemi gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO TBL_Firmalar (AD, YETKILISTATU, YETKILIADSOYAD, YETKILITC, SEKTOR, TELEFON1, TELEFON2, TELEFON3, MAIL, FAX, IL, ILCE, VERGIDAIRESI, ADRES, OZELKOD1, OZELKOD2, OZELKOD3) VALUES (@u1, @u2, @u3, @u4, @u5, @u6, @u7, @u8, @u9, @u10, @u11, @u12, @u13, @u14, @u15, @u16, @u17)", bgl.baglanti());
            cmd.Parameters.AddWithValue("@u1", txtad.Text);
            cmd.Parameters.AddWithValue("@u2", txtgorevli.Text);
            cmd.Parameters.AddWithValue("@u3", txtyetki.Text);
            cmd.Parameters.AddWithValue("@u4", msktc.Text);
            cmd.Parameters.AddWithValue("@u5", txtSektor.Text);
            cmd.Parameters.AddWithValue("@u6", msktel1.Text);
            cmd.Parameters.AddWithValue("@u7", msktel2.Text);
            cmd.Parameters.AddWithValue("@u8", msktel3.Text);
            cmd.Parameters.AddWithValue("@u9", txtmail.Text);
            cmd.Parameters.AddWithValue("@u10", mskfax.Text);
            cmd.Parameters.AddWithValue("@u11", cmbil.Text);
            cmd.Parameters.AddWithValue("@u12", cmbilce.Text);
            cmd.Parameters.AddWithValue("@u13", txtvergi.Text);
            cmd.Parameters.AddWithValue("@u14", rchadres.Text);
            cmd.Parameters.AddWithValue("@u15", txtkod.Text);
            cmd.Parameters.AddWithValue("@u16", txtkod2.Text);
            cmd.Parameters.AddWithValue("@u17", txtkod3.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt işlemi başarıyla gerçekleşmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            Temizle();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            SqlCommand komut1 = new SqlCommand("Select ILCE from ilceler where SEHIR = @d1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@d1", cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
    }

}

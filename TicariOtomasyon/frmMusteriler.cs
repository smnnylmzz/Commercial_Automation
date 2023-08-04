using DevExpress.XtraGrid.Views.Grid;
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

namespace TicariOtomasyon
{
    public partial class frmMusteriler : Form
    {
        public frmMusteriler()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_Musteri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
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

        private void frmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistesi();
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

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("insert into TBL_Musteri(AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE)values(@e1,@e2,@e3,@e4,@e5,@e6,@e7,@e8,@e9,@e10)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@e1", txtad.Text);
            komut2.Parameters.AddWithValue("@e2", txtSOYAD.Text);
            komut2.Parameters.AddWithValue("@e3", msktel1.Text);
            komut2.Parameters.AddWithValue("@e4", msktel2.Text);
            komut2.Parameters.AddWithValue("@e5", msktc.Text);
            komut2.Parameters.AddWithValue("@e6", txtmail.Text);
            komut2.Parameters.AddWithValue("@e7", cmbil.Text);
            komut2.Parameters.AddWithValue("@e8", cmbilce.Text);
            komut2.Parameters.AddWithValue("@e9", rchadres.Text);
            komut2.Parameters.AddWithValue("@e10", txtvergi.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri başarıyla kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }



        private void sİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DialogResult result = MessageBox.Show("Seçilen ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int selectedRow = gridView1.GetSelectedRows()[0]; //seçili satırların dizisini alıyoruz ve buradan ilk seçili satırın indeksini (selectedRow) alıyoruz.
                    int selectedProductId = Convert.ToInt32(gridView1.GetRowCellValue(selectedRow, "ID"));//seçili satırdaki "Ürün Sırası" sütununun değerini alıyoruz ve bunu selectedProductId değişkenine dönüştürüyoruz. Bu değer, silinecek ürünün ID'sini temsil ediyor.

                    bgl.baglanti();
                    SqlCommand komut2 = new SqlCommand("delete from TBL_Musteri where ID = @id", bgl.baglanti());
                    komut2.Parameters.AddWithValue("@id", selectedProductId);
                    komut2.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Seçilen ürün silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz ürünü seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seçilen ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlCommand komut3 = new SqlCommand("delete from TBL_Musteri where ID=@D1",bgl.baglanti());
                komut3.Parameters.AddWithValue("@D1",txtid.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Seçilen ürün silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["ID"].ToString();
            txtad.Text = dr["AD"].ToString();
            txtSOYAD.Text = dr["SOYAD"].ToString();
            msktel1.Text = dr["TELEFON"].ToString();
            msktel2.Text = dr["TELEFON2"].ToString();
            msktc.Text = dr["TC"].ToString();
            txtmail.Text = dr["MAIL"].ToString();
            cmbil.Text = dr["IL"].ToString();
            cmbilce.Text = dr["ILCE"].ToString();
            rchadres.Text = dr["ADRES"].ToString();
            txtvergi.Text = dr["VERGIDAIRE"].ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("update TBL_Musteri set AD=@A1, SOYAD=@A2, TELEFON=@A3, TELEFON2=@A4, TC=@A5, MAIL=@A6, IL=@A7, ILCE=@A8,ADRES=@A9,VERGIDAIRE=@A10 where ID=@A11", bgl.baglanti());
            komut3.Parameters.AddWithValue("@A1", txtad.Text);
            komut3.Parameters.AddWithValue("@A2", txtSOYAD.Text);
            komut3.Parameters.AddWithValue("@A3", msktel1.Text);
            komut3.Parameters.AddWithValue("@A4", msktel2.Text);
            komut3.Parameters.AddWithValue("@A5", msktc.Text);
            komut3.Parameters.AddWithValue("@A6", txtmail.Text);
            komut3.Parameters.AddWithValue("@A7", cmbil.Text);
            komut3.Parameters.AddWithValue("@A8", cmbilce.Text);
            komut3.Parameters.AddWithValue("@A9", rchadres.Text);
            komut3.Parameters.AddWithValue("@A10", txtvergi.Text);
            komut3.Parameters.AddWithValue("@A11", txtid.Text);

            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Güncelleme işlemi gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}

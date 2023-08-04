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
    public partial class frmUrunler : Form
    {
        public frmUrunler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID as 'Ürün Sırası', URUNAD as 'Ürünün Adı', MARKA, MODEL, YIL,ADET, ALISFIYAT as 'Alış Fiyatı', SATISFIYAT as 'Satış Fiyatı', DETAY as 'Detaylar' from TBL_Urunler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void frmUrunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            // Verileri Kaydetme
            bgl.baglanti();
            SqlCommand komut = new SqlCommand("Insert into TBL_Urunler(URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtmarka.Text);
            komut.Parameters.AddWithValue("@p3", txtmodel.Text);
            komut.Parameters.AddWithValue("@p4", mskdyil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse(numericadet.Value.ToString()));

            decimal alisFiyati;
            decimal satisFiyati;

            if (decimal.TryParse(txtalis.Text, out alisFiyati) && decimal.TryParse(txtsatis.Text, out satisFiyati))
            {
                komut.Parameters.AddWithValue("@p6", alisFiyati);
                komut.Parameters.AddWithValue("@p7", satisFiyati);
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir fiyat değeri girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            komut.Parameters.AddWithValue("@p8", rtchdetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün başarıyla kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seçilen ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bgl.baglanti();
                SqlCommand komut2 = new SqlCommand("delete from TBL_Urunler where ID=@d1", bgl.baglanti());
                komut2.Parameters.AddWithValue("@d1",txtid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Seçilen ürün silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
        }

        private void sİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DialogResult result = MessageBox.Show("Seçilen ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int selectedRow = gridView1.GetSelectedRows()[0];
                    int selectedProductId = Convert.ToInt32(gridView1.GetRowCellValue(selectedRow, "Ürün Sırası"));

                    bgl.baglanti();
                    SqlCommand komut2 = new SqlCommand("delete from TBL_Urunler where ID = @id", bgl.baglanti());
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["Ürün Sırası"].ToString();
                txtad.Text = dr["Ürünün Adı"].ToString();
                txtmarka.Text = dr["MARKA"].ToString();
                txtmodel.Text = dr["MODEL"].ToString();
                mskdyil.Text = dr["YIL"].ToString();
                numericadet.Value = decimal.Parse(dr["ADET"].ToString());
                txtalis.Text = dr["Alış Fiyatı"].ToString();
                txtsatis.Text = dr["Satış Fiyatı"].ToString();
                rtchdetay.Text = dr["Detaylar"].ToString();
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("update TBL_Urunler set URUNAD=@a1, MARKA=@a2, MODEL=@a3, YIL=@a4, ADET=@a5, ALISFIYAT=@a6, SATISFIYAT=@a7, DETAY=@a8 where ID=@a9", bgl.baglanti());
            komut3.Parameters.AddWithValue("@a1", txtad.Text);
            komut3.Parameters.AddWithValue("@a2", txtmarka.Text);
            komut3.Parameters.AddWithValue("@a3", txtmodel.Text);
            komut3.Parameters.AddWithValue("@a4", mskdyil.Text);
            komut3.Parameters.AddWithValue("@a5", int.Parse(numericadet.Value.ToString()));

            decimal alisFiyati;
            decimal satisFiyati;

            if (decimal.TryParse(txtalis.Text, out alisFiyati) && decimal.TryParse(txtsatis.Text, out satisFiyati))
            {
                komut3.Parameters.AddWithValue("@a6", alisFiyati);
                komut3.Parameters.AddWithValue("@a7", satisFiyati);
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir fiyat değeri girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            komut3.Parameters.AddWithValue("@a8", rtchdetay.Text);
            komut3.Parameters.AddWithValue("@a9", int.Parse(txtid.Text));
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Güncelleme işlemi gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
    }
}

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
    public partial class frmFatura : Form
    {
        public frmFatura()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        void faturalist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FaturaBilgi",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            txtid.Text = "";
            txtseri.Text = "";
            txtsira.Text = "";
            msktarih.Text = "";
            msksaat.Text = "";
            txtvergi.Text = "";
            txtalici.Text = "";
            txteden.Text = "";
            txtalan.Text = "";
            txtseri.Focus();
        }
        private void frmFatura_Load(object sender, EventArgs e)
        {
            faturalist();
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "")
            {
                SqlCommand cmd = new SqlCommand("insert into TBL_FaturaBilgi (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) VALUES(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", bgl.baglanti());
                cmd.Parameters.AddWithValue("@P1",txtseri.Text);
                cmd.Parameters.AddWithValue("@P2", txtsira.Text);
                cmd.Parameters.AddWithValue("@P3", msktarih.Text);
                cmd.Parameters.AddWithValue("@P4", msksaat.Text);
                cmd.Parameters.AddWithValue("@P5", txtvergi.Text);
                cmd.Parameters.AddWithValue("@P6", txtalici.Text);
                cmd.Parameters.AddWithValue("@P7", txteden.Text);
                cmd.Parameters.AddWithValue("@P8", txtalan.Text);
                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();
                faturalist();
                MessageBox.Show("Fatura Bilgisi Sisteme Kaydedilmiştir","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                temizle();

            }
            if(txtid.Text !="")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(txtfiyat.Text);
                miktar = Convert.ToDouble(txtmiktar.Text);
                tutar = miktar * fiyat;
                txttutar.Text = tutar.ToString();
                SqlCommand komut2 = new SqlCommand("insert into TBL_FaturaDetay (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) VALUES (@p1,@p2,@p3,@p4,@p5)",bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1",txtad.Text);
                komut2.Parameters.AddWithValue("@p2", txtmiktar.Text);
                komut2.Parameters.AddWithValue("@p3", txtfiyat.Text);
                komut2.Parameters.AddWithValue("@p4", txttutar.Text);
                komut2.Parameters.AddWithValue("@p5", txtfatudaid.Text);
                bgl.baglanti().Close();
                faturalist();
                MessageBox.Show("Fatura Bilgisi Sisteme Kaydedilmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();

            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtseri.Text = dr["SERI"].ToString();
                txtsira.Text = dr["SIRANO"].ToString();
                msktarih.Text = dr["TARIH"].ToString();
                msksaat.Text = dr["SAAT"].ToString();
                txtvergi.Text = dr["VERGIDAIRE"].ToString();
                txtalici.Text = dr["ALICI"].ToString();
                txteden.Text = dr["TESLIMEDEN"].ToString();
                txtalan.Text = dr["TESLIMALAN"].ToString();
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Seçilen ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("delete from TBL_FaturaBilgi where ID = @p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura silindi..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_FaturaBilgi set SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8 WHERE ID = @P9");

            komut.Parameters.AddWithValue("@P1", txtseri.Text);
            komut.Parameters.AddWithValue("@P2", txtsira.Text);
            komut.Parameters.AddWithValue("@P3", msktarih.Text);
            komut.Parameters.AddWithValue("@P4", msksaat.Text);
            komut.Parameters.AddWithValue("@P5", txtvergi.Text);
            komut.Parameters.AddWithValue("@P6", txtalici.Text);
            komut.Parameters.AddWithValue("@P7", txteden.Text);
            komut.Parameters.AddWithValue("@P8", txtalan.Text);
            komut.Parameters.AddWithValue("@P9", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            faturalist();
            MessageBox.Show("Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            frmurundetay frm = new frmurundetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                frm.id = dr["ID"].ToString();
            }
            frm.Show();
        }
    }
}

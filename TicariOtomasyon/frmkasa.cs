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
using DevExpress.Charts;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;

namespace TicariOtomasyon
{
    public partial class frmkasa : Form
    {
        public frmkasa()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketler",bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }
        void firmahareket()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute FirmaHareketler", bgl.baglanti());
            da2.Fill(dt2);
            gridControl1.DataSource = dt2;
        }
        void gider()
        {
            DataTable dt3 = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from TBL_Giderler",bgl.baglanti());
            dataAdapter.Fill(dt3);
            gridControl2.DataSource = dt3;
        }

        private void frmkasa_Load(object sender, EventArgs e)
        {
            musterihareket();
            firmahareket();
            gider();    


            //Toplam Tutar Hesaplama
            SqlCommand komut1 = new SqlCommand("Select Sum(TUTAR) from TBL_FaturaDetay",bgl.baglanti());
            SqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                lbltoplamtutar.Text = dr[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //Son ayın faturaları
            SqlCommand komut2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_Giderler order by ID asc", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblodeme.Text = dr2[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //Son ayın personel maaşları
            SqlCommand komut3 = new SqlCommand("Select Maaslar From TBL_Giderler order by ID asc", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblpersonelmaas.Text = dr3[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //Toplam Müşteri Sayısı
            SqlCommand komut4 = new SqlCommand("Select Count(*) From TBL_Musteri", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblmusteri.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Firma Sayısı
            SqlCommand komut5 = new SqlCommand("Select Count(*) From TBL_Firmalar", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblfirma.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Firma Şehir Sayısı
            SqlCommand komut6 = new SqlCommand("Select Count(Distinct(IL)) From TBL_Firmalar", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblil.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Müşteri Şehir Sayısı
            SqlCommand komut7 = new SqlCommand("Select Count(Distinct(IL)) From TBL_Musteri", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                lblil.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Personel Sayısı
            SqlCommand komut8 = new SqlCommand("Select Count(*) From TBL_Personel", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                lblpersonel.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Ürün Sayısı
            SqlCommand komut9 = new SqlCommand("Select Sum(Adet) From TBL_Urunler", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                lblstok.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();
        }

        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            //Elektrik
            if (sayac > 0 && sayac <= 5)
            {
                groupControl1.Text = "Elektrik";
                chartControl1.Series["AYLAR"].Points.Clear();
                chartControl1.BackColor = Color.Azure;
                SqlCommand komut10 = new SqlCommand("Select top 4 AY,ELEKTRIK from TBL_Giderler order by ID Desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            //Su
            if (sayac > 5 && sayac <= 10)
            {
                groupControl1.Text = "Su";
                chartControl1.Series["AYLAR"].Points.Clear();
                chartControl1.BackColor = Color.Bisque;
                SqlCommand komut11 = new SqlCommand("Select top 4 AY,SU from TBL_Giderler order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Doğalgaz
            if (sayac > 10 && sayac <= 15)
            {
                groupControl1.Text = "Doğalgaz";
                chartControl1.Series["AYLAR"].Points.Clear();
                chartControl1.BackColor = Color.CadetBlue;
                SqlCommand komut11 = new SqlCommand("Select top 4 AY,DOGALGAZ from TBL_Giderler order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //İnternet
            if (sayac > 15 && sayac <= 20)
            {
                groupControl1.Text = "İnternet";
                chartControl1.Series["AYLAR"].Points.Clear();
                chartControl1.BackColor = Color.Yellow;
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,INTERNET from TBL_Giderler order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Ekstra
            if (sayac > 20 && sayac <= 25)
            {
                groupControl1.Text = "Ekstra";
                chartControl1.Series["AYLAR"].Points.Clear();
                chartControl1.BackColor = Color.Pink;
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,EKSTRA from TBL_Giderler order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac == 26)
            {
                sayac = 0;
            }
        }
        int sayac2;

        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;

            //Elektrik
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl2.Text = "Elektrik";
                chartControl2.Series["AYLAR"].Points.Clear();
                chartControl2.BackColor = Color.Azure;
                SqlCommand komut10 = new SqlCommand("Select top 4 AY,ELEKTRIK from TBL_Giderler order by ID Asc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            //Su
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl2.Text = "Su";
                chartControl2.Series["AYLAR"].Points.Clear();
                chartControl2.BackColor = Color.Bisque;
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,SU from TBL_Giderler order by ID Asc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Doğalgaz
            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl2.Text = "Doğalgaz";
                chartControl2.Series["AYLAR"].Points.Clear();
                chartControl2.BackColor = Color.CadetBlue;
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,DOGALGAZ from TBL_Giderler order by ID Asc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //İnternet
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl2.Text = "İnternet";
                chartControl2.Series["AYLAR"].Points.Clear();
                chartControl2.BackColor = Color.Yellow;
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,INTERNET from TBL_Giderler order by ID Asc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Ekstra
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl2.Text = "Ekstra";
                chartControl2.Series["AYLAR"].Points.Clear();
                chartControl2.BackColor = Color.Pink;
                SqlCommand komut11 = new SqlCommand("Select Top 4 Ay,EKSTRA from TBL_Giderler order by ID Asc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}

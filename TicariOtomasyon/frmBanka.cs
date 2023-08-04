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
    public partial class frmBanka : Form
    {
        public frmBanka()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        void bankalist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt; 
        }
        void Temizle()
        {
            txtid.Text = "";
            txtad.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            txtsube.Text = "";
            txtiban.Text = "";
            txthesap.Text = "";
            txtyetkili.Text = "";
            msktarih.Text = "";
            txthesaptur.Text = "";
            lookUpEdit1.Text = "";
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
        void firmalar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD from TBL_Firmalar",bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.NullText = "Lütfen bir firma seçiniz";
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        } 

        private void frmBanka_Load(object sender, EventArgs e)
        {
            bankalist();
            Temizle();
            sehirlistesi();
            firmalar();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["ID"].ToString();
            txtad.Text = dr["BANKAADI"].ToString();
            cmbil.Text = dr["IL"].ToString();
            cmbilce.Text = dr["ILCE"].ToString();
            txtsube.Text = dr["SUBE"].ToString();
            txtiban.Text = dr["IBAN"].ToString();
            txthesap.Text = dr["HESAPNO"].ToString();
            txtyetkili.Text = dr["YETKILI"].ToString();
            msktarih.Text = dr["TARIH"].ToString();
            txthesaptur.Text = dr["HESAPTURU"].ToString();
            lookUpEdit1.Text = dr["AD"].ToString();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into TBL_Bankalar (BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TARIH,HESAPTURU,FIRMAID) values (@k1,@k2,@k3,@k4,@k5,@k6,@k7,@k8,@k9,@k10)",bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@k1",txtad.Text);
            komutkaydet.Parameters.AddWithValue("@k2", cmbil.Text);
            komutkaydet.Parameters.AddWithValue("@k3", cmbilce.Text);
            komutkaydet.Parameters.AddWithValue("@k4", txtsube.Text);
            komutkaydet.Parameters.AddWithValue("@k5", txtiban.Text);
            komutkaydet.Parameters.AddWithValue("@k6", txthesap.Text);
            komutkaydet.Parameters.AddWithValue("@k7", txtyetkili.Text);
            komutkaydet.Parameters.AddWithValue("@k8", msktarih.Text);
            komutkaydet.Parameters.AddWithValue("@k9", txthesaptur.Text);
            komutkaydet.Parameters.AddWithValue("@k10", lookUpEdit1.EditValue);
            komutkaydet.ExecuteNonQuery();
            bankalist();
            bgl.baglanti().Close();
            MessageBox.Show("Yeni banka sisteme işlenmiştir","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncellekomut = new SqlCommand("update TBL_Bankalar set BANKAADI=@A1, IL=@A2, ILCE=@A3, SUBE=@A4, IBAN=@A5, HESAPNO=@A6, YETKILI=@A7, TARIH=@A8,HESAPTURU=@A9,FIRMAID=@A10 where ID=@A11", bgl.baglanti());
            guncellekomut.Parameters.AddWithValue("@A1", txtad.Text);
            guncellekomut.Parameters.AddWithValue("@A2", cmbil.Text);
            guncellekomut.Parameters.AddWithValue("@A3", cmbilce.Text);
            guncellekomut.Parameters.AddWithValue("@A4", txtsube.Text);
            guncellekomut.Parameters.AddWithValue("@A5", txtiban.Text);
            guncellekomut.Parameters.AddWithValue("@A6", txthesap.Text);
            guncellekomut.Parameters.AddWithValue("@A7", txtyetkili.Text);
            guncellekomut.Parameters.AddWithValue("@A8", msktarih.Text);
            guncellekomut.Parameters.AddWithValue("@A9", txthesaptur.Text);
            guncellekomut.Parameters.AddWithValue("@A10", lookUpEdit1.EditValue);
            guncellekomut.Parameters.AddWithValue("@A11", txtid.Text);

            guncellekomut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Güncelleme işlemi gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bankalist();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seçilen ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlCommand silkomut = new SqlCommand("delete from TBL_Bankalar where ID=@D1", bgl.baglanti());
                silkomut.Parameters.AddWithValue("@D1", txtid.Text);
                silkomut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Seçilen banka kaydı silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bankalist();
            }
        }
    }
}

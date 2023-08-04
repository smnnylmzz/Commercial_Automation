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
    public partial class frmGiderler : Form
    {
        public frmGiderler()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        void giderlist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_Giderler",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void temizlik()
        {
            txtid.Text = "";
            cmbay.Text = "";
            cmbyil.Text = "";
            txtelektrik.Text = "";
            txtsu.Text = "";
            txtgaz.Text = "";
            txtnet.Text = "";
            txtmaas.Text = "";
            txtekstra.Text = "";
            rchnot.Text = "";
        }
        private void frmGiderler_Load(object sender, EventArgs e)
        {
            giderlist();
            temizlik();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                cmbay.Text = dr["AY"].ToString();
                cmbyil.Text = dr["YIL"].ToString();
                txtelektrik.Text = dr["ELEKTRIK"].ToString();
                txtsu.Text = dr["SU"].ToString();
                txtgaz.Text = dr["DOGALGAZ"].ToString();
                txtnet.Text = dr["INTERNET"].ToString();
                txtmaas.Text = dr["MAASLAR"].ToString();
                txtekstra.Text = dr["EKSTRA"].ToString();
                rchnot.Text = dr["NOTLAR"].ToString();
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizlik();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand kaydetkomut = new SqlCommand("insert into TBL_Giderler (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) VALUES (@T1,@T2,@T3,@T4,@T5,@T6,@T7,@T8,@T9)", bgl.baglanti());
            kaydetkomut.Parameters.AddWithValue("@T1",cmbay.Text);
            kaydetkomut.Parameters.AddWithValue("@T2", cmbyil.Text);
            kaydetkomut.Parameters.AddWithValue("@T3", decimal.Parse(txtelektrik.Text));
            kaydetkomut.Parameters.AddWithValue("@T4", decimal.Parse(txtsu.Text));
            kaydetkomut.Parameters.AddWithValue("@T5", decimal.Parse(txtgaz.Text));
            kaydetkomut.Parameters.AddWithValue("@T6", decimal.Parse(txtnet.Text));
            kaydetkomut.Parameters.AddWithValue("@T7", decimal.Parse(txtmaas.Text));
            kaydetkomut.Parameters.AddWithValue("@T8", decimal.Parse(txtekstra.Text));
            kaydetkomut.Parameters.AddWithValue("@T9", rchnot.Text);
            kaydetkomut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider bilgisi işlenmiştir..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizlik();
            giderlist();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seçilen gider kaydını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlCommand komut2 = new SqlCommand("delete from TBL_Giderler where ID=@i1", bgl.baglanti());
                komut2.Parameters.AddWithValue("@i1", txtid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Gider bilgisi silinmiştir..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                giderlist();
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand kaydetkomut1 = new SqlCommand("UPDATE TBL_Giderler SET AY=@N1, YIL=@N2, ELEKTRIK=@N3, SU=@N4, DOGALGAZ=@N5, INTERNET=@N6, MAASLAR=@N7, EKSTRA=@N8, NOTLAR=@N9 WHERE ID=@N10", bgl.baglanti());
            kaydetkomut1.Parameters.AddWithValue("@N1", cmbay.Text);
            kaydetkomut1.Parameters.AddWithValue("@N2", cmbyil.Text);
            kaydetkomut1.Parameters.AddWithValue("@N3", decimal.Parse(txtelektrik.Text));
            kaydetkomut1.Parameters.AddWithValue("@N4", decimal.Parse(txtsu.Text));
            kaydetkomut1.Parameters.AddWithValue("@N5", decimal.Parse(txtgaz.Text));
            kaydetkomut1.Parameters.AddWithValue("@N6", decimal.Parse(txtnet.Text));
            kaydetkomut1.Parameters.AddWithValue("@N7", decimal.Parse(txtmaas.Text));
            kaydetkomut1.Parameters.AddWithValue("@N8", decimal.Parse(txtekstra.Text));
            kaydetkomut1.Parameters.AddWithValue("@N9", rchnot.Text);
            kaydetkomut1.Parameters.AddWithValue("@N10", txtid.Text);
            kaydetkomut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgiler güncellendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderlist();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}

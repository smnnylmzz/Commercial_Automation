using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyon
{
    public partial class frmrapor : Form
    {
        public frmrapor()
        {
            InitializeComponent();
        }

        private void frmrapor_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DboTicariOtomasyonDataSet1.TBL_Musteri' table. You can move, or remove it, as needed.
            this.TBL_MusteriTableAdapter.Fill(this.DboTicariOtomasyonDataSet1.TBL_Musteri);
            // TODO: This line of code loads data into the 'DboTicariOtomasyonDataSet.TBL_Firmalar' table. You can move, or remove it, as needed.
            this.TBL_FirmalarTableAdapter.Fill(this.DboTicariOtomasyonDataSet.TBL_Firmalar);
            // TODO: This line of code loads data into the 'DboTicariOtomasyonDataSet.TBL_Firmalar' table. You can move, or remove it, as needed.


            this.reportViewer1.RefreshReport();
        }
    }
}

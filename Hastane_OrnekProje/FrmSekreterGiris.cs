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

namespace Hastane_OrnekProje
{
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglan = new sqlbaglantisi();
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand dr = new SqlCommand("Select * From dbo.Sekreter where SekreterTC=@p1 and SekreterSifre=@p2",baglan.baglanti());
            dr.Parameters.AddWithValue("@p1", mskTC.Text);
            dr.Parameters.AddWithValue("@p2",txtSifre.Text);
            SqlDataReader fr=dr.ExecuteReader();
            if(fr.Read())
            {
                FrmSekreterDetay frs=new FrmSekreterDetay();
                frs.TC = mskTC.Text;
                frs.Show();
                this.Hide();
            }
        }
    }
}

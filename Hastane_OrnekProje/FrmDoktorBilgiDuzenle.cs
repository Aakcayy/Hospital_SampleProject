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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglan = new sqlbaglantisi();
        public string TCno;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCno;
            SqlCommand cmd4 = new SqlCommand("Select * From dbo.Doktorlar where DoktorTC=@p1", baglan.baglanti());
            cmd4.Parameters.AddWithValue("@p1", mskTC.Text);
            SqlDataReader dr = cmd4.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                cmbBrans.Text  = dr[3].ToString();
                txtSifre.Text= dr[5].ToString();
            }
            baglan.baglanti().Close();
        }

        private void btnBilgileriDuzenle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd5 = new SqlCommand("Update dbo.Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 where DoktorTC=@p5", baglan.baglanti());
            cmd5.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd5.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd5.Parameters.AddWithValue("@p3", cmbBrans.Text);
            cmd5.Parameters.AddWithValue("@p4", txtSifre.Text);
            cmd5.Parameters.AddWithValue("@p5", mskTC.Text);
            cmd5.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
    }
}

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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }
        public string TCno;
        sqlbaglantisi baglan=new sqlbaglantisi();
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text= TCno;
            SqlCommand cmd = new SqlCommand("Select * From dbo.Hastalar where HastaTC=@p1",baglan.baglanti());
            cmd.Parameters.AddWithValue("@p1",mskTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();//dr[0] >ID çünkü
                txtSoyad.Text= dr[2].ToString();
                mskTelefon.Text = dr[4].ToString();//dr[3] > TC çünkü
                txtSifre.Text = dr[5].ToString();
                cmbCinsiyet.Text= dr[6].ToString();
            }
            baglan.baglanti().Close();
        }

        private void btnBilgileriDuzenle_Click(object sender, EventArgs e)
        {
            SqlCommand ty = new SqlCommand("Update dbo.Hastalar set HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 where HastaTC=@p6",baglan.baglanti());
            ty.Parameters.AddWithValue("@p1",txtAd.Text);
            ty.Parameters.AddWithValue("@p2", txtSoyad.Text);
            ty.Parameters.AddWithValue("@p3", mskTelefon.Text);
            ty.Parameters.AddWithValue("@p4", txtSifre.Text);
            ty.Parameters.AddWithValue("@p5", cmbCinsiyet.Text);
            ty.Parameters.AddWithValue("@p6", mskTC.Text);
            ty.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Bilgileriniz güncellenmiştir","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmHastaDetay df=new FrmHastaDetay();
            df.Show();
            this.Hide();
        }
    }
}

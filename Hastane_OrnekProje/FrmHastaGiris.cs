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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglan=new sqlbaglantisi();

        private void lnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayıt ty = new FrmHastaKayıt();
            ty.Show();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select *From dbo.Hastalar where HastaTC=@p1 and HastaSifre=@p2",baglan.baglanti());
            //baglanti komutunu çağırdığımızda zaten bağlantı otomatik açılmış oluyor bir daha yazmaya gerek kalmıyor,sadece en sonde kapat diyoruz.
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader rdr = komut.ExecuteReader();
            if(rdr.Read())
            {
                FrmHastaDetay ab=new FrmHastaDetay();
                ab.TC = mskTC.Text; //Yukarıdaki ab sayesinde diğer forma erişebildiğim için oraki public ögesine erişebildim
                ab.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı şifre veya Tc ");
            }
            baglan.baglanti().Close();
        }
    }
}

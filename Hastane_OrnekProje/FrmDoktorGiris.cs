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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglan= new sqlbaglantisi();
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand cmd=new SqlCommand("Select * From dbo.Doktorlar where DoktorTC=@p1 and DoktorSifre=@p2",baglan.baglanti());
            cmd.Parameters.AddWithValue("@p1",mskTC.Text);
            cmd.Parameters.AddWithValue("@p2",txtSifre.Text);
            SqlDataReader dr=cmd.ExecuteReader();
            if (dr.Read()) { 
                FrmDoktorDetay frs=new FrmDoktorDetay();
                frs.TC=mskTC.Text;
                frs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı şifre veya TC numarası","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            baglan.baglanti().Close();
        }
    }
}

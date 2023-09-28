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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglan=new sqlbaglantisi();
        public string TC;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;
            SqlCommand cmd = new SqlCommand("Select DoktorAd,DoktorSoyad From dbo.Doktorlar where DoktorTC=@p1",baglan.baglanti());
            cmd.Parameters.AddWithValue("@p1",lblTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            baglan.baglanti().Close();
            //randevular
            DataTable dt=new DataTable();
            SqlDataAdapter dp = new SqlDataAdapter("Select * From dbo.Randevular where RandevuDoktor='"+lblAdSoyad.Text+"'",baglan.baglanti());
            dp.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle frs=new FrmDoktorBilgiDuzenle();
            frs.TCno = lblTC.Text;
            frs.Show();
            
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular tr=new FrmDuyurular();
            tr.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secillen = dataGridView1.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView1.Rows[secillen].Cells[7].Value.ToString();
        }
    }
}

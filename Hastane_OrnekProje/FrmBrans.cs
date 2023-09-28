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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglan= new sqlbaglantisi();
        private void FrmBrans_Load(object sender, EventArgs e)
        {  DataTable dt = new DataTable();
            SqlDataAdapter dp = new SqlDataAdapter("Select * From dbo.Branslar",baglan.baglanti());
            dp.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into dbo.Branslar (BransAd) values(@p1)",baglan.baglanti());
            cmd.Parameters.AddWithValue("@p1",txtBrans.Text);
            cmd.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Branş Eklenmiştir","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("Delete From dbo.Branslar where BransID=@p1", baglan.baglanti());
            cmd1.Parameters.AddWithValue("@p1", txtID.Text);
            cmd1.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Brans silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("Update dbo.Doktorlar set BransAd=@p1 where BransID=@p2", baglan.baglanti());
            cmd2.Parameters.AddWithValue("@p1", txtBrans.Text);
            cmd2.Parameters.AddWithValue("@p2", txtID.Text);
          
            cmd2.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Branş Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
           
            
        }
    }
}

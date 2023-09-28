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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglan= new sqlbaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter dp1 = new SqlDataAdapter("Select * From dbo.Doktorlar", baglan.baglanti());
            dp1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Brans ekleme
            SqlCommand cb = new SqlCommand("Select BransAd From dbo.Branslar", baglan.baglanti());
            SqlDataReader dr = cb.ExecuteReader();
            while (dr.Read())
            {
                cmbBrans.Items.Add(dr[0]);//Combobox olduğu için bunun text i yok öge ekleyip çalıştırdığında ise istediğini seçeceksin
            }
            baglan.baglanti().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into dbo.Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values(@p1,@p2,@p3,@p4,@p5)",baglan.baglanti());
            cmd.Parameters.AddWithValue("@p1",txtAd.Text);
            cmd.Parameters.AddWithValue("@p2",txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3",cmbBrans.Text);
            cmd.Parameters.AddWithValue("@p4",mskTC.Text);
            cmd.Parameters.AddWithValue("@p5",txtSifre.Text);
            cmd.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Doktor eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();//cells[0] da ID olduğu için
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("Delete From dbo.Doktorlar where DoktorTC=@p1",baglan.baglanti());
            cmd1.Parameters.AddWithValue("@p1",mskTC.Text);
            cmd1.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Doktor silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("Update dbo.Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p5 where DoktorTC=@p4",baglan.baglanti());
            cmd2.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd2.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd2.Parameters.AddWithValue("@p3", cmbBrans.Text);
            cmd2.Parameters.AddWithValue("@p4", mskTC.Text);
            cmd2.Parameters.AddWithValue("@p5", txtSifre.Text);
            cmd2.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Doktor Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}

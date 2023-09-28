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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string TC;
        sqlbaglantisi baglan = new sqlbaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;
            SqlCommand cmd = new SqlCommand("Select HastaAd,HastaSoyad From dbo.Hastalar where HastaTC=@p1",baglan.baglanti());
            cmd.Parameters.AddWithValue("@p1",lblTC.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) //rd 0 ad,rd 1 ise soyad ,hastaad ve hasta soyad seçtiğimiz için böyle oldu yoksa * diyip hepsini seçseydik tablodaki en baştaki elemandan saymaya başlıyor yani 0 ID 1 ad 2 soyad gibi...
            {
                lblAdSoyad.Text = rd[0] + "  " + rd[1]; //<<C# ın sağladığı bir kolaylık boşluk bırakarak stringe çevirmek zorunda kalmadık
            }                                         //ama bunla aynı mantık sadece ad-soyad arasında boşluk olsun diye rd[0].ToString() +  rd[1].ToString()
            baglan.baglanti().Close();

            //randevu çekme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From dbo.Randevular where HastaTC="+TC,baglan.baglanti());
            da.Fill(dt);//aslında burada dataAdapter den gelen bilgileri dataTable ile oluşturduğumuz sanal tabloya kaydet diyoruz
            dataGridView1.DataSource = dt;//datagrid1 in veri kaynağını oluşturduğumuz sanal tablo ile karşılıyoruz.

            //Brans çekme
            SqlCommand cb = new SqlCommand("Select BransAd From dbo.Branslar",baglan.baglanti());
            SqlDataReader dr = cb.ExecuteReader();
            while (dr.Read())
            {
                cmbBrans.Items.Add(dr[0]);//Combobox olduğu için bunun text i yok öge ekleyip çalıştırdığında ise istediğini seçeceksin
            }
            baglan.baglanti().Close();

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand cb2 = new SqlCommand("Select DoktorAd,DoktorSoyad From dbo.Doktorlar where DoktorBrans=@p1",baglan.baglanti());
            cb2.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr2 = cb2.ExecuteReader();
            while (dr2.Read())
            {
                cmbDoktor.Items.Add(dr2[0]+" " + dr2[1]);
            }
            baglan.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tr = new DataTable();
            SqlDataAdapter dm = new SqlDataAdapter("Select * From dbo.Randevular where RandevuBrans='"+cmbBrans.Text+"'"+"and RandevuDoktor='"+cmbDoktor.Text+"'"+"and RandevuDurum=0",baglan.baglanti());
            dm.Fill(tr);
            dataGridView2.DataSource = tr;
        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCno = lblTC.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("Update dbo.Randevular set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where RandevuID=@p3",baglan.baglanti());
            cmd2.Parameters.AddWithValue("@p1",lblTC.Text);
            cmd2.Parameters.AddWithValue("@p2",rchSikayet.Text);
            cmd2.Parameters.AddWithValue("@p3",txtID.Text);
            cmd2.ExecuteNonQuery();
            rchSikayet.Text = "";
            txtID.Text = "";
            baglan.baglanti().Close();
            MessageBox.Show("Randevu Alınmıştır.\nSağlıklı günler dileriz.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}

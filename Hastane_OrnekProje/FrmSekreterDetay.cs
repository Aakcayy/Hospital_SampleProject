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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        public string TC;
        sqlbaglantisi baglan=new sqlbaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;
            SqlCommand cmd = new SqlCommand("Select SekreterAdSoyad From dbo.Sekreter where SekreterTC=@p1", baglan.baglanti());
            cmd.Parameters.AddWithValue("@p1", lblTC.Text); //AD-SOYAD
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) 
            {
                lblAdSoyad.Text = rd[0].ToString();
            }
            baglan.baglanti().Close();
            DataTable dt = new DataTable(); //Datagrid e brans çekme
            SqlDataAdapter dp = new SqlDataAdapter("Select * From dbo.Branslar ",baglan.baglanti());
            dp.Fill(dt);
            dataGridView1.DataSource = dt;
            //Datagrid e doktor çekme
            DataTable dt2 = new DataTable();
            SqlDataAdapter dp2 = new SqlDataAdapter("Select (DoktorAd+ ' ' +DoktorSoyad) as 'Doktorlar',DoktorBrans From dbo.Doktorlar", baglan.baglanti());
            dp2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            //Combobox a Brans çekme
            SqlCommand cb = new SqlCommand("Select BransAd From dbo.Branslar", baglan.baglanti());
            SqlDataReader dr = cb.ExecuteReader();
            while (dr.Read())
            {
                cmbBrans.Items.Add(dr[0]);//Combobox olduğu için bunun text i yok öge ekleyip çalıştırdığında ise istediğini seçeceksin
            }
            baglan.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand df = new SqlCommand("Insert into dbo.Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values(@p1,@p2,@p3,@p4)", baglan.baglanti());
            df.Parameters.AddWithValue("@p1",mskTarih.Text);
            df.Parameters.AddWithValue("@p2",mskSaat.Text);
            df.Parameters.AddWithValue("@p3",cmbBrans.Text);
            df.Parameters.AddWithValue("@p4",cmbDoktor.Text);
            df.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Randevunuz oluşturulmuştur","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand cb2 = new SqlCommand("Select DoktorAd,DoktorSoyad From dbo.Doktorlar where DoktorBrans=@p1", baglan.baglanti());
            cb2.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr2 = cb2.ExecuteReader();
            while (dr2.Read())
            {
                cmbDoktor.Items.Add(dr2[0] + " " + dr2[1]);
            }
            baglan.baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand rt = new SqlCommand("Insert into dbo.Duyurular (duyuru) values(@d1)",baglan.baglanti());
            rt.Parameters.AddWithValue("@d1", rchDuyuru.Text);
            rt.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Duyuru oluşturulmuştur", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli dg = new FrmDoktorPaneli();
            dg.Show();
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans dp = new FrmBrans();
            dp.Show();
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi db = new FrmRandevuListesi();
            db.Show();
        }

        private void btnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular kb = new FrmDuyurular();
            kb.Show();
        }
    }
}

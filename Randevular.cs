using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Randevular : Form
    {

        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeterinerKln;Integrated Security=True");
        public Randevular()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void button1_Click(object sender, EventArgs e)
        {           
            Hastalar hs = new Hastalar();
            hs.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {          
            İlaclar ilc = new İlaclar();
            ilc.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {           
            Odemeler odm = new Odemeler();
            odm.Show();
            this.Hide();
        }
        void Listele()
        {
            // Sütunlara verdiğin takma isimler (AS [...]), DataGridView'deki sütun adlarım olacak.
            string sorgu = @"SELECT R.RandevuId, H.Ad AS [Hasta Adı], R.Tarih, R.Saat, 
                     R.Durum AS [İşlem Türü], R.Ucret AS [Ücret] 
                     FROM RandevuTbl R 
                     INNER JOIN HastalarTbl H ON R.HastaId = H.HastaId";

            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvRandevular.DataSource = dt;
            dgvRandevular.Columns["Tarih"].DefaultCellStyle.Format = "dd.MM.yyyy";
        }
        void HastaListesiGetir()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();               
                SqlDataAdapter da = new SqlDataAdapter("SELECT HastaId, Ad FROM HastalarTbl", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbHastaAd.DataSource = dt;
                cmbHastaAd.DisplayMember = "Ad";       // Ekranda isimler görünecek 
                cmbHastaAd.ValueMember = "HastaId";   // Arka planda ID'ler tutulacak 

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hastalar yüklenirken hata oluştu: " + ex.Message);
            }
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();           
            string sorgu = "INSERT INTO RandevuTbl (HastaId, Tarih, Saat, Durum, Ucret) VALUES (@p1, @p2, @p3, @p4, @p5)";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
          
            komut.Parameters.AddWithValue("@p1", cmbHastaAd.SelectedValue);
            komut.Parameters.AddWithValue("@p2", dtpTarih.Value);
            komut.Parameters.AddWithValue("@p3", cmbSaat.Text);
            komut.Parameters.AddWithValue("@p4", cmbİslem.Text);
            komut.Parameters.AddWithValue("@p5", txtUcret.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Randevu başarıyla eklendi.");
            Listele();
        }
        private void dgvRandevular_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRandevular.CurrentRow != null)
            {
                // Seçilen satırdaki verileri kutulara dolduruyoruz
                cmbHastaAd.Text = dgvRandevular.CurrentRow.Cells["Hasta Adı"].Value.ToString();
                dtpTarih.Text = dgvRandevular.CurrentRow.Cells["Tarih"].Value.ToString();
                cmbSaat.Text = dgvRandevular.CurrentRow.Cells["Saat"].Value.ToString();               
                cmbİslem.Text = dgvRandevular.CurrentRow.Cells["İşlem Türü"].Value.ToString();
                txtUcret.Text = dgvRandevular.CurrentRow.Cells["Ücret"].Value.ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRandevular.CurrentRow == null) return;

                DialogResult cevap = MessageBox.Show("Bu randevuyu silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (cevap == DialogResult.Yes)
                {

                    if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                    // RandevuId'ye göre siliyoruz                   
                    int secilenId = Convert.ToInt32(dgvRandevular.CurrentRow.Cells["RandevuId"].Value);
                    SqlCommand komut = new SqlCommand("DELETE FROM RandevuTbl WHERE RandevuId=@p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", secilenId);

                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Randevu silindi.");
                    Listele();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();                             
                int secilenId = Convert.ToInt32(dgvRandevular.CurrentRow.Cells["RandevuId"].Value);
                //  ÇAKIŞMA KONTROLÜ (Kendi randevusu hariç, aynı saatte başka randevu var mı?)
                SqlCommand kontrol = new SqlCommand("SELECT COUNT(*) FROM RandevuTbl WHERE Tarih = @t1 AND Saat = @t2 AND RandevuId != @id", baglanti);
                kontrol.Parameters.AddWithValue("@t1", dtpTarih.Value.Date);
                kontrol.Parameters.AddWithValue("@t2", cmbSaat.Text);
                kontrol.Parameters.AddWithValue("@id", secilenId);

                if ((int)kontrol.ExecuteScalar() > 0)
                {
                    MessageBox.Show("Bu tarih ve saatte başka bir randevu zaten var!", "Uyarı");
                    baglanti.Close();
                    return;
                }

                //  GÜNCELLEME SORGUSU
                SqlCommand komut = new SqlCommand("UPDATE RandevuTbl SET HastaId=@p1, Tarih=@p2, Saat=@p3, Durum=@p4 WHERE RandevuId=@p5", baglanti);
                komut.Parameters.AddWithValue("@p1", cmbHastaAd.SelectedValue); // ID gönderiyoruz
                komut.Parameters.AddWithValue("@p2", dtpTarih.Value.Date);
                komut.Parameters.AddWithValue("@p3", cmbSaat.Text);
                komut.Parameters.AddWithValue("@p4", txtDurum.Text);
                komut.Parameters.AddWithValue("@p5", secilenId);

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Randevu başarıyla güncellendi.");
                Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }
        private void Randevular_Load(object sender, EventArgs e)
        {
            Listele();      // Tabloyu doldurur
            HastaListesiGetir();   // ComboBox'ı doldurur
        }
    }
}
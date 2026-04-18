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

namespace WindowsFormsApp1
{
    public partial class Odemeler : Form
    {
        // BAĞLANTI SATIRI
        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeterinerKln;Integrated Security=True");

        public Odemeler()
        {
            InitializeComponent();
        }

        // ÜST BUTONLAR (GEÇİŞLER)
        private void button5_Click(object sender, EventArgs e) { Application.Exit(); }
        private void button1_Click(object sender, EventArgs e) { Hastalar hs = new Hastalar(); hs.Show(); this.Hide(); }
        private void button2_Click(object sender, EventArgs e) { Randevular rd = new Randevular(); rd.Show(); this.Hide(); }
        private void button3_Click(object sender, EventArgs e) { İlaclar ilc = new İlaclar(); ilc.Show(); this.Hide(); }

        //  İŞLEM TÜRLERİNİ GETİR (RandevuTbl'den)
        void IslemTurleriniGetir()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                // Tabloda var olan 'Durum' sütunundaki tüm farklı işlemleri getirir
                SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT Durum FROM RandevuTbl", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbİslemTuru.DataSource = dt;
                cmbİslemTuru.DisplayMember = "Durum"; // Listede randevudaki 'Durum' bilgisi görünür
                cmbİslemTuru.SelectedIndex = -1;

                baglanti.Close();
            }
            catch (Exception ex)
            {
                // Hata alırsam hangi sütun eksik burada yazacak.
                MessageBox.Show("İşlem listesi hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        //  ÖDEMELERİ LİSTELE (DataGrid için)
        void OdemeListele()
        {
            try
            {
                string sorgu = "SELECT I.İslemId, H.Ad AS [Hasta Adı], I.İslemTuru, I.Ucret, I.OdemeDurumu " +
                               "FROM İslemlerTbl I INNER JOIN HastalarTbl H ON I.HastaId = H.HastaId";
                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvOdemeler.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Listeleme Hatası: " + ex.Message); }
        }

        //  HASTALARI GETİR
        void HastaListesiGetir()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT HastaId, Ad FROM HastalarTbl", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbHastaAd.DataSource = dt;
            cmbHastaAd.DisplayMember = "Ad";
            cmbHastaAd.ValueMember = "HastaId";
        }

        // FORM YÜKLENİRKEN
        private void Odemeler_Load(object sender, EventArgs e)
        {
            HastaListesiGetir();
            IslemTurleriniGetir();
            OdemeListele();
        }
       
        // EKLE BUTONU
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO İslemlerTbl (HastaId, İslemTuru, Ucret, OdemeDurumu) VALUES (@p1, @p2, @p3, @p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", cmbHastaAd.SelectedValue);
                komut.Parameters.AddWithValue("@p2", cmbİslemTuru.Text);                
                komut.Parameters.AddWithValue("@p3", Convert.ToInt32(txtUcret.Text));
                komut.Parameters.AddWithValue("@p4", cmbDurum.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("İşlem Kaydedildi");
                OdemeListele();
                Temizle();
            }
            catch (Exception ex) { MessageBox.Show("Ekleme Hatası: " + ex.Message); }
        }

        // GÜNCELLE BUTONU
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                int secilenId = Convert.ToInt32(dgvOdemeler.CurrentRow.Cells["İslemId"].Value);
                SqlCommand komut = new SqlCommand("UPDATE İslemlerTbl SET HastaId=@p1, İslemTuru=@p2, Ucret=@p3, OdemeDurumu=@p4 WHERE İslemId=@p5", baglanti);
                komut.Parameters.AddWithValue("@p1", cmbHastaAd.SelectedValue);
                komut.Parameters.AddWithValue("@p2", cmbİslemTuru.Text);
                komut.Parameters.AddWithValue("@p3", txtUcret.Text);
                komut.Parameters.AddWithValue("@p4", cmbDurum.Text);
                komut.Parameters.AddWithValue("@p5", secilenId);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncellendi");
                OdemeListele();
                Temizle();
            }
            catch (Exception ex) { MessageBox.Show("Güncelleme Hatası: " + ex.Message); }
        }

        // SİL BUTONU
        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                int secilenId = Convert.ToInt32(dgvOdemeler.CurrentRow.Cells["İslemId"].Value);
                SqlCommand komut = new SqlCommand("DELETE FROM İslemlerTbl WHERE İslemId=@p1", baglanti);
                komut.Parameters.AddWithValue("@p1", secilenId);
                komut.ExecuteNonQuery();
                baglanti.Close();
                OdemeListele();
                Temizle();
            }
            catch (Exception ex) { MessageBox.Show("Silme Hatası: " + ex.Message); }
        }

        void Temizle()
        {
            txtUcret.Clear();
            cmbİslemTuru.SelectedIndex = -1;
            cmbHastaAd.SelectedIndex = -1;
            cmbDurum.SelectedIndex = -1;
        }

        private void dgvOdemeler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Tıkladığım satırın boş olmadığından emin oluyorum
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = dgvOdemeler.Rows[e.RowIndex];

                // TextBox ve ComboBox'lara verileri aktaralım
                // Sütun isimleri veritabanı sorgundaki AS [İsim] kısımlarıyla aynı olmalı!
                cmbHastaAd.Text = satir.Cells["Hasta Adı"].Value.ToString();
                cmbİslemTuru.Text = satir.Cells["İslemTuru"].Value.ToString();
                txtUcret.Text = satir.Cells["Ucret"].Value.ToString();
                cmbDurum.Text = satir.Cells["OdemeDurumu"].Value.ToString();
            }
        }
    }
}
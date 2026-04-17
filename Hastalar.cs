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
    public partial class Hastalar : Form
    {
        public Hastalar()
        {
            InitializeComponent();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Butonlara tıklandığında formlar arası geçişi sağlamak için.
            Randevular rd = new Randevular();
            rd.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //Butonlara tıklandığında formlar arası geçişi sağlamak için.
            İlaclar ilc = new İlaclar();
            ilc.Show();
            this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //Butonlara tıklandığında formlar arası geçişi sağlamak için.
            Odemeler odm = new Odemeler();
            odm.Show();
            this.Hide();
        }
        private void btnHastaEkle_Click(object sender, EventArgs e)
        {
            //  Veritabanı bağlantı adresini tanımladım.
            string baglantiAdresi = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeterinerKln;Integrated Security=True";

            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                try
                {
                    baglanti.Open();

                    // ÖNCE MÜŞTERİYİ KAYDET VE ONUN ID'SİNİ AL
                    // (MusteriTbl'ye bilgileri yazıp, oluşan otomatik ID'yi SCOPE_IDENTITY ile çekiyoruz)
                    string mSorgu = "INSERT INTO MusteriTbl (Ad, Soyad, Telefon, Adres) VALUES ('" + txtHsAd.Text + "', '" + txtHsSoyad.Text + "'," +" '" + txtHsTel.Text + "', '" + txtHsAdres.Text + "'); SELECT SCOPE_IDENTITY();";
                    SqlCommand mKomut = new SqlCommand(mSorgu, baglanti);

                    // ExecuteScalar kullanarak veritabanından dönen o ID numarasını alıyoruz
                    int yeniMusteriId = Convert.ToInt32(mKomut.ExecuteScalar());

                    // ALINAN MÜŞTERİ ID'Sİ İLE HASTAYI KAYDET
                    string hSorgu = "INSERT INTO HastalarTbl (MusteriId, Ad, Tur, Cins, Yas) VALUES (" + yeniMusteriId + ", '" + txtHastaAd.Text + "'," +" '" + txtHastaTur.Text + "', '" + txtHastaCins.Text + "', '" + txtHastaYas.Text + "')";
                    SqlCommand hKomut = new SqlCommand(hSorgu, baglanti);
                    hKomut.ExecuteNonQuery();

                    // KULLANICIYA HABER VER
                    MessageBox.Show("Kayıt Başarıyla Tamamlandı! Hem sahip hem de dostumuz eklendi. ✔️");

                    // LİSTEYİ YENİLE (Sağdaki DataGridView)
                    Listele();

                    // KUTULARI TEMİZLE (Sol taraftaki TextBox'lar boşalsın)
                    Temizle();
                }
                catch (Exception ex)
                {
                    // Eğer bir hata olursa burası çalışacak ve nedenini söyleyecek.
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }        
        private void Listele()
        {
            // Bu metot verileri veritabanından çekip DataGridView'a basar
            string baglantiAdresi = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeterinerKln;Integrated Security=True";
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                string sorgu = "SELECT * FROM HastalarTbl";
                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["MusteriId"].Visible = false;
            }
        }
        private void Hastalar_Load(object sender, EventArgs e)
        {
            Listele(); // Daha önce yazdığımız listeleme fonksiyonunu çağırdım.
        }

         int secilenHastaId = 0; 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 1. Hasta Bilgilerini Al
                secilenHastaId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["HastaId"].Value);
                txtHastaAd.Text = dataGridView1.Rows[e.RowIndex].Cells["Ad"].Value.ToString();
                txtHastaTur.Text = dataGridView1.Rows[e.RowIndex].Cells["Tur"].Value.ToString();
                txtHastaCins.Text = dataGridView1.Rows[e.RowIndex].Cells["Cins"].Value.ToString();
                txtHastaYas.Text = dataGridView1.Rows[e.RowIndex].Cells["Yas"].Value.ToString();

                // 2. Müşteri Bilgilerini Al (MusteriId kullanarak veritabanından çekiyoruz)
                int mId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MusteriId"].Value);
                MusteriBilgileriniGetir(mId);
            }
        }

        // Yardımcı Metot: Müşteri bilgilerini kutulara doldurur
        private void MusteriBilgileriniGetir(int musteriId)
        {
            string baglantiAdresi = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeterinerKln;Integrated Security=True";
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                baglanti.Open();
                string sorgu = "SELECT * FROM MusteriTbl WHERE MusteriId = " + musteriId;
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                SqlDataReader oku = komut.ExecuteReader();

                if (oku.Read())
                {
                    txtHsAd.Text = oku["Ad"].ToString();
                    txtHsSoyad.Text = oku["Soyad"].ToString();
                    txtHsTel.Text = oku["Telefon"].ToString();
                    txtHsAdres.Text = oku["Adres"].ToString();
                }
            }

        }

        private void btnHastaGuncelle_Click(object sender, EventArgs e)
        {
            if (secilenHastaId == 0) { MessageBox.Show("Lütfen bir kayıt seçin!"); return; }

            string baglantiAdresi = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeterinerKln;Integrated Security=True";

            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                try
                {
                    baglanti.Open();

                    // Önce bu hastanın sahibi kim onu bulalım
                    string idSorgu = "SELECT MusteriId FROM HastalarTbl WHERE HastaId = " + secilenHastaId;
                    SqlCommand idKomut = new SqlCommand(idSorgu, baglanti);
                    int mId = Convert.ToInt32(idKomut.ExecuteScalar());

                    // 1. HASTA GÜNCELLEME
                    string hSorgu = "UPDATE HastalarTbl SET Ad='" + txtHastaAd.Text + "', Tur='" + txtHastaTur.Text + "', Cins='" + txtHastaCins.Text + "'," +
                        " Yas='" + txtHastaYas.Text + "' WHERE HastaId=" + secilenHastaId;
                    SqlCommand hKomut = new SqlCommand(hSorgu, baglanti);
                    hKomut.ExecuteNonQuery();

                    // 2. MÜŞTERİ (SAHİBİ) GÜNCELLEME
                    string mSorgu = "UPDATE MusteriTbl SET Ad='" + txtHsAd.Text + "', Soyad='" + txtHsSoyad.Text + "', Telefon='" + txtHsTel.Text + "'," +
                        " Adres='" + txtHsAdres.Text + "' WHERE MusteriId=" + mId;
                    SqlCommand mKomut = new SqlCommand(mSorgu, baglanti);
                    mKomut.ExecuteNonQuery();
                    MessageBox.Show("Tüm bilgiler (Hasta ve Sahibi) başarıyla güncellendi! ✨");
                    Listele();
                    Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void btnHastaSil_Click(object sender, EventArgs e)
        {
            if (secilenHastaId == 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz hastayı listeden seçin!");
                return;
            }

            DialogResult secenek = MessageBox.Show("Bu hastayı ve sahibini silmek istediğinize emin misiniz?", "Tamamen Silme Onayı",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (secenek == DialogResult.Yes)
            {
                string baglantiAdresi = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeterinerKln;Integrated Security=True";

                using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
                {
                    try
                    {
                        baglanti.Open();

                        //  ÖNCE HASTANIN BAĞLI OLDUĞU MÜŞTERİ ID'SİNİ BULALIM
                        string bulSorgu = "SELECT MusteriId FROM HastalarTbl WHERE HastaId = " + secilenHastaId;
                        SqlCommand bulKomut = new SqlCommand(bulSorgu, baglanti);
                        int silinecekMusteriId = Convert.ToInt32(bulKomut.ExecuteScalar());

                        // HASTAYI SİLELİM
                        string hSilSorgu = "DELETE FROM HastalarTbl WHERE HastaId = " + secilenHastaId;
                        SqlCommand hSilKomut = new SqlCommand(hSilSorgu, baglanti);
                        hSilKomut.ExecuteNonQuery();

                        // MÜŞTERİYİ SİLELİM
                        string mSilSorgu = "DELETE FROM MusteriTbl WHERE MusteriId = " + silinecekMusteriId;
                        SqlCommand mSilKomut = new SqlCommand(mSilSorgu, baglanti);
                        mSilKomut.ExecuteNonQuery();

                        MessageBox.Show("Kayıtlar (Hasta ve Sahibi) tamamen silindi.");

                        Listele();
                        Temizle();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata oluştu: " + ex.Message);
                    }
                }
            }
        }
        private void Temizle()
        {
            // Hasta Sahibi Bilgileri
            txtHsAd.Clear();
            txtHsSoyad.Clear();
            txtHsTel.Clear();
            txtHsAdres.Clear();

            // Hasta Bilgileri
            txtHastaAd.Clear();
            txtHastaTur.Clear();
            txtHastaCins.Clear();
            txtHastaYas.Clear();

            // Seçili ID'yi de sıfırlayalım ki karışıklık olmasın
            secilenHastaId = 0;
        }
    }
}

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
    public partial class İlaclar : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeterinerKln;Integrated Security=True");
        public İlaclar()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Butonlara tıklandığında formlar arası geçişi sağlamak için.
            Hastalar hs = new Hastalar();
            hs.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {          
            Randevular rd = new Randevular();
            rd.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {          
            Odemeler odm = new Odemeler();
            odm.Show();
            this.Hide();
        }
        void Temizle()
        {
            txtİlacAd.Text = "";
            txtMiktar.Text = "";
            txtFiyat.Text = "";          
        }
        void IlacListele()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();               
                SqlDataAdapter da = new SqlDataAdapter("SELECT UrunId, UrunAdi, StokMiktari, BirimFiyati FROM İlaclarTbl", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvİlaclar.DataSource = dt; 
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme Hatası: " + ex.Message);
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                SqlCommand komut = new SqlCommand("INSERT INTO İlaclarTbl (UrunAdi, StokMiktari, BirimFiyati) VALUES (@p1, @p2, @p3)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtİlacAd.Text);
                komut.Parameters.AddWithValue("@p2", Convert.ToInt32(txtMiktar.Text));
                komut.Parameters.AddWithValue("@p3", Convert.ToInt32(txtFiyat.Text));

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Yeni ilaç eklendi.", "Kayıt");

                IlacListele(); //LİSTELEME İŞLEMİ
                Temizle(); // ALANLARI SİL
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme Hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        private void dgvİlaclar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Tıklanan satırdaki verileri TextBox'lara alıyorum.
            txtİlacAd.Text = dgvİlaclar.CurrentRow.Cells["UrunAdi"].Value.ToString();
            txtMiktar.Text = dgvİlaclar.CurrentRow.Cells["StokMiktari"].Value.ToString();
            txtFiyat.Text = dgvİlaclar.CurrentRow.Cells["BirimFiyati"].Value.ToString();
        }
                
        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvİlaclar.CurrentRow == null) return;

                DialogResult onay = MessageBox.Show("Bu ilacı silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (onay == DialogResult.Yes)
                {
                    if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                    int secilenId = Convert.ToInt32(dgvİlaclar.CurrentRow.Cells["UrunId"].Value);
                    SqlCommand komut = new SqlCommand("DELETE FROM İlaclarTbl WHERE UrunId=@p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", secilenId);

                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("İlaç silindi ve alanlar temizlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    IlacListele(); // Listeyi yenile
                    Temizle();     // ALANLARI SİL
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme Hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvİlaclar.CurrentRow == null) return;

                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                // DataGridView'dan güncellenecek ilacın ID'sini alıyorum
                int secilenId = Convert.ToInt32(dgvİlaclar.CurrentRow.Cells["UrunId"].Value);

                SqlCommand komut = new SqlCommand("UPDATE İlaclarTbl SET UrunAdi=@p1, StokMiktari=@p2, BirimFiyati=@p3 WHERE UrunId=@p4", baglanti);
                komut.Parameters.AddWithValue("@p1", txtİlacAd.Text);
                komut.Parameters.AddWithValue("@p2", Convert.ToInt32(txtMiktar.Text));
                komut.Parameters.AddWithValue("@p3", Convert.ToInt32(txtFiyat.Text));
                komut.Parameters.AddWithValue("@p4", secilenId);

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("İlaç bilgileri güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                IlacListele(); // Listeyi yenile
                Temizle();     // ALANLARI SİL
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme Hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }
        void İlacListele()
        {
            try
            {
                // Bağlantı kapalıysa açıyorum
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT UrunId, UrunAdi, StokMiktari, BirimFiyati FROM İlaclarTbl", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvİlaclar.DataSource = dt;

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler getirilirken hata oluştu: " + ex.Message);
            }
        }
        private void İlaclar_Load(object sender, EventArgs e)
        {
            İlacListele();
        }
    }
}

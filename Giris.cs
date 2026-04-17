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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }    
        
        private void btnGiris_Click_1(object sender, EventArgs e)
        {
            string baglantiAdresi = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeterinerKln;Integrated Security=True";

            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                try
                {
                    baglanti.Open();

                    // Hem Ad hem Soyad aynı kutuda olacağı için SQL'de birleştirerek kontrol ediyoruz
                    // Örn: "Deniz Güler" yazarsa, tablodaki KullaniciAd + ' ' + KullaniciSoyad ile eşleşmeli ad soyad arasında boluk olacak 1 tane.
                    string sorgu = "SELECT * FROM KullanicilarTbl WHERE (KullaniciAd + ' ' + KullaniciSoyad) = @adSoyad AND KullaniciSifre = @sifre";

                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@adSoyad", txtGirisAd.Text);
                    komut.Parameters.AddWithValue("@sifre", txtGirisSifre.Text);

                    SqlDataReader oku = komut.ExecuteReader();

                    if (oku.Read()) // Eğer eşleşen bir kayıt varsa
                    {
                        string gorev = oku["KullaniciGorev"].ToString();
                        MessageBox.Show("Hoş geldiniz Sayın " + txtGirisAd.Text + "\nYetki: " + gorev);

                        // Ana Sayfaya geçiş
                        AnaSayfa ana = new AnaSayfa();
                        ana.Show();
                        this.Hide(); // Giriş penceresini gizle
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı! Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtGirisAd.Clear();
                        txtGirisSifre.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bağlantı hatası: " + ex.Message);
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkSifreGoster_CheckedChanged(object sender, EventArgs e)
        {
            // Eğer CheckBox işaretli ise karakterleri göster (null yaparak gizlemeyi kaldırıyoruz)
            if (chkSifreGoster.Checked)
            {
                txtGirisSifre.PasswordChar = '\0'; // '\0' boş karakter demektir, şifreyi görünür yapar
            }
            // Eğer işaretli değilse tekrar yıldız koy
            else
            {
                txtGirisSifre.PasswordChar = '*'; 
            }
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Randevular : Form
    {
        public Randevular()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Butonlara tıklandığında formlar arası geçişi sağlamak için.
            AnaSayfa hs = new AnaSayfa();
            hs.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Butonlara tıklandığında formlar arası geçişi sağlamak için.
            Hastalar hs = new Hastalar();
            hs.Show();
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
    }
}

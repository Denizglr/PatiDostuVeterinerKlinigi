namespace WindowsFormsApp1
{
    partial class Giris
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Giris));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtGirisSifre = new System.Windows.Forms.TextBox();
            this.txtGirisAd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGiris = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.chkSifreGoster = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-84, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(370, 216);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // txtGirisSifre
            // 
            this.txtGirisSifre.BackColor = System.Drawing.Color.OldLace;
            this.txtGirisSifre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGirisSifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtGirisSifre.Location = new System.Drawing.Point(309, 130);
            this.txtGirisSifre.Name = "txtGirisSifre";
            this.txtGirisSifre.PasswordChar = '*';
            this.txtGirisSifre.Size = new System.Drawing.Size(147, 14);
            this.txtGirisSifre.TabIndex = 8;
            // 
            // txtGirisAd
            // 
            this.txtGirisAd.BackColor = System.Drawing.Color.OldLace;
            this.txtGirisAd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGirisAd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtGirisAd.Location = new System.Drawing.Point(308, 72);
            this.txtGirisAd.Name = "txtGirisAd";
            this.txtGirisAd.Size = new System.Drawing.Size(147, 14);
            this.txtGirisAd.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.MistyRose;
            this.label2.Location = new System.Drawing.Point(244, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "ŞİFRE :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.MistyRose;
            this.label1.Location = new System.Drawing.Point(211, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "AD SOYAD :";
            // 
            // btnGiris
            // 
            this.btnGiris.BackColor = System.Drawing.Color.MistyRose;
            this.btnGiris.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGiris.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGiris.ForeColor = System.Drawing.Color.RosyBrown;
            this.btnGiris.Location = new System.Drawing.Point(333, 191);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(104, 39);
            this.btnGiris.TabIndex = 9;
            this.btnGiris.Text = "GİRİŞ ";
            this.btnGiris.UseVisualStyleBackColor = false;
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click_1);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.RosyBrown;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button5.ForeColor = System.Drawing.Color.MistyRose;
            this.button5.Location = new System.Drawing.Point(476, 0);
            this.button5.Margin = new System.Windows.Forms.Padding(0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(49, 34);
            this.button5.TabIndex = 15;
            this.button5.Text = "X";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // chkSifreGoster
            // 
            this.chkSifreGoster.AutoSize = true;
            this.chkSifreGoster.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chkSifreGoster.ForeColor = System.Drawing.Color.MistyRose;
            this.chkSifreGoster.Location = new System.Drawing.Point(328, 155);
            this.chkSifreGoster.Name = "chkSifreGoster";
            this.chkSifreGoster.Size = new System.Drawing.Size(112, 19);
            this.chkSifreGoster.TabIndex = 16;
            this.chkSifreGoster.Text = "Şifreyi Göster";
            this.chkSifreGoster.UseVisualStyleBackColor = true;
            this.chkSifreGoster.CheckedChanged += new System.EventHandler(this.chkSifreGoster_CheckedChanged);
            // 
            // Giris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(525, 264);
            this.Controls.Add(this.chkSifreGoster);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnGiris);
            this.Controls.Add(this.txtGirisSifre);
            this.Controls.Add(this.txtGirisAd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Giris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtGirisSifre;
        private System.Windows.Forms.TextBox txtGirisAd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGiris;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox chkSifreGoster;
    }
}


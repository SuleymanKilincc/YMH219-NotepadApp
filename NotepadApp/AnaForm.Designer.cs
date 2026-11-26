namespace NotepadApp
{
    partial class AnaForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            mnuDosya = new ToolStripMenuItem();
            mnuKaydet = new ToolStripMenuItem();
            mnuYenidenYukle = new ToolStripMenuItem();
            mnuDisaAktar = new ToolStripMenuItem();
            mnuAyrac = new ToolStripSeparator();
            mnuCikis = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            lblDurum = new ToolStripStatusLabel();
            lblDosyaYolu = new ToolStripStatusLabel();
            splitContainer = new SplitContainer();
            lstNotlar = new ListBox();
            pnlArama = new Panel();
            txtAra = new TextBox();
            lblAra = new Label();
            rtbIcerik = new RichTextBox();
            pnlBaslik = new Panel();
            lblTarihler = new Label();
            txtBaslik = new TextBox();
            lblBaslik = new Label();
            pnlButonlar = new FlowLayoutPanel();
            btnYeni = new Button();
            btnKaydet = new Button();
            btnSil = new Button();
            btnDisaAktar = new Button();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            pnlArama.SuspendLayout();
            pnlBaslik.SuspendLayout();
            pnlButonlar.SuspendLayout();
            SuspendLayout();
            //
            // menuStrip
            //
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { mnuDosya });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(982, 28);
            menuStrip.TabIndex = 0;
            //
            // mnuDosya
            //
            mnuDosya.DropDownItems.AddRange(new ToolStripItem[] { mnuKaydet, mnuYenidenYukle, mnuDisaAktar, mnuAyrac, mnuCikis });
            mnuDosya.Name = "mnuDosya";
            mnuDosya.Size = new Size(62, 24);
            mnuDosya.Text = "&Dosya";
            //
            // mnuKaydet
            //
            mnuKaydet.Name = "mnuKaydet";
            mnuKaydet.ShortcutKeys = Keys.Control | Keys.S;
            mnuKaydet.Size = new Size(260, 26);
            mnuKaydet.Text = "Notu &Kaydet";
            mnuKaydet.Click += btnKaydet_Click;
            //
            // mnuYenidenYukle
            //
            mnuYenidenYukle.Name = "mnuYenidenYukle";
            mnuYenidenYukle.ShortcutKeys = Keys.F5;
            mnuYenidenYukle.Size = new Size(260, 26);
            mnuYenidenYukle.Text = "Dosyadan &Yeniden Yükle";
            mnuYenidenYukle.Click += mnuYenidenYukle_Click;
            //
            // mnuDisaAktar
            //
            mnuDisaAktar.Name = "mnuDisaAktar";
            mnuDisaAktar.ShortcutKeys = Keys.Control | Keys.E;
            mnuDisaAktar.Size = new Size(260, 26);
            mnuDisaAktar.Text = ".txt Olarak &Dışa Aktar";
            mnuDisaAktar.Click += btnDisaAktar_Click;
            //
            // mnuAyrac
            //
            mnuAyrac.Name = "mnuAyrac";
            mnuAyrac.Size = new Size(257, 6);
            //
            // mnuCikis
            //
            mnuCikis.Name = "mnuCikis";
            mnuCikis.Size = new Size(260, 26);
            mnuCikis.Text = "Çı&kış";
            mnuCikis.Click += mnuCikis_Click;
            //
            // statusStrip
            //
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDurum, lblDosyaYolu });
            statusStrip.Location = new Point(0, 627);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(982, 26);
            statusStrip.TabIndex = 2;
            //
            // lblDurum
            //
            lblDurum.Name = "lblDurum";
            lblDurum.Size = new Size(50, 20);
            lblDurum.Text = "Hazır.";
            //
            // lblDosyaYolu
            //
            lblDosyaYolu.ForeColor = SystemColors.GrayText;
            lblDosyaYolu.Name = "lblDosyaYolu";
            lblDosyaYolu.Size = new Size(917, 20);
            lblDosyaYolu.Spring = true;
            lblDosyaYolu.TextAlign = ContentAlignment.MiddleRight;
            //
            // splitContainer
            //
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 28);
            splitContainer.Name = "splitContainer";
            //
            // splitContainer.Panel1
            //
            splitContainer.Panel1.Controls.Add(lstNotlar);
            splitContainer.Panel1.Controls.Add(pnlArama);
            splitContainer.Panel1.Padding = new Padding(8);
            //
            // splitContainer.Panel2
            //
            splitContainer.Panel2.Controls.Add(rtbIcerik);
            splitContainer.Panel2.Controls.Add(pnlBaslik);
            splitContainer.Panel2.Controls.Add(pnlButonlar);
            splitContainer.Panel2.Padding = new Padding(8);
            splitContainer.Size = new Size(982, 599);
            splitContainer.SplitterDistance = 330;
            splitContainer.TabIndex = 1;
            //
            // lstNotlar
            //
            lstNotlar.Dock = DockStyle.Fill;
            lstNotlar.FormattingEnabled = true;
            lstNotlar.IntegralHeight = false;
            lstNotlar.Location = new Point(8, 66);
            lstNotlar.Name = "lstNotlar";
            lstNotlar.Size = new Size(314, 525);
            lstNotlar.TabIndex = 1;
            lstNotlar.SelectedIndexChanged += lstNotlar_SelectedIndexChanged;
            //
            // pnlArama
            //
            pnlArama.Controls.Add(txtAra);
            pnlArama.Controls.Add(lblAra);
            pnlArama.Dock = DockStyle.Top;
            pnlArama.Location = new Point(8, 8);
            pnlArama.Name = "pnlArama";
            pnlArama.Size = new Size(314, 58);
            pnlArama.TabIndex = 0;
            //
            // txtAra
            //
            txtAra.Dock = DockStyle.Top;
            txtAra.Location = new Point(0, 20);
            txtAra.Name = "txtAra";
            txtAra.PlaceholderText = "Başlık veya içerikte ara...";
            txtAra.Size = new Size(314, 27);
            txtAra.TabIndex = 1;
            txtAra.TextChanged += txtAra_TextChanged;
            //
            // lblAra
            //
            lblAra.Dock = DockStyle.Top;
            lblAra.Location = new Point(0, 0);
            lblAra.Name = "lblAra";
            lblAra.Size = new Size(314, 20);
            lblAra.TabIndex = 0;
            lblAra.Text = "Notlar";
            //
            // rtbIcerik
            //
            rtbIcerik.Dock = DockStyle.Fill;
            rtbIcerik.Font = new Font("Segoe UI", 11F);
            rtbIcerik.Location = new Point(8, 94);
            rtbIcerik.Name = "rtbIcerik";
            rtbIcerik.Size = new Size(632, 449);
            rtbIcerik.TabIndex = 1;
            rtbIcerik.Text = "";
            //
            // pnlBaslik
            //
            pnlBaslik.Controls.Add(lblTarihler);
            pnlBaslik.Controls.Add(txtBaslik);
            pnlBaslik.Controls.Add(lblBaslik);
            pnlBaslik.Dock = DockStyle.Top;
            pnlBaslik.Location = new Point(8, 8);
            pnlBaslik.Name = "pnlBaslik";
            pnlBaslik.Size = new Size(632, 86);
            pnlBaslik.TabIndex = 0;
            //
            // lblTarihler
            //
            lblTarihler.Dock = DockStyle.Top;
            lblTarihler.ForeColor = SystemColors.GrayText;
            lblTarihler.Location = new Point(0, 52);
            lblTarihler.Name = "lblTarihler";
            lblTarihler.Size = new Size(632, 26);
            lblTarihler.TabIndex = 2;
            lblTarihler.Text = "Yeni not";
            lblTarihler.TextAlign = ContentAlignment.MiddleLeft;
            //
            // txtBaslik
            //
            txtBaslik.Dock = DockStyle.Top;
            txtBaslik.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtBaslik.Location = new Point(0, 20);
            txtBaslik.Name = "txtBaslik";
            txtBaslik.PlaceholderText = "Not başlığı";
            txtBaslik.Size = new Size(632, 34);
            txtBaslik.TabIndex = 1;
            //
            // lblBaslik
            //
            lblBaslik.Dock = DockStyle.Top;
            lblBaslik.Location = new Point(0, 0);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new Size(632, 20);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "Başlık";
            //
            // pnlButonlar
            //
            pnlButonlar.Controls.Add(btnYeni);
            pnlButonlar.Controls.Add(btnKaydet);
            pnlButonlar.Controls.Add(btnSil);
            pnlButonlar.Controls.Add(btnDisaAktar);
            pnlButonlar.Dock = DockStyle.Bottom;
            pnlButonlar.Location = new Point(8, 543);
            pnlButonlar.Name = "pnlButonlar";
            pnlButonlar.Padding = new Padding(0, 6, 0, 0);
            pnlButonlar.Size = new Size(632, 48);
            pnlButonlar.TabIndex = 2;
            //
            // btnYeni
            //
            btnYeni.AutoSize = true;
            btnYeni.Location = new Point(3, 9);
            btnYeni.Name = "btnYeni";
            btnYeni.Size = new Size(110, 32);
            btnYeni.TabIndex = 0;
            btnYeni.Text = "Yeni Not";
            btnYeni.UseVisualStyleBackColor = true;
            btnYeni.Click += btnYeni_Click;
            //
            // btnKaydet
            //
            btnKaydet.AutoSize = true;
            btnKaydet.Location = new Point(119, 9);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(110, 32);
            btnKaydet.TabIndex = 1;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = true;
            btnKaydet.Click += btnKaydet_Click;
            //
            // btnSil
            //
            btnSil.AutoSize = true;
            btnSil.Location = new Point(235, 9);
            btnSil.Name = "btnSil";
            btnSil.Size = new Size(110, 32);
            btnSil.TabIndex = 2;
            btnSil.Text = "Sil";
            btnSil.UseVisualStyleBackColor = true;
            btnSil.Click += btnSil_Click;
            //
            // btnDisaAktar
            //
            btnDisaAktar.AutoSize = true;
            btnDisaAktar.Location = new Point(351, 9);
            btnDisaAktar.Name = "btnDisaAktar";
            btnDisaAktar.Size = new Size(150, 32);
            btnDisaAktar.TabIndex = 3;
            btnDisaAktar.Text = ".txt Dışa Aktar";
            btnDisaAktar.UseVisualStyleBackColor = true;
            btnDisaAktar.Click += btnDisaAktar_Click;
            //
            // AnaForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 653);
            Controls.Add(splitContainer);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            MinimumSize = new Size(700, 450);
            Name = "AnaForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Not Defteri — YMH219";
            FormClosing += AnaForm_FormClosing;
            Load += AnaForm_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            pnlArama.ResumeLayout(false);
            pnlArama.PerformLayout();
            pnlBaslik.ResumeLayout(false);
            pnlBaslik.PerformLayout();
            pnlButonlar.ResumeLayout(false);
            pnlButonlar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem mnuDosya;
        private ToolStripMenuItem mnuKaydet;
        private ToolStripMenuItem mnuYenidenYukle;
        private ToolStripMenuItem mnuDisaAktar;
        private ToolStripSeparator mnuAyrac;
        private ToolStripMenuItem mnuCikis;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDurum;
        private ToolStripStatusLabel lblDosyaYolu;
        private SplitContainer splitContainer;
        private ListBox lstNotlar;
        private Panel pnlArama;
        private TextBox txtAra;
        private Label lblAra;
        private RichTextBox rtbIcerik;
        private Panel pnlBaslik;
        private Label lblTarihler;
        private TextBox txtBaslik;
        private Label lblBaslik;
        private FlowLayoutPanel pnlButonlar;
        private Button btnYeni;
        private Button btnKaydet;
        private Button btnSil;
        private Button btnDisaAktar;
    }
}

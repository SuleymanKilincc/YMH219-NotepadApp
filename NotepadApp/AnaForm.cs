using NotepadApp.Models;
using NotepadApp.Services;

namespace NotepadApp
{
    /// <summary>
    /// Ana pencere. Kullanıcı olaylarını (tıklama, seçim, arama) karşılar ve
    /// asıl işi NotDeposu sınıfına yaptırır. Form yalnızca ekranı yönetir.
    /// </summary>
    public partial class AnaForm : Form
    {
        private readonly NotDeposu _depo;

        // Editörde açık olan not. null ise kullanıcı henüz kaydedilmemiş yeni bir not yazıyor.
        private Not? _seciliNot;

        // Liste kodla doldurulurken SelectedIndexChanged olayının tetiklenmesini engeller.
        private bool _listeGuncelleniyor;

        public AnaForm()
        {
            InitializeComponent();
            _depo = new NotDeposu(NotDeposu.VarsayilanDosyaYolu());
        }

        // ---------------------------------------------------------------
        // Form açılış / kapanış
        // ---------------------------------------------------------------

        private void AnaForm_Load(object sender, EventArgs e)
        {
            lblDosyaYolu.Text = _depo.DosyaYolu;
            NotlariDosyadanYukle();
            EditoruTemizle();
        }

        private void AnaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!KaydedilmemisDegisiklikVar())
            {
                return;
            }

            DialogResult cevap = MessageBox.Show(
                "Kaydedilmemiş değişiklikler var. Kapatmadan önce kaydedilsin mi?",
                "Not Defteri", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (cevap == DialogResult.Cancel)
            {
                e.Cancel = true;            // kapanmayı iptal et
            }
            else if (cevap == DialogResult.Yes && !EditordekiNotuKaydet())
            {
                e.Cancel = true;            // kaydedilemediyse veriyi kaybetmemek için açık kal
            }
        }

        // ---------------------------------------------------------------
        // Dosya işlemleri (hata yönetimi burada)
        // ---------------------------------------------------------------

        private void NotlariDosyadanYukle()
        {
            try
            {
                YuklemeDurumu durum = _depo.Yukle();
                switch (durum)
                {
                    case YuklemeDurumu.Basarili:
                        DurumYaz($"{_depo.Notlar.Count} not dosyadan yüklendi.");
                        break;
                    case YuklemeDurumu.DosyaYok:
                        DurumYaz("Kayıt dosyası bulunamadı, boş not defteriyle başlandı.");
                        break;
                    case YuklemeDurumu.DosyaBozuk:
                        DurumYaz("Kayıt dosyası bozuk, boş not defteriyle başlandı.");
                        MessageBox.Show(
                            "Kayıt dosyası okunamadı (JSON biçimi bozuk).\n\n" +
                            $"Eski dosyanın yedeği alındı:\n{_depo.BozukDosyaYedegi}",
                            "Dosya bozuk", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                DurumYaz("Notlar yüklenemedi.");
                MessageBox.Show($"Notlar dosyadan okunamadı:\n{ex.Message}",
                    "Yükleme hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ListeyiYenile();
        }

        /// <returns>Dosyaya yazma başarılıysa true.</returns>
        private bool DosyayaYaz()
        {
            try
            {
                _depo.Kaydet();
                return true;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                MessageBox.Show($"Notlar dosyaya kaydedilemedi:\n{ex.Message}",
                    "Kayıt hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ---------------------------------------------------------------
        // Buton ve menü olayları
        // ---------------------------------------------------------------

        private void btnYeni_Click(object sender, EventArgs e)
        {
            if (!DegisiklikleriSorVeKaydet())
            {
                return;
            }
            EditoruTemizle();
            DurumYaz("Yeni not yazılıyor.");
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            EditordekiNotuKaydet();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (_seciliNot == null)
            {
                return;
            }

            DialogResult cevap = MessageBox.Show(
                $"\"{_seciliNot.Baslik}\" notu silinsin mi?",
                "Notu sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (cevap != DialogResult.Yes)
            {
                return;
            }

            _depo.Sil(_seciliNot);
            bool yazildi = DosyayaYaz();
            EditoruTemizle();
            ListeyiYenile();
            DurumYaz(yazildi ? "Not silindi." : "Not silindi ama dosyaya yazılamadı.");
        }

        private void btnDisaAktar_Click(object sender, EventArgs e)
        {
            if (_seciliNot == null)
            {
                MessageBox.Show("Dışa aktarmak için önce listeden bir not seçin veya notu kaydedin.",
                    "Not Defteri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Title = "Notu .txt olarak dışa aktar",
                Filter = "Metin dosyası (*.txt)|*.txt",
                FileName = GecerliDosyaAdi(_seciliNot.Baslik) + ".txt"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                NotDeposu.TxtOlarakDisaAktar(_seciliNot, dialog.FileName);
                DurumYaz($"Not dışa aktarıldı: {dialog.FileName}");
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                MessageBox.Show($"Dosya yazılamadı:\n{ex.Message}",
                    "Dışa aktarma hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuYenidenYukle_Click(object sender, EventArgs e)
        {
            if (!DegisiklikleriSorVeKaydet())
            {
                return;
            }
            NotlariDosyadanYukle();
            EditoruTemizle();
        }

        private void mnuCikis_Click(object sender, EventArgs e)
        {
            Close();   // FormClosing olayı kaydedilmemiş değişiklikleri sorar
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            ListeyiYenile(_seciliNot);
        }

        private void lstNotlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_listeGuncelleniyor || lstNotlar.SelectedItem is not Not tiklanan || tiklanan == _seciliNot)
            {
                return;
            }

            if (!DegisiklikleriSorVeKaydet())
            {
                // Kullanıcı vazgeçti: seçimi eski nota geri al.
                ListeyiYenile(_seciliNot);
                return;
            }

            NotuGoster(tiklanan);

            // Önceki not kaydedildiyse liste yeniden çizilmiş ve seçim kaymış olabilir.
            if (lstNotlar.SelectedItem != tiklanan)
            {
                ListeyiYenile(tiklanan);
            }
        }

        // ---------------------------------------------------------------
        // Yardımcı metotlar
        // ---------------------------------------------------------------

        /// <summary>
        /// Editördeki başlık/içerik ile yeni not ekler veya seçili notu günceller,
        /// sonra tüm listeyi dosyaya yazar (her işlemden sonra otomatik kayıt).
        /// </summary>
        private bool EditordekiNotuKaydet()
        {
            string baslik = txtBaslik.Text.Trim();
            string icerik = rtbIcerik.Text;

            if (baslik.Length == 0 && string.IsNullOrWhiteSpace(icerik))
            {
                MessageBox.Show("Boş not kaydedilemez. Bir başlık veya içerik yazın.",
                    "Not Defteri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (baslik.Length == 0)
            {
                baslik = IcerikteBaslikUret(icerik);
            }

            string mesaj;
            if (_seciliNot == null)
            {
                _seciliNot = _depo.Ekle(baslik, icerik);
                mesaj = "Yeni not eklendi";
            }
            else
            {
                _depo.Guncelle(_seciliNot, baslik, icerik);
                mesaj = "Not güncellendi";
            }

            bool yazildi = DosyayaYaz();
            NotuGoster(_seciliNot);
            ListeyiYenile(_seciliNot);
            DurumYaz(yazildi ? $"{mesaj} ve dosyaya kaydedildi." : $"{mesaj} ama dosyaya yazılamadı.");
            return yazildi;
        }

        /// <summary>
        /// Kaydedilmemiş değişiklik varsa kullanıcıya sorar.
        /// </summary>
        /// <returns>İşleme devam edilebilirse true, kullanıcı vazgeçtiyse false.</returns>
        private bool DegisiklikleriSorVeKaydet()
        {
            if (!KaydedilmemisDegisiklikVar())
            {
                return true;
            }

            DialogResult cevap = MessageBox.Show(
                "Açık nottaki değişiklikler kaydedilmedi. Kaydedilsin mi?",
                "Not Defteri", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            return cevap switch
            {
                DialogResult.Yes => EditordekiNotuKaydet(),
                DialogResult.No => true,
                _ => false
            };
        }

        private bool KaydedilmemisDegisiklikVar()
        {
            if (_seciliNot == null)
            {
                return txtBaslik.Text.Trim().Length > 0 || !string.IsNullOrWhiteSpace(rtbIcerik.Text);
            }
            return txtBaslik.Text.Trim() != _seciliNot.Baslik || rtbIcerik.Text != _seciliNot.Icerik;
        }

        private void ListeyiYenile(Not? secilecek = null)
        {
            _listeGuncelleniyor = true;
            lstNotlar.BeginUpdate();
            lstNotlar.Items.Clear();
            foreach (Not not in _depo.Ara(txtAra.Text))
            {
                lstNotlar.Items.Add(not);
            }
            if (secilecek != null && lstNotlar.Items.Contains(secilecek))
            {
                lstNotlar.SelectedItem = secilecek;
            }
            lstNotlar.EndUpdate();
            _listeGuncelleniyor = false;

            lblAra.Text = string.IsNullOrWhiteSpace(txtAra.Text)
                ? $"Notlar ({_depo.Notlar.Count})"
                : $"Notlar ({lstNotlar.Items.Count} / {_depo.Notlar.Count} eşleşme)";
        }

        private void NotuGoster(Not not)
        {
            _seciliNot = not;
            txtBaslik.Text = not.Baslik;
            rtbIcerik.Text = not.Icerik;
            lblTarihler.Text = $"Oluşturma: {not.OlusturmaTarihi:dd.MM.yyyy HH:mm}    ·    " +
                               $"Son düzenleme: {not.DuzenlemeTarihi:dd.MM.yyyy HH:mm}";
            btnSil.Enabled = true;
            btnDisaAktar.Enabled = true;
            mnuDisaAktar.Enabled = true;
        }

        private void EditoruTemizle()
        {
            _seciliNot = null;
            txtBaslik.Clear();
            rtbIcerik.Clear();
            lblTarihler.Text = "Yeni not — kaydedince listeye eklenecek.";
            ListeyiYenile();
            btnSil.Enabled = false;
            btnDisaAktar.Enabled = false;
            mnuDisaAktar.Enabled = false;
            txtBaslik.Focus();
        }

        private void DurumYaz(string mesaj)
        {
            lblDurum.Text = $"{DateTime.Now:HH:mm:ss}  {mesaj}";
        }

        /// <summary>Başlık boş bırakılırsa içeriğin ilk satırından (en fazla 40 karakter) başlık üretir.</summary>
        private static string IcerikteBaslikUret(string icerik)
        {
            string ilkSatir = icerik.Trim().Split('\n')[0].Trim();
            return ilkSatir.Length <= 40 ? ilkSatir : ilkSatir[..40] + "...";
        }

        /// <summary>Windows'ta dosya adında kullanılamayan karakterleri '_' ile değiştirir.</summary>
        private static string GecerliDosyaAdi(string ad)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                ad = ad.Replace(c, '_');
            }
            return string.IsNullOrWhiteSpace(ad) ? "not" : ad;
        }
    }
}

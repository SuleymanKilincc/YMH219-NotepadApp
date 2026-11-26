using System.Text.Encodings.Web;
using System.Text.Json;
using NotepadApp.Models;

namespace NotepadApp.Services
{
    /// <summary>Uygulama açılırken dosya okumasının nasıl sonuçlandığı.</summary>
    public enum YuklemeDurumu
    {
        Basarili,
        DosyaYok,
        DosyaBozuk
    }

    /// <summary>
    /// Notların bellekteki listesini (List&lt;Not&gt;) tutar ve bu listeyi
    /// JSON dosyasına kaydetme / dosyadan yükleme işlerini yapar.
    /// Form, dosya ve JSON ayrıntılarını bilmez; yalnızca bu sınıfı kullanır.
    /// </summary>
    public class NotDeposu
    {
        private readonly List<Not> _notlar = new();

        private static readonly JsonSerializerOptions JsonAyarlari = new()
        {
            WriteIndented = true,
            // Türkçe karakterler dosyada ş yerine ş olarak okunaklı yazılsın.
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public string DosyaYolu { get; }

        /// <summary>Son yüklemede dosya bozuk çıktıysa, alınan yedeğin yolu.</summary>
        public string? BozukDosyaYedegi { get; private set; }

        /// <summary>Dışarıya salt okunur liste verilir; ekleme/silme sadece bu sınıfın metotlarıyla yapılır.</summary>
        public IReadOnlyList<Not> Notlar => _notlar;

        public NotDeposu(string dosyaYolu)
        {
            DosyaYolu = dosyaYolu;
        }

        /// <summary>Varsayılan konum: %AppData%\YMH219-NotepadApp\notlar.json</summary>
        public static string VarsayilanDosyaYolu()
        {
            string klasor = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "YMH219-NotepadApp");
            return Path.Combine(klasor, "notlar.json");
        }

        public Not Ekle(string baslik, string icerik)
        {
            var not = new Not(baslik, icerik);
            _notlar.Add(not);
            return not;
        }

        public void Guncelle(Not not, string baslik, string icerik)
        {
            not.Guncelle(baslik, icerik);
        }

        public bool Sil(Not not)
        {
            return _notlar.Remove(not);
        }

        /// <summary>Başlıkta veya içerikte aranan metni geçen notlar, en son düzenlenen en üstte.</summary>
        public List<Not> Ara(string aranan)
        {
            IEnumerable<Not> sonuc = _notlar;
            if (!string.IsNullOrWhiteSpace(aranan))
            {
                sonuc = sonuc.Where(n =>
                    n.Baslik.Contains(aranan, StringComparison.CurrentCultureIgnoreCase) ||
                    n.Icerik.Contains(aranan, StringComparison.CurrentCultureIgnoreCase));
            }
            return sonuc.OrderByDescending(n => n.DuzenlemeTarihi).ToList();
        }

        /// <summary>
        /// Listeyi JSON'a serileştirip dosyaya yazar. Önce geçici dosyaya yazılır,
        /// sonra asıl dosyanın yerine konur; böylece yazma yarıda kesilirse eski dosya bozulmaz.
        /// </summary>
        public void Kaydet()
        {
            string? klasor = Path.GetDirectoryName(DosyaYolu);
            if (!string.IsNullOrEmpty(klasor))
            {
                Directory.CreateDirectory(klasor);
            }

            string json = JsonSerializer.Serialize(_notlar, JsonAyarlari);
            string geciciDosya = DosyaYolu + ".tmp";
            File.WriteAllText(geciciDosya, json);
            File.Move(geciciDosya, DosyaYolu, overwrite: true);
        }

        /// <summary>
        /// Dosyadaki JSON'u okuyup List&lt;Not&gt;'a geri dönüştürür (deserialize).
        /// Dosya yoksa boş listeyle başlanır. Dosya bozuksa yedeği alınır ve boş listeyle başlanır.
        /// </summary>
        public YuklemeDurumu Yukle()
        {
            _notlar.Clear();

            if (!File.Exists(DosyaYolu))
            {
                return YuklemeDurumu.DosyaYok;
            }

            try
            {
                string json = File.ReadAllText(DosyaYolu);
                List<Not>? okunan = JsonSerializer.Deserialize<List<Not>>(json, JsonAyarlari);
                if (okunan != null)
                {
                    _notlar.AddRange(okunan);
                }
                return YuklemeDurumu.Basarili;
            }
            catch (JsonException)
            {
                // Bozuk dosyanın üzerine yazıp kullanıcının verisini yok etmemek için yedekliyoruz.
                string yedek = DosyaYolu + $".bozuk-{DateTime.Now:yyyyMMdd-HHmmss}";
                File.Copy(DosyaYolu, yedek, overwrite: true);
                BozukDosyaYedegi = yedek;
                return YuklemeDurumu.DosyaBozuk;
            }
        }

        /// <summary>Tek bir notu düz metin dosyası olarak yazar.</summary>
        public static void TxtOlarakDisaAktar(Not not, string hedefDosya)
        {
            using var yazici = new StreamWriter(hedefDosya, append: false);
            yazici.Write(not.MetinOlarak());
        }
    }
}

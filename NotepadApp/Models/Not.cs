namespace NotepadApp.Models
{
    /// <summary>
    /// Tek bir notu temsil eder. Bu sınıfın nesneleri JSON dosyasına
    /// serileştirilerek (serialize) kaydedilir ve geri okunur.
    /// </summary>
    public class Not
    {
        // JSON serileştirici nesneyi geri oluştururken (deserialize)
        // parametresiz kurucuyu ve public set erişimcilerini kullanır.
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Baslik { get; set; } = string.Empty;
        public string Icerik { get; set; } = string.Empty;
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlemeTarihi { get; set; } = DateTime.Now;

        public Not()
        {
        }

        public Not(string baslik, string icerik)
        {
            Baslik = baslik;
            Icerik = icerik;
            OlusturmaTarihi = DateTime.Now;
            DuzenlemeTarihi = OlusturmaTarihi;
        }

        /// <summary>
        /// Notun başlığını ve içeriğini değiştirir, düzenleme tarihini günceller.
        /// Tarih güncellemesi burada yapıldığı için formun bunu hatırlaması gerekmez.
        /// </summary>
        public void Guncelle(string baslik, string icerik)
        {
            Baslik = baslik;
            Icerik = icerik;
            DuzenlemeTarihi = DateTime.Now;
        }

        /// <summary>
        /// Notu düz metin (.txt) olarak dışa aktarırken kullanılan biçim.
        /// </summary>
        public string MetinOlarak()
        {
            return $"{Baslik}{Environment.NewLine}" +
                   $"Oluşturma: {OlusturmaTarihi:dd.MM.yyyy HH:mm}{Environment.NewLine}" +
                   $"Düzenleme: {DuzenlemeTarihi:dd.MM.yyyy HH:mm}{Environment.NewLine}" +
                   $"{new string('-', 40)}{Environment.NewLine}" +
                   Icerik;
        }

        // ListBox bir nesneyi ekranda gösterirken ToString() çağırır.
        public override string ToString()
        {
            string baslik = string.IsNullOrWhiteSpace(Baslik) ? "(Başlıksız)" : Baslik;
            return $"{baslik}  —  {DuzenlemeTarihi:dd.MM.yyyy HH:mm}";
        }
    }
}

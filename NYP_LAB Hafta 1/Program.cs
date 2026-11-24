using System;
using System.Collections.Generic;
using System.Linq;

namespace AkilliEvYonetimi
{
    public enum CihazTuru
    {
        Lamba,
        Klima,
        GuvenlikSistemi
    }

    public class Cihaz
    {
        public int Kimlik { get; private set; }
        public string Ad { get; private set; }
        public CihazTuru Tur { get; private set; }
        public bool AcikMi { get; private set; }

        public Cihaz(int kimlik, string ad, CihazTuru tur)
        {
            Kimlik = kimlik;
            Ad = ad;
            Tur = tur;
            AcikMi = false; // Başlangıçta kapalı
        }

        public void Ac()
        {
            AcikMi = true;
            Console.WriteLine($"'{Ad}' (ID: {Kimlik}) açıldı.");
        }

        public void Kapat()
        {
            AcikMi = false;
            Console.WriteLine($"'{Ad}' (ID: {Kimlik}) kapatıldı.");
        }

        public void BilgileriGoster()
        {
            string durum = AcikMi ? "Açık" : "Kapalı";
            Console.WriteLine($"[ID: {Kimlik}] Ad: {Ad} | Tür: {Tur} | Durum: {durum}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Cihaz> cihazlar = new List<Cihaz>
            {
                new Cihaz(1, "Salon Avizesi", CihazTuru.Lamba),
                new Cihaz(2, "Yatak Odası Kliması", CihazTuru.Klima),
                new Cihaz(3, "Giriş Kamerası", CihazTuru.GuvenlikSistemi)
            };

            while (true)
            {
                Console.WriteLine("\n--- AKILLI EV CİHAZ YÖNETİMİ ---");
                Console.WriteLine("1. Cihazları Listele");
                Console.WriteLine("2. Yeni Cihaz Ekle");
                Console.WriteLine("3. Cihaz Aç");
                Console.WriteLine("4. Cihaz Kapat");
                Console.WriteLine("0. Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();
                Console.WriteLine();

                switch (secim)
                {
                    case "1":
                        if (cihazlar.Count == 0)
                        {
                            Console.WriteLine("Sistemde kayıtlı cihaz bulunmuyor.");
                        }
                        else
                        {
                            foreach (var cihaz in cihazlar)
                            {
                                cihaz.BilgileriGoster();
                            }
                        }
                        break;

                    case "2":
                        Console.Write("Cihaz Kimliği (ID): ");
                        if (!int.TryParse(Console.ReadLine(), out int yeniId))
                        {
                            Console.WriteLine("Hata: Geçerli bir sayısal kimlik giriniz.");
                            break;
                        }

                        if (cihazlar.Any(c => c.Kimlik == yeniId))
                        {
                            Console.WriteLine($"Hata: {yeniId} kimliğine sahip bir cihaz zaten mevcut!");
                            break;
                        }

                        Console.Write("Cihaz Adı: ");
                        string ad = Console.ReadLine()?.Trim();
                        if (string.IsNullOrEmpty(ad))
                        {
                            Console.WriteLine("Hata: Cihaz adı boş olamaz.");
                            break;
                        }

                        Console.WriteLine("Cihaz Türü seçiniz (0: Lamba, 1: Klima, 2: GuvenlikSistemi): ");
                        if (!int.TryParse(Console.ReadLine(), out int turSecim) || !Enum.IsDefined(typeof(CihazTuru), turSecim))
                        {
                            Console.WriteLine("Hata: Geçersiz cihaz türü.");
                            break;
                        }

                        CihazTuru tur = (CihazTuru)turSecim;
                        cihazlar.Add(new Cihaz(yeniId, ad, tur));
                        Console.WriteLine("Cihaz başarıyla eklendi (varsayılan durum: Kapalı).");
                        break;

                    case "3":
                        Console.Write("Açmak istediğiniz cihazın Kimliği (ID): ");
                        if (!int.TryParse(Console.ReadLine(), out int acId))
                        {
                            Console.WriteLine("Hata: Geçerli bir sayısal kimlik giriniz.");
                            break;
                        }

                        Cihaz acilacakCihaz = cihazlar.FirstOrDefault(c => c.Kimlik == acId);
                        if (acilacakCihaz == null)
                        {
                            Console.WriteLine($"Hata: {acId} kimliğine sahip bir cihaz bulunamadı.");
                        }
                        else
                        {
                            acilacakCihaz.Ac();
                        }
                        break;

                    case "4":
                        Console.Write("Kapatmak istediğiniz cihazın Kimliği (ID): ");
                        if (!int.TryParse(Console.ReadLine(), out int kapatId))
                        {
                            Console.WriteLine("Hata: Geçerli bir sayısal kimlik giriniz.");
                            break;
                        }

                        Cihaz kapatilacakCihaz = cihazlar.FirstOrDefault(c => c.Kimlik == kapatId);
                        if (kapatilacakCihaz == null)
                        {
                            Console.WriteLine($"Hata: {kapatId} kimliğine sahip bir cihaz bulunamadı.");
                        }
                        else
                        {
                            kapatilacakCihaz.Kapat();
                        }
                        break;

                    case "0":
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;

                    default:
                        Console.WriteLine("Hata: Geçersiz menü seçimi.");
                        break;
                }
            }
        }
    }
}
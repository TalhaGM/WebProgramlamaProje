using Microsoft.Identity.Client;

namespace SkyWander_Airlines.Models
{
    public class BiletBilgisi
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Email { get; set; }
        public string KoltukNumarasi { get; set; }
        public string Adres { get; set; }

        // Modelden gelen diğer alanlar
        public int RotaId { get; set; }
        public TimeSpan KalkisZamani { get; set; }
        public TimeSpan VarisZamani { get; set; }
        public string Nereden { get; set; }
        public string Nereye { get; set; }
        public string UcakTipi { get; set; }
        public DateTime Tarih { get; set; }
        public bool Durum { get; set; }

        public Rota Rota { get; set; }  
        // Diğer alanlar...


    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SkyWander_Airlines.Models
{
    public class Uyeler
    {
        [Key]
        public int ID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string Ulke { get; set; }
        public string Sehir { get; set; }
        public string Adres { get; set; }



    }
}

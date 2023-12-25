namespace SkyWander_Airlines.Models
{
    public class Yonetici
    {
        public int ID { get; set; }
        public string Nereden { get; set; }
        public string Nereye { get; set; }
        public int Kapasite { get; set; }
        public DateTime Tarih { get; set; }
        public TimeSpan KalkisZamani { get; set; }
        public TimeSpan VarisZamani { get; set; }
        public string UcakTipi { get; set; }

    }
}

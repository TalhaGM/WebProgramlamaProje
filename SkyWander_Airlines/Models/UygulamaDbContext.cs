using Microsoft.EntityFrameworkCore;
using SkyWander_Airlines.Models;


public class UygulamaDbContext : DbContext
{
    public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options)
      : base(options)
    {
    }

    public DbSet<Uyeler> Uyeler { get; set; }

    public DbSet<Uyeler> Kullanici { get; set; }

    public DbSet<Rota> Yonetici { get; set; }

    public DbSet<Rota> Rota { get; set; }

    public DbSet<BiletBilgisi> BiletBilgisi { get; set; }


}


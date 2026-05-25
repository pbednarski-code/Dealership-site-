using Microsoft.EntityFrameworkCore;
using DealerAutoMVC.Models;

namespace DealerAutoMVC.Data
{
    public class DealerAutoContext : DbContext
    {
        public DealerAutoContext(DbContextOptions<DealerAutoContext> options)
            : base(options)
        {
        }

        public DbSet<Marka> Marki { get; set; } = default!;
        public DbSet<ModelSamochodu> ModeleSamochodow { get; set; } = default!;
        public DbSet<Klient> Klienci { get; set; } = default!;
        public DbSet<Transakcja> Transakcje { get; set; } = default!;
        public DbSet<Wyposazenie> Wyposazenia { get; set; } = default!;
        public DbSet<Uzytkownik> Uzytkownicy { get; set; } = default!;
    }
}
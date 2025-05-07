using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TrackNWork.Models.DBModels;

namespace TrackNWork.Models
{
    public class SupabaseDBContext : DbContext
    {
        public SupabaseDBContext() : base("name=SupabaseConnection")
        {
            // Optional: Verbindung prüfen oder Logging aktivieren
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        // View
        public DbSet<ViewZeiteintragTagesansicht> VwZeiteintraegeTagesansicht { get; set; }

        // Optional: Weitere Tabellen wie Fahrzeuge, Geraete, Mitarbeiter etc. (nur falls du EF für Inserts nutzt)
        // public DbSet<Fahrzeug> Fahrzeuge { get; set; }
        // public DbSet<Geraet> Geraete { get; set; }
        // public DbSet<Mitarbeiter> Mitarbeiter { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tabellenname der View explizit setzen
            modelBuilder.Entity<ViewZeiteintragTagesansicht>()
                .ToTable("vw_zeiteintraege_tagesansicht", schemaName: "public")
                .HasKey(z => z.zeiteintrag_id);

            // Optionale Konventionen entfernen
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

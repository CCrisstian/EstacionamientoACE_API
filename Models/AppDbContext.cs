using Microsoft.EntityFrameworkCore;

namespace EstacionamientoACE_API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // 🔹 DbSets
        public DbSet<Estacionamiento> Estacionamientos { get; set; }
        public DbSet<Plaza> Plazas { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<Categoria_Vehiculo> CategoriasVehiculos { get; set; }
        public DbSet<Metodos_De_Pago> MetodosDePagos { get; set; }
        public DbSet<Acepta_Metodo_De_Pago> AceptaMetodosDePagos { get; set; }
        public DbSet<Tipos_Tarifa> TiposTarifas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🔹 Clave compuesta en Plaza
            modelBuilder.Entity<Plaza>()
                .HasKey(p => new { p.EstId, p.PlazaId });

            // 🔹 Relación Plaza → Estacionamiento
            modelBuilder.Entity<Plaza>()
                .HasOne(p => p.Estacionamiento)
                .WithMany(e => e.Plazas)
                .HasForeignKey(p => p.EstId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Relación Plaza → Categoria_Vehiculo
            modelBuilder.Entity<Plaza>()
                .HasOne(p => p.CategoriaVehiculo)
                .WithMany(c => c.Plazas)
                .HasForeignKey(p => p.CategoriaId)
                .HasConstraintName("FK_Plaza_Categoria_Vehiculo")
                .OnDelete(DeleteBehavior.SetNull);

            // 🔹 Clave compuesta para Acepta_Metodo_De_Pago
            modelBuilder.Entity<Acepta_Metodo_De_Pago>()
                .HasKey(amp => new { amp.EstId, amp.MetodoPagoId });

            // 🔹 Relación Tarifa → Estacionamiento
            modelBuilder.Entity<Tarifa>()
                .HasOne(t => t.Estacionamiento)
                .WithMany(e => e.Tarifas)
                .HasForeignKey(t => t.EstId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Relación Tarifa → Categoria_Vehiculo
            modelBuilder.Entity<Tarifa>()
                .HasOne(t => t.CategoriaVehiculo)
                .WithMany()
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.SetNull);

            // 🔹 Relación Tarifa → Tipos_Tarifa
            modelBuilder.Entity<Tarifa>()
                .HasOne(t => t.TipoTarifa)
                .WithMany(tt => tt.Tarifas)
                .HasForeignKey(t => t.TiposTarifaId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}

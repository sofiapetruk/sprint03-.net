using Microsoft.EntityFrameworkCore;
using Sprint02.Models;

namespace Sprint02.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }


        public DbSet<Moto> Motos { get; set; }
        public DbSet<StatusMoto> StatusMotos { get; set; }
        public DbSet<TipoMoto> TipoMotos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
  
            modelBuilder.Entity<Moto>(entity =>
            {

                entity.HasIndex(m => m.Placa).IsUnique();
                entity.HasIndex(m => m.NmChassi).IsUnique();

                entity.HasOne(m => m.status)
                      .WithMany(s => s.Motos)
                      .HasForeignKey(m => m.IdStatus)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Moto_StatusMoto");

                entity.HasOne(m => m.modelo)
                      .WithMany(t => t.Motos)
                      .HasForeignKey(m => m.IdTipo)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Moto_TipoMoto");
            });

            modelBuilder.Entity<StatusMoto>(entity =>
            {
                entity.Property(s => s.Status).IsRequired();
                entity.Property(s => s.Data).IsRequired();
            });

            modelBuilder.Entity<TipoMoto>(entity =>
            {
                entity.Property(t => t.NomeTipo).IsRequired();
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Nome).IsRequired();
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.Senha).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

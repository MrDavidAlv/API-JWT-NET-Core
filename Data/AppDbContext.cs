using Microsoft.EntityFrameworkCore;
using apiEmpleados.Models;

namespace apiEmpleados.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.id_usuario);
                entity.Property(e => e.usuario).IsRequired().HasMaxLength(50);
                entity.Property(e => e.contraseña).IsRequired();
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(e => e.id_tarea);
                entity.Property(e => e.titulo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.descripcion).HasMaxLength(500);
                entity.Property(e => e.fecha_creacion).IsRequired().HasDefaultValueSql("GETDATE()");
                
            });
        }
    }
}

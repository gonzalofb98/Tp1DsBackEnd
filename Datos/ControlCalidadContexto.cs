using Dominio.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Datos
{
    public class ControlCalidadContexto: IdentityDbContext<Usuario>
        {
        public ControlCalidadContexto()
        {

        }

        public ControlCalidadContexto(DbContextOptions<ControlCalidadContexto> opciones): base(opciones)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Defecto>()
                .ToTable("Defectos")
                .HasKey(m => m.Id);

            modelBuilder
                .Entity<Color>()
                .ToTable("Colores")
                .HasKey(m => m.Id);

            modelBuilder
                .Entity<Incidencia>()
                .ToTable("Incidencias")
                .HasKey(m => m.Id);

            modelBuilder
                .Entity<JornadaLaboral>()
                .ToTable("JornadasLaborales")
                .HasKey(m => m.Id);

            modelBuilder
                .Entity<LineaDeTrabajo>()
                .ToTable("LineasDeTrabajo")
                .HasKey(m => m.Id);

            modelBuilder
                .Entity<Modelo>()
                .ToTable("Modelos")
                .HasKey(m => m.Id);

            modelBuilder
                .Entity<OrdenDeProduccion>()
                .ToTable("OrdenesDeProduccion")
                .HasKey(m => m.Id);

            modelBuilder
                .Entity<Alerta>()
                .ToTable("Alertas")
                .HasKey(m => m.Id);

            modelBuilder
                .Entity<Turno>()
                .ToTable("Turnos")
                .HasKey(m => m.Id);

            base.OnModelCreating(modelBuilder);

        }
    }
}

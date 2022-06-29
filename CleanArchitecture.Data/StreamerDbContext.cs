using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Data
{
  public class StreamerDbContext : DbContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilders)
    {
      optionsBuilders.UseSqlServer("Data Source=localhost,1434; Initial Catalog=Streamer; User Id=sa; Password=mssql1Ipw")
          .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
          .EnableSensitiveDataLogging();
    }

    // StreamerId:Plain Old CLR Object para convenciones de relaciones del Fluent API automatica y no manual
    // en el caso de no segur la convencion aplico esta configuracion para especificar mi configuracion 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Streamer>() // Entidad a evaluar
        .HasMany(m => m.Videos) // tendra muchas instancioas de videos
        .WithOne(m => m.Streamer) // con un elemento
        .HasForeignKey(m => m.StreamerId) //StreamerId sigue la connencion de nombres y de no ser asi tengo que hacer esto. aqui no es necesario xq sigue la convencion
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict); // eliminacion en cascada

      //Defino la relacion de varios a varios entre videos y autores 
      modelBuilder.Entity<Video>()
      .HasMany(p => p.Actores)  // muchas instancias de la clase actores
      .WithMany(t => t.Videos)
      .UsingEntity<VideoActor>( // defino propiedades que representan la clave compuesta
        pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
      );
    }

    public DbSet<Streamer>? Streamers { get; set; }

    public DbSet<Video>? Videos { get; set; }

    public DbSet<Actor>? Actores { get; set; }

    public DbSet<Director>? Directores { get; set; }
  }
}

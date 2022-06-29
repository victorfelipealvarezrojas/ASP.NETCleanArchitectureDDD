
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
  public class Video : BaseDomainModel
  {
    public Video()
    {
      Actores = new HashSet<Actor>();
    }

    public string? Nombre { get; set; }

    public int StreamerId { get; set; }  // clave foranea estandar: usar nombre de la clase + id 

    public virtual Streamer? Streamer { get; set; } // virtual indica que sera sobrescrita por una clase derivada en el futuro
                                                    // ancla a la otra entidad

    public virtual ICollection<Actor>? Actores { get; set; }

    public virtual Director Director { get; set; } // ancla o soporte, no tiene una representacion en las propiedades pero permite a VideoId dentro de Video funcione
  }
}

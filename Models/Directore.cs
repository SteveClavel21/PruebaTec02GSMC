using System;
using System.Collections.Generic;

namespace PruebaTec02GSMC.Models
{
    public partial class Directore
    {
        public Directore()
        {
            Peliculas = new HashSet<Pelicula>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}

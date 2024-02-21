using System;
using System.Collections.Generic;

namespace PruebaTec02GSMC.Models
{
    public partial class Pelicula
    {
        public int PeliculaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }
        public int Id { get; set; }

        public virtual Directore IdNavigation { get; set; } = null!;
    }
}

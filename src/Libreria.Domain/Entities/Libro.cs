using System;

namespace Libreria.Domain.Entities
{
    public class Libro
    {
        public string IdLibro { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public int IdAutor { get; set; }
        public int IdGenero { get; set; }
        public int Estatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}

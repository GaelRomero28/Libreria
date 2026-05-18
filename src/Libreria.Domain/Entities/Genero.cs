using System;

namespace Libreria.Domain.Entities
{
    public class Genero
    {
        public int Id { get; set; }
        public string GeneroNombre { get; set; } = string.Empty;
        public int Estatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}

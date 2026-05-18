using System;

namespace Libreria.Domain.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        public string AutorNombre { get; set; } = string.Empty;
        public int Estatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}

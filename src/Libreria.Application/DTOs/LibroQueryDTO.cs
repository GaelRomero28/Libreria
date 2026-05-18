namespace Libreria.Application.DTOs
{
    public class LibroQueryDTO
    {
        public string IdLibro { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int IdAutor { get; set; }
        public int IdGenero { get; set; }
        public int Estatus { get; set; }
    }
}

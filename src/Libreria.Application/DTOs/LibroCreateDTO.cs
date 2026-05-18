using System.ComponentModel.DataAnnotations;

namespace Libreria.Application.DTOs
{
    /// <summary>
    /// Modelo para la creación de un nuevo Libro.
    /// </summary>
    public class LibroCreateDTO
    {
        /// <summary>
        /// Título del libro.
        /// </summary>
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(150, ErrorMessage = "El título no puede exceder los 150 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// ID del autor asociado.
        /// </summary>
        [Required(ErrorMessage = "El ID del autor es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del autor es inválido.")]
        public int IdAutor { get; set; }

        /// <summary>
        /// ID del género asociado.
        /// </summary>
        [Required(ErrorMessage = "El ID del género es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del género es inválido.")]
        public int IdGenero { get; set; }

        /// <summary>
        /// Año de publicación del libro.
        /// </summary>
        [Required(ErrorMessage = "El año de publicación es obligatorio.")]
        public int AnioPublicacion { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Libreria.Application.DTOs
{
    /// <summary>
    /// Modelo para la actualización de un Libro existente.
    /// </summary>
    public class LibroUpdateDTO
    {
        /// <summary>
        /// Nuevo título del libro.
        /// </summary>
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(150, ErrorMessage = "El título no puede exceder los 150 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// ID del nuevo autor asociado.
        /// </summary>
        [Required(ErrorMessage = "El ID del autor es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del autor es inválido.")]
        public int IdAutor { get; set; }

        /// <summary>
        /// ID del nuevo género asociado.
        /// </summary>
        [Required(ErrorMessage = "El ID del género es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del género es inválido.")]
        public int IdGenero { get; set; }

        /// <summary>
        /// Estatus del libro (1 = Activo, 0 = Inactivo).
        /// </summary>
        [Required(ErrorMessage = "El estatus es obligatorio.")]
        [Range(0, 1, ErrorMessage = "El estatus solo puede ser 0 o 1.")]
        public int Estatus { get; set; } = 1;
    }
}

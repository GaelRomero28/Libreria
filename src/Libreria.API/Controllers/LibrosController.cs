using System.Threading.Tasks;
using Libreria.Application.DTOs;
using Libreria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Libreria.API.Controllers
{
    /// <summary>
    /// Controlador para la gestión de libros de la biblioteca.
    /// </summary>
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroService _libroService;

        public LibrosController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        /// <summary>
        /// Obtiene la lista de todos los libros activos. Soporta búsqueda opcional.
        /// </summary>
        /// <param name="search">Parámetro opcional para filtrar por título o autor.</param>
        /// <returns>Lista de libros.</returns>
        /// <response code="200">Retorna la lista de libros.</response>
        /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? search)
        {
            var result = await _libroService.ObtenerTodosAsync(search);
            
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Obtiene un libro específico por su ID.
        /// </summary>
        /// <param name="id">El ID del libro (ej. A12).</param>
        /// <returns>El libro solicitado.</returns>
        /// <response code="200">Retorna el libro solicitado.</response>
        /// <response code="404">Si el libro no fue encontrado.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _libroService.ObtenerPorIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.Error);
        }

        /// <summary>
        /// Crea un nuevo libro y genera su ID único automáticamente.
        /// </summary>
        /// <param name="dto">Los datos del libro a crear.</param>
        /// <returns>El ID del libro creado.</returns>
        /// <response code="201">Retorna el ID y la ruta para consultar el libro.</response>
        /// <response code="400">Si los datos proporcionados no son válidos (Data Annotations).</response>
        /// <response code="422">Si ocurre un error en la lógica de negocio.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LibroCreateDTO dto)
        {
            var result = await _libroService.CrearAsync(dto);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Value }, new { id = result.Value });
            }

            return UnprocessableEntity(new { message = result.Error });
        }

        /// <summary>
        /// Actualiza la información de un libro existente.
        /// </summary>
        /// <param name="id">El ID del libro a actualizar.</param>
        /// <param name="dto">Los nuevos datos del libro.</param>
        /// <returns>Mensaje de éxito.</returns>
        /// <response code="200">Si el libro fue actualizado exitosamente.</response>
        /// <response code="400">Si los datos proporcionados no son válidos (Data Annotations).</response>
        /// <response code="422">Si el libro no existe o hay un error de negocio.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] LibroUpdateDTO dto)
        {
            var result = await _libroService.ActualizarAsync(id, dto);

            if (result.IsSuccess)
            {
                return Ok(new { message = "Libro actualizado exitosamente." });
            }

            return UnprocessableEntity(new { message = result.Error });
        }

        /// <summary>
        /// Elimina lógicamente un libro cambiando su estatus a inactivo.
        /// </summary>
        /// <param name="id">El ID del libro a eliminar.</param>
        /// <returns>Mensaje de éxito.</returns>
        /// <response code="200">Si el libro fue eliminado exitosamente.</response>
        /// <response code="422">Si el libro no existe o hay un error de negocio.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _libroService.EliminarAsync(id);

            if (result.IsSuccess)
            {
                return Ok(new { message = "Libro eliminado lógicamente." });
            }

            return UnprocessableEntity(new { message = result.Error });
        }
    }
}

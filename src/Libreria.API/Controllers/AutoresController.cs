using System.Threading.Tasks;
using Libreria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Libreria.API.Controllers
{
    /// <summary>
    /// Controlador para la gestión de autores.
    /// </summary>
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutoresController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        /// <summary>
        /// Obtiene la lista de todos los autores activos.
        /// </summary>
        /// <returns>Lista de autores.</returns>
        /// <response code="200">Retorna la lista de autores.</response>
        /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _autorService.ObtenerTodosAsync();
            
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }
    }
}

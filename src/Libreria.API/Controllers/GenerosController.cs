using System.Threading.Tasks;
using Libreria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Libreria.API.Controllers
{
    /// <summary>
    /// Controlador para la gestión de géneros.
    /// </summary>
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly IGeneroService _generoService;

        public GenerosController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        /// <summary>
        /// Obtiene la lista de todos los géneros activos.
        /// </summary>
        /// <returns>Lista de géneros.</returns>
        /// <response code="200">Retorna la lista de géneros.</response>
        /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _generoService.ObtenerTodosAsync();
            
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }
    }
}

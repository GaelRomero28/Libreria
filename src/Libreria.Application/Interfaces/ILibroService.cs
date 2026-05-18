using System.Collections.Generic;
using System.Threading.Tasks;
using Libreria.Application.DTOs;
using Libreria.Domain.Common;

namespace Libreria.Application.Interfaces
{
    public interface ILibroService
    {
        Task<Result<IEnumerable<LibroQueryDTO>>> ObtenerTodosAsync(string? search = null);
        Task<Result<LibroQueryDTO>> ObtenerPorIdAsync(string id);
        Task<Result<string>> CrearAsync(LibroCreateDTO dto);
        Task<Result> ActualizarAsync(string id, LibroUpdateDTO dto);
        Task<Result> EliminarAsync(string id);
    }
}

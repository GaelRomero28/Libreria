using System.Collections.Generic;
using System.Threading.Tasks;
using Libreria.Domain.Entities;

namespace Libreria.Domain.Interfaces
{
    public interface ILibroRepository
    {
        Task<IEnumerable<dynamic>> ObtenerTodosAsync(string? search = null);
        Task<dynamic?> ObtenerPorIdAsync(string id);
        Task<bool> ExisteIdAsync(string id);
        Task<bool> ExisteLibroAsync(string titulo, int idAutor, int idGenero, string? idExcluir = null);
        Task<int> InsertarAsync(Libro libro);
        Task<int> ActualizarAsync(Libro libro);
        Task<int> EliminarLogicoAsync(string id);
    }
}

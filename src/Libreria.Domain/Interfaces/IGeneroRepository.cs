using System.Collections.Generic;
using System.Threading.Tasks;
using Libreria.Domain.Entities;

namespace Libreria.Domain.Interfaces
{
    public interface IGeneroRepository
    {
        Task<IEnumerable<Genero>> ObtenerTodosAsync();
    }
}

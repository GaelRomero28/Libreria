using System.Collections.Generic;
using System.Threading.Tasks;
using Libreria.Domain.Entities;

namespace Libreria.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> ObtenerTodosAsync();
    }
}

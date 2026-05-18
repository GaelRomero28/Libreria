using System.Collections.Generic;
using System.Threading.Tasks;
using Libreria.Domain.Common;
using Libreria.Domain.Entities;

namespace Libreria.Application.Interfaces
{
    public interface IGeneroService
    {
        Task<Result<IEnumerable<Genero>>> ObtenerTodosAsync();
    }
}

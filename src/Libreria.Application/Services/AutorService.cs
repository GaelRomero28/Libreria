using System.Collections.Generic;
using System.Threading.Tasks;
using Libreria.Application.Interfaces;
using Libreria.Domain.Common;
using Libreria.Domain.Entities;
using Libreria.Domain.Interfaces;

namespace Libreria.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<Result<IEnumerable<Autor>>> ObtenerTodosAsync()
        {
            var autores = await _autorRepository.ObtenerTodosAsync();
            return Result<IEnumerable<Autor>>.Success(autores);
        }
    }
}

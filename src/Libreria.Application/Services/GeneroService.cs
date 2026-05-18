using System.Collections.Generic;
using System.Threading.Tasks;
using Libreria.Application.Interfaces;
using Libreria.Domain.Common;
using Libreria.Domain.Entities;
using Libreria.Domain.Interfaces;

namespace Libreria.Application.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<Result<IEnumerable<Genero>>> ObtenerTodosAsync()
        {
            var generos = await _generoRepository.ObtenerTodosAsync();
            return Result<IEnumerable<Genero>>.Success(generos);
        }
    }
}

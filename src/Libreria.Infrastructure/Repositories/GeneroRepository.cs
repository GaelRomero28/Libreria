using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Libreria.Domain.Entities;
using Libreria.Domain.Interfaces;

namespace Libreria.Infrastructure.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly IDbConnection _connection;

        public GeneroRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Genero>> ObtenerTodosAsync()
        {
            var sql = "SELECT id as Id, genero as GeneroNombre, estatus as Estatus, fecha_registro as FechaRegistro FROM tb_generos WHERE estatus = 1";
            return await _connection.QueryAsync<Genero>(sql);
        }
    }
}

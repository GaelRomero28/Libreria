using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Libreria.Domain.Entities;
using Libreria.Domain.Interfaces;

namespace Libreria.Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly IDbConnection _connection;

        public AutorRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Autor>> ObtenerTodosAsync()
        {
            var sql = "SELECT id as Id, autor as AutorNombre, estatus as Estatus, fecha_registro as FechaRegistro FROM tb_autores WHERE estatus = 1";
            return await _connection.QueryAsync<Autor>(sql);
        }
    }
}

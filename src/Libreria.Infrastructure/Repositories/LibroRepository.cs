using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Libreria.Domain.Entities;
using Libreria.Domain.Interfaces;

namespace Libreria.Infrastructure.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private readonly IDbConnection _connection;

        public LibroRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<dynamic>> ObtenerTodosAsync(string? search = null)
        {
            var sql = @"
                SELECT 
                    l.id_libro, 
                    l.titulo, 
                    a.autor, 
                    g.genero,
                    l.id_autor,
                    l.id_genero,
                    l.anio_publicacion,
                    l.estatus
                FROM tb_libros l
                INNER JOIN tb_autores a ON l.id_autor = a.id
                INNER JOIN tb_generos g ON l.id_genero = g.id";

            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND (l.titulo LIKE @Search OR a.autor LIKE @Search)";
                return await _connection.QueryAsync<dynamic>(sql, new { Search = $"%{search}%" });
            }

            return await _connection.QueryAsync<dynamic>(sql);
        }

        public async Task<dynamic?> ObtenerPorIdAsync(string id)
        {
            var sql = @"
                SELECT 
                    l.id_libro, 
                    l.titulo, 
                    a.autor, 
                    g.genero,
                    l.id_autor,
                    l.id_genero,
                    l.anio_publicacion,
                    l.estatus
                FROM tb_libros l
                INNER JOIN tb_autores a ON l.id_autor = a.id
                INNER JOIN tb_generos g ON l.id_genero = g.id
                WHERE l.id_libro = @Id";

            return await _connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { Id = id });
        }

        public async Task<bool> ExisteIdAsync(string id)
        {
            var sql = "SELECT COUNT(1) FROM tb_libros WHERE id_libro = @Id";
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { Id = id });
            return count > 0;
        }

        public async Task<bool> ExisteLibroAsync(string titulo, int idAutor, int idGenero, string? idExcluir = null)
        {
            var sql = "SELECT COUNT(1) FROM tb_libros WHERE titulo = @Titulo AND id_autor = @IdAutor AND id_genero = @IdGenero";
            if (!string.IsNullOrWhiteSpace(idExcluir))
            {
                sql += " AND id_libro != @IdExcluir";
            }
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { Titulo = titulo, IdAutor = idAutor, IdGenero = idGenero, IdExcluir = idExcluir });
            return count > 0;
        }

        public async Task<int> InsertarAsync(Libro libro)
        {
            var sql = @"
                INSERT INTO tb_libros (id_libro, titulo, id_autor, id_genero, anio_publicacion, estatus, fecha_registro)
                VALUES (@IdLibro, @Titulo, @IdAutor, @IdGenero, @AnioPublicacion, @Estatus, @FechaRegistro)";

            return await _connection.ExecuteAsync(sql, libro);
        }

        public async Task<int> ActualizarAsync(Libro libro)
        {
            var sql = @"
                UPDATE tb_libros
                SET titulo = @Titulo,
                    id_autor = @IdAutor,
                    id_genero = @IdGenero,
                    anio_publicacion = @AnioPublicacion,
                    estatus = @Estatus
                WHERE id_libro = @IdLibro";

            return await _connection.ExecuteAsync(sql, libro);
        }

        public async Task<int> EliminarLogicoAsync(string id)
        {
            var sql = @"
                UPDATE tb_libros
                SET estatus = 0
                WHERE id_libro = @Id AND estatus = 1";

            return await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}

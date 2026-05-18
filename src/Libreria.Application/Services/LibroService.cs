using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libreria.Application.DTOs;
using Libreria.Application.Interfaces;
using Libreria.Domain.Common;
using Libreria.Domain.Entities;
using Libreria.Domain.Interfaces;

namespace Libreria.Application.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;

        public LibroService(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public async Task<Result<IEnumerable<LibroQueryDTO>>> ObtenerTodosAsync(string? search = null)
        {
            var result = await _libroRepository.ObtenerTodosAsync(search);
            
            var dtos = result.Select(r => new LibroQueryDTO
            {
                IdLibro = r.id_libro,
                Titulo = r.titulo,
                Autor = r.autor,
                Genero = r.genero,
                IdAutor = r.id_autor,
                IdGenero = r.id_genero,
                Estatus = r.estatus
            });

            return Result<IEnumerable<LibroQueryDTO>>.Success(dtos);
        }

        public async Task<Result<LibroQueryDTO>> ObtenerPorIdAsync(string id)
        {
            var result = await _libroRepository.ObtenerPorIdAsync(id);

            if (result == null)
            {
                return Result<LibroQueryDTO>.Failure("Libro no encontrado.");
            }

            var dto = new LibroQueryDTO
            {
                IdLibro = result.id_libro,
                Titulo = result.titulo,
                Autor = result.autor,
                Genero = result.genero,
                IdAutor = result.id_autor,
                IdGenero = result.id_genero,
                Estatus = result.estatus
            };

            return Result<LibroQueryDTO>.Success(dto);
        }

        public async Task<Result<string>> CrearAsync(LibroCreateDTO dto)
        {
            string newId = await GenerarIdUnicoAsync();

            var libro = new Libro
            {
                IdLibro = newId,
                Titulo = dto.Titulo,
                IdAutor = dto.IdAutor,
                IdGenero = dto.IdGenero
            };

            var affectedRows = await _libroRepository.InsertarAsync(libro);

            if (affectedRows == 0)
            {
                return Result<string>.Failure("Hubo un error al insertar el libro en la base de datos.");
            }

            return Result<string>.Success(newId);
        }

        public async Task<Result> ActualizarAsync(string id, LibroUpdateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Result.Failure("El ID proporcionado es inválido.");
            }

            var existe = await _libroRepository.ExisteIdAsync(id);
            if (!existe)
            {
                return Result.Failure("El libro a actualizar no existe.");
            }

            var libro = new Libro
            {
                IdLibro = id,
                Titulo = dto.Titulo,
                IdAutor = dto.IdAutor,
                IdGenero = dto.IdGenero,
                Estatus = dto.Estatus
            };

            var affectedRows = await _libroRepository.ActualizarAsync(libro);

            if (affectedRows == 0)
            {
                return Result.Failure("Hubo un error al actualizar el libro en la base de datos.");
            }

            return Result.Success();
        }

        public async Task<Result> EliminarAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Result.Failure("El ID proporcionado es inválido.");
            }

            var existe = await _libroRepository.ExisteIdAsync(id);
            if (!existe)
            {
                return Result.Failure("El libro a eliminar no existe.");
            }

            var affectedRows = await _libroRepository.EliminarLogicoAsync(id);

            if (affectedRows == 0)
            {
                return Result.Failure("Hubo un error al eliminar el libro en la base de datos.");
            }

            return Result.Success();
        }

        private async Task<string> GenerarIdUnicoAsync()
        {
            string newId;
            bool exists;
            var random = new Random();

            do
            {
                char randomChar = (char)random.Next('A', 'Z' + 1);
                string randomNumbers = random.Next(0, 100).ToString("D2");
                newId = $"{randomChar}{randomNumbers}";

                exists = await _libroRepository.ExisteIdAsync(newId);
            } 
            while (exists);

            return newId;
        }
    }
}

using System.Threading.Tasks;
using Libreria.Application.DTOs;
using Libreria.Application.Services;
using Libreria.Domain.Entities;
using Libreria.Domain.Interfaces;
using Moq;
using Xunit;

namespace Libreria.Tests.Services
{
    public class LibroServiceTests
    {
        private readonly Mock<ILibroRepository> _libroRepositoryMock;
        private readonly LibroService _libroService;

        public LibroServiceTests()
        {
            _libroRepositoryMock = new Mock<ILibroRepository>();
            _libroService = new LibroService(_libroRepositoryMock.Object);
        }

        [Fact]
        public async Task CrearAsync_DebeRetornarExito_Y_NuevoId()
        {
            // Arrange
            var dto = new LibroCreateDTO
            {
                Titulo = "El Quijote",
                IdAutor = 1,
                IdGenero = 1
            };

            // Simulamos que el ID generado no existe (no hay colisión)
            _libroRepositoryMock.Setup(repo => repo.ExisteIdAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Simulamos inserción exitosa
            _libroRepositoryMock.Setup(repo => repo.InsertarAsync(It.IsAny<Libro>()))
                .ReturnsAsync(1);

            // Act
            var result = await _libroService.CrearAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Matches("^[A-Z][0-9]{2}$", result.Value); // Verifica el formato del ID (A12, B05)
            _libroRepositoryMock.Verify(repo => repo.InsertarAsync(It.IsAny<Libro>()), Times.Once);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_SiNoExiste_DebeRetornarFallo()
        {
            // Arrange
            string id = "X99";
            _libroRepositoryMock.Setup(repo => repo.ObtenerPorIdAsync(id))
                .Returns(Task.FromResult<dynamic?>(null));

            // Act
            var result = await _libroService.ObtenerPorIdAsync(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Libro no encontrado.", result.Error);
        }

        [Fact]
        public async Task ActualizarAsync_SiNoExiste_DebeRetornarFallo()
        {
            // Arrange
            string id = "X99";
            var dto = new LibroUpdateDTO { Titulo = "Nuevo", IdAutor = 1, IdGenero = 1 };
            
            _libroRepositoryMock.Setup(repo => repo.ExisteIdAsync(id))
                .ReturnsAsync(false);

            // Act
            var result = await _libroService.ActualizarAsync(id, dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El libro a actualizar no existe.", result.Error);
        }
    }
}

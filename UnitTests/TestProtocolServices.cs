using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.ApplicationCore.Services;
using ProtocolReception.Infrastructure.Repositories.Interfaces;

namespace UnitTests
{
    public class TestProtocolServices

    {
        private readonly IProtocolRepository _protocolRepository;
        private readonly ProtocolService _protocolService;
        private readonly ProtocolLogService _protocolLogService;

        public TestProtocolServices(IProtocolRepository protocolRepository, ProtocolService protocolService, ProtocolLogService protocolLogService)
        {
            _protocolRepository = protocolRepository;
            _protocolService = protocolService;
            _protocolLogService = protocolLogService;
        }

        [Fact]
        public async Task TestAddAsync()
        {
            // Arrange
            var protocol = new Protocol
            {
                Number = 1,
                Cpf = "12345678901",
                Rg = "123456789",
                Name = "Test Name",
                MotherName = "Test Mother",
                PhotoUrl = "http://example.com/photo.jpg"
            };

            // Act
            await _protocolService.AddAsync(protocol);

            // Assert
            var result = await _protocolRepository.GetByNumberAsync(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestGetByCpfAsync()
        {
            // Arrange
            var protocol = new Protocol
            {
                Number = 2,
                Cpf = "12345678901",
                Rg = "123456789",
                Name = "Test Name",
                MotherName = "Test Mother",
                PhotoUrl = "http://example.com/photo.jpg"
            };

            // Act
            await _protocolService.AddAsync(protocol);

            // Assert
            var result = await _protocolService.GetByCpfAsync("12345678901");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestGetByNumberAsync()
        {
            // Arrange
            var protocol = new Protocol
            {
                Number = 3,
                Cpf = "12345678901",
                Rg = "123456789",
                Name = "Test Name",
                MotherName = "Test Mother",
                PhotoUrl = "http://example.com/photo.jpg"
            };

            // Act
            await _protocolService.AddAsync(protocol);

            // Assert
            var result = await _protocolService.GetByNumberAsync(3);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestGetByRgAsync()
        {
            // Arrange
            var protocol = new Protocol
            {
                Number = 4,
                Cpf = "12345678901",
                Rg = "123456789",
                Name = "Test Name",
                MotherName = "Test Mother",
                PhotoUrl = "http://example.com/photo.jpg"
            };

            // Act
            await _protocolService.AddAsync(protocol);

            // Assert
            var result = await _protocolService.GetByRgAsync("123456789");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            // Arrange
            var protocol = new Protocol
            {
                Number = 5,
                Cpf = "12345678901",
                Rg = "123456789",
                Name = "Test Name",
                MotherName = "Test Mother",
                PhotoUrl = "http://example.com/photo.jpg"
            };

            // Act
            await _protocolService.AddAsync(protocol);

            // Assert
            var result = await _protocolService.GetAllAsync();
            Assert.NotNull(result);
        }
    }
}
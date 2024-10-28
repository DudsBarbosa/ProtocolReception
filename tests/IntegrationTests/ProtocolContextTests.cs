using Moq;
using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.ApplicationCore.Interfaces;
using ProtocolReception.ApplicationCore.Services;
using ProtocolReception.Infrastructure.Repositories.Interfaces;

namespace IntegrationTests
{
    public class ProtocolContextTests
    {
        [Fact]
        public async Task ManageConsumerMessage_WithNullMessage_ShouldAddLog()
        {
            // Arrange
            var protocolRepository = new Mock<IProtocolRepository>();
            var protocolLogRepository = new Mock<IProtocolLogRepository>();
            var protocolLogService = new ProtocolLogService(protocolRepository.Object, protocolLogRepository.Object);
            var message = string.Empty;

            // Act
            await protocolLogService.ManageConsumerMessage(message);

            // Assert
            protocolLogRepository.Verify(x => x.AddAsync(It.IsAny<ProtocolLog>()), Times.Once);
        }

        [Fact]
        public async Task AddProtocol_WithValidProtocol_ShouldAddProtocol()
        {
            // Arrange
            var protocolRepository = new Mock<IProtocolRepository>();
            var protocol = new Protocol
            {
                Number = 1,
                Cpf = "12345678901",
                Rg = "123456789",
                Name = "John Doe",
                MotherName = "Jane Doe",
                PhotoUrl = "http://example.com/photo.jpg"
            };
            var protocolService = new ProtocolService(protocolRepository.Object);

            // Act
            await protocolService.AddAsync(protocol);

            // Assert
            protocolRepository.Verify(x => x.AddAsync(It.IsAny<Protocol>()), Times.Once);
        }

        [Fact]
        public async Task GetProtocolByCpf_WithValidCpf_ShouldReturnProtocol()
        {
            // Arrange
            var protocolRepository = new Mock<IProtocolRepository>();
            var protocolService = new ProtocolService(protocolRepository.Object);
            var cpf = "12345678901";

            // Act
            await protocolService.GetByCpfAsync(cpf);

            // Assert
            protocolRepository.Verify(x => x.GetByCpfAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetProtocolByNumber_WithValidNumber_ShouldReturnProtocol()
        {
            // Arrange
            var protocolRepository = new Mock<IProtocolRepository>();
            var protocolService = new ProtocolService(protocolRepository.Object);
            var number = 1;

            // Act
            await protocolService.GetByNumberAsync(number);

            // Assert
            protocolRepository.Verify(x => x.GetByNumberAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetProtocolByRg_WithValidRg_ShouldReturnProtocol()
        {
            // Arrange
            var protocolRepository = new Mock<IProtocolRepository>();
            var protocolService = new ProtocolService(protocolRepository.Object);
            var rg = "123456789";

            // Act
            await protocolService.GetByRgAsync(rg);

            // Assert
            protocolRepository.Verify(x => x.GetByRgAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetProtocolByCpfAndCopy_WithValidCpfAndCopy_ShouldReturnProtocol()
        {
            // Arrange
            var protocolRepository = new Mock<IProtocolRepository>();
            var protocolService = new ProtocolService(protocolRepository.Object);
            var cpf = "12345678901";
            var copy = 1;

            // Act
            await protocolService.GetByCpfAndCopyAsync(cpf, copy);

            // Assert
            protocolRepository.Verify(x => x.GetByCpfAndCopyAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetAllProtocols_ShouldReturnAllProtocols()
        {
            // Arrange
            var protocolRepository = new Mock<IProtocolRepository>();
            var protocolService = new ProtocolService(protocolRepository.Object);

            // Act
            await protocolService.GetAllAsync();

            // Assert
            protocolRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}
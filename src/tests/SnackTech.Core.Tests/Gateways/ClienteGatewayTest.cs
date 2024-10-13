using FluentAssertions;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using SnackTech.Core.Gateways;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Xunit;

namespace SnackTech.Core.Tests.Gateways
{
    public class ClienteGatewayTests
    {
        private readonly IClienteDataSource _dataSource;
        private readonly ClienteGateway _clienteGateway;

        public ClienteGatewayTests()
        {
            _dataSource = Mock.Create<IClienteDataSource>();
            _clienteGateway = new ClienteGateway(_dataSource);
        }

        [Fact]
        public async Task CadastrarNovoCliente_DeveChamarInserirClienteAsync_DoDataSource()
        {
            // Arrange
            var cliente = new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191");

            // Act
            await _clienteGateway.CadastrarNovoCliente(cliente);

            // Assert
            Mock.Assert(() => _dataSource.InserirClienteAsync(Arg.IsAny<ClienteDto>()), Occurs.Once());
        }

        [Fact]
        public async Task CadastrarNovoCliente_DeveRetornarTrue_SeInserirClienteAsync_DoDataSource_RetornarTrue()
        {
            // Arrange
            var cliente = new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191");
            Mock.Arrange(() => _dataSource.InserirClienteAsync(Arg.IsAny<ClienteDto>())).ReturnsAsync(true);

            // Act
            var resultado = await _clienteGateway.CadastrarNovoCliente(cliente);

            // Assert
            resultado.Should().BeTrue();
        }

        [Fact]
        public async Task CadastrarNovoCliente_DeveRetornarFalse_SeInserirClienteAsync_DoDataSource_RetornarFalse()
        {
            // Arrange
            var cliente = new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191");
            Mock.Arrange(() => _dataSource.InserirClienteAsync(Arg.IsAny<ClienteDto>())).ReturnsAsync(false);

            // Act
            var resultado = await _clienteGateway.CadastrarNovoCliente(cliente);

            // Assert
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task ProcurarClientePorCpf_DeveChamarPesquisarPorCpfAsync_DoDataSource()
        {
            // Arrange
            var cpf = new CpfValido("00000000191");

            // Act
            await _clienteGateway.ProcurarClientePorCpf(cpf);

            // Assert
            Mock.Assert(() => _dataSource.PesquisarPorCpfAsync(Arg.IsAny<string>()), Occurs.Once());
        }

        [Fact]
        public async Task ProcurarClientePorCpf_DeveRetornarCliente_SePesquisarPorCpfAsync_DoDataSource_RetornarClienteDto()
        {
            // Arrange
            var cpf = new CpfValido("00000000191");
            var clienteDto = new ClienteDto { Id = Guid.NewGuid(), Nome = "nome", Email = "email@email.com", Cpf = cpf };
            Mock.Arrange(() => _dataSource.PesquisarPorCpfAsync(Arg.IsAny<string>())).ReturnsAsync(clienteDto);

            // Act
            var cliente = await _clienteGateway.ProcurarClientePorCpf(cpf);

            // Assert
            cliente.Should().NotBeNull();
            cliente.Id.Valor.Should().Be(clienteDto.Id);
            cliente.Nome.Valor.Should().Be(clienteDto.Nome);
            cliente.Email.Valor.Should().Be(clienteDto.Email);
            cliente.Cpf.Valor.Should().Be(clienteDto.Cpf);
        }

        [Fact]
        public async Task ProcurarClientePorCpf_DeveRetornarNull_SePesquisarPorCpfAsync_DoDataSource_RetornarNull()
        {
            // Arrange
            var cpf = new CpfValido("00000000191");
            Mock.Arrange(() => _dataSource.PesquisarPorCpfAsync(Arg.IsAny<string>())).Returns(Task.FromResult<ClienteDto?>(null));

            // Act
            var cliente = await _clienteGateway.ProcurarClientePorCpf(cpf);

            // Assert
            cliente.Should().BeNull();
        }

        [Fact]
        public async Task ProcurarClientePorEmail_DeveChamarPesquisarPorEmailAsync_DoDataSource()
        {
            // Arrange
            var email = new EmailValido("email@email.com");
        
            // Act
            await _clienteGateway.ProcurarClientePorEmail(email);
        
            // Assert
            Mock.Assert(() => _dataSource.PesquisarPorEmailAsync(Arg.IsAny<string>()), Occurs.Once());
        }
        
        [Fact]
        public async Task ProcurarClientePorEmail_DeveRetornarCliente_SePesquisarPorEmailAsync_DoDataSource_RetornarClienteDto()
        {
            // Arrange
            var email = new EmailValido("email@email.com");
            var clienteDto = new ClienteDto { Id = Guid.NewGuid(), Nome = "nome", Email = "email@email.com", Cpf = "00000000191" };
            Mock.Arrange(() => _dataSource.PesquisarPorEmailAsync(Arg.IsAny<string>())).ReturnsAsync(clienteDto);
        
            // Act
            var cliente = await _clienteGateway.ProcurarClientePorEmail(email);
        
            // Assert
            cliente.Should().NotBeNull();
            cliente.Id.Valor.Should().Be(clienteDto.Id);
            cliente.Nome.Valor.Should().Be(clienteDto.Nome);
            cliente.Email.Valor.Should().Be(clienteDto.Email);
            cliente.Cpf.Valor.Should().Be(clienteDto.Cpf);
        }
        
        [Fact]
        public async Task ProcurarClientePorEmail_DeveRetornarNull_SePesquisarPorCpfAsync_DoDataSource_RetornarNull()
        {
            // Arrange
            var email = new EmailValido("email@email.com");
            Mock.Arrange(() => _dataSource.PesquisarPorCpfAsync(Arg.IsAny<string>())).ReturnsAsync((ClienteDto?)null);
        
            // Act
            var cliente = await _clienteGateway.ProcurarClientePorEmail(email);
        
            // Assert
            cliente.Should().BeNull();
        }
        
        [Fact]
        public async Task ProcurarClientePorIdentificacao_DeveChamarPesquisarPorIdAsync_DoDataSource()
        {
            // Arrange
            var identificacao = new GuidValido(Guid.NewGuid());
        
            // Act
            await _clienteGateway.ProcurarClientePorIdentificacao(identificacao);
        
            // Assert
            Mock.Assert(() => _dataSource.PesquisarPorIdAsync(Arg.IsAny<Guid>()), Occurs.Once());
        }
        
        [Fact]
        public async Task ProcurarClientePorIdentificacao_DeveRetornarCliente_SePesquisarPorIdAsync_DoDataSource_RetornarClienteDto()
        {
            // Arrange
            var identificacao = new GuidValido(Guid.NewGuid());
            var clienteDto = new ClienteDto { Id = Guid.NewGuid(), Nome = "nome", Email = "email@email.com", Cpf = "00000000191" };
            Mock.Arrange(() => _dataSource.PesquisarPorIdAsync(Arg.IsAny<Guid>())).ReturnsAsync(clienteDto);
        
            // Act
            var cliente = await _clienteGateway.ProcurarClientePorIdentificacao(identificacao);
        
            // Assert
            cliente.Should().NotBeNull();
            cliente.Id.Valor.Should().Be(clienteDto.Id);
            cliente.Nome.Valor.Should().Be(clienteDto.Nome);
            cliente.Email.Valor.Should().Be(clienteDto.Email);
            cliente.Cpf.Valor.Should().Be(clienteDto.Cpf);
        }
        
        [Fact]
        public async Task ProcurarClientePorIdentificacao_DeveRetornarNull_SePesquisarPorIdAsync_DoDataSource_RetornarNull()
        {
            // Arrange
            var identificacao = new GuidValido(Guid.NewGuid());
            Mock.Arrange(() => _dataSource.PesquisarPorIdAsync(Arg.IsAny<GuidValido>())).ReturnsAsync((ClienteDto)null);
        
            // Act
            var cliente = await _clienteGateway.ProcurarClientePorIdentificacao(identificacao);
        
            // Assert
            cliente.Should().BeNull();
        }

        [Fact]
        public void ConverterParaDto_DeveRetornarClienteDto_ComDadosCorretos()
        {
            // Arrange
            var cliente = new Cliente(Guid.NewGuid(), "nome", new EmailValido("email@email.com"), new CpfValido("00000000191"));
        
            // Act
            var clienteDto = ClienteGateway.ConverterParaDto(cliente);
        
            // Assert
            clienteDto.Id.Should().Be(cliente.Id.Valor);
            clienteDto.Nome.Should().Be(cliente.Nome.Valor);
            clienteDto.Email.Should().Be(cliente.Email.Valor);
            clienteDto.Cpf.Should().Be(cliente.Cpf.Valor);
        }
        
        [Fact]
        public void ConverterParaEntidade_DeveRetornarCliente_ComDadosCorretos()
        {
            // Arrange
            var clienteDto = new ClienteDto { Id = Guid.NewGuid(), Nome = "nome", Email = "email@email.com", Cpf = "00000000191" };
        
            // Act
            var cliente = ClienteGateway.converterParaEntidade(clienteDto);
        
            // Assert
            cliente.Id.Valor.Should().Be(clienteDto.Id);
            cliente.Nome.Valor.Should().Be(clienteDto.Nome);
            cliente.Email.Valor.Should().Be(clienteDto.Email);
            cliente.Cpf.Valor.Should().Be(clienteDto.Cpf);
        }
    }
}
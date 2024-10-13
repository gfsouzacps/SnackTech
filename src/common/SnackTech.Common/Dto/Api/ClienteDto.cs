namespace SnackTech.Common.Dto.Api;

public class ClienteDto
{
    public Guid IdentificacaoCliente { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}


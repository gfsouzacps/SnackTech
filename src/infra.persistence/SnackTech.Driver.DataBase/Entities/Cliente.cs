namespace SnackTech.Driver.DataBase.Entities
{
    public class Cliente : Pessoa
    {
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;

    }
}
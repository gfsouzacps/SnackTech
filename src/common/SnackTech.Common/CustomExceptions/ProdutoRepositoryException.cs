namespace SnackTech.Common.CustomExceptions
{
    public class ProdutoRepositoryException : Exception
    {
        public ProdutoRepositoryException(string message) : base(message)
        {}
    }
}
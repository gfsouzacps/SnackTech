    namespace SnackTech.Domain.Exceptions.Driven;
    
    public class ProdutoRepositoryException : Exception
    {
        public ProdutoRepositoryException(string message) : base(message)
        {
        }
    }
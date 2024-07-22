using SnackTech.Domain.Enums;

namespace SnackTech.Domain.Guards
{
    public static class CustomGuards
    {
        public static void AgainstStringNullOrEmpty(string value, string parameterName){
            if(string.IsNullOrEmpty(value)){
                throw new ArgumentException($"{parameterName} não pode ser nulo ou vazio.",parameterName);
            }
        }

        public static void AgainstStringNullOrWhiteSpace(string value, string parameterName){
            if(string.IsNullOrWhiteSpace(value)){
                throw new ArgumentException($"{parameterName} não pode ser nulo, vazio ou texto em branco.", parameterName);
            }
        }

        public static void AgainstNegativeOrZeroValue(decimal value, string parameterName){
            if(value <= 0){
                throw new ArgumentException($"{parameterName} precisa ser maior do que zero.", parameterName);
            }
        }

        public static void AgainstObjectNull(object value, string paramName){
            if(value == null){
                throw new ArgumentException($"{paramName} não pode ser nulo.", paramName);
            }
        }

        public static CategoriaProduto AgainstInvalidCategoriaProduto(int value, string paramName){
            if(!Enum.IsDefined(typeof(CategoriaProduto),value)){
                throw new ArgumentException($"{paramName} não é um CategoriaProduto válido.",paramName);
            }
            else{
                return (CategoriaProduto)value;
            }
        }

        public static Guid AgainstInvalidGuid(string value, string paramName){
            if(Guid.TryParse(value,out Guid guid)){
                return guid;
            }
            else{
                throw new ArgumentException($"{paramName} não é um Guid válido.",paramName);
            }
        }
    }
}
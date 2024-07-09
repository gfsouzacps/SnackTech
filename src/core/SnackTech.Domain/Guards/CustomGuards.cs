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
    }
}
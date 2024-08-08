namespace SnackTech.Domain.Guards
{
    public static class CpfGuard
    {
        public static void AgainstInvalidCpf(string cpfValue, string parameterName){
            if(string.IsNullOrWhiteSpace(cpfValue))
                throw new ArgumentException($"{parameterName} não pode ser nulo ou espaços em branco.",parameterName);

            bool cpfEstaValido = true;
            try{
                cpfEstaValido = IsValidCPF(cpfValue);
            }
            catch(Exception ex){
                throw new ArgumentException($"{parameterName} gerou erro ao ser validado como CPF.",parameterName,ex);
            }

            if(!cpfEstaValido)
                throw new ArgumentException($"{parameterName} com valor {cpfValue} não é um CPF válido.", parameterName);
        }

        private static bool IsValidCPF(string cpf){
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            
            int soma = CalculateCpfSoma(multiplicador1,tempCpf);
            int resto = CalculateCpfResto(soma);

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            
            soma = CalculateCpfSoma(multiplicador2,tempCpf);
            resto = CalculateCpfResto(soma);

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private static int CalculateCpfSoma(int[] multipliers, string tempCpf){
            int soma = 0;
            for(int i = 0; i < multipliers.Length; i++){
               soma += int.Parse(tempCpf[i].ToString()) * multipliers[i];
            }

            return soma;
        }

        private static int CalculateCpfResto(int soma){
            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            return resto;
        }
    }
}
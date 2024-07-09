using System.Net.Mail;

namespace SnackTech.Domain.Guards
{
    public static class EmailGuard
    {
        public static void AgainstInvalidEmail(string emailValue, string parameterName){
            if(string.IsNullOrWhiteSpace(emailValue)){
                throw new ArgumentException($"{parameterName} não pode ser um e-mail nulo ou somente espaços em branco.", parameterName);
            }

            if(!IsValidEmail(emailValue)){
                throw new ArgumentException($"{parameterName} tem o valor {emailValue} que não é um e-mail válido.",parameterName);
            }
        }

        private static bool IsValidEmail(string email){
            try{
                var mailAddress = new MailAddress(email);
                return mailAddress != null;
            }
            catch(FormatException){
                return false;
            }
        }
    }
}
using Camp.Common.Exceptions;
using System.Text.RegularExpressions;

namespace Camp.BusinessLogicLayer.Validation
{
    public class CredentialsValidation
    {
        private const int MinLoginLength = 7;
        private const int MinPasswordLength = 7;

        public void Validate(string login, string password)
        {
            LoginValidate(login);
            PasswordValidate(password);
        }

        private void LoginValidate(string login)
        {
            if (login is null)
                throw new ValidateException("Login is required.");

            if (login.Length < MinLoginLength)
                throw new ValidateException($"Login must contains over {MinLoginLength - 1} symbols.");
        }

        private void PasswordValidate(string password)
        {
            if (password is null)
                throw new ValidateException("Password is required.");

            var hasMinimumChars = new Regex(@".{" + MinPasswordLength + @",}");

            if (!hasMinimumChars.IsMatch(password))
                throw new ValidateException($"Password must contains over {MinPasswordLength - 1} symbols.");
        }
    }
}

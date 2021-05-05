using System.Text.RegularExpressions;

namespace Password.Services
{
    public class DefaultPasswordValidator : IPasswordValidator
    {
        public bool IsValid(string password)
        {
            return HasAtLeastNineCharacters(password)
                && HasAtLeastOneDigit(password)
                && HasAtLeastOneLowerCaseCharacter(password)
                && HasAtLeastOneUpperCaseCharacter(password)
                && HasAtLeastOneSpecialCharacter(password)
                && HasNoRepeatedCharacter(password);
        }

        private bool HasAtLeastNineCharacters(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 9;
        }

        private bool HasAtLeastOneDigit(string password)
        {
            return Regex.IsMatch(password, "[0-9]");
        }

        private bool HasAtLeastOneLowerCaseCharacter(string password)
        {
            return Regex.IsMatch(password, "[a-z]");
        }

        private bool HasAtLeastOneUpperCaseCharacter(string password)
        {
            return Regex.IsMatch(password, "[A-Z]");
        }

        private bool HasAtLeastOneSpecialCharacter(string password)
        {
            return Regex.IsMatch(password, @"[!@#\$%\^&\*\(\)-\+]");
        }

        private bool HasNoRepeatedCharacter(string password)
        {
            string incrementalPassword = "";
            foreach (char character in password) 
            {
                if (incrementalPassword.IndexOf(character) >= 0)
                {
                    return false;
                }

                incrementalPassword += character;
            }

            return true;
        }
    }
}

namespace Password.API.Services
{
    public class DefaultPasswordValidator : IPasswordValidator
    {
        public bool IsValid(string password)
        {
            return true;
        }
    }
}

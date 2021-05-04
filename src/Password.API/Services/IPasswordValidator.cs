namespace Password.API.Services
{
    public interface IPasswordValidator
    {
        bool IsValid(string password);
    }
}

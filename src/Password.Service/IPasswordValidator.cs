namespace Password.Services
{
    public interface IPasswordValidator
    {
        bool IsValid(string password);
    }
}

namespace BL.Users
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}

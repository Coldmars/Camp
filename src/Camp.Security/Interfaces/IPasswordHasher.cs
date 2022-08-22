namespace Camp.Security.Interfaces
{
    public interface IPasswordHasher
    {
        byte[] GetHash(string password);

        string GetHashString(string password);
    }
}

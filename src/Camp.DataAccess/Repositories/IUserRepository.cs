using Camp.DataAccess.Entities;

namespace Camp.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);

        IQueryable<User> GetUserById(int userId);

        IQueryable<User> GetUserByCredentials(string login, string passwordHash);

        IQueryable<User> GetUsersByRole(int roleId);

        IQueryable<User> GetUserByLogin(string login);
    }
}

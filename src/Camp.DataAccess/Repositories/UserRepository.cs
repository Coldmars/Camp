using Camp.DataAccess.Data;
using Camp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Camp.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public IQueryable<User> GetUserById(int userId)
        {
            return _context
                        .Users
                        .Where(user => user.Id == userId)
                        .Include(user => user.Role);
        }

        public IQueryable<User> GetUserByCredentials(string login, string passwordHash)
        {
            return _context
                        .Users
                        .Where(user => user.Login == login &&
                                       user.PasswordHash == passwordHash)
                        .Include(user => user.Role);
        }

        public IQueryable<User> GetUsersByRole(int roleId)
        {
            return _context
                        .Users
                        .Where(user => user.RoleId == roleId);
        }

        public IQueryable<User> GetUserByLogin(string login)
        {
            return _context
                        .Users
                        .Where(user => user.Login == login);
        }
    }
}

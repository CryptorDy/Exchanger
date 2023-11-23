using Exchanger.Context;
using Exchanger.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Exchanger.Services
{
    public class UserService : IUserService
    {
        private AppDbContext _db;
        public UserService(AppDbContext context) 
        {
            _db = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetUser(Guid id)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found");
            }
            return user;
        }

        public async Task Create(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }
    }
}

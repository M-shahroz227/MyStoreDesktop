using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyStoreDesktop.Data;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DatabaseHelper _context;

        public UserService()
        {
            _context = new DatabaseHelper();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User updatedUser)
        {
            var existingUser = _context.Users.Find(updatedUser.Id);
            if (existingUser == null) return;

            existingUser.UserName = updatedUser.UserName;
            existingUser.Role = updatedUser.Role;

            // Password sirf tab update ho jab naya diya ho
            if (updatedUser.PasswordHash != null)
            {
                existingUser.PasswordHash = updatedUser.PasswordHash;
                existingUser.PasswordSalt = updatedUser.PasswordSalt;
            }

            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}

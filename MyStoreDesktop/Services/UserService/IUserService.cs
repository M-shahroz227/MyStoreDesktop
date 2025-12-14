using System.Collections.Generic;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services.UserService
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User updatedUser);
        void Delete(int id);
    }
}

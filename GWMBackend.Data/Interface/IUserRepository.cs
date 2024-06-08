using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Data.Interface
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        void EditUser(User user);
        bool ExistUserByRoleId(int RoleId);
        void AddUser(User user);
        List<User> GetAllUsersExceptCurrent(int userId);
        bool IsExistEmail(string email);
        bool IsExistUserName(string username);
        User GetUserLogin(string username, string password);
        User GetUserByUsername(string username);
    }
}

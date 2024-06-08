using GWMBackend.Core.Model.Base;
using GWMBackend.Domain.Models;

namespace GWMBackend.Service.Local.Interface
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        void EditUser(User user);
        bool ExistUserByRoleId(int RoleId);
        Tuple<bool, bool> CheckUserNameAndEmailExist(string email, string username);
        int AddUser(User user);
        User LoginUser(string username, string password);
        Token GenToken(User user);
        List<User> GetAllUsersExceptCurrent(int userId);
        User GetUserByUsername(string username);
    }
}

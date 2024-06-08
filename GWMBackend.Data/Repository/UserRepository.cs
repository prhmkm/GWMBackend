using GWMBackend.Data.Base;
using GWMBackend.Data.Interface;
using GWMBackend.Domain.Models;

namespace GWMBackend.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        GWM_DBContext _repositoryContext;
        public UserRepository(GWM_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public void AddUser(User user)
        {
            Create(user);
            Save();
        }

        public void EditUser(User user)
        {
            Update(user);
            Save();
        }

        public bool ExistUserByRoleId(int RoleId)
        {
            return FindByCondition(w => w.RoleId == RoleId).Any();
        }

        public List<User> GetAllUsers()
        {
            return FindByCondition(w => w.IsActive == true).ToList();
        }

        public List<User> GetAllUsersExceptCurrent(int userId)
        {
            return FindByCondition(w => w.Id != userId).ToList();
        }

        public User GetUserById(int userId)
        {
            return FindByCondition(w => w.Id == userId).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            var x = _repositoryContext.Users.Where(w => w.UserName == username);

            return x.FirstOrDefault();
        }

        public User GetUserLogin(string username, string password)
        {
            return FindByCondition(w => w.UserName == username && w.Password == password && w.IsActive == true).FirstOrDefault();
        }

        public bool IsExistEmail(string email)
        {
            return FindByCondition(w => w.Email == email && w.IsActive == true).Any();
        }

        public bool IsExistUserName(string username)
        {
            return FindByCondition(w => w.UserName == username && w.IsActive == true).Any();
        }
    }
}

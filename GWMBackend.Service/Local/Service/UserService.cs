using GWMBackend.Core.Helpers;
using GWMBackend.Core.Model.Base;
using GWMBackend.Data.Base;
using GWMBackend.Domain.Models;
using GWMBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GWMBackend.Service.Local.Service
{
    public class UserService : IUserService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public UserService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }
        public Tuple<bool, bool> CheckUserNameAndEmailExist(string email, string username)
        {
            bool emailValid = _repository.user.IsExistEmail(email.FixText());
            bool userNameValid = _repository.user.IsExistUserName(username.FixText());

            return Tuple.Create(emailValid, userNameValid);
        }
        public int AddUser(User user)
        {
            _repository.user.AddUser(user);
            return user.Id;
        }

        public List<User> GetAllUsers()
        {
            return _repository.user.GetAllUsers();
        }

        public User GetUserById(int userId)
        {
            return _repository.user.GetUserById(userId);
        }
        public void EditUser(User user)
        {
            _repository.user.EditUser(user);
        }
        public bool ExistUserByRoleId(int RoleId)
        {
            return _repository.user.ExistUserByRoleId(RoleId);
        }
        public User LoginUser(string username, string password)
        {
            return _repository.user.GetUserLogin(username.FixText(), password);
        }
        public Token GenToken(User user)
        {
            return new Token(GenerateToken(user));
        }
        private string GenerateToken(User user, int? tokenValidateInMinutes = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.TokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenValidateInMinutes ?? _appSettings.TokenValidateInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public List<User> GetAllUsersExceptCurrent(int userId)
        {
            return _repository.user.GetAllUsersExceptCurrent(userId);
        }

        public User GetUserByUsername(string username)
        {
            return _repository.user.GetUserByUsername(username);
        }

    }
}

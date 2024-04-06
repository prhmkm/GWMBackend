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
    public class EmailService : IEmailService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;
        public EmailService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Edit(Customer customer)
        {
            _repository.email.Edit(customer);
        }

        public Customer verifyCode(int id, string code)
        {
            return _repository.email.verifyCode(id, code);
        }
        public Token GenToken(Customer customer)
        {
            return new Token(GenerateToken(customer));
        }
        private string GenerateToken(Customer customer, int? tokenValidateInMinutes = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.TokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customer.Name),
                    new Claim(ClaimTypes.Role, "Customer"),
                    new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenValidateInMinutes ?? _appSettings.TokenValidateInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Customer verifyEmail(string email)
        {
            return _repository.email.verifyEmail(email);
        }

        public Customer GetById(int id)
        {
            return _repository.email.GetById(id);
        }
    }
}

using GWMBackend.Core.Model.Base;
using GWMBackend.Domain.Models;

namespace GWMBackend.Service.Local.Interface
{
    public interface IEmailService
    {
        Customer verifyEmail(string email);
        Customer verifyCode(int id, string code);
        Customer GetById(int id);
        Token GenToken(Customer customer);
        void Edit(Customer customer);

    }
}

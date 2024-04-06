using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Data.Interface
{
    public interface IEmailRepository
    {
        Customer verifyEmail(string email);
        Customer verifyCode(int id, string code);
        Customer GetById(int id);
        void Edit(Customer customer);

    }
}

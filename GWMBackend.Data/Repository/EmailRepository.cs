using GWMBackend.Data.Base;
using GWMBackend.Data.Interface;
using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Data.Repository
{
    public class EmailRepository : BaseRepository<Customer>, IEmailRepository
    {
        GWM_DBContext _repositoryContext;
        public EmailRepository(GWM_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Edit(Customer customer)
        {
            _repositoryContext.Update(customer);
            Save();
        }

        public Customer GetById(int id)
        {
            return _repositoryContext.Customers.Where(s =>
            s.Id == id).FirstOrDefault();
        }

        public Customer verifyCode(int id, string code)
        {
            return _repositoryContext.Customers.Where(s => s.Id == id &&
            s.VerificationCode == code).FirstOrDefault();
        }

        public Customer verifyEmail(string email)
        {
            return _repositoryContext.Customers.Where(s =>
            s.Email == email).FirstOrDefault();
        }
    }
}

using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Service.Remote.Interface
{
    public interface IEmailSmtpService
    {
        void SendEmail(Customer customer, string code);
    }
}

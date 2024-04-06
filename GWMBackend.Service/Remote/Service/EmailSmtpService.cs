using GWMBackend.Core.Model.Base;
using GWMBackend.Data.Base;
using GWMBackend.Domain.Models;
using GWMBackend.Service.Remote.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mail.Abstractions;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Service.Remote.Service
{
    public class EmailSmtpService : IEmailSmtpService
    {
        private readonly AppSettings _appSettings;
        public EmailSmtpService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public void SendEmail(Customer customer, string code)
        {

            var smtpClient = new System.Net.Mail.SmtpClient(_appSettings.EmailHost)
            {
                Port = _appSettings.EmailPort,
                Credentials = new NetworkCredential(_appSettings.Username, _appSettings.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            { 
                From = new MailAddress(_appSettings.Mail),
                Subject = "Your verification code!",
                Body = $"<h1>Hello {customer.Name}</h1>" +
                $"<h2>Your verification code is {code}</h2>" +
                $"<h3>From GWM</h3>",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(customer.Email);

            smtpClient.Send(mailMessage);
        }
    }
}

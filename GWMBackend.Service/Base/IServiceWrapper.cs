

using GWMBackend.Service.Local.Interface;
using GWMBackend.Service.Remote.Interface;

namespace GWMBackend.Service.Base
{
    public interface IServiceWrapper
    {
        IEmailService email { get; }
        IOrderService order { get; }
        IEmailSmtpService emailSmtpService { get; }
        void Save();
    }
}

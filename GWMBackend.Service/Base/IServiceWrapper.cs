

using GWMBackend.Service.Local.Interface;
using GWMBackend.Service.Remote.Interface;

namespace GWMBackend.Service.Base
{
    public interface IServiceWrapper
    {
        IEmailService email { get; }
        IOrderService order { get; }
        IEmailSmtpService emailSmtpService { get; }
        IProductService product { get; }
        IShopItemService shopItem { get; }
        ICustomerService customer { get; }
        IPhotoService photo { get; }
        IUserService user { get; }
        void Save();
    }
}

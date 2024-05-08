using Microsoft.Extensions.Options;
using GWMBackend.Core.Model.Base;
using GWMBackend.Data.Base;
using GWMBackend.Service.Local.Interface;
using GWMBackend.Service.Local.Service;
using GWMBackend.Service.Remote.Interface;
using GWMBackend.Service.Remote.Service;


namespace GWMBackend.Service.Base
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly IOptions<AppSettings> _appSettings;
        private IRepositoryWrapper _repository;
        public ServiceWrapper(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
            _repository = repository;
        }

        public IEmailService email => new EmailService(_repository, _appSettings);

        public IOrderService order => new OrderService(_repository);

        public IEmailSmtpService emailSmtpService => new EmailSmtpService(_appSettings);

        public IProductService product => new ProductService(_repository);

        public IShopItemService shopItem => new ShopItemService(_repository);

        public ICustomerService customer => new CustomerService(_repository);

        public IPhotoService photo => new PhotoService(_appSettings);

        public void Save()
        {
            _repository.Save();
        }
    }
}

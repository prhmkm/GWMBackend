using GWMBackend.Data.Interface;

namespace GWMBackend.Data.Base
{
    public interface IRepositoryWrapper
    {
        IEmailRepository email { get; }
        IOrderRepository order { get; }
        IProductRepository product { get; }
        IShopItemRepository shopItem { get; }
        ICustomerRepository customer { get; }   
        void Save();
    }
}

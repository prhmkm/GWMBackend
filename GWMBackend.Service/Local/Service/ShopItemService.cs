using GWMBackend.Data.Base;
using GWMBackend.Domain.Models;
using GWMBackend.Service.Local.Interface;

namespace GWMBackend.Service.Local.Service
{
    public class ShopItemService : IShopItemService
    {
        IRepositoryWrapper _repository;
        public ShopItemService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public void Add(ShopItem shopItem)
        {
            _repository.shopItem.Add(shopItem);
        }
    }
}

using GWMBackend.Data.Base;
using GWMBackend.Data.Interface;
using GWMBackend.Domain.Models;

namespace GWMBackend.Data.Repository
{
    public class ShopItemRepository : BaseRepository<ShopItem>, IShopItemRepository
    {
        GWM_DBContext _repositoryContext;
        public ShopItemRepository(GWM_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Add(ShopItem shopItem)
        {
            Create(shopItem);
            Save();
        }
    }
}

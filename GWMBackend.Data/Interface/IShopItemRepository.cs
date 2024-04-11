using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Data.Interface
{
    public interface IShopItemRepository
    {
        void Add(ShopItem shopItem);
    }
}

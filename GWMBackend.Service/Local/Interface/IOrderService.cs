using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Service.Local.Interface
{
    public interface IOrderService
    {
        int AddOrder(Order order);
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        void DeleteOrderById(int id);

    }
}

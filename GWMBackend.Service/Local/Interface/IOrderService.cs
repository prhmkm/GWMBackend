using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GWMBackend.Domain.DTOs.OrderDTO;

namespace GWMBackend.Service.Local.Interface
{
    public interface IOrderService
    {
        int AddOrder(Order order);
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        bool CheckOrders(int id);
        void DeleteOrderById(int id);
        List<BOGetAllOrders> BOGetAll();
        List<BucketAmount> GetAlBucketAmont();

    }
}

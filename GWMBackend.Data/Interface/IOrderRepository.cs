using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GWMBackend.Domain.DTOs.OrderDTO;

namespace GWMBackend.Data.Interface
{
    public interface IOrderRepository
    {
        int AddOrder(Order order);
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        void DeleteOrderById(int id);
        bool CheckOrders(int id);
        List<BOGetAllOrders> BOGetAll();
           
        
    }
}

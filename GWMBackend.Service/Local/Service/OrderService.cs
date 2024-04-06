using GWMBackend.Data.Base;
using GWMBackend.Domain.Models;
using GWMBackend.Service.Local.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Service.Local.Service
{
    public class OrderService : IOrderService
    {
        IRepositoryWrapper _repository;
        public OrderService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public void AddOrder(Order order)
        {
            _repository.order.AddOrder(order);
        }

        public void DeleteOrderById(int id)
        {
            _repository.order.DeleteOrderById(id);
        }

        public List<Order> GetAllOrders()
        {
            return _repository.order.GetAllOrders();
        }

        public Order GetOrderById(int id)
        {
            return _repository.order.GetOrderById(id);
        }
    }
}

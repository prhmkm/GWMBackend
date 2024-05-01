using GWMBackend.Data.Base;
using GWMBackend.Domain.DTOs;
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
        public int AddOrder(Order order)
        {
            return _repository.order.AddOrder(order);
        }

        public List<OrderDTO.BOGetAllOrders> BOGetAll()
        {
            return _repository.order.BOGetAll();
        }

        public bool CheckOrders(int id)
        {
            return _repository.order.CheckOrders(id);   
        }

        public void DeleteOrderById(int id)
        {
            _repository.order.DeleteOrderById(id);
        }

        public List<BucketAmount> GetAlBucketAmont()
        {
            return _repository.order.GetAllBucketAmont();
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

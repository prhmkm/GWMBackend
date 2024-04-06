using GWMBackend.Data.Base;
using GWMBackend.Data.Interface;
using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Data.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        GWM_DBContext _repositoryContext;
        public OrderRepository(GWM_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void AddOrder(Order order)
        {
            _repositoryContext.Orders.Add(order);
            _repositoryContext.SaveChanges();
        }

        public void DeleteOrderById(int id)
        {
            var res = _repositoryContext.Orders.Where(r => r.Id == id).FirstOrDefault();
            if (res != null)
            {
                _repositoryContext.Orders.Remove(res);
            }
        }

        public List<Order> GetAllOrders()
        {

            return _repositoryContext.Orders.Select(r =>
            new Order
            {
                Id = r.Id,
                BucketAmont = r.BucketAmont,
                CreationDatetime = r.CreationDatetime,
                CustomerId = r.CustomerId,
                PickupDate = r.PickupDate,
                ProductsId = r.ProductsId,
            }).ToList();

        }

        public Order GetOrderById(int id)
        {
            return _repositoryContext.Orders.Where(r => r.Id == id).FirstOrDefault();
        }
    }
}

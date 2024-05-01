using GWMBackend.Data.Base;
using GWMBackend.Data.Interface;
using GWMBackend.Domain.DTOs;
using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GWMBackend.Domain.DTOs.OrderDTO;

namespace GWMBackend.Data.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        GWM_DBContext _repositoryContext;
        public OrderRepository(GWM_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public int AddOrder(Order order)
        {
            _repositoryContext.Orders.Add(order);
            _repositoryContext.SaveChanges();
            return order.Id;
        }

        public List<OrderDTO.BOGetAllOrders> BOGetAll()
        {
            return _repositoryContext.Orders.Select(s =>
            new BOGetAllOrders
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                CustomerName = _repositoryContext.Customers.FirstOrDefault(o => o.Id == s.CustomerId).FirstName + " " + _repositoryContext.Customers.FirstOrDefault(o => o.Id == s.CustomerId).LastName,
                PickupDate = s.PickupDate,
                BucketAmount = _repositoryContext.BucketAmounts.FirstOrDefault(o => o.Id == s.BucketAmountId).Title,
                IsDone = s.IsDone,
                RegistrationDate = s.CreationDatetime,
                Products = _repositoryContext.ShopItems
                .Where(o => o.OrderId == s.Id)
                .Select(o =>
                new BOProducts
                {
                    Id = o.Id,
                    Title = _repositoryContext.Products.FirstOrDefault(x => x.Id == o.ProductId).Title,
                    Quantity = o.Amount
                }).ToList()
            }).ToList();
        }

        public bool CheckOrders(int id)
        {
            return _repositoryContext.Orders
                .Where(s => s.CustomerId == id && s.IsDone == false)
                .Any();
        }

        public void DeleteOrderById(int id)
        {
            var res = _repositoryContext.Orders.Where(r => r.Id == id).FirstOrDefault();
            if (res != null)
            {
                _repositoryContext.Orders.Remove(res);
            }
        }

        public List<BucketAmount> GetAllBucketAmont()
        {
            return _repositoryContext.BucketAmounts.ToList();
        }

        public List<Order> GetAllOrders()
        {

            return _repositoryContext.Orders.Select(r =>
            new Order
            {
                Id = r.Id,
                BucketAmountId = r.BucketAmountId,
                CreationDatetime = r.CreationDatetime,
                CustomerId = r.CustomerId,
                PickupDate = r.PickupDate,
                //ProductsId = r.ProductsId,
            }).ToList();

        }

        public Order GetOrderById(int id)
        {
            return _repositoryContext.Orders.Where(r => r.Id == id).FirstOrDefault();
        }
    }
}

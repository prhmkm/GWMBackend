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
    public class ProductService : IProductService
    {
        IRepositoryWrapper _repository;
        public ProductService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public void Add(Product product)
        {
            _repository.product.Add(product);
        }

        public void Edit(Product product)
        {
            _repository.product.Edit(product);
        }

        public List<ProductDTO.GetAllProducts> GetAll()
        {
            return _repository.product.GetAll();
        }

        public Product GetById(int id)
        {
            return _repository.product.GetById(id);    
        }
    }
}

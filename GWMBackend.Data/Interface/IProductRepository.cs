﻿using GWMBackend.Domain.Models;
using static GWMBackend.Domain.DTOs.ProductDTO;

namespace GWMBackend.Data.Interface
{
    public interface IProductRepository
    {
        List<GetAllProducts> GetAll();
        List<BOGetAllProducts> BOGetAll();
        void Add(Product product);
        void Edit(Product product);   
        Product GetById(int id);
    }
}

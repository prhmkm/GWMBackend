using GWMBackend.Data.Base;
using GWMBackend.Data.Interface;
using GWMBackend.Domain.DTOs;
using GWMBackend.Domain.Models;

namespace GWMBackend.Data.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        GWM_DBContext _repositoryContext;
        public ProductRepository(GWM_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Add(Product product)
        {
            Create(product);
            Save();
        }

        public void Edit(Product product)
        {
            Update(product);
            Save();
        }

        public List<ProductDTO.GetAllProducts> GetAll()
        {
            return _repositoryContext.Products.Select(s =>
            new ProductDTO.GetAllProducts
            {
                Id = s.Id,
                Title = s.Title,
                Inventory = s.Inventory,
                Photo = s.Photo
            }).ToList();
        }

        public Product GetById(int id)
        {
            return _repositoryContext.Products.FirstOrDefault(s => s.Id == id);
        }
    }
}

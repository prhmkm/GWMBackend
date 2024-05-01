using GWMBackend.Data.Base;
using GWMBackend.Data.Interface;
using GWMBackend.Domain.DTOs;
using GWMBackend.Domain.Models;

namespace GWMBackend.Data.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        GWM_DBContext _repositoryContext;
        public CustomerRepository(GWM_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Add(Customer customer)
        {
            Create(customer);
            Save();
        }

        public List<CustomerDTO.BOGetAllCustomers> BOGetAllNewCustomers()
        {
            return _repositoryContext.Customers
                .Where(o => o.IsRegister == false)
                .Select(s =>
            new CustomerDTO.BOGetAllCustomers
            {
                Id = s.Id,
                RoleId = s.RoleId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                PhoneNumber = s.PhoneNumber,
                Email = s.Email,
                Address = s.Address,
                RestaurantName = s.RestaurantName,
                ZipCode = s.ZipCode,
                IsActive = s.IsActive,
                JoinDate = s.JoinDate,
                IsRegister = s.IsRegister,
            }).OrderBy(w => w.JoinDate)
            .ToList();
        }

        public List<CustomerDTO.BOGetAllCustomers> BOGetAllRegisteredCustomers()
        {
            return _repositoryContext.Customers
                .Where(o => o.IsRegister == true)
                .Select(s =>
            new CustomerDTO.BOGetAllCustomers
            {
                Id = s.Id,
                RoleId = s.RoleId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                PhoneNumber = s.PhoneNumber,
                Email = s.Email,
                Address = s.Address,
                RestaurantName = s.RestaurantName,
                ZipCode = s.ZipCode,
                IsActive = s.IsActive,
                JoinDate = s.JoinDate,
                IsRegister = s.IsRegister,
            }).OrderBy(w => w.JoinDate)
            .ToList();
        }

        public void Edit(Customer customer)
        {
            Update(customer);
            Save();
        }
    }
}

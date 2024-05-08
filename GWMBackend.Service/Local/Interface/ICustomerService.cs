using GWMBackend.Domain.Models;
using static GWMBackend.Domain.DTOs.CustomerDTO;

namespace GWMBackend.Service.Local.Interface
{
    public interface ICustomerService
    {
        List<BOGetAllCustomers> BOGetAllNewCustomers();
        List<BOGetAllCustomers> BOGetAllRegisteredCustomers();
        void Add(Customer customer);
        void Edit(Customer customer);
        Customer GetById(int id);
    }
}

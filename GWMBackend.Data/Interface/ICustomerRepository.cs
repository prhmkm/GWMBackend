using GWMBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GWMBackend.Domain.DTOs.CustomerDTO;

namespace GWMBackend.Data.Interface
{
    public interface ICustomerRepository
    {
        List<BOGetAllCustomers> BOGetAllNewCustomers();
        List<BOGetAllCustomers> BOGetAllRegisteredCustomers();
        void Add(Customer customer);
        void Edit(Customer customer);
        Customer GetById(int id);   

    }
}

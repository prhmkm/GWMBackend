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
    public class CustomerService : ICustomerService
    {
        IRepositoryWrapper _repository;
        public CustomerService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public void Add(Customer customer)
        {
            _repository.customer.Add(customer);
        }

        public List<CustomerDTO.BOGetAllCustomers> BOGetAllNewCustomers()
        {
            return _repository.customer.BOGetAllNewCustomers();
        }

        public List<CustomerDTO.BOGetAllCustomers> BOGetAllRegisteredCustomers()
        {
            return _repository.customer.BOGetAllRegisteredCustomers();
        }

        public void Edit(Customer customer)
        {
            _repository.customer.Edit(customer);
        }

        public Customer GetById(int id)
        {
            return _repository.customer.GetById(id);
        }
    }
}

using System;
using CRMSystem.Application.Interfaces;
using CRMSystem.Domain.Entities;
using CRMSystem.Persistence.DataAccess;

namespace CRMSystem.Persistence.Repositories;

public class CustomerRepository : Repository<Customer> , ICustomerRepository
{
    public CustomerRepository(CRMDB context) : base(context)
    {
    }
}
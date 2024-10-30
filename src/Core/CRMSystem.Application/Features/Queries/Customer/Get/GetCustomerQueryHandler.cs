using CRMSystem.Application.Common.Exceptions;
using CRMSystem.Application.Features.Queries.Customer.ViewModels;
using CRMSystem.Application.Features.Queries.Models;
using CRMSystem.Application.Interfaces;
using MediatR;

namespace CRMSystem.Application.Features.Queries.Customer.Get;

public class GetCustomerQueryHandler(ICustomerRepository customerRepository) : IRequestHandler<GetCustomerQuery, CustomerViewModel>
{
    public async Task<CustomerViewModel> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetAsync(c => c.Id == request.Id);

        if (customer == null)
        {
            throw new NotFoundException("Customers not found.");
        }

        return new CustomerViewModel
        {
            Id = customer.Id,
            Name = customer.Name,
            Surname = customer.Surname,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber
        };
    }
}
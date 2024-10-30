using CRMSystem.Application.Features.Queries.Customer.ViewModels;
using MediatR;

namespace CRMSystem.Application.Features.Queries.Customer.Get;

public class GetCustomerQuery : IRequest<CustomerViewModel>
{
    public int Id { get; set; }
}
using MediatR;
using CRMSystem.Application.Features.Queries.Models;
using CRMSystem.Application.Features.Queries.Customer.ViewModels;

namespace CRMSystem.Application.Features.Queries.Customer.GetAll;

public class GetAllCustomerQuery : IRequest<ReturnItemModel<CustomerViewModel>>
{
    public FilterModel? FilterModel { get; set; }
    public PaginationModel? PaginationModel { get; set; }

}
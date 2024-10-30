using CRMSystem.Application.Common.Exceptions;
using CRMSystem.Application.Features.Queries.Customer.ViewModels;
using CRMSystem.Application.Features.Queries.Models;
using CRMSystem.Application.Interfaces;
using MediatR;

namespace CRMSystem.Application.Features.Queries.Customer.GetAll;

public class GetAllCustomerQueryHandler(ICustomerRepository customerRepository) : IRequestHandler<GetAllCustomerQuery, ReturnItemModel<CustomerViewModel>>
{
    public async Task<ReturnItemModel<CustomerViewModel>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var customers = await customerRepository.GetAllAsync();

        // Filter the results
        customers = ApplyFilter(customers, request.FilterModel);

        int count = customers.Count();

        // Apply pagination logic
        customers = ApplyPagination(customers, request.PaginationModel);

        if (customers == null)
        {
            throw new NotFoundException("Customers not found.");
        }

        var viewModels = new List<CustomerViewModel>();

        foreach (var customer in customers)
        {
            var viewModel = new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };

            viewModels.Add(viewModel);
        }

        return new ReturnItemModel<CustomerViewModel>
        {
            Items = viewModels,
            Count = count
        };
    }
    // Filtration Logic
    private IEnumerable<Domain.Entities.Customer> ApplyFilter(IEnumerable<Domain.Entities.Customer> customers, FilterModel? filter)
    {
        if (filter == null) return customers;

        if (filter.CreatedAfter.HasValue)
        {
            customers = customers.Where(fp => fp.CreateDate >= filter.CreatedAfter.Value);
        }
        if (filter.CreatedBefore.HasValue)
        {
            customers = customers.Where(fp => fp.CreateDate <= filter.CreatedBefore.Value);
        }

        if (string.IsNullOrEmpty(filter.SearchTerm)) return customers;

        filter.SearchTerm = filter.SearchTerm.ToLower();
        customers = customers.Where(c => c.Name.ToLower().Contains(filter.SearchTerm) || 
                                    c.Surname.ToLower().Contains(filter.SearchTerm) || 
                                    c.Email.ToLower().Contains(filter.SearchTerm) || 
                                    c.PhoneNumber.ToLower().Contains(filter.SearchTerm) );

        return customers;
    }

    // Pagination Logic
    private IEnumerable<Domain.Entities.Customer> ApplyPagination(IEnumerable<Domain.Entities.Customer> customers, PaginationModel? paginationModel)
    {
        if (paginationModel == null || (paginationModel.PageSize == 0 && paginationModel.PageNumber == 0)) return customers;

        return customers.Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize).Take(paginationModel.PageSize);
    }
}
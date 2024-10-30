using CRMSystem.Application.Interfaces;
using CRMSystem.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Commands.Customer.Create
{
    public class CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUserManager userManager) : IRequestHandler<CreateCustomerCommand, bool>
    {

        public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var userId = userManager.GetCurrentUserId();

            Domain.Entities.Customer customer = new();

            customer.SetDetails(request.PhoneNumber, request.Email, request.Name, request.Surname)
                .SetCredentials(userId);


            await customerRepository.AddAsync(customer);
            await customerRepository.SaveAsync();

            return true;
        }
    }
}
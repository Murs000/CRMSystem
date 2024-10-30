using CRMSystem.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Commands.Customer.Update
{
    public class UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUserManager userManager) : IRequestHandler<UpdateCustomerCommand, bool>
    {
        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var userId = userManager.GetCurrentUserId();
            var customer = await customerRepository.GetAsync(c => c.Id == request.Id);

            customer.SetDetails(request.PhoneNumber, request.Email, request.Name, request.Surname)
                .SetCredentials(userId);

            customerRepository.Update(customer);

            await customerRepository.SaveAsync();

            return true;
        }
    }
}
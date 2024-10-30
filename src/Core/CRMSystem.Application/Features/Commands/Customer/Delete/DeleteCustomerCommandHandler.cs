using CRMSystem.Application.Common.Exceptions;
using CRMSystem.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Commands.Customer.Delete
{
    public class DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUserManager userManager) : IRequestHandler<DeleteCustomerCommand, bool>
    {
        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var userId = userManager.GetCurrentUserId();

            var customer = await customerRepository.GetAsync(c => c.Id == request.Id);
            if (customer == null)
            {
                throw new NotFoundException();
            }
            customer.SetCredentials(userId);

            customerRepository.SoftDelete(customer);
            await customerRepository.SaveAsync();

            return true;
        }
    }
}
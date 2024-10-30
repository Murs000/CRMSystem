using MediatR;

namespace CRMSystem.Application.Features.Commands.Customer.Delete
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
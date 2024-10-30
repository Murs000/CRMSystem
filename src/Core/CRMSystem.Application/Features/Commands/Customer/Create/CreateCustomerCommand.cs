using MediatR;

namespace CRMSystem.Application.Features.Commands.Customer.Create
{
    public class CreateCustomerCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
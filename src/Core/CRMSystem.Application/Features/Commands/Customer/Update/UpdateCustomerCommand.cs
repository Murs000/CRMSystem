using MediatR;

namespace CRMSystem.Application.Features.Commands.Customer.Update
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
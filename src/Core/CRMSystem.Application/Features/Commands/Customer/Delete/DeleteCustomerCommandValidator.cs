using System;
using FluentValidation;

namespace CRMSystem.Application.Features.Commands.Customer.Delete;

public class DeleteCustomerCommandValidator: AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

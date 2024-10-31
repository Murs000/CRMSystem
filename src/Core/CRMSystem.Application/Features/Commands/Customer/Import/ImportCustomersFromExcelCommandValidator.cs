using System;
using FluentValidation;

namespace CRMSystem.Application.Features.Commands.Customer.Import;

public class ImportCustomersFromExcelCommandValidator : AbstractValidator<ImportCustomersFromExcelCommand>
{
    public ImportCustomersFromExcelCommandValidator()
    {
        RuleFor(x => x.ExcelFile).NotEmpty();
    }
}
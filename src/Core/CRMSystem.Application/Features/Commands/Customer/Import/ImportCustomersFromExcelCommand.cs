using MediatR;

namespace CRMSystem.Application.Features.Commands.Customer.Import
{
    public class ImportCustomersFromExcelCommand : IRequest<bool>
    {
        public byte[] ExcelFile { get; set; }
    }
}
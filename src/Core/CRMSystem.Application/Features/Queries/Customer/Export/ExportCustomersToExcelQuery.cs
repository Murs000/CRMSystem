using CRMSystem.Application.Features.Queries.Models;
using MediatR;

namespace CRMSystem.Application.Features.Queries.Customer.Export
{
    public class ExportCustomersToExcelQuery : IRequest<ReturnDocumentModel>
    {
        public FilterModel? FilterModel { get; set; }
        public PaginationModel? PaginationModel { get; set; }
    }
}
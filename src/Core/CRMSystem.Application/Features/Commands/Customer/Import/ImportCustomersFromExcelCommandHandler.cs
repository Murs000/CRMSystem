using ClosedXML.Excel;
using CRMSystem.Application.Interfaces;
using CRMSystem.Domain.Entities;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Commands.Customer.Import
{
    public class ImportCustomersFromExcelCommandHandler(ICustomerRepository customerRepository) : IRequestHandler<ImportCustomersFromExcelCommand, bool>
    {
        public async Task<bool> Handle(ImportCustomersFromExcelCommand request, CancellationToken cancellationToken)
        {
            using var stream = new MemoryStream(request.ExcelFile);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet("Customers");

            // Skipping the header row

            foreach (var row in worksheet.RowsUsed())
            {
                // Check if the row contains data in the expected columns
                if (string.IsNullOrWhiteSpace(row.Cell(2).GetString()) &&
                    string.IsNullOrWhiteSpace(row.Cell(3).GetString()) &&
                    string.IsNullOrWhiteSpace(row.Cell(4).GetString()) &&
                    string.IsNullOrWhiteSpace(row.Cell(5).GetString()))
                {
                    continue; // Skip empty rows
                }
                var customer = new Domain.Entities.Customer
                {
                    Name = row.Cell(2).GetString(),
                    Surname = row.Cell(3).GetString(),
                    Email = row.Cell(4).GetString(),
                    PhoneNumber = row.Cell(5).GetString()
                };

                await customerRepository.AddAsync(customer);
            }

            await customerRepository.SaveAsync();
            return true;
        }
    }
}
using ClosedXML.Excel;
using CRMSystem.Application.Features.Queries.Models;
using CRMSystem.Application.Interfaces;
using CRMSystem.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Queries.Customer.Export
{
    public class ExportCustomersToExcelQueryHandler(ICustomerRepository customerRepository) : IRequestHandler<ExportCustomersToExcelQuery, ReturnDocumentModel>
    {
        public async Task<ReturnDocumentModel> Handle(ExportCustomersToExcelQuery request, CancellationToken cancellationToken)
        {
            var customers = await customerRepository.GetAllAsync();

            // Filter the results
            customers = ApplyFilter(customers, request.FilterModel);

            int count = customers.Count();

            // Apply pagination logic
            customers = ApplyPagination(customers, request.PaginationModel);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Customers");

            // Add header row
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "Surname";
            worksheet.Cell(1, 4).Value = "Email";
            worksheet.Cell(1, 5).Value = "PhoneNumber";

            // Populate data rows
            int row = 2;
            foreach (var customer in customers)
            {
                worksheet.Cell(row, 1).Value = customer.Id;
                worksheet.Cell(row, 2).Value = customer.Name;
                worksheet.Cell(row, 3).Value = customer.Surname;
                worksheet.Cell(row, 4).Value = customer.Email;
                worksheet.Cell(row, 5).Value = customer.PhoneNumber;
                row++;
            }
            worksheet.Cell(row, 1).Value = $"count{count}";

            byte[] fileData = [];

            // Save to memory stream and return byte array
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                fileData = stream.ToArray();
            }

            return new ReturnDocumentModel
            {
                Name = "Customers",
                Bytes = fileData,
                Type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
        // Filtration Logic
        private IEnumerable<Domain.Entities.Customer> ApplyFilter(IEnumerable<Domain.Entities.Customer> customers, FilterModel? filter)
        {
            if (filter == null) return customers;

            if (filter.CreatedAfter.HasValue)
            {
                customers = customers.Where(fp => fp.CreateDate >= filter.CreatedAfter.Value);
            }
            if (filter.CreatedBefore.HasValue)
            {
                customers = customers.Where(fp => fp.CreateDate <= filter.CreatedBefore.Value);
            }

            if (string.IsNullOrEmpty(filter.SearchTerm)) return customers;

            filter.SearchTerm = filter.SearchTerm.ToLower();
            customers = customers.Where(c => c.Name.ToLower().Contains(filter.SearchTerm) ||
                                        c.Surname.ToLower().Contains(filter.SearchTerm) ||
                                        c.Email.ToLower().Contains(filter.SearchTerm) ||
                                        c.PhoneNumber.ToLower().Contains(filter.SearchTerm));

            return customers;
        }

        // Pagination Logic
        private IEnumerable<Domain.Entities.Customer> ApplyPagination(IEnumerable<Domain.Entities.Customer> customers, PaginationModel? paginationModel)
        {
            if (paginationModel == null || (paginationModel.PageSize == 0 && paginationModel.PageNumber == 0)) return customers;

            return customers.Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize).Take(paginationModel.PageSize);
        }
    }
}
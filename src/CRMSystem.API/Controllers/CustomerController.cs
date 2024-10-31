using CRMSystem.Application.Features.Commands.Customer.Create;
using CRMSystem.Application.Features.Commands.Customer.Delete;
using CRMSystem.Application.Features.Commands.Customer.Import;
using CRMSystem.Application.Features.Commands.Customer.Update;
using CRMSystem.Application.Features.Queries.Customer.Export;
using CRMSystem.Application.Features.Queries.Customer.Get;
using CRMSystem.Application.Features.Queries.Customer.GetAll;
using CRMSystem.Application.Features.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController(IMediator mediator) : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationModel? paginationModel, [FromQuery] FilterModel? filterModel)
        {
            return Ok(await mediator.Send(new GetAllCustomerQuery{ FilterModel = filterModel, PaginationModel = paginationModel}));
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            return Ok(await mediator.Send(new GetCustomerQuery { Id = id }));
        }

        [HttpPost("create")]
        public async Task<IActionResult> GetCurrentUser(CreateCustomerCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UpdateCustomerCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> UpdateUser(int id)
        {
            return Ok(await mediator.Send(new DeleteCustomerCommand { Id = id }));
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportCustomersToExcel([FromQuery] PaginationModel? paginationModel, [FromQuery] FilterModel? filterModel)
        {
            var result = await mediator.Send(new ExportCustomersToExcelQuery{ FilterModel = filterModel, PaginationModel = paginationModel});

            // Return the Excel file as a downloadable file
            return File(result.Bytes, result.Type, result.Name);
        }
        [HttpPost("import")]
        public async Task<IActionResult> ImportCustomersFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var command = new ImportCustomersFromExcelCommand{ ExcelFile = stream.ToArray()};

            return Ok(await mediator.Send(command));
        }
    }
}

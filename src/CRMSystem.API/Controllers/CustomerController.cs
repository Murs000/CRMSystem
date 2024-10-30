using CRMSystem.Application.Features.Commands.Customer.Create;
using CRMSystem.Application.Features.Commands.Customer.Delete;
using CRMSystem.Application.Features.Commands.Customer.Update;
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

        [HttpGet("create")]
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
    }
}

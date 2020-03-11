using CQRSApp.Application.Commands;
using CQRSApp.Application.Queries.Department;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSApp.Api.Controllers
{
    [Route("api/department")]
    public class DepartmentController: Controller
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add-department")]
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody]CreateDepartmentCommand department)
        {
            var command = await _mediator.Send(department);
            return Ok(command);
        }


        [HttpGet("get-department/{id}")]
        public async Task<IActionResult> GetDepartment(Guid id)
        {
            var query = await _mediator.Send(new GetDepartmentQuery(id));
            if (query == null)
                return NotFound();
            return Ok(query);
        }


        [HttpGet("get-departments")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _mediator.Send(new GetAllDepartmentQuery());
            return Ok(departments);
        }


        [HttpDelete("Remove-department/{id}")]
        public async Task<IActionResult> RemoveDepartment(Guid id)
        {
            var query = await _mediator.Send(new DeleteDepartmentCommand(id));
            return Ok(query);
        }

    }
}

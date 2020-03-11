using CQRSApp.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSApp.Api.Controllers
{
    [Route("api/course")]
    public class CourseController: Controller
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add-course")]
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody]CreateCourseCommand course)
        {
            var command = await _mediator.Send(course);
            return Ok(command);
        }


        [Route("add-value")]
        [HttpPost]
        public IActionResult AddValue(int id, string name)
        {
            return Ok(name);
        }
        // GET api/values
        [HttpGet("values")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}

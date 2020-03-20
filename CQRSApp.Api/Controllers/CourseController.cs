using CQRSApp.Application.Commands;
using CQRSApp.Application.Course.Queries;
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


        [HttpGet("get-courset")]
        public async Task<IActionResult> GetDepartment(GetAllCoursesQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}

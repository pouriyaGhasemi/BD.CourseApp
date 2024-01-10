using Asp.Versioning;
using BD.CourseApp.Core.ApplicationService.AssignCourses;
using BD.CourseApp.Core.Domain.AssignedCourses.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BD.CourseApp.Endpoint.Api.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/assigncourse")]
    public class AssignCourseController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> AssinCourse([FromServices] CreateAssignCourseHandler handler ,[FromBody] AssignedCourse assignedCourse)
        {
            await handler.Handle(assignedCourse);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> UnAssinCourse([FromServices] DeleteAssignCourseHandler handler, [FromBody] AssignedCourse assignedCourse)
        {
            await handler.Handle(assignedCourse);
            return Ok();
        }
    }
}

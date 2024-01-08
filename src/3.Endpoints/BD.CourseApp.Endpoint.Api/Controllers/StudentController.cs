using Asp.Versioning;
using BD.CourseApp.Core.ApplicationService.Students;
using BD.CourseApp.Core.Domain.Students.DTOS;
using BD.CourseApp.Core.Domain.Students.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BD.CourseApp.Endpoint.Api.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/students")]

    public class StudentsController : ControllerBase
    {
        [HttpGet("{Id}")]
        public async Task<ActionResult<Student>> Get([FromServices] GetStudentHandler request, [FromRoute] string Id) {
            var result= await request.Handle(Id);
            if (result is null)
                return NotFound();
            return  result;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromServices] StudentCreateHandler createStudentHandler, StudentCreateDTO studentCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await createStudentHandler.Handle(studentCreate);
            return Ok();
        }
    }
}

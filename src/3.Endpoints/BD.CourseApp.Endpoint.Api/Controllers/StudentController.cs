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
        public async Task<ActionResult<StudentOutDTO>> Get([FromServices] GetStudentHandler request, [FromRoute] string Id) {
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentOutDTO>>> GetAll([FromServices] GetAllCreateHandler getAllCreateHandler
            , [FromQuery]string? name, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok();
        }
    }
}

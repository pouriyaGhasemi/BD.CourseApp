using Asp.Versioning;
using BD.CourseApp.Core.ApplicationService.Students;
using BD.CourseApp.Core.Domain.Students.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace BD.CourseApp.Endpoint.Api.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/students")]

    public class StudentsController : ControllerBase
    {
        [HttpGet("{Id}")]
        public async Task<ActionResult<StudentOutDTO>> Get([FromServices] GetStudentHandler getStudentHandler, [FromRoute] Guid Id) {
            var result= await getStudentHandler.Handle(Id);
            if (result is null)
                return NotFound();
            return  result;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromServices] CreateStudentHandler createStudentHandler, StudentCreateDTO studentCreate)
        {
            //ToDo: dont get GUID from client and create it in service and send it as response.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await createStudentHandler.Handle(studentCreate);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentOutDTO>>> GetAll([FromServices] GetAllStudentsHandler getAllCreateHandler
            , [FromQuery]string? name, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            //ToDo:Create generic paged Object for all paged list.
            var result = await getAllCreateHandler.Handle(name, pageNumber,pageSize);
            if (result is null)
                return NoContent();
            return result.ToList();
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromServices] UpdateStudentHandler updateStudentHandler, StudentUpdateDTO studentCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await updateStudentHandler.Handle(studentCreate);
            return Ok();
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete([FromServices] DeleteStudentHandler deleteStudentHandler, [FromRoute] Guid Id)
        {
            await deleteStudentHandler.Handle(Id);
            return Ok();
        }
    }
}

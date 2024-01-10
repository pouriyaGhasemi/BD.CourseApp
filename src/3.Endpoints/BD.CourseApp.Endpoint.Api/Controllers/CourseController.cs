using Asp.Versioning;
using BD.CourseApp.Core.ApplicationService.Courses;
using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Courses.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BD.CourseApp.Endpoint.Api.Controllers
{


    namespace BD.CourseApp.Endpoint.Api.Controllers
    {
        [ApiController]
        [ApiVersion(1)]
        [Route("api/v{version:apiVersion}/courses")]
        public class CoursesController : ControllerBase
        {
            private readonly ICourseRepository _courseRepository;

            public CoursesController(ICourseRepository courseRepository)
            {
                _courseRepository = courseRepository;
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<CourseOutDTO>> Get([FromServices]GetCourseHandler handler,Guid id)
            {
                var course = await handler.Handle(id);
                if (course == null)
                    return NotFound();
                return Ok(course);
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<CourseOutDTO>>> GetAll([FromServices]GetAllCoursesHandler handler)
            {
                var courses = await handler.Handle();
                return Ok(courses);
            }

            [HttpPost]
            public async Task<ActionResult> Create([FromServices] CreateCourseHandler handler ,[FromBody] CourseCreateDTO course)
            {
                //ToDo: dont get GUID from client and create it in service and send it as response.
                await handler.Handle(course);
                return Ok();
            }

            [HttpPut]
            public async Task<ActionResult> Update([FromServices] UpdateCourseHandler handler ,[FromBody] CourseUpdateDTO course)
            {
                await handler.Handle(course);
                return Ok();
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete([FromServices] DeleteCourseHandler handler ,Guid id)
            {
                await handler.Handle(id);
                return Ok();
            }
        }
    }

}

using Asp.Versioning;
using BD.CourseApp.Core.ApplicationService.Courses;
using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Courses.DTOs;
using BD.CourseApp.Core.Domain.Students.DTOS;
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

            [HttpGet("{id}")]
            public async Task<ActionResult<CourseOutDTO>> Get([FromServices]GetCourseHandler handler, [FromRoute] Guid id)
            {
                var course = await handler.Handle(id);
                if (course == null)
                    return NotFound();
                return Ok(course);
            }
            [HttpGet("{id}/students")]
            public async Task<ActionResult<IEnumerable< StudentOutDTO>>> GetStudents([FromServices] GetStudentsOfCourseHandler handler,[FromRoute] Guid id)
            {
                var students = await handler.Handle(id);
                if (students == null)
                    return NoContent();
                return Ok(students);
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<CourseOutDTO>>> GetAll([FromServices]GetAllCoursesHandler handler)
            {
                var courses = await handler.Handle();
                if (courses is null)
                    return NoContent();
                return Ok(courses);
            }

            [HttpPost]
            public async Task<ActionResult<Guid>> Create([FromServices] CreateCourseHandler handler ,[FromBody] CourseCreateDTO course)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var courseId= await handler.Handle(course);
                return courseId;
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

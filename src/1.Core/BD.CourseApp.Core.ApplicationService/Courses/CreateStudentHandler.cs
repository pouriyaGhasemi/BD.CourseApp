using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Courses.DTOs;

namespace BD.CourseApp.Core.ApplicationService.Courses
{
    public class CreateCourseHandler
    {
        private readonly ICourseRepository _CourseRepository;
        public CreateCourseHandler(ICourseRepository CourseRepository)
        {
            _CourseRepository = CourseRepository;
        }
        public async Task<Guid> Handle(CourseCreateDTO courseCreate)
        {
            Guid courseId = Guid.NewGuid();
            await _CourseRepository.CreateAsync(courseCreate, courseId);
            return courseId;
        }
    }
}

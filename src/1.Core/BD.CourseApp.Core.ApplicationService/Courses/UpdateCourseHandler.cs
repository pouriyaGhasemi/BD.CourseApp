using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Courses.DTOs;

namespace BD.CourseApp.Core.ApplicationService.Courses
{
    public class UpdateCourseHandler
    {
        private readonly ICourseRepository _CourseRepository;
        public UpdateCourseHandler(ICourseRepository CourseRepository)
        {
            _CourseRepository = CourseRepository;
        }
        public async Task Handle(CourseUpdateDTO CourseUpdate)
        {
            var Course=await _CourseRepository.GetByIdAsync(CourseUpdate.CourseId);
            if (Course is null)
                throw new KeyNotFoundException($"{nameof(Course)} ID:{CourseUpdate.CourseId}");
            await _CourseRepository.UpdateAsync(CourseUpdate);
        }
    }
}

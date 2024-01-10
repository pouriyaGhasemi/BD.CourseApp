using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Students.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.CourseApp.Core.ApplicationService.Courses
{
    public class GetStudentsOfCourseHandler
    {
        private readonly ICourseRepository _courseRepository;
        public GetStudentsOfCourseHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;

        }
        public async Task<IEnumerable<StudentOutDTO>> Handle(Guid courseId)
        {
            var Course = await _courseRepository.GetByIdAsync(courseId);
            if (Course is null)
                throw new KeyNotFoundException($"{nameof(Course)} ID:{courseId}");
            return await _courseRepository.GetStudentsByCourseId(courseId);


        }
    }
}

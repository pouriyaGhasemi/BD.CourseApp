using BD.CourseApp.Core.Domain.AssignedCourses.Contracts;
using BD.CourseApp.Core.Domain.AssignedCourses.Entities;
using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Students.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.CourseApp.Core.ApplicationService.AssignCourses
{
    public class CreateAssignCourseHandler
    {
        private readonly IAssignedCourseRepository _assignedCourseRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public CreateAssignCourseHandler(IAssignedCourseRepository assignedCourseRepository
            , ICourseRepository CourseRepository
            , IStudentRepository studentRepository)
        {
            _assignedCourseRepository = assignedCourseRepository;
            _courseRepository = CourseRepository;
            _studentRepository = studentRepository;
        }
        public async Task Handle(AssignedCourse assignedCourse)
        {
            var course = await _courseRepository.GetByIdAsync(assignedCourse.CourseId);
            if (course is null) throw new KeyNotFoundException($"{nameof(course)} not found. id:{assignedCourse.CourseId} ");
            
            var student = await _studentRepository.GetByIdAsync(assignedCourse.StudentId);
            if (student is null) throw new KeyNotFoundException($"{nameof(student)} not found. id:{assignedCourse.StudentId} ");
            
            await _assignedCourseRepository.Create(assignedCourse.CourseId, assignedCourse.StudentId);
        }
    }
}

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
    public class DeleteAssignCourseHandler
    {
        private readonly IAssignedCourseRepository _assignedCourseRepository;

        public DeleteAssignCourseHandler(IAssignedCourseRepository assignedCourseRepository)
        {
            _assignedCourseRepository = assignedCourseRepository;
        }
        public async Task Handle(AssignedCourse assignedCourse)
        {
            var assignedCourseExist = await _assignedCourseRepository.GetByIdAsync(assignedCourse.CourseId, assignedCourse.StudentId);
            if (assignedCourseExist is null) throw new KeyNotFoundException($"{nameof(assignedCourse)} not found. courseId:{assignedCourse.CourseId} studentId:{assignedCourse.StudentId}");
            await _assignedCourseRepository.Delete(assignedCourse.CourseId, assignedCourse.StudentId);
        }
    }
}

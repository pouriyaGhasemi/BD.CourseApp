using System.Collections.Frozen;
using BD.CourseApp.Core.Domain.Courses;

namespace BD.CourseApp.Core.Domain.Students
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string? Name { get; set; }
        public FrozenSet<Course>? Courses { get; set; }
    }
}

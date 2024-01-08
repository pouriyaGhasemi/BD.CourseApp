using System.Collections.Frozen;
using BD.CourseApp.Core.Domain.Students.Entities;

namespace BD.CourseApp.Core.Domain.Courses
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string? Title { get; set; }
        public int CategoryId { get; set; }
        public FrozenSet<Student>? Students { get; set; }
    }
}

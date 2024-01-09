using System.Collections.Frozen;
using BD.CourseApp.Core.Domain.Categories.Entities;
using BD.CourseApp.Core.Domain.Students.Entities;

namespace BD.CourseApp.Core.Domain.Courses.Entites
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string? Title { get; set; }
        public Category Category { get; set; }
        public FrozenSet<Student>? Students { get; set; }
    }
}

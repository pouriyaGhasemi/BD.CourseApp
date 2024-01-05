using System.Collections.Frozen;

namespace BD.CourseApp.Core.Domain.Entities
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string? Name { get; set; }
        public FrozenSet<Course>? Students { get; set; }
    }
}

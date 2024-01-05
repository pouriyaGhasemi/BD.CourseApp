using System.Collections.Frozen;

namespace BD.CourseApp.Core.Domain.Entities
{
    public class Course
    {
        public long CourseId { get; set; }
        public string? Title { get; set; }
        public int CategoryId { get; set; }
        public FrozenSet<Student>? Students { get; set; }
    }
}

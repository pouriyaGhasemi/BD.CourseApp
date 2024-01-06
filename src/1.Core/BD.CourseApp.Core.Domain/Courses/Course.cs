using System.Collections.Frozen;
using BD.CourseApp.Core.Domain.Students;

namespace BD.CourseApp.Core.Domain.Courses
{
    public class Course
    {
        public long CourseId { get; set; }
        public string? Title { get; set; }
        public int CategoryId { get; set; }
        public FrozenSet<Student>? Students { get; set; }
    }
}

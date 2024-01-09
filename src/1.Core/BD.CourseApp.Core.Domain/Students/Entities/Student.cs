using System.Collections.Frozen;
using System.ComponentModel.DataAnnotations;
using BD.CourseApp.Core.Domain.Courses.Entites;

namespace BD.CourseApp.Core.Domain.Students.Entities
{
    public class Student
    {
        public Guid StudentId { get; set; }
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }
        public FrozenSet<Course>? Courses { get; set; }
    }
}

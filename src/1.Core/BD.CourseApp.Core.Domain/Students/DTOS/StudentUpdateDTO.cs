using BD.CourseApp.Core.Domain.Students.Entities;

namespace BD.CourseApp.Core.Domain.Students.DTOS
{
    public class StudentUpdateDTO
    {
        public Guid StudentId { get; set; }
        public string? Name { get; set; }
    }
}

using BD.CourseApp.Core.Domain.Categories.Entities;

namespace BD.CourseApp.Core.Domain.Courses.DTOs
{
    public class CourseOutDTO
    {
        public Guid CourseId { get; set; }
        public string? Title { get; set; }
        public required Category Category { get; set; }
    }
}

namespace BD.CourseApp.Core.Domain.Courses.DTOs
{
    public class CourseUpdateDTO
    {
        public Guid CourseId { get; set; }
        public string? Title { get; set; }
        public int CategoryId { get; set; }
    }
}
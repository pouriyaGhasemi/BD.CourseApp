using System.ComponentModel.DataAnnotations;

namespace BD.CourseApp.Core.Domain.Courses.DTOs
{
    public class CourseCreateDTO
    {

        [StringLength(200, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Title { get; set; }
        public int CategoryId { get; set; }
    }
}
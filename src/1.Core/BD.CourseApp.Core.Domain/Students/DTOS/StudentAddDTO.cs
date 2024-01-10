using System.ComponentModel.DataAnnotations;

namespace BD.CourseApp.Core.Domain.Students.DTOS
{
    public record StudentCreateDTO
    {
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }
    }
}

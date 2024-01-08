using System.ComponentModel.DataAnnotations;

namespace BD.CourseApp.Core.Domain.Students.DTOS
{
    public record StudentCreateDTO
    {
        public string? Name { get; set; }
    }
}

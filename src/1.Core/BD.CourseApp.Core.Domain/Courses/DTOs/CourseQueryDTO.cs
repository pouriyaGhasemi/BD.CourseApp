using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.CourseApp.Core.Domain.Courses.DTOs
{
    public class CourseQueryDTO
    {
        public Guid CourseId { get; set; }
        public string? Title { get; set; }
        public int CategoryId { get; set; }
    }
}

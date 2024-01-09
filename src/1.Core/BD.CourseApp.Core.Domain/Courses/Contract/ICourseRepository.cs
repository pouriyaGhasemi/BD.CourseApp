using BD.CourseApp.Core.Domain.Courses.DTOs;
using BD.CourseApp.Core.Domain.Courses.Entites;

namespace BD.CourseApp.Core.Domain.Courses.Contract
{
    public interface ICourseRepository
    {
        Task<Course?> GetByIdAsync(Guid id);
        Task<IEnumerable<CourseOutDTO>> GetAllAsync();
        Task<IEnumerable<CourseOutDTO>> GetByFilterAsync(string name, int categoryId, int? pageNumber, int? pageSize);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Guid id);
        Task AssignCourse(Guid CourseId, Guid StudentId);
        Task UnAssignCourse(Guid CourseId, Guid StudentId);
        Task UnAssignAllCourses( Guid StudentId);
    }
}

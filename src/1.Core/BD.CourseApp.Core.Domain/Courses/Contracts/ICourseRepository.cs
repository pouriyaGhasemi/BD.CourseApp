using BD.CourseApp.Core.Domain.Courses.DTOs;
using BD.CourseApp.Core.Domain.Courses.Entites;

namespace BD.CourseApp.Core.Domain.Courses.Contracts
{
    public interface ICourseRepository
    {
        Task<CourseQueryDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<CourseQueryDTO>> GetAllAsync();
        Task<IEnumerable<CourseQueryDTO>> GetByCategoryIdAsync(int categoryId);
        Task CreateAsync(CourseCreateDTO course);
        Task UpdateAsync(CourseUpdateDTO course);
        Task DeleteAsync(Guid id);
    }
}

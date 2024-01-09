using BD.CourseApp.Core.Domain.Students.DTOS;
using BD.CourseApp.Core.Domain.Students.Entities;

namespace BD.CourseApp.Core.Domain.Students.Contracts
{
    public interface IStudentRepository
    {
        Task CreateAsync(Student student);
        Task<Student?> GetByIdAsync(Guid id);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<StudentOutDTO>> GetAllAsync(string? nameFilter, int? pageNumber, int? pageSize);
    }
}

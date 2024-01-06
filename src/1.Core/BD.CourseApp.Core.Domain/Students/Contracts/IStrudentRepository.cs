using BD.CourseApp.Core.Domain.Students;

namespace BD.CourseApp.Core.Domain.Students.Contracts
{
    public interface IStudentRepository
    {
        Task CreateAsync(Student student);
        Task<Student> GetByIdAsync(Guid id);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Student>> GetAllAsync(string nameFilter, int pageNumber, int pageSize);
    }
}

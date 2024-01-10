using BD.CourseApp.Core.Domain.AssignedCourses.Entities;

namespace BD.CourseApp.Core.Domain.AssignedCourses.Contracts
{
    public interface IAssignedCourseRepository
    {
        Task<AssignedCourse?> GetByIdAsync(Guid courseId, Guid studentId);
        Task Create(Guid courseId, Guid studentId);
        Task Delete(Guid courseId, Guid studentId);
        Task DeleteAllByStudentId(Guid studentId);
        Task DeleteAllByCourseId(Guid courseId);
    }
}

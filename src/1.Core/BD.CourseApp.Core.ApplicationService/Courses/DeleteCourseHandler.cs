using BD.CourseApp.Core.Domain.AssignedCourses.Contracts;
using BD.CourseApp.Core.Domain.Courses.Contracts;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;

namespace BD.CourseApp.Core.ApplicationService.Courses
{
    public class DeleteCourseHandler
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAssignedCourseRepository _assignedCourseRepository;

        public DeleteCourseHandler(ICourseRepository courseRepository, IAssignedCourseRepository assignedCourseRepository)
        {
            _courseRepository = courseRepository;
            _assignedCourseRepository = assignedCourseRepository;

        }
        public async Task Handle(Guid id)
        {

            var Course = await _courseRepository.GetByIdAsync(id);
            if (Course is null)
                return;
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _assignedCourseRepository.DeleteAllByCourseId(id);
            await _courseRepository.DeleteAsync(id);
            scope.Complete();

        }
    }
}

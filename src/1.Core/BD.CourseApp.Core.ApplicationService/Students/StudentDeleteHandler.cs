using BD.CourseApp.Core.Domain.AssignedCourses.Contracts;
using BD.CourseApp.Core.Domain.Students.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BD.CourseApp.Core.ApplicationService.Students
{
    public class DeleteStudentHandler
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAssignedCourseRepository _assignedCourseRepository;

        public DeleteStudentHandler(IStudentRepository studentRepository, IAssignedCourseRepository assignedCourseRepository)
        {
            _studentRepository = studentRepository;
            _assignedCourseRepository = assignedCourseRepository;
        }
        public async Task Handle(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student is null)
                return;
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _assignedCourseRepository.DeleteAllByStudentId(id);
            await _studentRepository.DeleteAsync(id);
            scope.Complete();
        }
    }
}

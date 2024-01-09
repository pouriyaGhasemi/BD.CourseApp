using BD.CourseApp.Core.Domain.Students.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.CourseApp.Core.ApplicationService.Students
{
    public class DeleteStudentHandler
    {
        private readonly IStudentRepository _studentRepository;

        public DeleteStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task Handle(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student is null)
                return;
            await _studentRepository.DeleteAsync(id);
        }
    }
}

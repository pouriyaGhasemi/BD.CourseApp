using BD.CourseApp.Core.Domain.Students.Contracts;
using BD.CourseApp.Core.Domain.Students.DTOS;
using BD.CourseApp.Core.Domain.Students.Entities;

namespace BD.CourseApp.Core.ApplicationService.Students
{
    public class StudentCreateHandler
    {
        private readonly IStudentRepository _studentRepository;
        public StudentCreateHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task Handle(StudentCreateDTO studentCreate)
        {
            var student = new Student() { StudentId = Guid.NewGuid(), Name = studentCreate.Name };
            await _studentRepository.CreateAsync(student);
        }
    }
}

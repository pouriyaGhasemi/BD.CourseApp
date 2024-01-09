using BD.CourseApp.Core.Domain.Students.Contracts;
using BD.CourseApp.Core.Domain.Students.DTOS;
using BD.CourseApp.Core.Domain.Students.Entities;

namespace BD.CourseApp.Core.ApplicationService.Students
{
    public class UpdateStudentHandler
    {
        private readonly IStudentRepository _studentRepository;
        public UpdateStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task Handle(StudentUpdateDTO studentUpdate)
        {
            var student=await _studentRepository.GetByIdAsync(studentUpdate.StudentId);
            if (student is null)
                throw new KeyNotFoundException($"{nameof(student)} ID:{studentUpdate.StudentId}");
            await _studentRepository.UpdateAsync(student);
        }
    }
}

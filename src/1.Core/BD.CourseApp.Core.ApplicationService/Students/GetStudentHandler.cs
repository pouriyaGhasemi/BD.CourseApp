using BD.CourseApp.Core.Domain.Students.Contracts;
using BD.CourseApp.Core.Domain.Students.DTOS;

namespace BD.CourseApp.Core.ApplicationService.Students
{
    public class GetStudentHandler
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<StudentOutDTO> Handle(Guid id)
        {
            var student= await _studentRepository.GetByIdAsync(id);
            if (student is null)
                throw new KeyNotFoundException($"Student not found, Id: {id}");
            StudentOutDTO studentOutDTO = new StudentOutDTO() {Name= student.Name,StudentId= student.StudentId };
            return studentOutDTO;
        }
    }
}

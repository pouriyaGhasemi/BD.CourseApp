using BD.CourseApp.Core.Domain.Students.Contracts;
using BD.CourseApp.Core.Domain.Students.Entities;

namespace BD.CourseApp.Core.ApplicationService.Students
{
    public class GetStudentHandler
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<Student> Handle(string Id)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(Id);
            var validId= Guid.TryParse(Id, out Guid studentId);
            if(!validId)
                throw new ArgumentException(nameof(Id)+" is not valid GUID");
            return await _studentRepository.GetByIdAsync(studentId);

        }
    }
}

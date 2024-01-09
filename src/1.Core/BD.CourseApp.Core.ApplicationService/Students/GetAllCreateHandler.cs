using BD.CourseApp.Core.Domain.Students.Contracts;
using BD.CourseApp.Core.Domain.Students.DTOS;

namespace BD.CourseApp.Core.ApplicationService.Students
{
    public class GetAllCreateHandler
    {
        private readonly IStudentRepository _studentRepository;

        public GetAllCreateHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<IEnumerable<StudentOutDTO>> Handle(string? nameFilter, int pageNumber, int pageSize)
        {
            return await _studentRepository.GetAllAsync(nameFilter, pageNumber, pageSize);
        }
    }
}


using BD.CourseApp.Core.Domain.Categories.Contracts;
using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Courses.DTOs;

namespace BD.CourseApp.Core.ApplicationService.Courses
{
    public class CreateCourseHandler
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryService _categoryService;
        public CreateCourseHandler(ICourseRepository courseRepository, ICategoryService categoryService)
        {
            _courseRepository = courseRepository;
            _categoryService = categoryService;
        }
        public async Task<Guid> Handle(CourseCreateDTO courseCreate)
        {
            var category=await _categoryService.GetCategoryById(courseCreate.CategoryId);
            if (category is null) 
                throw new ArgumentException($"Invalid CategoryId, Id:{courseCreate.CategoryId}");
            
            Guid courseId = Guid.NewGuid();
            await _courseRepository.CreateAsync(courseCreate, courseId);
            return courseId;
        }
    }
}

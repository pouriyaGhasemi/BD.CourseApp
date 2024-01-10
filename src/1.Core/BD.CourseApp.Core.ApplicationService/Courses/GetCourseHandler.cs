using BD.CourseApp.Core.Domain.Categories.Contracts;
using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Courses.DTOs;

namespace BD.CourseApp.Core.ApplicationService.Courses
{
    public class GetCourseHandler
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryService _categoryService;

        public GetCourseHandler(ICourseRepository courseRepository, ICategoryService categoryService)
        {
            _courseRepository = courseRepository;
            _categoryService = categoryService;
        }
        public async Task<CourseOutDTO> Handle(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course is null)
                throw new KeyNotFoundException($"course not found, Id: {id}");

            CourseOutDTO courseOutDTO = new CourseOutDTO() { 
                Category = await _categoryService.GetCategoryById(course.CategoryId)
                ,CourseId=course.CourseId
                ,Title=course.Title };

            return courseOutDTO;
        }
    }
}


using BD.CourseApp.Core.Domain.Categories.Contracts;
using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Courses.DTOs;
using BD.CourseApp.Core.Domain.Courses.Entites;
using System.Linq;


namespace BD.CourseApp.Core.ApplicationService.Courses
{
    public class GetAllCoursesHandler
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryService _categoryService;

        public GetAllCoursesHandler(ICourseRepository courseRepository, ICategoryService categoryService)
        {
            _courseRepository = courseRepository;
            _categoryService = categoryService;
        }
        public async Task<IEnumerable<CourseOutDTO>?> Handle()
        {
            var rawCourses = await _courseRepository.GetAllAsync();
            if (rawCourses == null) return null;

            var courseOutDTOs = rawCourses.Select(async course => new CourseOutDTO
            {
                Category = await _categoryService.GetCategoryById(course.CategoryId),
                CourseId = course.CourseId,
                Title = course.Title
            });

            return await Task.WhenAll(courseOutDTOs);
        }
    }
}


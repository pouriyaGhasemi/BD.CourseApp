
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
            List<CourseOutDTO> courseOutDTOs = new List<CourseOutDTO>();
            var rawCourses=await _courseRepository.GetAllAsync();
            if (rawCourses == null)
                return null;
            foreach (var course in rawCourses)
            {
                courseOutDTOs.Add(new CourseOutDTO()
                {
                    Category = await _categoryService.GetCategoryById(course.CategoryId),
                    CourseId = course.CourseId,
                    Title = course.Title
                }); 
            }
          return courseOutDTOs;
        }
    }
}


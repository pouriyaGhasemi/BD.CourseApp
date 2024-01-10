using BD.CourseApp.Core.ApplicationService.Courses;
using BD.CourseApp.Core.Domain.Categories.Contracts;
using BD.CourseApp.Core.Domain.Categories.Entities;
using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Courses.DTOs;
using BD.CourseApp.Core.Domain.Students.Entities;
using Moq;
using System.Transactions;

namespace BD.CourseApp.Tests.Services
{
    public class CourseServiceTests
    {
        private readonly Mock<ICourseRepository> _courseRepositoryMock = new Mock<ICourseRepository>();
        private readonly Mock<ICategoryService> _categoryServiceMock = new Mock<ICategoryService>();
        public CourseServiceTests()
        {
        }
        [Fact]
        public async Task should_output_has_categoryname()
        {
            //Arrange
            Category _category = new Category() { Id = 1, Name = "Programming" };
            CourseQueryDTO _course = new CourseQueryDTO() { CourseId = Guid.NewGuid(), Title = "title Programming", CategoryId = 1 };
            _categoryServiceMock.Setup(s => s.GetCategoryById(_category.Id))
             .Returns(Task.FromResult(_category));
            _courseRepositoryMock.Setup(s => s.GetAllAsync())
                .Returns(Task.FromResult((IEnumerable<CourseQueryDTO>)new List<CourseQueryDTO>() { _course }));
            GetAllCoursesHandler handler = new GetAllCoursesHandler(_courseRepositoryMock.Object, _categoryServiceMock.Object);

            //Act
            //ToDo:This test is not independent. Get size of all records, then add two item and check if the size is +2
            var result =await handler.Handle();
            //Assert
            result.Should().NotBeNull();
            result.First().Category.Name.Should().Be(_category.Name);
            
        }
    }
}

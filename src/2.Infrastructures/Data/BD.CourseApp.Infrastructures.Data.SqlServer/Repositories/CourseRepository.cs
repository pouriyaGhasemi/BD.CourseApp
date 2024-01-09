using BD.CourseApp.Core.Domain.Categories.Entities;
using BD.CourseApp.Core.Domain.Courses.Contract;
using BD.CourseApp.Core.Domain.Courses.DTOs;
using BD.CourseApp.Core.Domain.Courses.Entites;
using BD.CourseApp.Infrastructures.Services.Outbound;
using Dapper;
using System.Data.SqlClient;

namespace BD.CourseApp.Infrastructures.Data.SqlServer.Repositories
{

    public class CourseRepository : ICourseRepository
    {
        private readonly SqlConnection _connection;
        private readonly ICategoryService _categoryService;
        private const int _defaultPageNumber = 1;
        private const int _defaultPageSize = 10;

        public CourseRepository(SqlConnection dbConnection, ICategoryService categoryService)
        {
            _connection = dbConnection;
            _categoryService = categoryService;
        }

        public async Task AddAsync(Course course)
        {
            var sql = "insert into Courses (CourseId, Name, CategoryId) values (@CourseId, @Name, @CategoryId)";
            await _connection.ExecuteAsync(sql, course);
        }

        public async Task<Course?> GetByIdAsync(Guid id)
        {
            var sql = @"
        select c.CourseId, c.Title, cat.CategoryId, cat.Name
        from Courses c
        inner join Categories cat ON c.CategoryId = cat.CategoryId
        where c.CourseId = @CourseId";
            var courses = await _connection.QueryAsync<Course, Category, Course>(
                sql,
                (Course, category) =>
                {
                    Course.Category = category;
                    return Course;
                },
                new { CourseId = id },
                splitOn: "CategoryId"
            );
            if (courses.Count() > 1)
                throw new Exception();
            return courses.FirstOrDefault();
        }


        public async Task UpdateAsync(Course course)
        {
            var sql = "update Courses set Name = @Name, CategoryId = @CategoryId where CourseId = @CourseId";
            await _connection.ExecuteAsync(sql, course);
        }

        public async Task DeleteAsync(Guid id)
        {
            var sql = "delete from Courses where CourseId = @CourseId";
            await _connection.ExecuteAsync(sql, new { CourseId = id });
        }

        public async Task<IEnumerable<CourseOutDTO>> GetAllAsync()
        {
            var sql = @"
            select c.CourseId, c.Title, cat.CategoryId, cat.Name
            from Courses c
            inner join Categories cat ON c.CategoryId = cat.CategoryId";
            var courses = await _connection.QueryAsync<CourseOutDTO, Category, CourseOutDTO>(
                sql,
                (courseDto, category) =>
                {
                    courseDto.Category = category;
                    return courseDto;
                },
                splitOn: "CategoryId"
            );
            return courses;
        }


        public async Task<IEnumerable<CourseOutDTO>> GetByFilterAsync(string name, int categoryId, int? pageNumber, int? pageSize)
        {
            pageNumber ??= _defaultPageNumber;
            pageSize ??= _defaultPageSize;
            var sql = @"
        select c.CourseId, c.Title, cat.CategoryId, cat.Name
        from Courses c
        inner join Categories cat ON c.CategoryId = cat.CategoryId
        where (@NameFilter IS NULL OR c.Title LIKE '%' + @NameFilter + '%')
        AND (@CategoryIdFilter IS NULL OR c.CategoryId = @CategoryIdFilter)
        order by c.Title
        offset @PageSize * (@PageNumber - 1) rows
        fetch next @PageSize rows only";
            var courses = await _connection.QueryAsync<CourseOutDTO, Category, CourseOutDTO>(
                sql,
                (courseDto, category) =>
                {
                    courseDto.Category = category;
                    return courseDto;
                },
                new { NameFilter = name, CategoryIdFilter = categoryId, PageNumber = pageNumber, PageSize = pageSize },
                splitOn: "CategoryId"
            );
            return courses;
        }

        public async Task AssignCourse(Guid courseId, Guid studentId)
        {
            var sql = "insert into StudentCourses (CourseId, StudentId) values (@CourseId, @StudentId)";
            await _connection.ExecuteAsync(sql, new { CourseId = courseId, StudentId = studentId });
        }
        public async Task UnAssignCourse(Guid courseId, Guid studentId)
        {
            var sql = "delete from StudentCourses where CourseId = @CourseId AND StudentId = @StudentId";
            await _connection.ExecuteAsync(sql, new { CourseId = courseId, StudentId = studentId });
        }

        public async Task UnAssignAllCourses(Guid studentId)
        {
            var sql = "delete from StudentCourses where StudentId = @StudentId";
            await _connection.ExecuteAsync(sql, new { StudentId = studentId });
        }
    }
}


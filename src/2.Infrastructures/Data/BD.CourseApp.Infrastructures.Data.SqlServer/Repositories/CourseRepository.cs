using BD.CourseApp.Core.Domain.Categories.Contracts;
using BD.CourseApp.Core.Domain.Categories.Entities;
using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Courses.DTOs;
using BD.CourseApp.Core.Domain.Courses.Entites;
using Dapper;
using System.Data.SqlClient;

namespace BD.CourseApp.Infrastructures.Data.SqlServer.Repositories
{

    public class CourseRepository : ICourseRepository
    {
        private readonly SqlConnection _connection;
        private const int _defaultPageNumber = 1;
        private const int _defaultPageSize = 10;

        public CourseRepository(SqlConnection dbConnection)
        {
            _connection = dbConnection;
        }

        public async Task CreateAsync(CourseCreateDTO course)
        {
            var sql = "insert into Courses (CourseId, Title, CategoryId) values (@CourseId, @Title, @CategoryId)";
            await _connection.ExecuteAsync(sql, course);
        }

        public async Task<CourseQueryDTO?> GetByIdAsync(Guid id)
        {
            var sql = @" select CourseId, Title, CategoryId from Courses 
            where CourseId=@CourseId";
            return await _connection.QuerySingleOrDefaultAsync<CourseQueryDTO>(sql,new { CourseId = id });
        }


        public async Task UpdateAsync(CourseUpdateDTO course)
        {
            var sql = "update Courses set Title = @Title, CategoryId = @CategoryId where CourseId = @CourseId";
            await _connection.ExecuteAsync(sql, course);
        }

        public async Task DeleteAsync(Guid id)
        {

            var sql = "delete from Courses where CourseId = @CourseId";
            await _connection.ExecuteAsync(sql, new { CourseId = id });
        }

        public async Task<IEnumerable<CourseQueryDTO>> GetAllAsync()
        {
            var courses = await _connection.QueryAsync<CourseQueryDTO>("select CourseId, Title, CategoryId from Courses ");
            return courses;
        }

        public async Task<IEnumerable<CourseQueryDTO>> GetByCategoryIdAsync(int categoryId)
        {
            var courses = await _connection.QueryAsync<CourseQueryDTO>("select CourseId, Title, CategoryId from Courses where CategoryId = @CategoryId "
                , new { CategoryId = categoryId });
            return courses;
        }
    }
}


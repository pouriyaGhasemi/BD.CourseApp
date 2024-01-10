using BD.CourseApp.Core.Domain.AssignedCourses.Contracts;
using BD.CourseApp.Core.Domain.AssignedCourses.Entities;
using BD.CourseApp.Core.Domain.Courses.Entites;
using Dapper;
using System.Data.SqlClient;

namespace BD.CourseApp.Infrastructures.Data.SqlServer.Repositories
{
    public class AssignedCourseRepository: IAssignedCourseRepository
    {
        private readonly SqlConnection _connection;

        public AssignedCourseRepository(SqlConnection dbConnection)
        {
            _connection = dbConnection;
        }
        public async Task<AssignedCourse?> GetByIdAsync(Guid courseId, Guid studentId)
        {
            var sql = "select * from StudentCourses where CourseId = @CourseId AND StudentId = @StudentId";
            return await _connection.QuerySingleOrDefaultAsync<AssignedCourse>(sql, new { CourseId = courseId, StudentId = studentId });
        }
        public async Task Create(Guid courseId, Guid studentId)
        {
            var sql = "insert into StudentCourses (CourseId, StudentId) values (@CourseId, @StudentId)";
            await _connection.ExecuteAsync(sql, new { CourseId = courseId, StudentId = studentId });
        }
        public async Task Delete(Guid courseId, Guid studentId)
        {
            var sql = "delete from StudentCourses where CourseId = @CourseId AND StudentId = @StudentId";
            await _connection.ExecuteAsync(sql, new { CourseId = courseId, StudentId = studentId });
        }

        public async Task DeleteAllByStudentId(Guid studentId)
        {
            var sql = "delete from StudentCourses where StudentId = @StudentId";
            await _connection.ExecuteAsync(sql, new { StudentId = studentId });
        }
        public async Task DeleteAllByCourseId(Guid courseId)
        {
            var sql = "delete from StudentCourses where CourseId = @CourseId";
            await _connection.ExecuteAsync(sql, new { CourseId = courseId });
        }
    }
}

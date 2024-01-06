using System.Data.SqlClient;
using Dapper;
using BD.CourseApp.Core.Domain.Students;
using BD.CourseApp.Core.Domain.Students.Contracts;
using BD.CourseApp.Infrastructures.Data.SqlServer.Extentions;

public class StudentRepository : IStudentRepository
{
    private readonly SqlConnection connection;

    public StudentRepository(SqlConnection dbConnection)
    {

        connection = dbConnection;
    }

    public async Task CreateAsync(Student student)
    {
        var sql = "insert into Students (StudentId, Name) values (@StudentId, @Name)";
        await connection.ExecuteAsync(sql, student);
    }

    public async Task<Student> GetByIdAsync(Guid id)
    {
        return await connection.QuerySingleOrDefaultAsync<Student>(
            "select * from Students where StudentId = @StudentId", new { StudentId = id });
    }

    public async Task UpdateAsync(Student student)
    {
        var sql = "update Students set Name = @Name where StudentId = @StudentId";
        await connection.ExecuteAsync(sql, student);
    }

    public async Task DeleteAsync(Guid id)
    {
        await connection.ExecuteAsync(
            "delete from Students where StudentId = @StudentId", new { StudentId = id });
    }

    public async Task<IEnumerable<Student>> GetAllAsync(string? name, int pageNumber, int pageSize)
    {
        QueryBuilder queryBuilder = new QueryBuilder();
        queryBuilder.Select("select * from Students ")
            .like("Name", name)
            .OrderBy("Name")
            .PageBy(pageNumber, pageSize);

        return await connection.QueryAsync<Student>(queryBuilder.Build(), new { NameFilter = name, PageNumber = pageNumber, PageSize = pageSize });

    }
}


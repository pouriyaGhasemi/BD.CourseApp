using System.Data.SqlClient;
using Dapper;
using BD.CourseApp.Core.Domain.Students.Contracts;
using BD.CourseApp.Infrastructures.Data.SqlServer.Extentions;
using BD.CourseApp.Core.Domain.Students.Entities;
using BD.CourseApp.Core.Domain.Students.DTOS;

public class StudentRepository : IStudentRepository
{
    private readonly SqlConnection _connection;
    private const int _defaultPageNumber = 1;
    private const int _defaultPageSize = 10;

    public StudentRepository(SqlConnection dbConnection)
    {
        _connection = dbConnection;
    }

    public async Task CreateAsync(Student student)
    {
        var sql = "insert into Students (StudentId, Name) values (@StudentId, @Name)";

        await _connection.ExecuteAsync(sql, student);
    }

    public async Task<Student?> GetByIdAsync(Guid id)
    {
        return await _connection.QuerySingleOrDefaultAsync<Student>(
            "select * from Students where StudentId = @StudentId", new { StudentId = id });
    }

    public async Task UpdateAsync(Student student)
    {
        var sql = "update Students set Name = @Name where StudentId = @StudentId";
        await _connection.ExecuteAsync(sql, student);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _connection.ExecuteAsync(
            "delete from Students where StudentId = @StudentId", new { StudentId = id });
    }

    public async Task<IEnumerable<StudentOutDTO>> GetAllAsync(string? name, int? pageNumber, int? pageSize)
    {
        pageNumber ??= _defaultPageNumber;
        pageSize ??= _defaultPageSize;
        QueryBuilder queryBuilder = new QueryBuilder();
        queryBuilder.Select("select * from Students ")
            .like("Name", name)
            .PageBy((int)pageNumber, (int)pageSize, "Name");
        var query = queryBuilder.Build();
        return await _connection.QueryAsync<StudentOutDTO>(query, new { NameFilter = name, PageNumber = pageNumber, PageSize = pageSize });

    }
}


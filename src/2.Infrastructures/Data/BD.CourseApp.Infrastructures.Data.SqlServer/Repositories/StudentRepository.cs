using System.Data.SqlClient;
using Dapper;
using BD.CourseApp.Core.Domain.Contracts;
using BD.CourseApp.Core.Domain.Entities;

public class StudentRepository : IStudentRepository
{
    private readonly string _connectionString;

    public StudentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task CreateAsync(Student student)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "INSERT INTO Students (StudentId, Name) VALUES (@StudentId, @Name)";
        await connection.ExecuteAsync(sql, student);

    }

    public async Task<Student> GetByIdAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleOrDefaultAsync<Student>("SELECT * FROM Students WHERE StudentId = @StudentId", new { StudentId = id });
    }

    public async Task UpdateAsync(Student student)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "UPDATE Students SET Name = @Name WHERE StudentId = @StudentId";
        await connection.ExecuteAsync(sql, student);

    }

    public async Task DeleteAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync("DELETE FROM Students WHERE StudentId = @StudentId", new { StudentId = id });
    }

    public async Task<IEnumerable<Student>> GetAllAsync(string nameFilter, int pageNumber, int pageSize)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"
                SELECT * FROM Students 
                WHERE (@NameFilter IS NULL OR Name LIKE '%' + @NameFilter + '%')
                ORDER BY Name
                OFFSET @PageSize * (@PageNumber - 1) ROWS
                FETCH NEXT @PageSize ROWS ONLY";

        return await connection.QueryAsync<Student>(sql, new { NameFilter = nameFilter, PageNumber = pageNumber, PageSize = pageSize });

    }
}
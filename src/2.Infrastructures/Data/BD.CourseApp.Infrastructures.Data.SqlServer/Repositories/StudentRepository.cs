using BD.CourseApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.CourseApp.Infrastructures.Data.SqlServer.Repositories
{
    public class StudentRepository
    {
        private string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();

            using SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var student = new Student
                    {
                        StudentId = new Guid(reader["StudentId"].ToString()),
                        Name = reader["Name"].ToString()
                    };
                    students.Add(student);
                }
            }
            connection.Close();

            return students;
        }
    }
}

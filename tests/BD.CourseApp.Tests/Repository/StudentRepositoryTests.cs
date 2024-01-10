using BD.CourseApp.Core.Domain.Students.Contracts;
using BD.CourseApp.Core.Domain.Students.Entities;
using BD.CourseApp.Infrastructures.Data.SqlServer.Repositories;
using BD.CourseApp.Tests.Base;
using System.Transactions;

namespace BD.CourseApp.Tests.Repository
{
    public class StudentRepositoryTests : IClassFixture<DbConnectionFixture>
    {
        private readonly IStudentRepository _studentRepository;
        public StudentRepositoryTests(DbConnectionFixture fixture)
        {
            _studentRepository = new StudentRepository(fixture.DbConnection);
        }
        [Fact]
        public async Task should_create_student()
        {
            //Arrange
            Student testStudent = new Student() { StudentId = Guid.NewGuid(), Name = "st1" };
            //Act
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _studentRepository.CreateAsync(testStudent);
            var fetchedstudent = await _studentRepository.GetByIdAsync(testStudent.StudentId);
            //Assert
            fetchedstudent.Should().NotBeNull();
            fetchedstudent.Name.Should().Be(testStudent.Name);
            fetchedstudent.StudentId.Should().Be(testStudent.StudentId);
        }
        [Fact]
        public async Task should_update_student()
        {
            //Arrange
            string newName = "st2";
            Student testStudent = new Student() { StudentId = Guid.NewGuid(), Name = "st1" };
            //Act
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _studentRepository.CreateAsync(testStudent);
            testStudent.Name = newName;
            await _studentRepository.UpdateAsync(testStudent);
            var fetchedstudent = await _studentRepository.GetByIdAsync(testStudent.StudentId);
            
            //Assert
            fetchedstudent.Should().NotBeNull();
            fetchedstudent.StudentId.Should().Be(testStudent.StudentId);
            fetchedstudent.Name.Should().Be(newName);
        }

        [Fact]
        public async Task should_delete_student()
        {
            //Arrange
            Student testStudent = new Student() { StudentId = Guid.NewGuid(), Name = "st1" };
            //Act
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _studentRepository.CreateAsync(testStudent);
            await _studentRepository.DeleteAsync(testStudent.StudentId);
            var fetchedstudent = await _studentRepository.GetByIdAsync(testStudent.StudentId);

            //Assert
            fetchedstudent.Should().BeNull();
        }

        [Fact]
        public async Task should_get_all_student()
        {
            //Arrange
            Student testStudent = new Student() { StudentId = Guid.NewGuid(), Name = "st1" };
            Student testStudent2 = new Student() { StudentId = Guid.NewGuid(), Name = "st2" };
            //Act
            //ToDo:This test is not independent. Get size of all records, then add two item and check if the size is +2
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _studentRepository.CreateAsync(testStudent);
            await _studentRepository.CreateAsync(testStudent2);
            var studentS = await _studentRepository.GetAllAsync(null,1,100);
            //Assert
            studentS.Should().NotBeNull();
            studentS.Where(w => new List<Guid> {testStudent.StudentId,testStudent2.StudentId }.Contains(w.StudentId))
                .Count().Should().Be(2);
        }
    }
}

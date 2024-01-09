using BD.CourseApp.Core.Domain.Students.Contracts;
using BD.CourseApp.Core.Domain.Students.Entities;
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
        public async Task should_create_strudent()
        {
            //arrange
            Student testStudent = new Student() { StudentId = Guid.NewGuid(), Name = "st1" };
            //act
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _studentRepository.CreateAsync(testStudent);
            var fetchedStrudent = await _studentRepository.GetByIdAsync(testStudent.StudentId);
            //Assert
            fetchedStrudent.Should().NotBeNull();
            fetchedStrudent.Name.Should().Be(testStudent.Name);
            fetchedStrudent.StudentId.Should().Be(testStudent.StudentId);
        }
        [Fact]
        public async Task should_update_strudent()
        {
            string newName = "st2";
            //arrange
            Student testStudent = new Student() { StudentId = Guid.NewGuid(), Name = "st1" };
            //act
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _studentRepository.CreateAsync(testStudent);
            testStudent.Name = newName;
            await _studentRepository.UpdateAsync(testStudent);
            var fetchedStrudent = await _studentRepository.GetByIdAsync(testStudent.StudentId);
            
            //Assert
            fetchedStrudent.Should().NotBeNull();
            fetchedStrudent.StudentId.Should().Be(testStudent.StudentId);
            fetchedStrudent.Name.Should().Be(newName);
        }

        [Fact]
        public async Task should_delete_strudent()
        {
            //arrange
            Student testStudent = new Student() { StudentId = Guid.NewGuid(), Name = "st1" };
            //act
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _studentRepository.CreateAsync(testStudent);
            await _studentRepository.DeleteAsync(testStudent.StudentId);
            var fetchedStrudent = await _studentRepository.GetByIdAsync(testStudent.StudentId);

            //Assert
            fetchedStrudent.Should().BeNull();
        }

        [Fact]
        public async Task should_get_all_strudent()
        {
            //arrange
            Student testStudent = new Student() { StudentId = Guid.NewGuid(), Name = "st1" };
            Student testStudent2 = new Student() { StudentId = Guid.NewGuid(), Name = "st2" };
            //act
            //ToDo:This test is not indipented. Get size of all, then add two item and check if the size is +2
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _studentRepository.CreateAsync(testStudent);
            await _studentRepository.CreateAsync(testStudent2);
            var strudentS = await _studentRepository.GetAllAsync(null,1,100);
            //Assert
            strudentS.Should().NotBeNull();
            strudentS.Where(w => new List<Guid> {testStudent.StudentId,testStudent2.StudentId }.Contains(w.StudentId))
                .Count().Should().Be(2);
        }
    }
}

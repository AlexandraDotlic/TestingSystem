using Core.ApplicationServices;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CoreApplicationServicesTests
{
    [TestClass]
    public class AddStudentToGroupTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private GroupService GroupService;
        private ExaminerService ExaminerService;
        private StudentService StudentService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(new string[] { });
            CoreUnitOfWork = new CoreEfCoreUnitOfWork(DbContext);
            GroupService = new GroupService(CoreUnitOfWork);
            ExaminerService = new ExaminerService(CoreUnitOfWork);
            StudentService = new StudentService(CoreUnitOfWork);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;
            
        }

        [TestMethod]
        public async Task TestAddStudentToGroupSuccess()
        {
            string externalEId = Guid.NewGuid().ToString();
            string externalSId = Guid.NewGuid().ToString();


            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            int studentId = await StudentService.CreateStudent("StudentIme", "StudentPrezime", externalSId);
            var groupId = await GroupService.CreateGroup("grupa", externalEId);
            var group = await CoreUnitOfWork.GroupRepository.GetFirstOrDefaultWithIncludes(g => g.Id == groupId, g => g.Students);
            var student = await CoreUnitOfWork.StudentRepository.GetFirstOrDefaultWithIncludes(s => s.Id == studentId, s => s.Group);
            await GroupService.AddStudentToGroup(groupId, studentId);

            Assert.AreEqual(1, group.Students.Count);
            Assert.AreEqual(student.Group.Id, group.Id);
            Assert.AreEqual(true, group.Students.Contains(student));

        }

        [TestMethod]
        public async Task TestAddStudentToGroupFail()
        {
            string externalEId = Guid.NewGuid().ToString();
            string externalSId = Guid.NewGuid().ToString();
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            int studentId = await StudentService.CreateStudent("StudentIme", "StudentPrezime", externalSId);
            var groupId = await GroupService.CreateGroup("grupa", externalEId);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await GroupService.AddStudentToGroup(groupId, 123), $"Student with Id 123 does not exist");
        }

    }
}

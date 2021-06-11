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
    public class DeleteStudentTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private StudentService StudentService;
        private ExaminerService ExaminerService;
        private GroupService GroupService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(new string[] { });
            CoreUnitOfWork = new CoreEfCoreUnitOfWork(DbContext);
            StudentService = new StudentService(CoreUnitOfWork);
            ExaminerService = new ExaminerService(CoreUnitOfWork);
            GroupService = new GroupService(CoreUnitOfWork);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {

            await DbContext.DisposeAsync();
            DbContext = null;
            
        }

        [TestMethod]
        public async Task TestDeleteStudentSuccess()
        {
            int id  = await StudentService.CreateStudent("Ime", "Prezime", "123", "email@email.com");

            await StudentService.DeleteStudent(id);
            var student = await CoreUnitOfWork.StudentRepository.GetById(id);

            Assert.AreEqual(student, null);

        }
        [TestMethod]
        public async Task TestDeleteStudentWithGroupSuccess()
        {
            string externalEId = Guid.NewGuid().ToString();
            string externalSId = Guid.NewGuid().ToString();

            int studentId = await StudentService.CreateStudent("Ime", "Prezime", "email1@email.com", externalSId);
            int examinerId = await ExaminerService.CreateExaminer("Ime2", "Prezime2", externalEId);
            var groupId = await GroupService.CreateGroup("grupa", externalEId);
            var group = await CoreUnitOfWork.GroupRepository.GetById(groupId);
            await GroupService.AddStudentToGroup(groupId, studentId);
            await StudentService.DeleteStudent(studentId);

            var student = await CoreUnitOfWork.StudentRepository.GetById(studentId);

            Assert.AreEqual(student, null);
            Assert.AreNotEqual(group, null);

        }


        [TestMethod]
        public async Task TestDeleteStudentFail()
        {
            var examiner = await CoreUnitOfWork.ExaminerRepository.GetById(100);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await StudentService.DeleteStudent(100), $"Student with Id={100} doesn't exist");
        }
    }
}

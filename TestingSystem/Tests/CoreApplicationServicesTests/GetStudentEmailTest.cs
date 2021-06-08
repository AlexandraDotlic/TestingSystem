using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.ApplicationServices;
using Core.Domain.Repositories;
using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CoreApplicationServicesTests
{
    [TestClass]
    public class GetStudentEmail
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private StudentService StudentService;
        private ExaminerService ExaminerService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(new string[] { });
            CoreUnitOfWork = new CoreEfCoreUnitOfWork(DbContext);
            StudentService = new StudentService(CoreUnitOfWork);
            ExaminerService = new ExaminerService(CoreUnitOfWork);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;
        }

        [TestMethod]
        public async Task GetStudentEmailSuccess()
        {
            string externalId = Guid.NewGuid().ToString();
            await ExaminerService.CreateExaminer("Ime", "Prezime", externalId);

            string externalSId = Guid.NewGuid().ToString();

            int id = await StudentService.CreateStudent("Ime", "Prezime", "imeprezime@mail.com", externalSId);
            var student = await CoreUnitOfWork.StudentRepository.GetById(id);

            string email = await StudentService.GetStudentEmail(student.Id);

            Assert.AreEqual("imeprezime@mail.com", email);
        }

        [TestMethod]
        public async Task GetStudentEmailFail()
        {

            string externalId = Guid.NewGuid().ToString();
            await ExaminerService.CreateExaminer("Ime", "Prezime", "123");

            string externalSId = Guid.NewGuid().ToString();

            int id = await StudentService.CreateStudent("Ime", "Prezime", "imeprezime@gmail.com", "111");
            var student = await CoreUnitOfWork.StudentRepository.GetById(id);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await StudentService.GetStudentEmail(100), $"Student with id 100 does not exist!");
        }

    }
}

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
    public class CreateStudentTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private StudentService StudentService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(new string[] { });
            CoreUnitOfWork = new CoreEfCoreUnitOfWork(DbContext);
            StudentService = new StudentService(CoreUnitOfWork);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;
            
        }

        [TestMethod]
        public async Task TestCreateStudentSuccess()
        {
            int id  = await StudentService.CreateStudent("Ime", "Prezime", "1234");
            var student = await CoreUnitOfWork.StudentRepository.GetById(id);

            Assert.AreEqual(id, student.Id);
            Assert.AreEqual("Ime", student.FirstName);
            Assert.AreEqual("Prezime", student.LastName);
            Assert.AreEqual("1234", student.ExternalId);

        }

        [TestMethod]
        public async Task TestCreateStudentFail()
        {
            int id = await StudentService.CreateStudent("Ime", "Prezime", "1234");
            var examiner = await CoreUnitOfWork.ExaminerRepository.GetById(id);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await StudentService.CreateStudent("Ime", "Prezime", null), $"AccountId must not be null");



        }

    }
}

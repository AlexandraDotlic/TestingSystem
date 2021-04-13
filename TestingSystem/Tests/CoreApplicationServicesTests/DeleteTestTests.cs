
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
    public class DeleteTestTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private GroupService GroupService;
        private ExaminerService ExaminerService;
        private StudentService StudentService;
        private TestService TestService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(new string[] { });
            CoreUnitOfWork = new CoreEfCoreUnitOfWork(DbContext);
            GroupService = new GroupService(CoreUnitOfWork);
            ExaminerService = new ExaminerService(CoreUnitOfWork);
            StudentService = new StudentService(CoreUnitOfWork);
            TestService = new TestService(CoreUnitOfWork);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;

        }

        [TestMethod]
        public async Task DeleteTestSuccess()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(examinerId, "Test1", dateTime);

            await TestService.DeleteTest(testId);
            Test test = await CoreUnitOfWork.TestRepository.GetById(testId);

            Assert.AreEqual(null, test);
          

        }

        [TestMethod]
        public async Task DeleteTestFail()
        {

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestService.DeleteTest(100), $"Test does not exist");

        }
    }
}
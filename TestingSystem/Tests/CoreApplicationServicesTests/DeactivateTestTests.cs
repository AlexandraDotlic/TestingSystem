using Core.ApplicationServices;
using Core.Domain.Repositories;
using Core.Domain.Entites;
using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CoreApplicationServicesTests
{

    [TestClass]
    public class DectivateTestTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private ExaminerService ExaminerService;
        private TestService TestService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(new string[] { });
            CoreUnitOfWork = new CoreEfCoreUnitOfWork(DbContext);
            ExaminerService = new ExaminerService(CoreUnitOfWork);
            TestService = new TestService(CoreUnitOfWork);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;
        }

        [TestMethod]
        public async Task DectivateTestSuccess()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(examinerId, "Test1", dateTime);

            var test = await CoreUnitOfWork.TestRepository.GetFirstOrDefaultWithIncludes(t => t.Id == testId);
            await TestService.DectivateTest(testId);

            Assert.AreEqual(false, test.IsActive);
        }

        [TestMethod]
        public async Task DectivateTestFail()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(examinerId, "Test1", dateTime);

            var test = await CoreUnitOfWork.TestRepository.GetFirstOrDefaultWithIncludes(t => t.Id == testId);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestService.DectivateTest(1000), $"Test with Id 1000 does not exist");

        }
    }
}

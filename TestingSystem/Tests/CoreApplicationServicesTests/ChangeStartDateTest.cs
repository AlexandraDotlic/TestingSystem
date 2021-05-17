using Core.ApplicationServices;
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
    public class ChangeStartDateTest
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
        public async Task ChangeStartDateTestSuccess()
        {
            string externalEId = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            DateTime dateTime = DateTime.Parse("2021-04-25 08:00:00.0000000");
            short testId = await TestService.CreateTest(externalEId, "Test1", dateTime);

            var test = await CoreUnitOfWork.TestRepository.GetById(testId);
            DateTime changedDateTime = DateTime.Parse("2021-04-30 16:00:00.0000000");

            await TestService.ChangeStartDate(testId, changedDateTime);


            Assert.AreEqual(true, test.IsActive);
            Assert.AreEqual(changedDateTime, test.StartDate);
        }

        [TestMethod]
        public async Task ChangeStartDateTestFail()
        {
            string externalEId = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            DateTime dateTime = DateTime.Parse("2021-04-25 08:00:00.0000000");
            short testId = await TestService.CreateTest(externalEId, "Test1", dateTime);

            var test = await CoreUnitOfWork.TestRepository.GetById(testId);
            DateTime changedDateTime = DateTime.Parse("2021-04-30 16:00:00.0000000");

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestService.ChangeStartDate(100,changedDateTime), $"Test with Id 100 does not exist");

        }

    }
}

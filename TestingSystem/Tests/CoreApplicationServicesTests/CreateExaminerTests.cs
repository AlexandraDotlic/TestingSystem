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
    public class CreateExaminerTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private ExaminerService ExaminerService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(new string[] { });
            CoreUnitOfWork = new CoreEfCoreUnitOfWork(DbContext);
            ExaminerService = new ExaminerService(CoreUnitOfWork);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;
            
        }

        [TestMethod]
        public async Task TestCreateExaminerSuccess()
        {
            string externalEId = Guid.NewGuid().ToString();

            int id = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            var examiner = await CoreUnitOfWork.ExaminerRepository.GetById(id);

            Assert.AreEqual(id, examiner.Id);
            Assert.AreEqual("Ime", examiner.FirstName);
            Assert.AreEqual("Prezime", examiner.LastName);
            Assert.AreEqual(externalEId, examiner.ExternalId);

        }

        [TestMethod]
        public async Task TestCreateExaminerFail()
        {
            string externalEId = Guid.NewGuid().ToString();

            int id = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            var examiner = await CoreUnitOfWork.ExaminerRepository.GetById(id);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await ExaminerService.CreateExaminer("Ime", "Prezime", null), $"AccountId must not be null");



        }

    }
}

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
    public class SetExaminerFirstNameTests
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

        [TestCleanup]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;
        }

        [TestMethod]
        public async Task SetExaminerFirstNameSuccess()
        {
            string externalEId = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            var examiner = await CoreUnitOfWork.ExaminerRepository.GetById(examinerId);
            await ExaminerService.SetExaminerFirstName(externalEId, "Marko");

            Assert.AreEqual(examiner.FirstName, "Marko");
        }

        [TestMethod]
        public async Task SetExaminerFirstNameFail()
        {
            var examiner = await CoreUnitOfWork.ExaminerRepository.GetById(100);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await ExaminerService.SetExaminerFirstName("1000", "Marko"), $"Examiner with with Id={100} doesn't existt");
        }
    }
}
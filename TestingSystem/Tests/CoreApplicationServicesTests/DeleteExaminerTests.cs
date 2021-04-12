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
    public class DeleteExaminerTests
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
        public async Task TestDeleteExaminerSuccess()
        {
            int id  = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");

            await ExaminerService.DeleteExaminer(id);
            var examiner = await CoreUnitOfWork.ExaminerRepository.GetById(id);

            Assert.AreEqual(examiner, null);

        }

        [TestMethod]
        public async Task TestDeleteExaminerFail()
        {
            var examiner = await CoreUnitOfWork.ExaminerRepository.GetById(100);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await ExaminerService.DeleteExaminer(100), $"Examiner with Id={100} doesn't exist");



        }

    }
}

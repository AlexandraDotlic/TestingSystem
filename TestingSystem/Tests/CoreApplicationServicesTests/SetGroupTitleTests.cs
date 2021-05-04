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
    public class SetGroupTitleTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private ExaminerService ExaminerService;
        private GroupService GroupService;

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
        public async Task SetGroupTitleSuccess()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            short groupId = await GroupService.CreateGroup("Grupa", examinerId);
            
            var group = await CoreUnitOfWork.GroupRepository.GetById(groupId);
            await GroupService.SetGroupTitle(groupId, "Novi naziv" );

            Assert.AreEqual(group.Title, "Novi naziv");
        }

        [TestMethod]
        public async Task SetGroupTitleFail()
        {
            var group = await CoreUnitOfWork.GroupRepository.GetById(100);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await GroupService.SetGroupTitle(1000, "Naziv"), $"Group with with Id={100} doesn't existt");
        }
    }
}

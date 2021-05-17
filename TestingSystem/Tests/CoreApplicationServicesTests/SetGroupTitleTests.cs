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
            GroupService = new GroupService(CoreUnitOfWork);

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
            string externalEId = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            short groupId = await GroupService.CreateGroup("Grupa", externalEId);
            
            var group = await CoreUnitOfWork.GroupRepository.GetById(groupId);
            await GroupService.SetGroupTitle(groupId, "Novi naziv" );

            Assert.AreEqual(group.Title, "Novi naziv");
        }

        [TestMethod]
        public async Task SetGroupTitleFail()
        {
            short id = 1000;
            var group = await CoreUnitOfWork.GroupRepository.GetById(id);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await GroupService.SetGroupTitle(id, "Naziv"), $"Group with with Id={id} doesn't existt");
        }
    }
}

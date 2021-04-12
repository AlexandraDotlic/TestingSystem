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
    public class DeleteGroupTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private GroupService GroupService;
        private ExaminerService ExaminerService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(new string[] { });
            CoreUnitOfWork = new CoreEfCoreUnitOfWork(DbContext);
            GroupService = new GroupService(CoreUnitOfWork);
            ExaminerService = new ExaminerService(CoreUnitOfWork);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;
            
        }

        [TestMethod]
        public async Task TestDeleteGroupSuccess()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            var groupId = await GroupService.CreateGroup("grupa", examinerId);
            await GroupService.DeleteGroup(groupId);
            var group = await CoreUnitOfWork.GroupRepository.GetById(groupId);

            Assert.AreEqual(group, null);
           

        }

        [TestMethod]
        public async Task TestCreateGroupFail()
        {

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await GroupService.DeleteGroup(1000), $"Group with id {1000} does not exist");
        }

    }
}

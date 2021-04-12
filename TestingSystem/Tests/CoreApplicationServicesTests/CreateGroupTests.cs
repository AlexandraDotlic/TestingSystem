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
    public class CreateGroupTests
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
            CoreUnitOfWork.ClearTracker();
            IReadOnlyCollection<Group> groups = await CoreUnitOfWork.GroupRepository.GetAllList();

            if (groups != null && groups.Count != 0)
            {
                foreach (var item in groups)
                {
                    await CoreUnitOfWork.GroupRepository.Delete(item);
                    await CoreUnitOfWork.SaveChangesAsync();
                }
                
            }

            await DbContext.DisposeAsync();
            DbContext = null;
            
        }

        [TestMethod]
        public async Task TestCreateGroupSuccess()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            var groupId = await GroupService.CreateGroup("grupa", examinerId);
            var group = await CoreUnitOfWork.GroupRepository.GetById(groupId);

            Assert.AreEqual(groupId, group.Id);
            Assert.AreEqual("grupa", group.Title);
            Assert.AreEqual(examinerId, group.ExaminerId);

        }

        [TestMethod]
        public async Task TestCreateGroupFail()
        {

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await GroupService.CreateGroup("grupa", 1000), $"Examiner with id = {1000} not exist");
        }

    }
}

using Core.ApplicationServices;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CoreApplicationServicesTests
{
    [TestClass]
    public class GetAllGroupsForExaminer
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private GroupService GroupService;
        private ExaminerService ExaminerService;
        private StudentService StudentService;
        private TestService TestService;
        private QuestionService QuestionService;

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
            QuestionService = new QuestionService(CoreUnitOfWork);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;

        }

        [TestMethod]
        public async Task GetAllGroupsForExaminerSuccess()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            short groupId1 = await GroupService.CreateGroup("Grupa1", examinerId);
            short groupId2 = await GroupService.CreateGroup("Grupa2", examinerId);

            Group group1 = await CoreUnitOfWork.GroupRepository
                .GetFirstOrDefaultWithIncludes(g => g.Id == groupId1);
            Group group2 = await CoreUnitOfWork.GroupRepository
                .GetFirstOrDefaultWithIncludes(g => g.Id == groupId2);

            ICollection<Core.ApplicationServices.DTOs.GroupDTO> groupDtos = await GroupService.GetAllGroupsForExaminer(examinerId);

            Assert.AreEqual(2, groupDtos.Count);
            Assert.AreEqual(group1.Title, groupDtos.FirstOrDefault().Title);

        }

    }
}
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
    public class GetAllTestsForExaminer
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
        public async Task GetAllTestsForExaminerSuccess()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            DateTime dateTime = DateTime.Now;

            short testId1 = await TestService.CreateTest(examinerId, "Test1", dateTime);
            short testId2 = await TestService.CreateTest(examinerId, "Test2", dateTime);

            Test test1 = await CoreUnitOfWork.TestRepository
                .GetFirstOrDefaultWithIncludes(t => t.Id == testId1, t => t.Questions);

            Test test2 = await CoreUnitOfWork.TestRepository
                .GetFirstOrDefaultWithIncludes(t => t.Id == testId2, t => t.Questions);


            ICollection<Core.ApplicationServices.DTOs.TestDTO> testDtos = await TestService.GetAllTestsForExaminer(examinerId);

            Assert.AreEqual(2, testDtos.Count);
            Assert.AreEqual(test1.Title, testDtos.FirstOrDefault().Title);

        }

    }
}

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
    public class AddQuestionToTestTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private GroupService GroupService;
        private ExaminerService ExaminerService;
        private StudentService StudentService;
        private TestService TestService;

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
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;

        }

        [TestMethod]
        public async Task AddQuestionToTestSuccess()
        {
            string externalId = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalId);
            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(externalId, "Test1", dateTime);

            Test test = await CoreUnitOfWork.TestRepository
                .GetFirstOrDefaultWithIncludes(t => t.Id == testId, t => t.Questions);

            string questionText = "Pitanje 1";
            ICollection<Tuple<string, bool>> answeOptionTuples = new List<Tuple<string, bool>>
            {
                new Tuple<string, bool>("opcija 1", true),
                new Tuple<string, bool>("opcija 2", false)
            };

            await TestService.AddQuestionToTest(testId, questionText, answeOptionTuples);
            Assert.AreEqual(1, test.Questions.Count);
            
            Assert.AreEqual(questionText, test.Questions.FirstOrDefault().QuestionText);
            Assert.AreEqual(1, test.Questions.FirstOrDefault().QuestionScore);
            Assert.AreEqual(1, test.TestScore);

        }

        [TestMethod]
        public async Task AddQuestionToTestFail1()
        {
            string questionText = "Pitanje 1";

            ICollection<Tuple<string, bool>> answeOptionTuples = new List<Tuple<string, bool>>
            {
                new Tuple<string, bool>("opcija 1", true),
                new Tuple<string, bool>("opcija 2", false)
            };
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestService.AddQuestionToTest(100, questionText, answeOptionTuples), $"Test does not exist");



        }

        [TestMethod]
        public async Task AddQuestionToTestFail2()
        {
            string questionText = "Pitanje 1";
            string externalId = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalId);
            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(externalId, "Test1", dateTime);


            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestService.AddQuestionToTest(testId, questionText, null), $"AnswerOptions cant be null");



        }
    }
}
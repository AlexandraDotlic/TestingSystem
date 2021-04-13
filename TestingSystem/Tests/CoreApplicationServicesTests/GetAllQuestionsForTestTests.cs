
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
    public class GetAllQuestionsForTestTests
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
        public async Task GetAllQuestionsForTestSuccess()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(examinerId, "Test1", dateTime);

            Test test = await CoreUnitOfWork.TestRepository
                .GetFirstOrDefaultWithIncludes(t => t.Id == testId, t => t.Questions);

            string questionText = "Pitanje 1";
            ICollection<Tuple<string, bool>> answeOptionTuples = new List<Tuple<string, bool>>
            {
                new Tuple<string, bool>("opcija 1", true),
                new Tuple<string, bool>("opcija 2", false)
            };

            await TestService.AddQuestionToTest(testId, questionText, answeOptionTuples);

            ICollection<Core.ApplicationServices.DTOs.QuestionDTO> questionDtos = await QuestionService.GetAllQuestionsForTest(testId);

            Assert.AreEqual(test.Questions.Count, questionDtos.Count);
            
            
            Assert.AreEqual(test.Questions.FirstOrDefault().QuestionText, questionDtos.FirstOrDefault().QuestionText);
            Assert.AreEqual(test.Questions.FirstOrDefault().QuestionScore, questionDtos.FirstOrDefault().QuestionScore);
            Assert.AreEqual(2, questionDtos.FirstOrDefault().AnswerOptions.Count);

        }

    }
}
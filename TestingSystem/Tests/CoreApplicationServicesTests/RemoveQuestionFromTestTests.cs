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
    public class RemoveQuestionFromTestTests
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
        public async Task RemoveQuestionFromTestSuccess()
        {
            string externalEId = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(externalEId, "Test1", dateTime);

            Test test = await CoreUnitOfWork.TestRepository
                .GetFirstOrDefaultWithIncludes(t => t.Id == testId, t => t.Questions);

            string questionText1 = "Pitanje 1";
            ICollection<Tuple<string, bool>> answerOptionTuples1 = new List<Tuple<string, bool>>
            {
                new Tuple<string, bool>("opcija 1", true),
                new Tuple<string, bool>("opcija 2", false)
            };

            string questionText2= "Pitanje 2";
            ICollection<Tuple<string, bool>> answerOptionTuples2 = new List<Tuple<string, bool>>
            {
                new Tuple<string, bool>("opcija 1", true),
                new Tuple<string, bool>("opcija 2", false)
            };

            await TestService.AddQuestionToTest(testId, questionText1, answerOptionTuples1);
            await TestService.AddQuestionToTest(testId, questionText2, answerOptionTuples2);

            Question question = test.Questions.FirstOrDefault();
            await TestService.RemoveQuestionFromTest(testId, question.Id);

            Assert.AreEqual(1, test.Questions.Count);
            Assert.AreEqual(questionText2, test.Questions.FirstOrDefault().QuestionText);

        }


    }
}
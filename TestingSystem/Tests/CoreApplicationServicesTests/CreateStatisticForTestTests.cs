
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
    public class CreateStatisticForTestTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private GroupService GroupService;
        private ExaminerService ExaminerService;
        private StudentService StudentService;
        private TestService TestService;
        private QuestionService QuestionService;
        private TestStatisticService TestStatisticService;

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
            TestStatisticService = new TestStatisticService(CoreUnitOfWork);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;

        }

        public async Task<Tuple<short, int, int>> CreateTestWithQuestions(string externalId, string testTitle)
        {
            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(externalId, "Test1", dateTime);
            string questionText1 = "Pitanje 1";
            ICollection<Tuple<string, bool>> answeOptionTuples1 = new List<Tuple<string, bool>>
            {
                new Tuple<string, bool>("opcija 1", true),
                new Tuple<string, bool>("opcija 2", false)
            };

            await TestService.AddQuestionToTest(testId, questionText1, answeOptionTuples1);

            var question1 = await CoreUnitOfWork.QuestionRepository.GetFirstOrDefaultWithIncludes(q => q.QuestionText == questionText1 && q.TestId == testId);
            var question1Id = question1.Id;
            string questionText2 = "Pitanje 2";
            ICollection<Tuple<string, bool>> answeOptionTuples2 = new List<Tuple<string, bool>>
            {
                new Tuple<string, bool>("opcija 1", true),
                new Tuple<string, bool>("opcija 2", false),
                new Tuple<string, bool>("opcija 3", true)
            };

            await TestService.AddQuestionToTest(testId, questionText2, answeOptionTuples2);
            var question2 = await CoreUnitOfWork.QuestionRepository.GetFirstOrDefaultWithIncludes(q => q.QuestionText == questionText2 && q.TestId == testId);
            var question2Id = question2.Id;

            return new Tuple<short, int, int>(testId, question1Id, question2Id);
        }
        [TestMethod]
        public async Task CreateTestStatisticSuccess()
        {
            string externalEId = Guid.NewGuid().ToString();
            string externalSId1 = Guid.NewGuid().ToString();
            string externalSId2 = Guid.NewGuid().ToString();
            string externalSId3 = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            int studentId1 = await StudentService.CreateStudent("student1", "Prezimes", "email1@email.com", externalSId1);
            int studentId2 = await StudentService.CreateStudent("student2", "Prezimes", "email2@email.com", externalSId2);
            int studentId3 = await StudentService.CreateStudent("student3", "Prezimes", "email3@email.com", externalSId3);


            var (testId, question1Id, question2Id) = await CreateTestWithQuestions(externalEId, "Title test");

            //student 1
            var s1question1Response = new Tuple<int, ICollection<string>>(question1Id, new List<string> { "opcija 1" });
            var s1question2Response = new Tuple<int, ICollection<string>>(question2Id, new List<string> { "opcija 2", "opcija 3" });

            var student1TestScore = await TestService.TakeTheTest(testId, externalSId1, new List<Tuple<int, ICollection<string>>> { s1question1Response, s1question2Response });

            IReadOnlyCollection<StudentTestQuestion> student1TestQuestons = await CoreUnitOfWork.StudentTestQuestionRepository
                .SearchByWithIncludes(stq => stq.StudentTest.StudentId == studentId1
                && stq.StudentTest.TestId == testId
                , stq => stq.Responses, stq => stq.StudentTest);

            //student 2

            var s2question1Response = new Tuple<int, ICollection<string>>(question1Id, new List<string> { "opcija 2" });
            var s2question2Response = new Tuple<int, ICollection<string>>(question2Id, new List<string> { "opcija 2"});

            var student2TestScore = await TestService.TakeTheTest(testId, externalSId2, new List<Tuple<int, ICollection<string>>> { s2question1Response, s2question2Response });

            IReadOnlyCollection<StudentTestQuestion> student2TestQuestons = await CoreUnitOfWork.StudentTestQuestionRepository
                .SearchByWithIncludes(stq => stq.StudentTest.StudentId == studentId2
                && stq.StudentTest.TestId == testId
                , stq => stq.Responses, stq => stq.StudentTest);

            var s3question1Response = new Tuple<int, ICollection<string>>(question1Id, new List<string> { "opcija 1" });
            var s3question2Response = new Tuple<int, ICollection<string>>(question2Id, new List<string> { "opcija 2" });

            var student3TestScore = await TestService.TakeTheTest(testId, externalSId3, new List<Tuple<int, ICollection<string>>> { s2question1Response, s2question2Response });

            IReadOnlyCollection <StudentTestQuestion> student3TestQuestons = await CoreUnitOfWork.StudentTestQuestionRepository
                .SearchByWithIncludes(stq => stq.StudentTest.StudentId == studentId3
                && stq.StudentTest.TestId == testId
                , stq => stq.Responses, stq => stq.StudentTest);


            var Id = await TestStatisticService.CreateStatisticForTest(testId, externalEId);
            var testStatistic = await CoreUnitOfWork.TestStatisticRepository.GetById(Id);
            Assert.AreEqual(33, testStatistic.PercentageOfStudentsWhoPassedTheTest);
            Assert.AreEqual(3, testStatistic.NumberOfStudentsWhoTookTheTest);

        }

        [TestMethod]
        public async Task CreateTestStatisticSuccess2()
        {
            string externalEId = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            var (testId, question1Id, question2Id) = await CreateTestWithQuestions(externalEId, "Title test");

            var Id = await TestStatisticService.CreateStatisticForTest(testId, externalEId);
            var testStatistic = await CoreUnitOfWork.TestStatisticRepository.GetById(Id);

            Assert.AreEqual(null, testStatistic);
            Assert.AreEqual(Id, 0);



        }

        [TestMethod]
        public async Task CreateTestStatisticFail1()
        {
            string externalEId = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            var (testId, question1Id, question2Id) = await CreateTestWithQuestions(externalEId, "Title test");



            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestStatisticService.CreateStatisticForTest(testId, "100"), $"Examiner does not exist");

        }

        [TestMethod]
        public async Task CreateTestStatisticFail2()
        {
            string externalEId = Guid.NewGuid().ToString();

            string externalSId = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            int studentId = await StudentService.CreateStudent("ImeS", "Prezimes", "email@email.com", externalSId);


            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestStatisticService.CreateStatisticForTest(100, "123"), $"Test does not exist");


        }

      
    }
}

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
    public class GetAllStatisticsForTestTests
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

        public async Task<Tuple<short, int, int>> CreateTestWithQuestions(int examinerId, string testTitle)
        {
            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(examinerId, "Test1", dateTime);
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
        public async Task GetAllTestStatisticSuccess()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            int studentId1 = await StudentService.CreateStudent("student1", "Prezimes", "12345");
            int studentId2 = await StudentService.CreateStudent("student2", "Prezimes", "123456");
            int studentId3 = await StudentService.CreateStudent("student3", "Prezimes", "123256");


            var (testId, question1Id, question2Id) = await CreateTestWithQuestions(examinerId, "Title test");

            //student 1
            var s1question1Response = new Tuple<int, ICollection<string>>(question1Id, new List<string> { "opcija 1" });
            var s1question2Response = new Tuple<int, ICollection<string>>(question2Id, new List<string> { "opcija 2", "opcija 3" });

            var student1TestScore = await TestService.TakeTheTest(testId, studentId1, new List<Tuple<int, ICollection<string>>> { s1question1Response, s1question2Response });

            IReadOnlyCollection<StudentTestQuestion> student1TestQuestons = await CoreUnitOfWork.StudentTestQuestionRepository
                .SearchByWithIncludes(stq => stq.StudentTest.StudentId == studentId1
                && stq.StudentTest.TestId == testId
                , stq => stq.Responses, stq => stq.StudentTest);

            //student 2

            var s2question1Response = new Tuple<int, ICollection<string>>(question1Id, new List<string> { "opcija 2" });
            var s2question2Response = new Tuple<int, ICollection<string>>(question2Id, new List<string> { "opcija 2"});

            var student2TestScore = await TestService.TakeTheTest(testId, studentId2, new List<Tuple<int, ICollection<string>>> { s2question1Response, s2question2Response });

            IReadOnlyCollection<StudentTestQuestion> student2TestQuestons = await CoreUnitOfWork.StudentTestQuestionRepository
                .SearchByWithIncludes(stq => stq.StudentTest.StudentId == studentId2
                && stq.StudentTest.TestId == testId
                , stq => stq.Responses, stq => stq.StudentTest);

            var s3question1Response = new Tuple<int, ICollection<string>>(question1Id, new List<string> { "opcija 1" });
            var s3question2Response = new Tuple<int, ICollection<string>>(question2Id, new List<string> { "opcija 2" });

            var student3TestScore = await TestService.TakeTheTest(testId, studentId3, new List<Tuple<int, ICollection<string>>> { s2question1Response, s2question2Response });

            IReadOnlyCollection<StudentTestQuestion> student3TestQuestons = await CoreUnitOfWork.StudentTestQuestionRepository
                .SearchByWithIncludes(stq => stq.StudentTest.StudentId == studentId3
                && stq.StudentTest.TestId == testId
                , stq => stq.Responses, stq => stq.StudentTest);


            var Id = await TestStatisticService.CreateStatisticForTest(testId, examinerId);

            ICollection<Core.ApplicationServices.DTOs.TestStatisticDTO> testStatistics = await TestStatisticService.GetAllStatisticsForTest(testId, examinerId);
           
            Assert.AreEqual(1, testStatistics.Count);
            Assert.AreEqual(Id, testStatistics.FirstOrDefault().Id);

        }


        [TestMethod]
        public async Task GetAllTestStatisticFail1()
        {
           
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            var (testId, question1Id, question2Id) = await CreateTestWithQuestions(examinerId, "Title test");


            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestStatisticService.GetAllStatisticsForTest(testId, 100), $"Examiner does not exist");

        }

        [TestMethod]
        public async Task GetAllTestStatisticFail2()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            int studentId = await StudentService.CreateStudent("ImeS", "Prezimes", "12345");


            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestStatisticService.GetAllStatisticsForTest(100, examinerId), $"Test does not exist");


        }

      
    }
}
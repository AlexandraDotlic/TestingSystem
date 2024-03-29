﻿
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
    public class TakeTheTestTests
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
        public async Task TakeTheTestSuccess()
        {

            string externalSId = Guid.NewGuid().ToString();
            string externalEId = Guid.NewGuid().ToString();

            int studentId = await StudentService.CreateStudent("ImeS", "Prezime", "email@email.com", externalSId);
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);


            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(externalEId, "Test1", dateTime);
            string questionText1 = "Pitanje 1";
            //trebalo bi da bude tipa free text jer ima jedan odgovor
            ICollection<Tuple<string, bool>> answeOptionTuples1 = new List<Tuple<string, bool>>
            {
                new Tuple<string, bool>("odgovor", true),
            };

            await TestService.AddQuestionToTest(testId, questionText1, answeOptionTuples1);

            var question1 = await  CoreUnitOfWork.QuestionRepository.GetFirstOrDefaultWithIncludes(q => q.QuestionText == questionText1 && q.TestId == testId);
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

            var question1Response = new Tuple<int, ICollection<string>>(question1Id, new List<string> { "sta god da unesem tacno je" });
            var question2Response = new Tuple<int, ICollection<string>>(question2Id, new List<string> { "opcija 2", "opcija 3" });

            var studentTestScore = await TestService.TakeTheTest(testId, externalSId, new List<Tuple<int, ICollection<string>>> { question1Response, question2Response });

            IReadOnlyCollection<StudentTestQuestion> studentTestQuestons = await CoreUnitOfWork.StudentTestQuestionRepository
                .SearchByWithIncludes(stq => stq.StudentTest.StudentId == studentId
                && stq.StudentTest.TestId == testId
                , stq => stq.Responses, stq => stq.StudentTest);
            Assert.AreEqual(question1.Type, QuestionType.FreeText);
            Assert.AreEqual(2, studentTestQuestons.Count);
            Assert.AreEqual(2, studentTestScore.StudentTestScore);

        }



        [TestMethod]
        public async Task TakeTheTestFail()
        {
            string externalSId = Guid.NewGuid().ToString();
            string externalEId = Guid.NewGuid().ToString();

            int studentId = await StudentService.CreateStudent("ImeS", "Prezime", "email@email.com", externalSId);
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);

            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(externalEId, "Test1", dateTime);
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

            var question1Response = new Tuple<int, ICollection<string>>(question1Id, new List<string> { "opcija 1" });
            var question2Response = new Tuple<int, ICollection<string>>(question2Id, new List<string> { "opcija 2", "opcija 3" });
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestService.TakeTheTest(100, externalSId, new List<Tuple<int, ICollection<string>>> { question1Response, question2Response }), $"Test does not exist");



        }

        [TestMethod]
        public async Task TakeTheTestFail2()
        {
            string externalSId = Guid.NewGuid().ToString();
            string externalEId = Guid.NewGuid().ToString();

            int studentId = await StudentService.CreateStudent("ImeS", "Prezime", "email@email.com", externalSId);
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);


            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(externalEId, "Test1", dateTime);
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

            var question1Response = new Tuple<int, ICollection<string>>(question1Id, new List<string> { "opcija 1" });
            var question2Response = new Tuple<int, ICollection<string>>(question2Id, new List<string> { "opcija 2", "opcija 3" });
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestService.TakeTheTest(testId, "100", new List<Tuple<int, ICollection<string>>> { question1Response, question2Response }), $"Student does not exist");



        }

        [TestMethod]
        public async Task TakeTheTestFail3()
        {
            string externalSId = Guid.NewGuid().ToString();
            string externalEId = Guid.NewGuid().ToString();

            int studentId = await StudentService.CreateStudent("ImeS", "Prezime", "email@email.com", externalSId);
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);

            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(externalEId, "Test1", dateTime);
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

            var question1Response = new Tuple<int, ICollection<string>>(question1Id, new List<string> { "opcija 1" });
            var question2Response = new Tuple<int, ICollection<string>>(question2Id, new List<string> { "opcija 2", "opcija 3" });
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestService.TakeTheTest(testId, externalSId, null), $"Collection of responses can't be null");



        }
    }
}
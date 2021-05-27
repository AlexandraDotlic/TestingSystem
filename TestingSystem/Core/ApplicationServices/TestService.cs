using Core.ApplicationServices.DTOs;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices
{
    public class TestService
    {
        private readonly ICoreUnitOfWork UnitOfWork;
        public TestService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<short> CreateTest(string externalUserId, string title, DateTime startDate)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetFirstOrDefaultWithIncludes(e => e.ExternalId == externalUserId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with external Id {externalUserId} not exist");
            }
            Test test = new Test(examiner, title, startDate);
            await UnitOfWork.TestRepository.Insert(test);
            await UnitOfWork.SaveChangesAsync();
            return test.Id;
        }

        public async Task DeleteTest(short testId)
        {
            Test test = await UnitOfWork.TestRepository.GetById(testId);
            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }
            await UnitOfWork.TestRepository.Delete(test);
            await UnitOfWork.SaveChangesAsync();

        }

        public async Task AddQuestionToTest(short testId, string questionText, ICollection<Tuple<string, bool>> answerOptionTuples)
        {
            Test test = await UnitOfWork.TestRepository.GetFirstOrDefaultWithIncludes(t => t.Id == testId, t => t.Questions);
            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }
            ICollection<AnswerOption> answerOptionsCollection = new List<AnswerOption>();
            if (answerOptionTuples == null || answerOptionTuples.Count == 0)
            {
                throw new ArgumentNullException($"{nameof(answerOptionTuples)}");
            }
            foreach (var option in answerOptionTuples)
            {
                answerOptionsCollection.Add(new AnswerOption(option.Item1, option.Item2));
            }
            Question question = new Question(questionText, answerOptionsCollection);
            test.AddQuestion(question);
            await UnitOfWork.TestRepository.Update(test);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task RemoveQuestionFromTest(short testId, int questionId)
        {
            Test test = await UnitOfWork.TestRepository.GetFirstOrDefaultWithIncludes(t => t.Id == testId, t => t.Questions);
            Question question = await UnitOfWork.QuestionRepository.GetById(questionId);

            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }

            if (question == null)
            {
                throw new ArgumentNullException($"{nameof(Question)} with Id {questionId} not exist");
            }

            test.RemoveQuestion(question);
            await UnitOfWork.TestRepository.Update(test);
            await UnitOfWork.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="studentId"></param>
        /// <param name="questonResponsesCollection">Kolekcija odgovora za svako pitanje</param>
        /// <returns></returns>
        public async Task<StudentTestScoreDTO> TakeTheTest(short testId, string externalStudentId, ICollection<Tuple<int, ICollection<string>>> questonResponsesCollection)
        {
            Test test = await UnitOfWork.TestRepository.GetById(testId);
            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }
            Student student = await UnitOfWork.StudentRepository.GetFirstOrDefaultWithIncludes(s => s.ExternalId == externalStudentId);
            if (student == null)
            {
                throw new ArgumentNullException($"{nameof(Student)} with Id {externalStudentId} not exist");
            }

            if (questonResponsesCollection == null || questonResponsesCollection.Count == 0)
            {
                throw new ArgumentNullException($"{nameof(questonResponsesCollection)} is null or empty");

            }
            IReadOnlyCollection<Question> testQuestions = await UnitOfWork.QuestionRepository
                .SearchByWithIncludes(q => q.TestId == testId, q => q.AnswerOptions);

            ICollection<StudentTestQuestion> studentTestQuestions = new List<StudentTestQuestion>();

            foreach (var questionResponse in questonResponsesCollection)
            {
                var question = testQuestions.Where(q => q.Id == questionResponse.Item1).FirstOrDefault();
                ICollection<StudentTestQuestionResponse> studentTestQuestionResponses = new List<StudentTestQuestionResponse>();
                foreach (var response in questionResponse.Item2)
                {
                    AnswerOption answer = question.AnswerOptions.Where(ao => ao.OptionText == response).FirstOrDefault();
                    var studentTestQuestionResponse = new StudentTestQuestionResponse(answer.OptionText, answer.IsCorrect ? 1 : 0);
                    studentTestQuestionResponses.Add(studentTestQuestionResponse);

                }
                var studentTestQuestion = new StudentTestQuestion(question.Id, studentTestQuestionResponses);
                studentTestQuestions.Add(studentTestQuestion);
            }
            await UnitOfWork.BeginTransactionAsync();
            StudentTest studentTest = new StudentTest(student, test, studentTestQuestions);
            student.AddStudentTest(studentTest);

            await UnitOfWork.StudentRepository.Update(student);
            await UnitOfWork.SaveChangesAsync();
            await UnitOfWork.CommitTransactionAsync();
            return new StudentTestScoreDTO
            {
                StudentId = student.Id,
                TestId = testId,
                TotalTestScore = test.TestScore,
                StudentTestScore = studentTest.Score


            };
        }

        public async Task<ICollection<TestDTO>> GetAllTests()
        {
            IReadOnlyCollection<Test> tests = await UnitOfWork.TestRepository.GetAllList();
            List<TestDTO> testDTOs = tests == null || tests.Count == 0
                ? new List<TestDTO>()
                : tests.Select(t => new TestDTO(t.Id, t.Title, t.ExaminerId, t.StartDate, t.IsActive, t.TestScore)).ToList();
            return testDTOs;
        }

        public async Task<ICollection<TestDTO>> GetAllTestsForExaminer(string externalExaminerId)
        {
            IReadOnlyCollection<Test> tests = await UnitOfWork.TestRepository.SearchByWithIncludes(t => t.Examiner.ExternalId == externalExaminerId, t => t.Examiner);

            List<TestDTO> testDTOs = tests == null || tests.Count == 0
                ? null
                : tests.Select(t => new TestDTO(t.Id, t.Title, t.ExaminerId, t.StartDate, t.IsActive, t.TestScore)).ToList();
            return testDTOs;

        }
         public async Task ActivateTest(short testId)
         {
                Test test = await UnitOfWork.TestRepository.GetById(testId);
                if (test == null)
                {
                    throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
                }

                test.Activate();
                await UnitOfWork.TestRepository.Update(test);
                await UnitOfWork.SaveChangesAsync();

         }

        public async Task DeactivateTest(short testId)
        {
            Test test = await UnitOfWork.TestRepository.GetById(testId);
            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }

            test.Deactivate();
            await UnitOfWork.TestRepository.Update(test);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task ChangeStartDate(short testId, DateTime startDate)
        {
            Test test = await UnitOfWork.TestRepository.GetById(testId);
            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }

            test.ChangeStartDate(startDate);
            await UnitOfWork.TestRepository.Update(test);
            await UnitOfWork.SaveChangesAsync();

        }


    }
    }

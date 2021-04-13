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

        public async Task<short> CreateTest(int examinerId, string title, DateTime startDate, DateTime? endDate = null)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetById(examinerId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with Id {examinerId} not exist");
            }
            Test test = new Test(examiner, title, startDate, endDate);
            await UnitOfWork.TestRepository.Insert(test);
            await UnitOfWork.SaveChangesAsync();
            return test.Id;
        }

        public async Task DeleteTest(short testId)
        {
            Test test = await UnitOfWork.TestRepository.GetById(testId);
            if(test == null)
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
            if(answerOptionTuples == null || answerOptionTuples.Count == 0)
            {
                throw new ArgumentNullException($"{nameof(answerOptionTuples)}");
            }
            foreach (var option in answerOptionTuples)
            {
                answerOptionsCollection.Add(new AnswerOption(option.Item1, option.Item2));
            }
            Question question = new Question(questionText, answerOptionsCollection);
            test.AddTestQuestion(question);
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
        public async Task TakeTheTest(short testId, int studentId, ICollection<Tuple<int, ICollection<string>>> questonResponsesCollection)
        {
            Test test = await UnitOfWork.TestRepository.GetById(testId);
            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }
            Student student = await UnitOfWork.StudentRepository.GetById(studentId);
            if (student == null)
            {
                throw new ArgumentNullException($"{nameof(Student)} with Id {studentId} not exist");
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
                var studentTestQuestion = new StudentTestQuestion(student, question, studentTestQuestionResponses);
                studentTestQuestions.Add(studentTestQuestion);
            }
            await UnitOfWork.BeginTransactionAsync();
            foreach (var studentTestQuestion in studentTestQuestions)
            {
                student.AddStudentTestQuestions(studentTestQuestion);
            }
            await UnitOfWork.StudentRepository.Update(student);
            await UnitOfWork.SaveChangesAsync();
            await UnitOfWork.CommitTransactionAsync();
        }
    }
}

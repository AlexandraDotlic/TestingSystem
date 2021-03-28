using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Entites
{
    public class StudentTestQuestion
    {
        public int Id { get; private set; }
        public int StudentId { get; private set; }
        public Student Student { get; private set; }
        public int QuestionId { get; private set; }
        public Question Question { get; private set; }
        public short TestId { get; private set; }
        public Test Test { get; private set; }
        public ICollection<StudentTestQuestionResponse> Responses { get; private set; }
        public int Score => Responses.Sum(r => r.ResponseScore);

        public StudentTestQuestion() 
        {
            Responses = new List<StudentTestQuestionResponse>();
        }

        public StudentTestQuestion(Student student, Question question, Test test, ICollection<StudentTestQuestionResponse> responses)
        {
            Student = student;
            StudentId = student.Id;
            Question = question;
            QuestionId = question.Id;
            Test = test;
            TestId = test.Id;
            Responses = responses;
        }

    }
}

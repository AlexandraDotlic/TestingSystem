﻿using System;
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
        public ICollection<StudentTestQuestionResponse> Responses { get; private set; }
        public int Score { get; private set; }

        public StudentTestQuestion() 
        {
            Responses = new List<StudentTestQuestionResponse>();
        }

        public StudentTestQuestion(Student student, Question question, ICollection<StudentTestQuestionResponse> responses)
        {
            Student = student;
            StudentId = student.Id;
            Question = question;
            QuestionId = question.Id;
            TestId = question.Test.Id;
            Responses = responses;
            Score = Responses == null || Responses.Count == 0 ? 0 : Responses.Sum(r => r.ResponseScore); 
        }

    }
}

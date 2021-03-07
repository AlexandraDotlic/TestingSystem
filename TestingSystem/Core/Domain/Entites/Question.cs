using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class Question
    {
        public int Id { get; protected set; }
        public string QuestionText { get; protected set; }
        public string Answer { get; private set; }
        public ICollection<TestQuestion> TestQuestions { get; private set; }

        protected Question()
        {
            TestQuestions = new List<TestQuestion>();
        }

        public Question(string questionText, string answer)
        {
            TestQuestions = new List<TestQuestion>();
            QuestionText = questionText;
            Answer = answer;
        }
    }
}

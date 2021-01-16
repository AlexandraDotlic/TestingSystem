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

        protected Question()
        { }

        protected Question(string questionText, string answer)
        {
            QuestionText = questionText;
            Answer = answer;
        }
    }
}

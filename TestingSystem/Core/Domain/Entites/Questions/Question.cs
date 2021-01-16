using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites.Questions
{
    public abstract class Question
    {
        public int Id { get; protected set; }
        public string QuestionText { get; protected set; }

        protected Question()
        { }

        protected Question(string questionText): this()
        {
            QuestionText = questionText;
        }
    }
}

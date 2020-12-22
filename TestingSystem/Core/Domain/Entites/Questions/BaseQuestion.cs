using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites.Questions
{
    public abstract class BaseQuestion
    {
        public int Id { get; protected set; }
        public string Question { get; protected set; }

        protected BaseQuestion()
        { }

        protected BaseQuestion(string question): this()
        {
            Question = question;
        }
    }
}

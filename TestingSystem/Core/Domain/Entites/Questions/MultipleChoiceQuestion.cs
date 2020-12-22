using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites.Questions
{
    public class MultipleChoiceQuestion : BaseQuestion
    {
        public bool Answer { get; private set; }

        public MultipleChoiceQuestion() : base()
        { }

        public MultipleChoiceQuestion(string question, bool answer) : base(question)
        {
            Answer = answer;
        }
    }
}

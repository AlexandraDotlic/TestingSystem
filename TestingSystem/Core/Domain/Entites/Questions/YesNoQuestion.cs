using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites.Questions
{
    public class YesNoQuestion : BaseQuestion
    {
        public bool Answer { get; private set; }

        public YesNoQuestion() : base()
        {}

        public YesNoQuestion(string question, bool answer) : base(question)
        {
            this.Answer = answer;
        }

    }
}

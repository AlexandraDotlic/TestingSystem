using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class TestQuestion
    {
        public short TestId { get; private set; }
        public Test Test { get; private set; }
        public int QuestionId { get; private set; }
        public Question Question { get; private set; }

        public TestQuestion() { }

    }
}
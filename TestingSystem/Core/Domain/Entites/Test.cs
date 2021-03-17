using System;
using System.Collections.Generic;

namespace Core.Domain.Entites
{
    public class Test
    {
        public short Id { get; private set; }
        public string Title { get; private set; }
        public ICollection<TestQuestion> TestQuestions { get; private set; }
        public ICollection<Group> Groups { get; private set; }
        
        public Test(string title) 
        {
            Title = title;
            TestQuestions = new List<TestQuestion>();
            Groups = new List<Group>();
        }

        private Test()
        {
            TestQuestions = new List<TestQuestion>();
            Groups = new List<Group>();
        }
        public void AddTestQuestion(TestQuestion testQuestion)
        {
            TestQuestions.Add(testQuestion);
        }

    }
}

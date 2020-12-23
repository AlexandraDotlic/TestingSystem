using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Entites.Questions;

namespace Core.Domain.Entites
{
    public class Test
    {
        public short Id { get; private set; }
        public string Title { get; private set; }
        public ICollection<BaseQuestion> Questions { get; private set; }
        public ICollection<Group> Groups { get; private set; }

        public Test(string title) 
        {
            Title = title;
            Questions = new List<BaseQuestion>();
            Groups = new List<Group>();
        }

        private Test()
        {
            Questions = new List<BaseQuestion>();
            Groups = new List<Group>();
        }
    }
}

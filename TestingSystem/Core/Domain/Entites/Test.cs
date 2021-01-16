using System.Collections.Generic;

namespace Core.Domain.Entites
{
    public class Test
    {
        public short Id { get; private set; }
        public string Title { get; private set; }
        public ICollection<Question> Questions { get; private set; }
        public ICollection<Group> Groups { get; private set; }

        public Test(string title) 
        {
            Title = title;
            Questions = new List<Question>();
            Groups = new List<Group>();
        }

        private Test()
        {
            Questions = new List<Question>();
            Groups = new List<Group>();
        }
    }
}

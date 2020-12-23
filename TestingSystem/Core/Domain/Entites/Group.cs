using System.Collections.Generic;

namespace Core.Domain.Entites
{
    public class Group
    {
        public short Id { get; private set; }
        public string Title { get; private set; }
        public short ExaminerId { get; private set; }
        public short TestId { get; private set; }
        public ICollection<Student> Students { get; private set; }
        public Test Test { get; private set; }


        private Group()
        {
            Students = new List<Student>();
        }

        public Group(string title, short examinerId, short testId)
        {
            Title = title;
            ExaminerId = examinerId;
            TestId = testId;
            Students = new List<Student>();
        }

    }
}

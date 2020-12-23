using System.Collections.Generic;

namespace Core.Domain.Entites
{
    class Group
    {
        public short Id { get; private set; }
        public string Title { get; private set; }
        public short ExaminerId { get; private set; }
        public short TestId { get; private set; }
        public ICollection<Student> Students { get; private set; }

        public Group()
        {

        }

        public Group(short _Id, string _Title, short _ExaminerId, short _TestId, ICollection<Student> _Students)
        {
            Id = _Id;
            Title = _Title;
            ExaminerId = _ExaminerId;
            TestId = _TestId;
            Students = _Students;
        }

    }
}

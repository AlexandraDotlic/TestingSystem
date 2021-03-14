using System.Collections.Generic;

namespace Core.Domain.Entites
{
    public class Group
    {
        public short Id { get; private set; }
        public string Title { get; private set; }
        public int ExaminerId { get; private set; }
        public Examiner Examiner { get; private set; }
        public short TestId { get; private set; }
        public Test Test { get; private set; }
        public ICollection<StudentGroup> StudentGroups { get; private set; }

        private Group()
        {
            StudentGroups = new List<StudentGroup>();
        }

        public Group(string title, int examinerId, short testId)
        {
            Title = title;
            ExaminerId = examinerId;
            TestId = testId;
            StudentGroups = new List<StudentGroup>();
        }

        public void AddStudentGroup(StudentGroup studentGroup)
        {
            StudentGroups.Add(studentGroup);
        }

    }
}

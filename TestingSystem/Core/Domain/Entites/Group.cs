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

        public Group(string title, Examiner examiner, Test test)
        {
            Title = title;
            Examiner = examiner;
            ExaminerId = examiner.Id;
            Test = test;
            TestId = test.Id;
            StudentGroups = new List<StudentGroup>();
        }

        public void AddStudentGroup(StudentGroup studentGroup)
        {
            StudentGroups.Add(studentGroup);
        }

    }
}

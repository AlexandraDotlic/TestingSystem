using System.Collections.Generic;

namespace Core.Domain.Entites
{
    public class Group
    {
        /// <summary>
        /// Id
        /// </summary>
        public short Id { get; private set; }
        /// <summary>
        /// Naziv
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Referenca na ispitivaca koji je napravio grupu
        /// </summary>
        public int ExaminerId { get; private set; }
        public Examiner Examiner { get; private set; }
        /// <summary>
        /// Kolekcija studenata koji pripadaju datoj grupi
        /// </summary>
        public ICollection<Student> Students { get; private set; }

        private Group()
        {
            Students = new List<Student>();
        }

        public Group(string title, Examiner examiner)
        {
            Title = title;
            Examiner = examiner;
            ExaminerId = examiner.Id;

            Students = new List<Student>();
        }

        public void AddStudentToGroup(Student student)
        {
            Students.Add(student);
        }

        public void RemoveStudentFromGroup(Student student)
        {
            Students.Remove(student);
        }

    }
}

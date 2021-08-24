using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Entites
{
    /// <summary>
    /// Klasa koja predstavlja vezu izmedju studenta i testa
    /// </summary>
    public class StudentTest
    {

        /// <summary>
        /// Id Studenta
        /// </summary>
        public int StudentId { get; private set; }
        public Student Student { get; private set; }
        /// <summary>
        /// Id grupe u kojoj je student
        /// </summary>
        public short? StudentGroupId { get; private set; }
        /// <summary>
        /// Id testa koji student polaze
        /// </summary>
        public short TestId { get; private set; }
        public Test Test { get; private set; }
        /// <summary>
        /// Kolekcija pitanja sa testa
        /// </summary>
        public ICollection<StudentTestQuestion> Questions { get; private set; }
        /// <summary>
        /// Maksimalni broj poena na testu
        /// </summary>
        public int Score { get; private set; }
        /// <summary>
        /// Da li je test polozen
        /// </summary>
        public bool IsTestPassed { get; private set; }
        public StudentTest()
        {

        }
        public StudentTest(Student student, Test test, ICollection<StudentTestQuestion> questions)
        {
            Student = student;
            StudentId = student.Id;
            StudentGroupId = student.GroupId;
            Test = test;
            TestId = test.Id;
            Questions = questions;
            Score = questions.Sum(q => q.Score);
            if((test.TestScore / 2) > Score)
            {
                IsTestPassed = false;
            }
            else
            {
                IsTestPassed = true;
            }
        }
    }
}

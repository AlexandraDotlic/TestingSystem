using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Entites
{
    public class StudentTest
    {
        public int StudentId { get; private set; }
        public Student Student { get; private set; }
        public short TestId { get; private set; }
        public Test Test { get; private set; }
        public ICollection<StudentTestQuestion> Questions { get; private set; }
        public int Score { get; private set; }
        public bool IsTestPassed { get; private set; }
        public StudentTest()
        {

        }
        public StudentTest(Student student, Test test, ICollection<StudentTestQuestion> questions)
        {
            Student = student;
            StudentId = student.Id;
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

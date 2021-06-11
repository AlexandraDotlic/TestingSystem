using Core.ApplicationServices.DTOs;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.ApplicationServices
{
    public class StudentService
    {
        private readonly ICoreUnitOfWork UnitOfWork;

        public StudentService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<int> CreateStudent(string firstName, string lastName, string email, string externalId)
        {
            if (string.IsNullOrEmpty(externalId))
            {
                throw new ArgumentNullException($"ExternalId must not be null");
            }
            Student newStudent = new Student(firstName, lastName, email, externalId);
            await UnitOfWork.StudentRepository.Insert(newStudent);
            await UnitOfWork.SaveChangesAsync();
            return newStudent.Id;
        }

        public async Task DeleteStudent(int studentId)
        {
            Student student = await UnitOfWork.StudentRepository.GetById(studentId);
            if (student == null)
            {
                throw new ArgumentNullException($"Student with Id {studentId} doesn't exist.");
            }
            await UnitOfWork.BeginTransactionAsync();
            if (student.GroupId != null)
            {
                Group group = await UnitOfWork.GroupRepository.GetById(student.GroupId);
                group.RemoveStudentFromGroup(student);
                await UnitOfWork.GroupRepository.Update(group);
                await UnitOfWork.SaveChangesAsync();

            }
            await UnitOfWork.StudentRepository.Delete(student);
            await UnitOfWork.SaveChangesAsync();
            await UnitOfWork.CommitTransactionAsync();
        }

        public async Task SetStudentFirstName(string externalId, string firstName)
        {
            if (string.IsNullOrEmpty(externalId))
            {
                throw new ArgumentNullException($"ExternalId must not be null");
            }
            Student student = await UnitOfWork.StudentRepository.GetFirstOrDefaultWithIncludes(s => s.ExternalId == externalId);
            if (student == null)
            {
                throw new ArgumentNullException($"{nameof(Student)} with externalId {externalId} not exist");
            }

            student.SetFirstName(firstName);
            await UnitOfWork.StudentRepository.Update(student);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task SetStudentLastName(string externalId, string lastName)
        {
            if (string.IsNullOrEmpty(externalId))
            {
                throw new ArgumentNullException($"ExternalId must not be null");
            }
            Student student = await UnitOfWork.StudentRepository.GetFirstOrDefaultWithIncludes(s => s.ExternalId == externalId);
            if (student == null)
            {
                throw new ArgumentNullException($"{nameof(Student)} with externalId {externalId} not exist");
            }

            student.SetLastName(lastName);
            await UnitOfWork.StudentRepository.Update(student);
            await UnitOfWork.SaveChangesAsync();

        }

        public async Task<ICollection<StudentDTO>> GetAllStudentsForGroup(short groupId)
        {
            IReadOnlyCollection<Student> students = await UnitOfWork.StudentRepository.SearchByWithIncludes(s => s.GroupId == groupId);

            List<StudentDTO> studentDTOs = students == null || students.Count == 0
                ? null
                : students.Select(s => new StudentDTO(s.Id, s.FirstName, s.LastName, s.GroupId)).ToList();
            return studentDTOs;

        }

        public async Task<ICollection<StudentDTO>> GetAllStudents()
        {
            IReadOnlyCollection<Student> students = await UnitOfWork.StudentRepository.GetAllList();


            List<StudentDTO> studentDTOs = students == null || students.Count == 0
                ? null
                : students.Select(s => new StudentDTO(s.Id, s.FirstName, s.LastName, s.GroupId)).ToList();
            return studentDTOs;

        }


        public async Task<StudentTestScoreDTO> GetStudentTestResult(string studentExternalId, short testId)
        {
            if (string.IsNullOrEmpty(studentExternalId))
            {
                throw new ArgumentNullException($"ExternalId must not be null");
            }
            Student student = await UnitOfWork.StudentRepository.GetFirstOrDefaultWithIncludes(s => s.ExternalId == studentExternalId, s => s.StudentTests);
            if (student == null)
            {
                throw new ArgumentNullException($"{nameof(Student)} with externalId {studentExternalId} not exist");
            }
            Test test = await UnitOfWork.TestRepository.GetById(testId);
            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }
            StudentTest studentTest = student.StudentTests.Where(st => st.TestId == testId).FirstOrDefault();
            if (studentTest == null)
            {
                throw new ArgumentNullException($"{nameof(Student)} did not take the test with id={testId}");
            }
            return new StudentTestScoreDTO
            {
                StudentId = student.Id,
                TestId = test.Id,
                TotalTestScore = test.TestScore,
                StudentTestScore = studentTest.Score
            };

        }

        public async Task<ICollection<StudentTestScoreDTO>> GetStudentResultsForAllTakenTests(string studentExternalId)
        {
            if (string.IsNullOrEmpty(studentExternalId))
            {
                throw new ArgumentNullException($"ExternalId must not be null");
            }
            Student student = await UnitOfWork.StudentRepository.GetFirstOrDefaultWithIncludes(s => s.ExternalId == studentExternalId, s => s.StudentTests);
            if (student == null)
            {
                throw new ArgumentNullException($"{nameof(Student)} with externalId {studentExternalId} not exist");
            }

            ICollection<StudentTest> studentTests = student.StudentTests;
            ICollection<StudentTestScoreDTO> studentTestScoreDTOs = new List<StudentTestScoreDTO>();
            foreach (var studentTest in studentTests)
            {
                var test = await UnitOfWork.TestRepository.GetById(studentTest.TestId);
                studentTestScoreDTOs.Add(new StudentTestScoreDTO
                {
                    StudentId = student.Id,
                    TestId = studentTest.TestId,
                    TestName = studentTest.Test.Title,
                    TotalTestScore = test.TestScore,
                    StudentTestScore = studentTest.Score
                }); ;
            }
            return studentTestScoreDTOs;

        }


        public async Task<string> GetStudentEmail(int studentId)
        {
            Student student = await UnitOfWork.StudentRepository.GetById(studentId);

            if (student == null)
            {
                throw new ArgumentNullException($"{nameof(Student)} with externalId {studentId} not exist");
            }

            return student.GetEmail();
        }

    }
}

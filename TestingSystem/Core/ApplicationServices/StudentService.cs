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
    /// <summary>
    /// Servis studenta
    /// </summary>
    public class StudentService
    {
        private readonly ICoreUnitOfWork UnitOfWork;

        public StudentService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Servisni task za keiranje studenta
        /// </summary>
        /// <param name="firstName">Ime</param>
        /// <param name="lastName">Prezime</param>
        /// <param name="email">Email</param>
        /// <param name="externalId">Eksterni id iz baze za autentifikaciju</param>
        /// <returns>int - id studenta</returns>
        /// <exception cref="ArgumentNullException"></exception>

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

        /// <summary>
        /// Servisni task za brisanje studenta
        /// </summary>
        /// <param name="studentId">Id studenta</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

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

        /// <summary>
        /// Servisni task za postavljanje imena studenta
        /// </summary>
        /// <param name="externalId">Eksterni id iz baze za autentifikaciju</param>
        /// <param name="firstName">Ime koje se postavlja</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

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


        /// <summary>
        /// Servisni task za postavljanje prezimena studenta
        /// </summary>
        /// <param name="externalId">Eksterni id iz baze za autentifikaciju</param>
        /// <param name="lastName">Prezime koje se postavlja</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

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

        /// <summary>
        /// Servisni task koji vraca kolekciju studenata koji pripadaju grupi
        /// </summary>
        /// <param name="groupId">Id grupe</param>
        /// <returns>Kolekcija studenata iz grupe</returns>

        public async Task<ICollection<StudentDTO>> GetAllStudentsForGroup(short groupId)
        {
            IReadOnlyCollection<Student> students = await UnitOfWork.StudentRepository.SearchByWithIncludes(s => s.GroupId == groupId);

            List<StudentDTO> studentDTOs = students == null || students.Count == 0
                ? null
                : students.Select(s => new StudentDTO(s.Id, s.FirstName, s.LastName, s.GroupId)).ToList();
            return studentDTOs;

        }

        /// <summary>
        /// Servisni task koji vraca kolekciju svih studenata iz baze
        /// </summary>
        /// <returns>Kolekcija svih studenata iz baze</returns>
        public async Task<ICollection<StudentDTO>> GetAllStudents()
        {
            IReadOnlyCollection<Student> students = await UnitOfWork.StudentRepository.GetAllList();


            List<StudentDTO> studentDTOs = students == null || students.Count == 0
                ? null
                : students.Select(s => new StudentDTO(s.Id, s.FirstName, s.LastName, s.GroupId)).ToList();
            return studentDTOs;

        }

        /// <summary>
        /// Servisni task koji vraca rezultat sa testa koji je polagao student
        /// </summary>
        /// <param name="studentExternalId">Eksterni id studenta iz baze za autentifikaciju</param>
        /// <param name="testId">id testa</param>
        /// <returns>Podaci o polaganju testa od strane studenta koji ukljucuju njegov rezultat i maksimalni ostvarivi rezultat na tom testu.</returns>
        /// <exception cref="ArgumentNullException"></exception>

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

        /// <summary>
        /// Servisni task koji vraca rezultate studenta na svim testovima koje je polagao
        /// </summary>
        /// <param name="studentExternalId">Eksterni Id studenta iz baze za autentifikaciju</param>
        /// <returns>Kolekcija svih rezultata koje je student ostvario na testovima</returns>
        /// <exception cref="ArgumentNullException"></exception>

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

        /// <summary>
        /// Servisni metod koji vraca email studenta
        /// </summary>
        /// <param name="studentId">Id studenta</param>
        /// <returns>string - email studenta</returns>
        /// <exception cref="ArgumentNullException"></exception>

        public async Task<string> GetStudentEmail(int studentId)
        {
            Student student = await UnitOfWork.StudentRepository.GetById(studentId);

            if (student == null)
            {
                throw new ArgumentNullException($"{nameof(Student)} with id {studentId} not exist");
            }

            return student.Email;
        }

    }
}

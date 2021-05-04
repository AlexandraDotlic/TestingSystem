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

        public async Task<int> CreateStudent(string firstName, string lastName, string accountId)
        {
            if (string.IsNullOrEmpty(accountId))
            {
                throw new ArgumentNullException($"AccountId must not be null");
            }
            Student newStudent = new Student(firstName, lastName, accountId);
            await UnitOfWork.StudentRepository.Insert(newStudent);
            await UnitOfWork.SaveChangesAsync();
            return newStudent.Id;
        }

        public async Task DeleteStudent(int studentId)
        {
            Student student = await UnitOfWork.StudentRepository.GetById(studentId);
            if(student == null)
            {
                throw new ArgumentNullException($"Student with Id {studentId} doesn't exist.");
            }
            await UnitOfWork.BeginTransactionAsync();
            if(student.GroupId != null)
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

        public async Task<ICollection<StudentDTO>> GetAllStudentsForGroup(short groupId)
        {
            IReadOnlyCollection<Student> students = await UnitOfWork.StudentRepository.SearchByWithIncludes(s => s.GroupId == groupId);

            List<StudentDTO> studentDTOs = students == null || students.Count == 0
                ? null
                : students.Select(s => new StudentDTO(s.Id, s.FirstName, s.LastName, s.GroupId)).ToList();
            return studentDTOs;
        }
    }
}

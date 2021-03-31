using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices
{
    public class StudentService
    {
        private readonly ICoreUnitOfWork unitOfWork;

        public StudentService(ICoreUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<int> CreateStudent(string firstName, string lastName, int accountId)
        {
            Student newStudent = new Student(firstName, lastName, accountId);
            await unitOfWork.StudentRepository.Insert(newStudent);
            await unitOfWork.SaveChangesAsync();
            return newStudent.Id;
        }

        public async Task DeleteStudent(int studentId)
        {
            Student studentForDelete = await unitOfWork.StudentRepository.GetById(studentId);
            if(studentForDelete == null)
            {
                throw new ArgumentNullException($"Student with Id {studentId} doesn't exist.");
            }
            await unitOfWork.StudentRepository.Delete(studentForDelete);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

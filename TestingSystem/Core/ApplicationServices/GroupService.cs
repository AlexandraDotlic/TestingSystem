using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices
{
    public class GroupService
    {
        private readonly ICoreUnitOfWork UnitOfWork;
        public GroupService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<short> CreateGroup(string title, int examinerId, short testId)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetById(examinerId);
            Test test = await UnitOfWork.TestRepository.GetById(testId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with Id {examinerId} not exist");
            }
            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }

            Group group = new Group(title, examiner, test);
 
            await UnitOfWork.GroupRepository.Insert(group);
            await UnitOfWork.SaveChangesAsync();
            return group.Id;
        }

        public async Task DeleteGroup(short groupId)
        {
            Group group = await UnitOfWork.GroupRepository.GetById(groupId);
            if (group == null)
            {
                throw new ArgumentNullException($"{nameof(Group)} with Id {groupId} not exist");
            }
            await UnitOfWork.GroupRepository.Delete(group);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task AddStudentToGroup(short groupId, short studentId)
        {
            //Group group = await UnitOfWork.GroupRepository.GetFirstOrDefaultWithIncludes(g => g.Id == groupId, g => g.StudentGroups);
            //Student student = await UnitOfWork.StudentRepository.GetById(studentId);

            //if (group == null)
            //{
            //    throw new ArgumentNullException($"{nameof(Group)} with Id {groupId} not exist");
            //}

            //if (student == null)
            //{
            //    throw new ArgumentNullException($"{nameof(Student)} with Id {studentId} not exist");
            //}

            //Student student = new Student(firstname, lastname);
           // StudentTestQuestion studentGroup = new StudentTestQuestion(group, student);
            //group.AddStudentGroup(studentGroup);
            //await UnitOfWork.GroupRepository.Update(group);
            //await UnitOfWork.SaveChangesAsync();
        }

    }
}

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
            Group group = new Group(title, examinerId, testId);
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
        }

        public async Task AddTestToGroup(short groupId, string title)
        {
            Group group = await UnitOfWork.GroupRepository.GetFirstOrDefaultWithIncludes(g => g.Id == groupId, g => g.Test);
            if (group == null)
            {
                throw new ArgumentNullException($"{nameof(Group)} with Id {groupId} not exist");
            }
            Test test = new Test(title);
            test.AddTestGroup(group);
            await UnitOfWork.GroupRepository.Update(group);
            await UnitOfWork.SaveChangesAsync();
        }


        public async Task AddStudentToGroup(short groupId, string firstname, string lastname, string studentResponse)
        {
            Group group = await UnitOfWork.GroupRepository.GetFirstOrDefaultWithIncludes(g => g.Id == groupId, g => g.StudentGroups);
            if (group == null)
            {
                throw new ArgumentNullException($"{nameof(Group)} with Id {groupId} not exist");
            }
            Student student = new Student(firstname, lastname);
            StudentGroup studentGroup = new StudentGroup(studentResponse);
            group.AddStudentGroup(studentGroup);
            await UnitOfWork.GroupRepository.Update(group);
            await UnitOfWork.SaveChangesAsync();
        }

    }
}

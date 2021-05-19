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
    public class GroupService
    {
        private readonly ICoreUnitOfWork UnitOfWork;
        public GroupService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<short> CreateGroup(string title, string externalExaminerId)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetFirstOrDefaultWithIncludes(e => e.ExternalId == externalExaminerId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with Id {externalExaminerId} not exist");
            }
         
            Group group = new Group(title, examiner);
 
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

        public async Task AddStudentToGroup(short groupId, int studentId)
        {
            Group group = await UnitOfWork.GroupRepository.GetFirstOrDefaultWithIncludes(g => g.Id == groupId, g => g.Students);
            Student student = await UnitOfWork.StudentRepository.GetById(studentId);

            if (group == null)
            {
                throw new ArgumentNullException($"{nameof(Group)} with Id {groupId} not exist");
            }

            if (student == null)
            {
                throw new ArgumentNullException($"{nameof(Student)} with Id {studentId} not exist");
            }
            group.AddStudentToGroup(student);
            await UnitOfWork.GroupRepository.Update(group);
            await UnitOfWork.SaveChangesAsync();
        }


        public async Task<ICollection<GroupDTO>> GetAllGroupsForExaminer(string externalExaminerId)
        {
            IReadOnlyCollection<Group> groups = await UnitOfWork.GroupRepository.SearchByWithIncludes(g => g.Examiner.ExternalId == externalExaminerId, g => g.Examiner);

            List<GroupDTO> groupDTOs = groups == null || groups.Count == 0
                ? null
                : groups.Select(g => new GroupDTO(g.Id, g.Title, g.ExaminerId)).ToList();
            return groupDTOs;
        }


        public async Task SetGroupTitle(short groupId, string title)
        {
            Group group = await UnitOfWork.GroupRepository.GetById(groupId);
            if (group == null)
            {
                throw new ArgumentNullException($"{nameof(Group)} with Id {groupId} not exist");
            }

            group.SetTitle(title);
            await UnitOfWork.GroupRepository.Update(group);
            await UnitOfWork.SaveChangesAsync();

        }


    }
}

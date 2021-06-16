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
    /// Servis grupe
    /// </summary>
    public class GroupService
    {
        private readonly ICoreUnitOfWork UnitOfWork;
        public GroupService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Servisni task za kreiranje grupe od strane ispitivaca
        /// </summary>
        /// <param name="title">Naziv grupe</param>
        /// <param name="externalExaminerId">Eksterni Id ispitivaca</param>
        /// <returns>short - id grupe</returns>
        /// <exception cref="ArgumentNullException"></exception>

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

        /// <summary>
        /// Servisni task za brisanje grupe
        /// </summary>
        /// <param name="groupId">Id grupe</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

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

        /// <summary>
        /// Servisni task za dodavanje studenta u grupu
        /// </summary>
        /// <param name="groupId">Id grupe</param>
        /// <param name="studentId">Id studenta</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

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

        /// <summary>
        /// Servisni task koji vraca sve grupe koje je kreirao jedan ispitivac
        /// </summary>
        /// <param name="externalExaminerId">Eksterni id ispitivaca iz baze za autentifikaciju</param>
        /// <returns>Kolekcija grupa koje je kreirao ispitivac</returns>

        public async Task<ICollection<GroupDTO>> GetAllGroupsForExaminer(string externalExaminerId)
        {
            IReadOnlyCollection<Group> groups = await UnitOfWork.GroupRepository.SearchByWithIncludes(g => g.Examiner.ExternalId == externalExaminerId, g => g.Examiner);

            List<GroupDTO> groupDTOs = groups == null || groups.Count == 0
                ? null
                : groups.Select(g => new GroupDTO(g.Id, g.Title, g.ExaminerId)).ToList();
            return groupDTOs;
        }

        /// <summary>
        /// Servisni task koji vraca sve grupe iz baze
        /// </summary>
        /// <returns>Kolekcija svih grupa iz baze</returns>
        public async Task<ICollection<GroupDTO>> GetAllGroups()
        {
            IReadOnlyCollection<Group> groups = await UnitOfWork.GroupRepository.GetAllList();


            List<GroupDTO> groupDTOs = groups == null || groups.Count == 0
                ? null
                : groups.Select(g => new GroupDTO(g.Id, g.Title, g.ExaminerId)).ToList();

            return groupDTOs;

        }

        /// <summary>
        /// Servisni task koji grupi postavlja naziv
        /// </summary>
        /// <param name="groupId">Id grupe</param>
        /// <param name="title">Naziv grupe</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

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

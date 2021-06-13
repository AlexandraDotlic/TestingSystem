using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using System.Threading.Tasks;

namespace Core.ApplicationServices
{
    public class ExaminerService
    {
        private readonly ICoreUnitOfWork UnitOfWork;

        public ExaminerService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="externalId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> CreateExaminer(string firstName, string lastName, string externalId)
        {
            if (string.IsNullOrEmpty(externalId))
            {
                throw new ArgumentNullException($"ExternalId must not be null");
            }
            Examiner examiner = new Examiner(firstName, lastName, externalId);
            await UnitOfWork.ExaminerRepository.Insert(examiner);
            await UnitOfWork.SaveChangesAsync();
            return examiner.Id;
        }

        public async Task DeleteExaminer(int examinerId)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetById(examinerId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with Id {examinerId} not exist");
            }
            await UnitOfWork.ExaminerRepository.Delete(examiner);
            await UnitOfWork.SaveChangesAsync();
        }


        public async Task SetExaminerFirstName(string externalId, string firstName)
        {
            if (string.IsNullOrEmpty(externalId))
            {
                throw new ArgumentNullException($"ExternalId must not be null");
            }
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetFirstOrDefaultWithIncludes(e => e.ExternalId == externalId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with external Id {externalId} not exist");
            }

            examiner.SetFirstName(firstName);
            await UnitOfWork.ExaminerRepository.Update(examiner);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task SetExaminerLastName(string externalId, string lastName)
        {
            if (string.IsNullOrEmpty(externalId))
            {
                throw new ArgumentNullException($"ExternalId must not be null");
            }
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetFirstOrDefaultWithIncludes(e => e.ExternalId == externalId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with external Id {externalId} not exist");
            }

            examiner.SetLastName(lastName);
            await UnitOfWork.ExaminerRepository.Update(examiner);
            await UnitOfWork.SaveChangesAsync();
        }

    }
}

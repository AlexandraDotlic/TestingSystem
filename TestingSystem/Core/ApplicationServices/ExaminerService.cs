using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using System.Threading.Tasks;

namespace Core.ApplicationServices
{
    /// <summary>
    /// Servis ispitivaca
    /// </summary>
    public class ExaminerService
    {
        private readonly ICoreUnitOfWork UnitOfWork;

        public ExaminerService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        /// <summary>
        /// Servisni task za kreiranje ispitivaca
        /// </summary>
        /// <param name="firstName">Ime ispitivaca</param>
        /// <param name="lastName">Prezime ispitivaca</param>
        /// <param name="externalId">Eksterni Id prema bazi za autentifikaciju</param>
        /// <returns>int - id ispitivaca</returns>
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

        /// <summary>
        /// Servisni task za brisanje ispitivaca
        /// </summary>
        /// <param name="examinerId">Identifikator ispitivaca</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Servisni task za postavljanje imena ispitivaca
        /// </summary>
        /// <param name="externalId">Eksterni identifikator prema bazi za autentifikaciju</param>
        /// <param name="firstName">Ime ispitivaca koje se postavlja</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Servisni task za postavljanje prezimena ispitivaca
        /// </summary>
        /// <param name="externalId">Eksterni identifikator prema bazi za autentifikaciju</param>
        /// <param name="lastName">Prezime koje se postavlja</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

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

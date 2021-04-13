﻿using System;
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

        public async Task<int> CreateExaminer(string firstName, string lastName, string accountId)
        {
            if (string.IsNullOrEmpty(accountId))
            {
                throw new ArgumentNullException($"AccountId must not be null");
            }
            Examiner examiner = new Examiner(firstName, lastName, accountId);
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
    }
}

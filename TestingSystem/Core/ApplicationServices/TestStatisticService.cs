using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices
{
    public class TestStatisticService
    {
        private readonly ICoreUnitOfWork UnitOfWork;
        public TestStatisticService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<long> CreateStatisticForTest(short testId, int examinerId)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetById(examinerId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with Id {examinerId} not exist");
            }
            Test test = await UnitOfWork.TestRepository.GetFirstOrDefaultWithIncludes(t => t.Id == testId, t => t.StudentTests);
            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }

            int numberOfStudentsWhoTookTheTest = test.StudentTests == null ? 0 : test.StudentTests.Count;
            int numberOfStudentsWhoPassedTheTest = 0;

            if (test.StudentTests == null || numberOfStudentsWhoTookTheTest == 0) return 0;

            foreach (var studentTest in test.StudentTests)
            {
                if (studentTest.IsTestPassed)
                    numberOfStudentsWhoPassedTheTest++;
            }

            int percentageOfStudentsWhoPassedTheTest = (int)(((decimal) numberOfStudentsWhoPassedTheTest / numberOfStudentsWhoTookTheTest) * 100);

            TestStatistic testStatistic = new TestStatistic(testId, test.Title, examinerId, percentageOfStudentsWhoPassedTheTest, numberOfStudentsWhoTookTheTest);

            await UnitOfWork.TestStatisticRepository.Insert(testStatistic);
            await UnitOfWork.SaveChangesAsync();

            return testStatistic.Id;
        }
    }
}

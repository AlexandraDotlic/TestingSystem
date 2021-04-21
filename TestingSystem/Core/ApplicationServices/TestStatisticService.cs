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
    public class TestStatisticService
    {
        private readonly ICoreUnitOfWork UnitOfWork;
        public TestStatisticService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<long> CreateStatisticForTest(short testId, int examinerId, short? groupId = null)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetById(examinerId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with Id {examinerId} not exist");
            }
            Test test = await UnitOfWork.TestRepository.GetFirstOrDefaultWithIncludes(t => t.Id == testId,
                    t => t.StudentTests.Where(st => st.StudentGroupId == groupId));
           
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

        public async Task<ICollection<TestStatisticDTO>> GetAllStatisticsForTest(short testId, int examinerId)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetById(examinerId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with Id {examinerId} not exist");
            }
            Test test = await UnitOfWork.TestRepository.GetById(testId);

            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }

            IReadOnlyCollection<TestStatistic> testStatistics = await UnitOfWork.TestStatisticRepository.SearchByWithIncludes(ts => ts.TestId == testId && ts.ExaminerId == examinerId);
            ICollection<TestStatisticDTO> testStatisticDTOs = testStatistics == null || testStatistics.Count == 0 
                ? null
                : testStatistics.Select(ts => new TestStatisticDTO(ts.Id, ts.TestId, ts.TestTitle, ts.ExaminerId, ts.PercentageOfStudentsWhoPassedTheTest, ts.NumberOfStudentsWhoTookTheTest, ts.Date))
                .OrderByDescending(ts => ts.Date)
                .ToList();
            return testStatisticDTOs;
        }

        public async Task<TestStatisticDTO> GetStatisticForTestbyDate(short testId, int examinerId, DateTime date)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetById(examinerId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with Id {examinerId} not exist");
            }
            Test test = await UnitOfWork.TestRepository.GetById(testId);

            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }

            TestStatistic testStatistic = await UnitOfWork.TestStatisticRepository
                .GetFirstOrDefaultWithIncludes(ts => ts.TestId == testId 
                                && ts.ExaminerId == examinerId
                                && ts.Date == date);
            var testStatisticDTO = testStatistic != null
                ? new TestStatisticDTO(
                    testStatistic.Id,
                    testStatistic.TestId,
                    testStatistic.TestTitle,
                    testStatistic.ExaminerId,
                    testStatistic.PercentageOfStudentsWhoPassedTheTest,
                    testStatistic.NumberOfStudentsWhoTookTheTest,
                    testStatistic.Date
                    )
                : null;
            return testStatisticDTO;

        }

    }
}

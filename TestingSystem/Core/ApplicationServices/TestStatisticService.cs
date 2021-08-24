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
    /// Servis statistika za test
    /// </summary>
    public class TestStatisticService
    {
        private readonly ICoreUnitOfWork UnitOfWork;
        public TestStatisticService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Servisni task koji kreira statistiku za sva polaganja testova koje je kreirao odredjeni ispitivac, sa mogucnoscu posmatranja po grupama
        /// </summary>
        /// <param name="testId">Identifikator testa</param>
        /// <param name="externalExaminerId">Eksterni id ispitivaca iz baze za autentifikaciju</param>
        /// <param name="groupId">Identifikator grupe - neobavezan za dodavanje</param>
        /// <returns>long - identifikator kreirane statistike</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<long> CreateStatisticForTest(short testId, string externalExaminerId, short? groupId = null)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetFirstOrDefaultWithIncludes(e => e.ExternalId == externalExaminerId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with externalId {externalExaminerId} not exist");
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

            TestStatistic testStatistic = new TestStatistic(testId, test.Title, examiner.Id, percentageOfStudentsWhoPassedTheTest, numberOfStudentsWhoTookTheTest);

            await UnitOfWork.TestStatisticRepository.Insert(testStatistic);
            await UnitOfWork.SaveChangesAsync();

            return testStatistic.Id;
        }

        /// <summary>
        /// Servisni task koji vraca kolekciju svih statistika za test koji je kreirao ispitivac
        /// </summary>
        /// <param name="testId">Identifikator testa</param>
        /// <param name="externalExaminerId">Eksterni identifikator ispitivaca u bazi za autentifikaciju</param>
        /// <returns>Kolekcija statistika za test</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<ICollection<TestStatisticDTO>> GetAllStatisticsForTest(short testId, string externalExaminerId)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetFirstOrDefaultWithIncludes(e => e.ExternalId == externalExaminerId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with externalId {externalExaminerId} not exist");
            }
            Test test = await UnitOfWork.TestRepository.GetById(testId);

            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }

            IReadOnlyCollection<TestStatistic> testStatistics = await UnitOfWork.TestStatisticRepository.SearchByWithIncludes(ts => ts.TestId == testId && ts.ExaminerId == examiner.Id);
            ICollection<TestStatisticDTO> testStatisticDTOs = testStatistics == null || testStatistics.Count == 0 
                ? null
                : testStatistics.Select(ts => new TestStatisticDTO(ts.Id, ts.TestId, ts.TestTitle, ts.ExaminerId, ts.PercentageOfStudentsWhoPassedTheTest, ts.NumberOfStudentsWhoTookTheTest, ts.Date))
                .OrderByDescending(ts => ts.Date)
                .ToList();
            return testStatisticDTOs;
        }

        /// <summary>
        /// Servisni task koji vraca kolekciju svih statistika za test koji je kreirao ispitivac, za odredjeni datum polaganja
        /// </summary>
        /// <param name="testId">Identifikator testa</param>
        /// <param name="externalExaminerId">Eksterni identifikator ispitivaca</param>
        /// <param name="date">Datum za koji se gleda statistika</param>
        /// <returns>Kolekcija statistika za test polagan odredjenog datuma</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TestStatisticDTO> GetStatisticForTestbyDate(short testId, string externalExaminerId, DateTime date)
        {
            Examiner examiner = await UnitOfWork.ExaminerRepository.GetFirstOrDefaultWithIncludes(e => e.ExternalId == externalExaminerId);
            if (examiner == null)
            {
                throw new ArgumentNullException($"{nameof(Examiner)} with Id {externalExaminerId} not exist");
            }
            Test test = await UnitOfWork.TestRepository.GetById(testId);

            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }

            TestStatistic testStatistic = await UnitOfWork.TestStatisticRepository
                .GetFirstOrDefaultWithIncludes(ts => ts.TestId == testId 
                                && ts.ExaminerId == examiner.Id
                                && ts.Date.Date == date.Date);
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

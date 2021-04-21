using Applications.WebClient.DTOs;
using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Applications.WebClient.Responses;
using Core.ApplicationServices;
using Core.Domain.Services.External.JobService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestStatisticController : ControllerBase
    {
        private readonly TestStatisticService TestStatisticService;
        private readonly IJobService JobService;
        private readonly QuestionService QuestionService;
        private readonly ILogger<TestStatisticController> Logger;
        private readonly int examinerId = 1; //temp
        private readonly int studentId = 1;

        public TestStatisticController(
            TestStatisticService testStatisticService,
            QuestionService questionService,
            IJobService jobService,
            ILogger<TestStatisticController> logger)
        {
            TestStatisticService = testStatisticService;
            QuestionService = questionService;
            JobService = jobService;
            Logger = logger;
        }

        [HttpPost]
        [Route("CreateTestStatistic")]
        public async Task<IActionResult> CreateTestStatistic(CreateTestStatisticRequest createTestStatisticRequest)
        {
            try
            {
                await JobService.EnqueueJob<TestStatisticService>(ts => ts.CreateStatisticForTest(createTestStatisticRequest.TestId, examinerId, createTestStatisticRequest.GroupId));
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        [HttpPost]
        [Route("ScheduleTestStatisticCreation")]
        public async Task<IActionResult> ScheduleTestStatisticCreation(CreateTestStatisticRequest createTestStatisticRequest)
        {
            try
            {
                var delay = (TimeSpan)createTestStatisticRequest.CreationDate?.Subtract(DateTime.Now);
                if(delay.TotalSeconds < 0)
                {
                    throw new InvalidOperationException("Creation date cannot be erlier than today");
                }
                await JobService.ScheduleJob<TestStatisticService>(ts => ts.CreateStatisticForTest(createTestStatisticRequest.TestId, examinerId, createTestStatisticRequest.GroupId), delay);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }


        [HttpPost]
        [Route("ScheduleMonthlyTestStatisticCreation")]
        public async Task<IActionResult> ScheduleMonthlyTestStatisticCreation(CreateTestStatisticRequest createTestStatisticRequest)
        {
            try
            {
               
                await JobService.CreateMonthlyRecurringJob<TestStatisticService>(ts => ts.CreateStatisticForTest(createTestStatisticRequest.TestId, examinerId, createTestStatisticRequest.GroupId));
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        [HttpGet]
        [Route("GetAllStatisticsForTest/{testId}")]
        public async Task<ActionResult<GetAllStatisticsForTestResponse>> GetAllStatisticsForTest(short testId)
        {
            try
            {
                System.Collections.Generic.ICollection<Core.ApplicationServices.DTOs.TestStatisticDTO> testStatistics = await TestStatisticService.GetAllStatisticsForTest(testId, examinerId);
                var response = testStatistics == null 
                    ? null
                    : new GetAllStatisticsForTestResponse
                {
                    TestStatistics = testStatistics.Select(ts => new TestStatisticDTO(ts.Id, ts.TestId, ts.TestTitle, ts.PercentageOfStudentsWhoPassedTheTest, ts.NumberOfStudentsWhoTookTheTest, ts.Date)).ToList()
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        [HttpGet]
        [Route("GetStatisticForTestByDate")]
        public async Task<ActionResult<TestStatisticDTO>> GetStatisticForTestByDate(short testId, DateTime date)
        {
            try
            {
                Core.ApplicationServices.DTOs.TestStatisticDTO testStatistic = await TestStatisticService.GetStatisticForTestbyDate(testId, examinerId, date);

                var response = testStatistic == null 
                    ? null
                    : new TestStatisticDTO(testStatistic.Id, testStatistic.TestId, testStatistic.TestTitle, testStatistic.PercentageOfStudentsWhoPassedTheTest, testStatistic.NumberOfStudentsWhoTookTheTest, testStatistic.Date);

                return Ok(response);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

    }
}

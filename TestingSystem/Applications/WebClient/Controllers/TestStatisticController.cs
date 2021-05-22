using Applications.WebClient.DTOs;
using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Applications.WebClient.Responses;
using Core.ApplicationServices;
using Core.Domain.Services.External.JobService;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> CreateTestStatistic(CreateTestStatisticRequest createTestStatisticRequest)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                await JobService.EnqueueJob<TestStatisticService>(ts => ts.CreateStatisticForTest(createTestStatisticRequest.TestId, currentUserId, createTestStatisticRequest.GroupId));
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
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> ScheduleTestStatisticCreation(CreateTestStatisticRequest createTestStatisticRequest)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                var delay = (TimeSpan)createTestStatisticRequest.CreationDate?.Subtract(DateTime.Now);
                if(delay.TotalSeconds < 0)
                {
                    throw new InvalidOperationException("Creation date cannot be erlier than today");
                }
                await JobService.ScheduleJob<TestStatisticService>(ts => ts.CreateStatisticForTest(createTestStatisticRequest.TestId, currentUserId, createTestStatisticRequest.GroupId), delay);
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
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> ScheduleMonthlyTestStatisticCreation(CreateTestStatisticRequest createTestStatisticRequest)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                await JobService.CreateMonthlyRecurringJob<TestStatisticService>(ts => ts.CreateStatisticForTest(createTestStatisticRequest.TestId, currentUserId, createTestStatisticRequest.GroupId));
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
        [Authorize(Policy = "IsExaminer")]
        public async Task<ActionResult<GetAllStatisticsForTestResponse>> GetAllStatisticsForTest(short testId)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                System.Collections.Generic.ICollection<Core.ApplicationServices.DTOs.TestStatisticDTO> testStatistics = await TestStatisticService.GetAllStatisticsForTest(testId, currentUserId);
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
        [Authorize(Policy = "IsExaminer")]
        public async Task<ActionResult<TestStatisticDTO>> GetStatisticForTestByDate(short testId, DateTime date)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                Core.ApplicationServices.DTOs.TestStatisticDTO testStatistic = await TestStatisticService.GetStatisticForTestbyDate(testId, currentUserId, date);

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

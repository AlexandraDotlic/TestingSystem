using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Core.ApplicationServices;
using Core.Domain.Services.External.JobService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

    }
}

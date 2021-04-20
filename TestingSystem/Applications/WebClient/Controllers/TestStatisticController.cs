using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Core.ApplicationServices;
using Core.Domain.Services.External.JobService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        //Create statistic for test
        [HttpPost]
        [Route("CreateTestStatistic")]
        public async Task<IActionResult> CreateTestStatistic(CreateTestStatisticRequest createTestStatisticRequest)
        {
            try
            {
                await JobService.EnqueueJob<TestStatisticService>(ts => ts.CreateStatisticForTest(createTestStatisticRequest.TestId, examinerId));
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        //Create statistic for test with delay
        [HttpPost]
        [Route("CreateTestStatisticWithDelay")]
        public async Task<IActionResult> CreateTestStatisticWithDelay(CreateTestStatisticRequest createTestStatisticRequest)
        {
            try
            {
                var delay = (TimeSpan)createTestStatisticRequest.CreationDate?.Subtract(DateTime.Now);
                if(delay.TotalSeconds < 0)
                {
                    throw new InvalidOperationException("Creation date must be later than today");
                }
                await JobService.ScheduleJob<TestStatisticService>(ts => ts.CreateStatisticForTest(createTestStatisticRequest.TestId, examinerId), delay);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        //Schedule creating statistics periodicly???
    }
}

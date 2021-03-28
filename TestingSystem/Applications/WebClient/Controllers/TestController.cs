using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Core.ApplicationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Applications.WebClient.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestService TestService;
        private readonly ILogger<TestController> Logger;

        public TestController(
            TestService testService,
            ILogger<TestController> logger)
        {
            TestService = testService;
            Logger = logger;
        }

        [HttpPost]
        [Route("CreateTest")]
        public async Task<IActionResult> CreateTest(CreateTestRequest createTestRequest)
        {
            try
            {
                var result = await TestService.CreateTest(createTestRequest.Title);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        [HttpPost]
        [Route("AddQuestionToTest")]
        public async Task<IActionResult> AddQuestionToTest(AddQuestionToTestRequest addQuestionToTestRequest)
        {
            try
            {
                await TestService.AddQuestionToTest(addQuestionToTestRequest.TestId, addQuestionToTestRequest.QuestionId);
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

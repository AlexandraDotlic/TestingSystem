using Applications.WebClient.DTOs;
using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Applications.WebClient.Responses;
using Core.ApplicationServices;
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
    public class TestController : ControllerBase
    {
        private readonly TestService TestService;
        private readonly QuestionService QuestionService;
        private readonly ILogger<TestController> Logger;
        private readonly int examinerId = 1; //temp
        private readonly int studentId = 1;

        public TestController(
            TestService testService,
            QuestionService questionService,
            ILogger<TestController> logger)
        {
            TestService = testService;
            QuestionService = questionService;
            Logger = logger;
        }

        [HttpPost]
        [Route("CreateTest")]
        public async Task<IActionResult> CreateTest(CreateTestRequest createTestRequest)
        {
            try
            {
                var result = await TestService.CreateTest(examinerId, createTestRequest.Title, createTestRequest.StartDate, createTestRequest.EndDate);
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
                ICollection<Tuple<string, bool>> answerOptions = new List<Tuple<string, bool>>();
                foreach (var item in addQuestionToTestRequest.AnswerOptions)
                {
                    answerOptions.Add(new Tuple<string, bool>(item.OptionText, item.IsCorrect));
                }
                await TestService.AddQuestionToTest(addQuestionToTestRequest.TestId, addQuestionToTestRequest.QuestionText, answerOptions);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        [HttpGet]
        [Route("GetQuestionAndAnswers")]
        public async Task<IActionResult> GetQuestionAndAnswers(GetQuestionAndAnswersRequest requestBody)
        {
            try
            {
                ICollection<Core.ApplicationServices.DTOs.QuestionDTO> questions = await QuestionService.GetAllQuestionsForTest(requestBody.testId);
                var response = questions.Where(e => e.Id == requestBody.questionId).FirstOrDefault();
                return Ok(response);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        [HttpGet]
        [Route("GetAllQuestionsForTest/{testId}")]
        public async Task<ActionResult<GetAllQuestionsForTestResponse>> GetAllQuestionsForTest(short testId)
        {
            try
            {
                ICollection<Core.ApplicationServices.DTOs.QuestionDTO> questions = await QuestionService.GetAllQuestionsForTest(testId);
                var response = new GetAllQuestionsForTestResponse
                {
                    Questions = questions.Select(q => new QuestionDTO(q.Id, q.QuestionText, q.QuestionScore, q.AnswerOptions)).ToList()
                };
                return response;

            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }


        [HttpPost]
        [Route("TakeTheTest")]
        public async Task<ActionResult<TakeTheTestResponse>> TakeTheTest(TakeTheTestRequest takeTheTestRequest)
        {
            try
            {
                List<Tuple<int, ICollection<string>>> testResponse = takeTheTestRequest.Response.Select(r => new Tuple<int, ICollection<string>>(r.QuestionId, r.Responses)).ToList();
                Core.ApplicationServices.DTOs.StudentTestScoreDTO studentTestScore = await TestService.TakeTheTest(takeTheTestRequest.TestId, studentId, testResponse);
                var response = new TakeTheTestResponse
                {
                    StudentScore = studentTestScore.StudentTestScore,
                    TotalTestScore = studentTestScore.TotalTestScore
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        [HttpPost]
        [Route("ActivateTest/{testId}")]
        public async Task<IActionResult> ActivateTest(short testId)
        {
            try
            {
                await TestService.ActivateTest(testId);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        [HttpPost]
        [Route("DectivateTest/{testId}")]
        public async Task<IActionResult> DectivateTest(short testId)
        {
            try
            {
                await TestService.DectivateTest(testId);
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

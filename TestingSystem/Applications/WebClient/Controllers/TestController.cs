using Applications.WebClient.DTOs;
using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Applications.WebClient.Responses;
using Auth.Domain.Entities;
using Core.ApplicationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Applications.WebClient.Controllers
{
    /// <summary>
    /// Kontroler klasa testa
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestService TestService;
        private readonly QuestionService QuestionService;
        private readonly ILogger<TestController> Logger;


        public TestController(
            TestService testService,
            QuestionService questionService,
            ILogger<TestController> logger)
        {
            TestService = testService;
            QuestionService = questionService;
            Logger = logger;
        }

        /// <summary>
        /// Kontroler za kreiranje testa
        /// </summary>
        /// <param name="createTestRequest"></param>
        [HttpPost]
        [Route("CreateTest")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> CreateTest(CreateTestRequest createTestRequest)
        {
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            try
            {
                var result = await TestService.CreateTest(currentUserId, createTestRequest.Title, createTestRequest.StartDate);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Kontroler za vracanje svih kreiranih testova iz baze
        /// </summary>
        [HttpGet]
        [Route("GetAllTests")]
        public async Task<IActionResult> GetAllTests()
        {
            try
            {
                var result = await TestService.GetAllTests();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Kontroler za vracanje svih dostupnih testova za polaganje datog studenta
        /// </summary>
        [HttpGet]
        [Route("GetAllAvailableTestsForStudent")]
        [Authorize(Policy = "IsStudent")]
        public async Task<ActionResult<GetAllAvailableTestsForStudentResponse>> GetAllAvailableTestsForStudent()
        {
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            try
            {
                ICollection<Core.ApplicationServices.DTOs.TestDTO> result = await TestService.GetAllAvailableTestsForStudent(currentUserId);
                var response = new GetAllAvailableTestsForStudentResponse
                {
                    Tests = result.Select(r => new TestDTO(r.Id, r.Title, r.ExaminerId, r.StartDate, r.IsActive, r.TestScore)).ToList()
                };
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }


        /// <summary>
        /// Kontroler za dodavanje pitanja u test
        /// </summary>
        /// <param name="addQuestionToTestRequest"></param>
        [HttpPost]
        [Route("AddQuestionToTest")]
        [Authorize(Policy = "IsExaminer")]
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

        /// <summary>
        /// Kontroler za uklanjanje pitanja sa testa
        /// </summary>
        /// <param name="removeQuestionFromTestRequest"></param>
        [HttpPost]
        [Route("RemoveQuestionFromTest")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> RemoveQuestionFromTest(RemoveQuestionFromTestRequest removeQuestionFromTestRequest)
        {
            try
            {
                await TestService.RemoveQuestionFromTest(removeQuestionFromTestRequest.TestId, removeQuestionFromTestRequest.QuestionId);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Kontroler za vracanje pitanja sa njegovim odgovorima
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="testId"></param>
        [HttpGet]
        [Route("GetQuestionAndAnswers")]
        [Authorize]
        public async Task<IActionResult> GetQuestionAndAnswers(short questionId, short testId)
        {
            try
            {
                ICollection<Core.ApplicationServices.DTOs.QuestionDTO> questions = await QuestionService.GetAllQuestionsForTest(testId);
                var response = questions.Where(e => e.Id == questionId).FirstOrDefault();
                return Ok(response);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Kontroler za vracanje svih pitanja sa jednog testa
        /// </summary>
        /// <param name="testId"></param>
        [HttpGet]
        [Route("GetAllQuestionsForTest/{testId}")]
        [Authorize]
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


        /// <summary>
        /// Kontroler koji vraca rezulat polaganja testa od strane studenta
        /// </summary>
        /// <param name="takeTheTestRequest"></param>
        [HttpPost]
        [Route("TakeTheTest")]
        [Authorize(Policy = "IsStudent")]
        public async Task<ActionResult<TakeTheTestResponse>> TakeTheTest(TakeTheTestRequest takeTheTestRequest)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                List<Tuple<int, ICollection<string>>> testResponse = takeTheTestRequest.Response.Select(r => new Tuple<int, ICollection<string>>(r.QuestionId, r.Responses)).ToList();
                Core.ApplicationServices.DTOs.StudentTestScoreDTO studentTestScore = await TestService.TakeTheTest(takeTheTestRequest.TestId, currentUserId, testResponse);
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

        /// <summary>
        /// Kontroler za aktivaciju neaktivnog testa
        /// </summary>
        /// <param name="testId"></param>
        [HttpPost]
        [Route("ActivateTest/{testId}")]
        [Authorize(Policy = "IsExaminer")]
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

        /// <summary>
        /// Kontroler za deaktivaciju aktivnog testa
        /// </summary>
        /// <param name="testId"></param>
        [HttpPost]
        [Route("DeactivateTest/{testId}")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> DeactivateTest(short testId)
        {
            try
            {
                await TestService.DeactivateTest(testId);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Kontroler za izmenu datuma pocetka mogucnosti polaganja testa
        /// </summary>
        /// <param name="changeStartDateRequest"></param>
        [HttpPost]
        [Route("ChangeStartDate")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> ChangeStartDate(ChangeStartDateRequest changeStartDateRequest)
        {
            try
            {
                await TestService.ChangeStartDate(changeStartDateRequest.TestId, changeStartDateRequest.StartDate);
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

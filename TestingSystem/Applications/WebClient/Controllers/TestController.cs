﻿using Applications.WebClient.DTOs;
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
        [Route("GetAllQuestionsForTest/{testId}")]
        public async Task<ActionResult<GetAllQuestionsForTestResponse>> GetAllQuestionsForTest(short testId)
        {
            try
            {
                ICollection<Core.ApplicationServices.DTOs.QuestionDTO> questions = await QuestionService.GetAllQuestionsForTest(testId);
                var response = new GetAllQuestionsForTestResponse
                {
                    Questions = questions.Select(q => new QuestionDTO(q.TestId, q.QuestionText, q.QuestionScore, q.AnswerOptions)).ToList()
                };
                return response;

            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }


    }
}

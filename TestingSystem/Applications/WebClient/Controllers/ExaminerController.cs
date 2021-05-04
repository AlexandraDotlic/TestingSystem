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
    public class ExaminerController : ControllerBase
    {
        private readonly ExaminerService ExaminerService;
        private readonly TestService TestService;
        private readonly ILogger<ExaminerController> Logger;

        public ExaminerController(
           ExaminerService examinerService,
           TestService testService,
           ILogger<ExaminerController> logger)
        {
            ExaminerService = examinerService;
            TestService = testService;
            Logger = logger;
        }

        [HttpGet]
        [Route("GetAllTestsForExaminer/{examinerId}")]
        public async Task<ActionResult<GetAllTestsForExaminerResponse>> GetAllTestsForExaminer(int examinerId)
        {
            try
            {
                ICollection<Core.ApplicationServices.DTOs.TestDTO> tests = await TestService.GetAllTestsForExaminer(examinerId);

                var response = new GetAllTestsForExaminerResponse
                {
                    Tests = tests.Select(t => new TestDTO(t.Id, t.Title, t.ExaminerId, t.StartDate, t.IsActive, t.TestScore)).ToList()

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
        [Route("SetExaminerFirstName/{examinerId}")]
        public async Task<IActionResult> SetExaminerFirstName(int examinerId, string firstName)
        {
            try
            {
                await ExaminerService.SetExaminerFirstName(examinerId, firstName);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        [HttpPost]
        [Route("SetExaminerLastName/{examinerId}")]
        public async Task<IActionResult> SetExaminerLastName(int examinerId, string lastName)
        {
            try
            {
                await ExaminerService.SetExaminerLastName(examinerId, lastName);
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
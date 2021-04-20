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
        private readonly GroupService GroupService;
        private readonly ILogger<ExaminerController> Logger;

        public ExaminerController(
           ExaminerService examinerService,
           TestService testService,
           GroupService groupsService,
           ILogger<ExaminerController> logger)
        {
            ExaminerService = examinerService;
            TestService = testService;
            GroupService = groupsService;
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
                    Tests = tests.Select(t => new TestDTO(t.Id, t.Title, t.ExaminerId, t.StartDate, t.EndDate, t.IsActive, t.TestScore)).ToList()

                };
                return response;

            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }


        [HttpGet]
        [Route("GetAllGroupsForExaminer/{examinerId}")]
        public async Task<ActionResult<GetAllGroupsForExaminerResponse>> GetAllGroupsForExaminer(int examinerId)
        {
            try
            {
                ICollection<Core.ApplicationServices.DTOs.GroupDTO> groups = await GroupService.GetAllGroupsForExaminer(examinerId);

                var response = new GetAllGroupsForExaminerResponse
                {
                    Groups = groups.Select(g => new GroupDTO(g.Id, g.Title, g.ExaminerId)).ToList()

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
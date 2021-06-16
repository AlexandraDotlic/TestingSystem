using Applications.WebClient.DTOs;
using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Applications.WebClient.Responses;
using Core.ApplicationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Controllers
{
    /// <summary>
    /// Kontroler klasa ispitivaca
    /// </summary>
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


        /// <summary>
        /// Kontroler koji vraca sve testove koje je kreirao ispitivac
        /// </summary>
        [HttpGet]
        [Route("GetAllTestsForExaminer")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<ActionResult<GetAllTestsForExaminerResponse>> GetAllTestsForExaminer()
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                ICollection<Core.ApplicationServices.DTOs.TestDTO> tests = await TestService.GetAllTestsForExaminer(currentUserId);

                var response = new GetAllTestsForExaminerResponse
                {
                    Tests = tests == null || tests.Count == 0 
                    ?  new List<TestDTO>()
                    : tests.Select(t => new TestDTO(t.Id, t.Title, t.ExaminerId, t.StartDate, t.IsActive, t.TestScore)).ToList()

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
        /// Kontroler za postavljanje imena ispitivaca
        /// </summary>
        /// <param name="firstName"></param>
        [HttpPost]
        [Route("SetExaminerFirstName")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> SetExaminerFirstName(string firstName)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                await ExaminerService.SetExaminerFirstName(currentUserId, firstName);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Kontroler za postavljanje prezimena ispitivaca
        /// </summary>
        /// <param name="lastName"></param>
        [HttpPost]
        [Route("SetExaminerLastName")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> SetExaminerLastName(string lastName)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                await ExaminerService.SetExaminerLastName(currentUserId, lastName);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }
    

        /// <summary>
        /// Kontroler koji vraca sve grupe koje je kreirao ispitivac
        /// </summary>
        [HttpGet]
        [Route("GetAllGroupsForExaminer")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<ActionResult<GetAllGroupsForExaminerResponse>> GetAllGroupsForExaminer()
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                ICollection<Core.ApplicationServices.DTOs.GroupDTO> groups = await GroupService.GetAllGroupsForExaminer(currentUserId);

                var response = new GetAllGroupsForExaminerResponse
                {
                    Groups = groups == null || groups.Count == 0
                    ? new List<GroupDTO>()
                    : groups.Select(g => new GroupDTO(g.Id, g.Title, g.ExaminerId)).ToList()

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
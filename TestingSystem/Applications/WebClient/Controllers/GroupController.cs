using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Core.ApplicationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Applications.WebClient.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupController : ControllerBase

    {

        private readonly GroupService GroupService;
        private readonly ILogger<GroupController> Logger;
        private readonly int examinerId = 1; //temp

        public GroupController(
            GroupService groupService,
            ILogger<GroupController> logger)
        {
            GroupService = groupService;
            Logger = logger;
        }

        [HttpPost]
        [Route("CreateGroup")]
        public async Task<IActionResult> CreateGroup(CreateGroupRequest createGroupRequest)
        {
            try
            {
                var result = await GroupService.CreateGroup(createGroupRequest.Title, examinerId);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }


        [HttpPost]
        [Route("AddStudentToGroup")]
        public async Task<IActionResult> AddStudentToGroup(AddStudentToGroupRequest addStudentToGroupRequest)
        {
            try
            {
                await GroupService.AddStudentToGroup(addStudentToGroupRequest.GroupId, addStudentToGroupRequest.StudentId);
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

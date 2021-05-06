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
    public class GroupController : ControllerBase

    {

        private readonly GroupService GroupService;
        private readonly StudentService StudentService;
        private readonly ILogger<GroupController> Logger;
        private readonly int examinerId = 1; //temp

        public GroupController(
            GroupService groupService,
            StudentService studentService,
            ILogger<GroupController> logger)
        {
            GroupService = groupService;
            StudentService = studentService;
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

        [HttpGet]
        [Route("GetAllStudentsForGroup/{groupId}")]
        public async Task<ActionResult<GetAllStudentsForGroupResponse>> GetAllStudentsForGroup(short groupId)
        {
            try
            {
                ICollection<Core.ApplicationServices.DTOs.StudentDTO> students = await StudentService.GetAllStudentsForGroup(groupId);
                var response = new GetAllStudentsForGroupResponse
                {
                    Students = students.Select(s => new StudentDTO(s.Id, s.FirstName, s.LastName, s.GroupId)).ToList()
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
        [Route("SetGroupTitle/{groupId}")]
        public async Task<IActionResult> SetGroupTitle(short groupId, string title)
        {
            try
            {
                await GroupService.SetGroupTitle(groupId, title);
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

    using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly ICurrentUserService _currentUser;

        public RequestController(IRequestService requestService, ICurrentUserService currentUser)
        {
            _requestService = requestService;
            _currentUser = currentUser;
        }

        // GET: api/request
        //[Authorize(Roles = "Consultant")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequests()
        {
            var requests = await _requestService.GetFormsAsync();
            return Ok(requests);
        }

        // GET: api/request/{id}
        //[Authorize(Roles = "Consultant")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestDto>> GetRequest(int id)
        {
            var request = await _requestService.GetFormByIdAsync(id);
            if (request == null)
                return NotFound();

            return Ok(request);
        }

        // GET: api/request/my-requests
        [HttpGet("my-requests")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetCustomerRequests()
        {
            var userIdClaim = _currentUser.UserId;

            if (userIdClaim == null)
                return Unauthorized();

            var requests = await _requestService.GetRequestsByCustomerIdAsync(userIdClaim);
            return Ok(requests);
        }

        // POST: api/request
        //[HttpPost]
        //public async Task<ActionResult<RequestDto>> CreateRequest(RequestCreateDto createDto)
        //{
        //    var created = await _requestService.CreateFormAsync(createDto);
        //    return CreatedAtAction(nameof(GetRequest), new { id = created.FormId }, created);
        //}

        // POST: api/request/draft
        [HttpPost]
        public async Task<ActionResult<RequestDto>> CreateDraftRequest(RequestCreateDto draftDto)
        {
            var userIdClaim = _currentUser.UserId;

            if (userIdClaim == null)
                return Unauthorized();

            var draft = await _requestService.CreateDraftRequestAsync(userIdClaim, draftDto);
            return Ok(draft);
        }

        // POST: api/request/{id}/cancel
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelRequest(int id)
        {
            var userIdClaim = _currentUser.UserId;

            if (userIdClaim == null)
                return Unauthorized();

            var success = await _requestService.CancelRequest(userIdClaim, id);
            if (!success)
                return BadRequest("Cancellation is only possible when the request is in 'Draft' status.");

            return Ok();
        }

        // PUT: api/request/{id}
        //[Authorize(Roles = "Consultant")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, RequestCreateDto updateDto)
        {
            var updated = await _requestService.UpdateFormAsync(id, updateDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }
    }
}

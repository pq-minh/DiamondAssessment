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
            var requests = await _requestService.GetAllAsync();
            return Ok(requests);
        }

        // GET: api/request/{id}
        //[Authorize(Roles = "Consultant")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestDto>> GetRequest(int id)
        {
            var request = await _requestService.GetRequestByIdAsync(id);
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

        // For Consultant to creating a draft request
        [HttpPost("Create-request")]
        public async Task<IActionResult> CreateRequest([FromBody] CreateRequestDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdRequest = await _requestService.CreateRequestAsync(createDto);

            return CreatedAtAction(nameof(GetRequest), new { id = createdRequest.RequestId }, createdRequest);
        }

        // PUT: api/request/{id}
        //[Authorize(Roles = "Consultant")]
        [HttpPut("Update-request/{id}")]
        public async Task<IActionResult> UpdateRequest(int id, CreateRequestDto updateDto)
        {
            var updated = await _requestService.UpdateFormAsync(id, updateDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/request/Delete/{id}
        [HttpDelete("Delete-request/{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var result = await _requestService.DeleteRequestAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // PATCH: api/request/Cancel-request/{id}
        [HttpPatch("Cancel-request/{id}")]
        public async Task<IActionResult> CancelRequest(int id)
        {
            var result = await _requestService.CancelRequestAsync(id);

            if (!result)
                return NotFound();

            return Ok(new { message = "Request đã được hủy thành công." });
        }

        // Draft -----------------------------------------------------------------------------------------------------------------------------------------

        // For Customer to creating a draft request
        [HttpPost("Create-draft")]
        public async Task<ActionResult<RequestDto>> CreateDraftRequest(CreateDraftRequestDto draftDto)
        {
            var userIdClaim = _currentUser.UserId; // sẽ null nếu không có token


            if (userIdClaim == null)
                return Unauthorized();

            var draft = await _requestService.CreateDraftRequestAsync(userIdClaim, draftDto);
            return Ok(draft);
        }

        [HttpPut("SubmitRequest/{requestId}")]
        public async Task<IActionResult> SubmitRequest(int requestId)
        {
            var result = await _requestService.SubmitRequestAsync(requestId);
            if (!result) return BadRequest("Cannot submit request.");
            return Ok("Request submitted successfully.");
        }

        // PATCH: api/request/cancel-draft/{id}
        [HttpPatch("cancel-draft/{id}")]
        public async Task<IActionResult> CancelDraftRequest(int id)
        {
            var userIdClaim = _currentUser.UserId;
            if (userIdClaim == null)
                return Unauthorized();

            var success = await _requestService.CancelRequest(userIdClaim, id);
            if (!success)
                return BadRequest("Cancellation is only possible when the request is in 'Draft' status.");

            return Ok("Draft request cancelled successfully.");
        }

    }
}

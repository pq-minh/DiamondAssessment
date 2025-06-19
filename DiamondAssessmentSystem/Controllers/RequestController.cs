    using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
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

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET: api/request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequests()
        {
            var requests = await _requestService.GetFormsAsync();
            return Ok(requests);
        }

        // GET: api/request/{id}
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
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetMyRequests()
        {
            var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim, out int customerId))
                return Unauthorized();

            var requests = await _requestService.GetRequestsByCustomerIdAsync(customerId);
            return Ok(requests);
        }

        // POST: api/request
        [HttpPost]
        public async Task<ActionResult<RequestDto>> CreateRequest(RequestCreateDto createDto)
        {
            var created = await _requestService.CreateFormAsync(createDto);
            return CreatedAtAction(nameof(GetRequest), new { id = created.FormId }, created);
        }

        // POST: api/request/draft
        [HttpPost("draft")]
        public async Task<ActionResult<RequestDto>> CreateDraftRequest(RequestCreateDto draftDto)
        {
            var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int customerId))
                return Unauthorized();

            draftDto.CustomerId = customerId; // gán customerId từ JWT
            var draft = await _requestService.CreateDraftRequestAsync(draftDto);
            return Ok(draft);
        }

        // POST: api/request/{id}/cancel
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelRequest(int id)
        {
            var success = await _requestService.CancelRequestAsync(id);
            if (!success)
                return BadRequest("Cancellation is only possible when the request is in 'Draft' status.");

            return Ok();
        }

        // PUT: api/request/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, RequestCreateDto updateDto)
        {
            var updated = await _requestService.UpdateFormAsync(id, updateDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/request/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var deleted = await _requestService.DeleteFormAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

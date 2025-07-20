using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;
        private readonly ICurrentUserService _currentUser;

        public ResultController(IResultService resultService, ICurrentUserService currentUser)
        {
            _resultService = resultService;
            _currentUser = currentUser;
        }

        // GET: api/Result
        //[Authorize(Roles = "Consultant")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultDto>>> GetResults()
        {
            var results = await _resultService.GetResultsAsync();
            return Ok(results);
        }

        [HttpGet("cust")]
        public async Task<ActionResult<IEnumerable<ResultDto>>> GetPersonalResults()
        {

            var userId = _currentUser.UserId;

            if (userId == null)
                return Unauthorized();

            var results = await _resultService.GetPersonalResults(userId);
            return Ok(results);
        }

        // GET: api/Result/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultDto>> GetResult(int id)
        {
            var result = await _resultService.GetResultByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET
        [HttpGet("diamond/{diamondId:int}")]
        public async Task<IActionResult> GetResultsByDiamondId(int diamondId)
        {
            var results = await _resultService.GetResultsByDiamondIdAsync(diamondId);
            if (results == null || !results.Any())
            {
                return NotFound($"No results found for DiamondId = {diamondId}");
            }

            return Ok(results);
        }


        // POST: api/Result
        [HttpPost]
        public async Task<ActionResult<ResultDto>> PostResult(ResultCreateDto resultCreateDto)
        {
            var createdResult = await _resultService.CreateResultAsync(resultCreateDto);
            return CreatedAtAction(nameof(GetResult), new { id = createdResult.ResultId }, createdResult);
        }

        // PUT: api/Result/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(int id, ResultCreateDto resultCreateDto)
        {
            var updated = await _resultService.UpdateResultAsync(id, resultCreateDto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Result/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var deleted = await _resultService.DeleteResultAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

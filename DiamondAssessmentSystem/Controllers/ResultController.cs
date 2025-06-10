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

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        // GET: api/Result
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultDto>>> GetResults()
        {
            var results = await _resultService.GetResultsAsync();
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

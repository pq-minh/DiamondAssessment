using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _formService;

        public RequestController(IRequestService formService)
        {
            _formService = formService;
        }

        // GET: api/Form
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetForms()
        {
            var forms = await _formService.GetFormsAsync();
            return Ok(forms);
        }

        // GET: api/Form/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestDto>> GetForm(int id)
        {
            var form = await _formService.GetFormByIdAsync(id);
            if (form == null)
            {
                return NotFound();
            }

            return Ok(form);
        }

        // POST: api/Form
        [HttpPost]
        public async Task<ActionResult<RequestDto>> PostForm(RequestCreateDto formCreateDto)
        {
            var createdForm = await _formService.CreateFormAsync(formCreateDto);
            return CreatedAtAction(nameof(GetForm), new { id = createdForm.FormId }, createdForm);
        }

        // PUT: api/Form/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutForm(int id, RequestCreateDto formCreateDto)
        {
            var updated = await _formService.UpdateFormAsync(id, formCreateDto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Form/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForm(int id)
        {
            var deleted = await _formService.DeleteFormAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

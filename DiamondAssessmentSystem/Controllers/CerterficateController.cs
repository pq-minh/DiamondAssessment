using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICerterficateService _certificateService;

        public CertificateController(ICerterficateService certificateService)
        {
            _certificateService = certificateService;
        }

        // GET: api/Certificate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CertificateDto>>> GetCertificates()
        {
            var certificates = await _certificateService.GetCertificatesAsync();
            return Ok(certificates);
        }

        // GET: api/Certificate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CertificateDto>> GetCertificate(int id)
        {
            var certificate = await _certificateService.GetCertificateByIdAsync(id);
            if (certificate == null)
            {
                return NotFound();
            }

            return Ok(certificate);
        }

        // POST: api/Certificate
        [HttpPost]
        public async Task<ActionResult<CertificateDto>> PostCertificate(CertificateCreateDto certificateCreateDto)
        {
            var createdCertificate = await _certificateService.CreateCertificateAsync(certificateCreateDto);
            return CreatedAtAction(nameof(GetCertificate), new { id = createdCertificate.CertId }, createdCertificate);
        }

        // PUT: api/Certificate/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertificate(int id, CertificateCreateDto certificateCreateDto)
        {
            var updated = await _certificateService.UpdateCertificateAsync(id, certificateCreateDto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Certificate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            var deleted = await _certificateService.DeleteCertificateAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

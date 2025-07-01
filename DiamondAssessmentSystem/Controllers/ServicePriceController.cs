using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePriceController : ControllerBase
    {
        private readonly IServicePriceService _servicePriceService;
        private readonly IMapper _mapper;

        public ServicePriceController(IServicePriceService servicePriceService, IMapper mapper)
        {
            _servicePriceService = servicePriceService;
            _mapper = mapper;
        }

        // GET: api/ServicePrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicePriceDto>>> GetServicePrices()
        {
            var servicePrices = await _servicePriceService.GetServicePrices();

            if (servicePrices == null)
            {
                return NotFound("No service prices found.");
            }

            return Ok(servicePrices);
        }

        [HttpGet("act")]
        public async Task<ActionResult<IEnumerable<ServicePriceDto>>> GetServicePrices(string status)
        {
            var servicePrices = await _servicePriceService.GetServicePrices(status);

            if (servicePrices == null)
            {
                return NotFound("No service prices found.");
            }

            return Ok(servicePrices);
        }

        // GET: api/ServicePrice/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicePriceDto>> GetServicePrice(int id)
        {
            var servicePrice = await _servicePriceService.GetServicePrice(id);

            if (servicePrice == null)
            {
                return NotFound($"Service price with ID {id} not found.");
            }

            return Ok(servicePrice);
        }

        // POST: api/ServicePrice
        [HttpPost]
        public async Task<ActionResult<ServicePriceDto>> PostServicePrice(ServicePriceCreateDto servicePriceCreateDto)
        {
            if (servicePriceCreateDto == null)
            {
                return BadRequest("Invalid service price data.");
            }

            var createdServicePriceDto = await _servicePriceService.PostServicePrice(servicePriceCreateDto);

            // Return the created resource with a Location header pointing to the newly created resource
            return CreatedAtAction(nameof(GetServicePrice), new { id = createdServicePriceDto.ServiceId }, createdServicePriceDto);
        }

        // PUT: api/ServicePrice/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServicePrice(int id, ServicePriceCreateDto servicePriceCreateDto)
        {
            if (servicePriceCreateDto == null)
            {
                return BadRequest("Invalid service price data.");
            }

            var updated = await _servicePriceService.UpdateServicePrice(id, servicePriceCreateDto);

            if (!updated)
            {
                return NotFound($"Service price with ID {id} not found.");
            }

            return NoContent();
        }

        // DELETE: api/ServicePrice/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicePrice(int id)
        {
            var deleted = await _servicePriceService.DeleteServicePrice(id);

            if (!deleted)
            {
                return NotFound($"Service price with ID {id} not found.");
            }

            return NoContent();
        }
    }
}

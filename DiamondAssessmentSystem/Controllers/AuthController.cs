using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IAuthService _authService;

        //public AuthController(IAuthService authService)
        //{
        //    _authService = authService;
        //}

        //// POST: api/Auth/Login
        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(LoginDto loginDto)
        //{
        //    try
        //    {
        //        var loginResponse = await _authService.Login(loginDto);
        //        return Ok(loginResponse);
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        return Unauthorized(ex.Message);
        //    }
        //}

        //// POST: api/Auth/RegisterCustomer
        //[HttpPost("RegisterCustomer")]
        //public async Task<ActionResult<AccountDto>> RegisterCustomer(RegisterDto registerDto)
        //{
        //    try
        //    {
        //        var accountDto = await _authService.RegisterCustomer(registerDto);
        //        return CreatedAtAction(nameof(RegisterCustomer), accountDto);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //// POST: api/Auth/RegisterAdmin
        //[HttpPost("RegisterAdmin")]
        //public async Task<ActionResult<AccountDto>> RegisterAdmin(AccountDto registerDto)
        //{
        //    try
        //    {
        //        var accountDto = await _authService.RegisterAdmin(registerDto);
        //        return CreatedAtAction(nameof(RegisterAdmin), accountDto);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}

using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private readonly IAccountService _accountService;

        //public AccountController(IAccountService accountService)
        //{
        //    _accountService = accountService;
        //}

        //// GET: api/Account
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts()
        //{
        //    var accountDtos = await _accountService.GetAccounts();
        //    return Ok(accountDtos);
        //}

        //// GET: api/Account/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<AccountDto>> GetAccount(int id)
        //{
        //    var accountDto = await _accountService.GetAccountById(id);
        //    if (accountDto == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(accountDto);
        //}

        //// PUT: api/Account/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAccount(int id, AccountDto accountDto)
        //{
        //    if (id != accountDto.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var updated = await _accountService.UpdateAccount(id, accountDto);

        //    if (!updated)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        //// DELETE: api/Account/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAccount(int id)
        //{
        //    var deleted = await _accountService.DeleteAccount(id);

        //    if (!deleted)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}
    }
}

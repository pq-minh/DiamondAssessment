using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class AccountService : IAccountService
    {
        //private readonly IAccountRepository _accountRepository;

        //public AccountService(IAccountRepository accountRepository)
        //{
        //    _accountRepository = accountRepository;
        //}

        //// Get all accounts
        //public async Task<IEnumerable<AccountDto>> GetAccounts()
        //{
        //    var accounts = await _accountRepository.GetAccountsAsync();
        //    var accountDtos = accounts.Select(a => new AccountDto
        //    {
        //        Id = a.AccId,
        //        Username = a.Username,
        //        Password = a.Password, // Never return password in production
        //        Role = a.Role ?? 0
        //    });

        //    return accountDtos;
        //}

        //// Get account by Id
        //public async Task<AccountDto> GetAccountById(int id)
        //{
        //    var account = await _accountRepository.GetAccountByIdAsync(id);
        //    if (account == null)
        //    {
        //        return null;
        //    }

        //    return new AccountDto
        //    {
        //        Id = account.AccId,
        //        Username = account.Username,
        //        Password = account.Password, // Never return password in production
        //        Role = account.Role ?? 0
        //    };
        //}

        //// Update an existing account
        //public async Task<bool> UpdateAccount(int id, AccountDto accountDto)
        //{
        //    if (id != accountDto.Id)
        //    {
        //        throw new ArgumentException("Account ID mismatch.");
        //    }

        //    var account = new Account
        //    {
        //        AccId = id,
        //        Username = accountDto.Username,
        //        Password = accountDto.Password,
        //        Role = accountDto.Role
        //    };

        //    var updated = await _accountRepository.UpdateAccountAsync(account);
        //    return updated;
        //}

        //// Delete an account by ID
        //public async Task<bool> DeleteAccount(int id)
        //{
        //    var deleted = await _accountRepository.DeleteAccountAsync(id);
        //    return deleted;
        //}
    }
}

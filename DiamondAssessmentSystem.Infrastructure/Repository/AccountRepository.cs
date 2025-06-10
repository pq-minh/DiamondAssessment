using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        //private readonly DiamondAssessmentDbContext _context;

        //public AccountRepository(DiamondAssessmentDbContext context)
        //{
        //    _context = context;
        //}

        //// Register a new account
        //public async Task<Account> RegisterAsync(Account account)
        //{
        //    _context.Accounts.Add(account);
        //    await _context.SaveChangesAsync();
        //    return account;
        //}

        //// Login with username and password
        //public async Task<Account> LoginAsync(string username, string password)
        //{
        //    var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Username == username);
        //    if (account == null || account.Password != password)
        //    {
        //        return null;
        //    }
        //    return account;
        //}

        //// Check if a user exists by username
        //public async Task<bool> UserExistsAsync(string username)
        //{
        //    return await _context.Accounts.AnyAsync(a => a.Username == username);
        //}

        //// Get all accounts
        //public async Task<IEnumerable<Account>> GetAccountsAsync()
        //{
        //    return await _context.Accounts.ToListAsync();
        //}

        //// Get an account by ID
        //public async Task<Account> GetAccountByIdAsync(int id)
        //{
        //    return await _context.Accounts.FindAsync(id);
        //}

        //// Update an account
        //public async Task<bool> UpdateAccountAsync(Account account)
        //{
        //    _context.Entry(account).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!await AccountExistsAsync(account.AccId))
        //        {
        //            return false;
        //        }
        //        throw;
        //    }
        //}

        //// Delete an account
        //public async Task<bool> DeleteAccountAsync(int id)
        //{
        //    var account = await _context.Accounts.FindAsync(id);
        //    if (account == null)
        //    {
        //        return false;
        //    }

        //    _context.Accounts.Remove(account);
        //    await _context.SaveChangesAsync();

        //    return true;
        //}

        //// Check if an account exists by ID
        //private async Task<bool> AccountExistsAsync(int id)
        //{
        //    return await _context.Accounts.AnyAsync(e => e.AccId == id);
        //}

        //#region Database Queries (Moved to the bottom)

        //// Fetches all accounts from the database
        //private async Task<IEnumerable<Account>> FetchAllAccounts()
        //{
        //    return await _context.Accounts.ToListAsync();
        //}

        //// Finds a specific account by its ID
        //private async Task<Account> FetchAccountById(int id)
        //{
        //    return await _context.Accounts.FindAsync(id);
        //}

        //#endregion
    }
}

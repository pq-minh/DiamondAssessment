using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        //private readonly IAccountRepository _accountRepository;
        //private readonly IConfiguration _configuration;
        //private readonly DiamondAssessmentSystemContext _context;
        //private readonly IMapper _mapper;

        //public AuthService(IAccountRepository accountRepository, IConfiguration configuration, DiamondAssessmentSystemContext context, IMapper mapper)
        //{
        //    _accountRepository = accountRepository;
        //    _configuration = configuration;
        //    _context = context;
        //    _mapper = mapper;
        //}

        //public async Task<LoginResponseDto> Login(LoginDto loginDto)
        //{
        //    if (loginDto == null)
        //    {
        //        throw new ArgumentException("Invalid login request.");
        //    }

        //    // Fetch account from repository
        //    var account = await _accountRepository.LoginAsync(loginDto.Username, loginDto.Password);

        //    if (account == null)
        //    {
        //        throw new UnauthorizedAccessException("Invalid username or password.");
        //    }

        //    var token = GenerateJwtToken(account);

        //    // Prepare login response
        //    var loginResponse = new LoginResponseDto { Token = token };

        //    // Role-based login logic
        //    if (account.Role == (int)Role.Customer)
        //    {
        //        var customer = await _context.Customers
        //                                      .Include(c => c.Acc)
        //                                      .FirstOrDefaultAsync(c => c.AccId == account.AccId);

        //        if (customer == null)
        //        {
        //            throw new UnauthorizedAccessException("Customer not found for the given account.");
        //        }

        //        // Map Customer entity to CustomerDto using AutoMapper
        //        var customerDto = _mapper.Map<CustomerDto>(customer);

        //        loginResponse.Customer = customerDto; // Ensure this property exists in LoginResponseDto
        //    }
        //    else if (account.Role == (int)Role.Staff)
        //    {
        //        var staff = await _context.Staff
        //                                   .Include(s => s.Acc)
        //                                   .FirstOrDefaultAsync(s => s.AccId == account.AccId);

        //        if (staff == null)
        //        {
        //            throw new UnauthorizedAccessException("Staff not found for the given account.");
        //        }

        //        // Map Staff entity to StaffDto using AutoMapper
        //        var staffDto = _mapper.Map<StaffDto>(staff);

        //        loginResponse.Staff = staffDto; // Ensure this property exists in LoginResponseDto
        //    }

        //    return loginResponse;
        //}

        //// Register a new account for Customer
        //public async Task<AccountDto> RegisterCustomer(RegisterDto registerDto)
        //{
        //    if (await _accountRepository.UserExistsAsync(registerDto.Username))
        //    {
        //        throw new ArgumentException("Username already exists.");
        //    }

        //    var account = new Account
        //    {
        //        Username = registerDto.Username,
        //        Password = registerDto.Password,
        //        Role = (int)Role.Customer // Assign Customer Role
        //    };

        //    var createdAccount = await _accountRepository.RegisterAsync(account);

        //    // Map Account entity to AccountDto using AutoMapper
        //    return _mapper.Map<AccountDto>(createdAccount);
        //}

        //// Register a new account for Admin
        //public async Task<AccountDto> RegisterAdmin(AccountDto registerDto)
        //{
        //    if (await _accountRepository.UserExistsAsync(registerDto.Username))
        //    {
        //        throw new ArgumentException("Username already exists.");
        //    }

        //    var account = new Account
        //    {
        //        Username = registerDto.Username,
        //        Password = registerDto.Password,
        //        Role = registerDto.Role // Ensure the correct Role is assigned
        //    };

        //    var createdAccount = await _accountRepository.RegisterAsync(account);

        //    // Map Account entity to AccountDto using AutoMapper
        //    return _mapper.Map<AccountDto>(createdAccount);
        //}

        //// Generate JWT token for the user
        //private string GenerateJwtToken(Account account)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, account.Username),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(ClaimTypes.Role, ((Role)account.Role).ToString())
        //    };

        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["Jwt:Issuer"],
        //        audience: _configuration["Jwt:Issuer"],
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(30),
        //        signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}

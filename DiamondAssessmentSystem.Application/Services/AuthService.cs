using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(
            IUserRepository userRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AccountDto> RegisterCustomerAsync(RegisterDto registerDto)
        {
            var newUser = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserType = "Customer",
                Status = "Active"
            };

            var result = await _userRepository.RegisterCustomerAsync(newUser, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"Không thể tạo tài khoản: {errors}");
            }

            var roles = await _userRepository.GetUserRolesAsync(newUser);

            return new AccountDto
            {
                UserId = newUser.Id,
                Username = newUser.UserName,
                Role = roles.FirstOrDefault() ?? "Customer"
            };
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.ValidateUserCredentialsAsync(loginDto.Username, loginDto.Password);
            if (user == null)
                throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không chính xác.");

            var roles = await _userRepository.GetUserRolesAsync(user);
            var token = GenerateJwtToken(user, roles);

            return new LoginResponseDto
            {
                Token = token,
                Username = user.UserName,
                Roles = roles.ToList()
            };
        }

        private string GenerateJwtToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserType", user.UserType ?? "")
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

using DiamondAssessmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string? UserName =>
            _httpContextAccessor.HttpContext?.User?.Identity?.Name;

        public string? Role =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

        public int? AssociatedId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User?.FindFirst("AssociatedId")?.Value;
                return int.TryParse(value, out var id) ? id : (int?)null;
            }
        }

        public int? EmployeeId => int.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("EmployeeId"), out var id) ? id : (int?)null;

        public int? CustomerId
        {
            get
            {
                var role = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
                var associatedIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("AssociatedId")?.Value;
                return role == "Customer" && int.TryParse(associatedIdClaim, out var id) ? id : (int?)null;
            }
        }

        //public int? EmployeeId => (Role == "Employee" || Role == "Manager") ? AssociatedId : null;

    }
}

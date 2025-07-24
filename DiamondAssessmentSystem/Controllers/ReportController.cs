using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ICurrentUserService _currentUser;

        public ReportController(IReportService reportService, ICurrentUserService currentUser)
        {
            _reportService = reportService;
            _currentUser = currentUser;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= DateTime.Today.AddDays(-7);
            toDate ??= DateTime.Today;

            var dashboard = await BuildManagerDashboardAsync(fromDate.Value, toDate.Value);
            return Ok(dashboard);
        }

        private async Task<ManagerDashboardDTO> BuildManagerDashboardAsync(DateTime fromDate, DateTime toDate)
        {
            var accountsPerDay = await _reportService.GetAccountCreatedPerDayAsync(fromDate, toDate);
            var totalOrders = await _reportService.GetTotalOrderCountAsync();
            var ordersByType = await _reportService.GetOrderCountByTypeAsync();
            var totalRequests = await _reportService.GetTotalRequestChosenAsync();
            var ordersByStatus = await _reportService.GetOrderStatusReportAsync(fromDate, toDate);
            var requestStatus = await _reportService.GetRequestStatusReportAsync(fromDate, toDate);

            return new ManagerDashboardDTO
            {
                FromDate = fromDate,
                ToDate = toDate,
                AccountsCreatedPerDay = accountsPerDay,
                TotalOrders = totalOrders,
                OrdersByType = ordersByType,
                TotalRequestChosen = totalRequests,
                OrderStatusStats = ordersByStatus,
                RequestStatusStats = requestStatus
            };
        }

    }
}

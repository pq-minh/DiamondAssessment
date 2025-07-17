using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DiamondAssessmentDbContext _context;

        public EmployeeRepository(DiamondAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeByUserIdAsync(string userId)
        {
            return await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            //_context.Entry(employee).State = EntityState.Modified;
            _context.Employees.Update(employee);
            _context.Users.Update(employee.User);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EmployeeExistsAsync(employee.EmployeeId))
                {
                    return false;
                }
                throw;
            }
        }

        // Kiểm tra xem nhân viên có tồn tại hay không
        private async Task<bool> EmployeeExistsAsync(int id)
        {
            return await _context.Employees.AnyAsync(e => e.EmployeeId == id);
        }

        public async Task<bool> UpdateEmployeeUserRoleAsync(int employeeId, string newRole)
        {
            var employee = await _context.Employees.Include(e => e.User).FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (employee == null || employee.User == null) return false;

            employee.User.UserType = newRole;
            _context.Users.Update(employee.User);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

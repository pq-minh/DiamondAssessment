using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly DiamondAssessmentDbContext _context;

        public CertificateRepository(DiamondAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Certificate>> GetCertificatesAsync()
        {
            return await _context.Certificates
                                 .Include(c => c.Result)  
                                 .ToListAsync();
        }

        public async Task<Certificate> GetCertificateByIdAsync(int id)
        {
            return await _context.Certificates
                                 .Include(c => c.Result)
                                 .FirstOrDefaultAsync(c => c.CertificateId == id);
        }

        public async Task<Certificate> CreateCertificateAsync(Certificate certificate)
        {
            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();
            return certificate;
        }

        public async Task<bool> UpdateCertificateAsync(Certificate certificate)
        {
            _context.Entry(certificate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CertificateExistsAsync(certificate.CertificateId)) // Thay CertId thành CertificateId
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteCertificateAsync(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate == null)
            {
                return false;
            }

            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> CertificateExistsAsync(int id)
        {
            return await _context.Certificates.AnyAsync(e => e.CertificateId == id); // Sửa thành CertificateId
        }
    }
}

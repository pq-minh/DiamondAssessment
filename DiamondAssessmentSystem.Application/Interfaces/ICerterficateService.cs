using DiamondAssessmentSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Interfaces
{
    public interface ICerterficateService
    {
        Task<IEnumerable<CertificateDto>> GetCertificatesAsync();
        Task<CertificateDto> GetCertificateByIdAsync(int id);
        Task<CertificateDto> CreateCertificateAsync(CertificateCreateDto certificateCreateDto);
        Task<bool> UpdateCertificateAsync(int id, CertificateCreateDto certificateCreateDto);
        Task<bool> DeleteCertificateAsync(int id);
    }
}

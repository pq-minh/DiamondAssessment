using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class CertificateService : ICerterficateService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IMapper _mapper;

        public CertificateService(ICertificateRepository certificateRepository, IMapper mapper)
        {
            _certificateRepository = certificateRepository;
            _mapper = mapper;
        }

        // GET: api/Certificate
        public async Task<IEnumerable<CertificateDto>> GetCertificatesAsync()
        {
            var certificates = await _certificateRepository.GetCertificatesAsync();
            return _mapper.Map<IEnumerable<CertificateDto>>(certificates); // Sử dụng AutoMapper để map từ entity sang DTO
        }

        // GET: api/Certificate/5
        public async Task<CertificateDto> GetCertificateByIdAsync(int id)
        {
            var certificate = await _certificateRepository.GetCertificateByIdAsync(id);
            if (certificate == null)
            {
                return null;  // Nếu không tìm thấy certificate, trả về null
            }

            return _mapper.Map<CertificateDto>(certificate); // Sử dụng AutoMapper để map từ entity sang DTO
        }

        // POST: api/Certificate
        public async Task<CertificateDto> CreateCertificateAsync(CertificateCreateDto certificateCreateDto)
        {
            var certificate = _mapper.Map<Certificate>(certificateCreateDto);  // Map từ CertificateCreateDto sang entity Certificate

            var createdCertificate = await _certificateRepository.CreateCertificateAsync(certificate);
            return _mapper.Map<CertificateDto>(createdCertificate); // Map từ entity sang DTO
        }

        // PUT: api/Certificate/5
        public async Task<bool> UpdateCertificateAsync(int id, CertificateCreateDto certificateCreateDto)
        {
            var existingCertificate = await _certificateRepository.GetCertificateByIdAsync(id);
            if (existingCertificate == null)
            {
                return false;  // Nếu không tìm thấy certificate, trả về false
            }

            // Cập nhật thông tin cho certificate hiện tại
            _mapper.Map(certificateCreateDto, existingCertificate);  // Map từ DTO vào entity hiện tại

            return await _certificateRepository.UpdateCertificateAsync(existingCertificate); // Cập nhật certificate trong DB
        }

        // DELETE: api/Certificate/5
        public async Task<bool> DeleteCertificateAsync(int id)
        {
            return await _certificateRepository.DeleteCertificateAsync(id); // Xóa certificate trong DB
        }
    }
}

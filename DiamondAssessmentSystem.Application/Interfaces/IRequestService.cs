using DiamondAssessmentSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Interfaces
{
    public interface IRequestService
    {
        // Lấy danh sách tất cả form
        Task<IEnumerable<RequestDto>> GetFormsAsync();

        // Lấy form theo ID
        Task<RequestDto> GetFormByIdAsync(int id);

        // Tạo một form mới
        Task<RequestDto> CreateFormAsync(RequestCreateDto formCreateDto);

        // Cập nhật form
        Task<bool> UpdateFormAsync(int id, RequestCreateDto formCreateDto);

        // Xóa form theo ID
        Task<bool> DeleteFormAsync(int id);
    }
}

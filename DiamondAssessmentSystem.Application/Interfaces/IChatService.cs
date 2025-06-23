using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Infrastructure.Enums;
using DiamondAssessmentSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Interfaces
{
    public interface IChatService
    {
        Task<ConversationDTO> StartOrGetConversationAsync(int customerId);
        Task AssignEmployeeAsync(int conversationId, int employeeId);
        Task SendMessageAsync(int conversationId, CreateMessageDTO dto);
        Task<List<ChatLogDTO>> GetChatHistoryAsync(int conversationId);
        Task<List<ConversationDTO>> GetUnassignedConversationsAsync();

    }
}

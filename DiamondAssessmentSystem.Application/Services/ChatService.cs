using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.Enums;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IConversationRepository _conversationRepo;
        private readonly IChatLogRepository _chatLogRepo;
        private readonly IMapper _mapper;

        public ChatService(IConversationRepository conversationRepo, IChatLogRepository chatLogRepo, IMapper mapper)
        {
            _conversationRepo = conversationRepo;
            _chatLogRepo = chatLogRepo;
            _mapper = mapper;
        }

        public async Task<ConversationDTO> StartOrGetConversationAsync(int customerId)
        {
            var existing = await _conversationRepo.GetOpenConversationByCustomerAsync(customerId);
            if (existing != null) return _mapper.Map<ConversationDTO>(existing);

            var newConv = new Conversation
            {
                CustomerId = customerId,
                Status = "open",
                CreatedAt = DateTime.Now
            };

            await _conversationRepo.AddAsync(newConv);
            return _mapper.Map<ConversationDTO>(newConv);
        }

        public async Task AssignEmployeeAsync(int conversationId, int employeeId)
        {
            var conv = await _conversationRepo.GetByIdAsync(conversationId);
            if (conv == null || conv.EmployeeId != null)
                throw new InvalidOperationException("Conversation has already been assigned.");

            conv.EmployeeId = employeeId;
            await _conversationRepo.UpdateAsync(conv);
        }

        public async Task SendMessageAsync(int conversationId, CreateMessageDTO dto)
        {
            var chat = new ChatLog
            {
                ConversationId = conversationId,
                SenderId = dto.SenderId,
                SenderRole = dto.SenderRole,
                MessageType = dto.MessageType,
                Message = dto.Message,
                FilePath = dto.FilePath,
                FileName = dto.FileName,
                FileSize = dto.FileSize,
                SentAt = DateTime.Now
            };

            await _chatLogRepo.AddAsync(chat);
        }

        public async Task<List<ChatLogDTO>> GetChatHistoryAsync(int conversationId)
        {
            var logs = await _chatLogRepo.GetByConversationIdAsync(conversationId);
            return _mapper.Map<List<ChatLogDTO>>(logs);
        }

        public async Task<List<ConversationDTO>> GetUnassignedConversationsAsync()
        {
            var list = await _conversationRepo.GetUnassignedConversationsAsync();
            return _mapper.Map<List<ConversationDTO>>(list);
        }

    }
}

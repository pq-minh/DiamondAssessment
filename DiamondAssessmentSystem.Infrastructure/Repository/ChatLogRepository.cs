using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class ChatLogRepository : IChatLogRepository
    {
        private readonly DiamondAssessmentDbContext _context;

        public ChatLogRepository(DiamondAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChatLog>> GetByConversationIdAsync(int conversationId)
        {
            return await _context.ChatLogs
                .Where(c => c.ConversationId == conversationId)
                .OrderBy(c => c.SentAt)
                .ToListAsync();
        }

        public async Task AddAsync(ChatLog chatLog)
        {
            _context.ChatLogs.Add(chatLog);
            await _context.SaveChangesAsync();
        }
    }
}

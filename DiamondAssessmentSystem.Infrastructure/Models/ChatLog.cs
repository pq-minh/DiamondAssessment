using DiamondAssessmentSystem.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public class ChatLog
{
    public int ChatId { get; set; }

    public int ConversationId { get; set; }

    public int SenderId { get; set; }
    public SenderRole SenderRole { get; set; }

    public MessageType MessageType { get; set; }
    public string? Message { get; set; }

    public string? FilePath { get; set; }
    public string? FileName { get; set; }
    public int? FileSize { get; set; }

    public bool IsRead { get; set; } = false;
    public DateTime SentAt { get; set; } = DateTime.Now;

    // Navigation
    public Conversation Conversation { get; set; } = null!;
}

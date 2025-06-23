using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public class Conversation
{
    public int ConversationId { get; set; }

    public int CustomerId { get; set; }
    public int? EmployeeId { get; set; }     // Nullable → chưa có người tư vấn

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Status { get; set; } = "open"; // 'open', 'closed'

    // Navigation
    public Customer Customer { get; set; } = null!;
    public Employee? Employee { get; set; }
    public ICollection<ChatLog> ChatLogs { get; set; } = new List<ChatLog>();
}


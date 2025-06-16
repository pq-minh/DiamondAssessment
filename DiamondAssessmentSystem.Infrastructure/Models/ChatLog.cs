using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public partial class ChatLog
{
    public int ChatId { get; set; }

    public int? RequestId { get; set; }

    public int? CustomerId { get; set; }

    public int? EmployeeId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string MessageType { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Request? Request { get; set; }
}

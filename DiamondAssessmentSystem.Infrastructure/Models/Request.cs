using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public partial class Request
{
    public int RequestId { get; set; }

    public int CustomerId { get; set; }

    public DateOnly RequestDate { get; set; }

    public string ServiceType { get; set; } = null!;

    public int? EmployeeId { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<ChatLog> ChatLogs { get; set; } = new List<ChatLog>();

    public virtual ICollection<CommitmentRecord> CommitmentRecords { get; set; } = new List<CommitmentRecord>();

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<SealingRecord> SealingRecords { get; set; } = new List<SealingRecord>();
}

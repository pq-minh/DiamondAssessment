using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public Guid UserId { get; set; }

    public string? Position { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<ChatLog> ChatLogs { get; set; } = new List<ChatLog>();

    public virtual ICollection<CommitmentRecord> CommitmentRecords { get; set; } = new List<CommitmentRecord>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<SealingRecord> SealingRecords { get; set; } = new List<SealingRecord>();

    public virtual ICollection<ServicePriceAudit> ServicePriceAudits { get; set; } = new List<ServicePriceAudit>();

    public virtual ICollection<ServicePrice> ServicePrices { get; set; } = new List<ServicePrice>();
}

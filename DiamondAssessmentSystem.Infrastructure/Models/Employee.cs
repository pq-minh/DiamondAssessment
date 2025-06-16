using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public decimal? Salary { get; set; }

    public string UserId { get; set; }

    public User User { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<ChatLog> ChatLogs { get; set; } = new List<ChatLog>();

    public virtual ICollection<CommitmentRecord> CommitmentRecords { get; set; } = new List<CommitmentRecord>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<SealingRecord> SealingRecords { get; set; } = new List<SealingRecord>();

    public virtual ICollection<ServicePriceAudit> ServicePriceAudits { get; set; } = new List<ServicePriceAudit>();

    public virtual ICollection<ServicePrice> ServicePrices { get; set; } = new List<ServicePrice>();
}

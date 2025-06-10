using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public Guid UserId { get; set; }

    public decimal? Idcard { get; set; }

    public string? Address { get; set; }

    public string? UnitName { get; set; }

    public string? TaxCode { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<ChatLog> ChatLogs { get; set; } = new List<ChatLog>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}

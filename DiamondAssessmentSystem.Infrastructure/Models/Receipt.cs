using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public partial class Receipt
{
    public int ReceiptId { get; set; }

    public int RequestId { get; set; }

    public DateOnly ReceiptDate { get; set; }

    public decimal? OrdCarat { get; set; }

    public string? OrdColor { get; set; }

    public string? OrdCut { get; set; }

    public string? OrdOther { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Request Request { get; set; } = null!;
}

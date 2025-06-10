using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly OrderDate { get; set; }

    public int CustomerId { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public int? ConsultantId { get; set; }

    public int? ReceiptId { get; set; }

    public int? SealingId { get; set; }

    public int? CommitmentId { get; set; }

    public string ServiceType { get; set; } = null!;

    public virtual CommitmentRecord? Commitment { get; set; }

    public virtual Employee? Consultant { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Receipt? Receipt { get; set; }

    public virtual SealingRecord? Sealing { get; set; }
}

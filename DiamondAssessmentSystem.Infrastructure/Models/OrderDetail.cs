using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ServiceId { get; set; }

    public string ServiceType { get; set; } = null!;

    public int? ResultId { get; set; }

    public bool? IsAccepted { get; set; }

    public string Status { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Result? Result { get; set; }

    public virtual ServicePrice Service { get; set; } = null!;
}

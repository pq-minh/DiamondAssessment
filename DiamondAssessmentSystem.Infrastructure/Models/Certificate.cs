using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public partial class Certificate
{
    public int CertificateId { get; set; }

    public int ResultId { get; set; }

    public string? CertificateNumber { get; set; }

    public DateTime IssueDate { get; set; }

    public virtual Result Result { get; set; } = null!;
}

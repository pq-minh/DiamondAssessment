using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public partial class Certificate
{
    public int CertificateId { get; set; }

    public int ResultId { get; set; }

    public DateOnly IssueDate { get; set; }

    public virtual Result Result { get; set; } = null!;
}

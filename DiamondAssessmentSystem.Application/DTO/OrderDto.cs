using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Application.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        //public int? ConsultantId { get; set; }
        //public int? SealingId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceType { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = null!;
        //public int? CommitmentId { get; set; }

        //public virtual CommitmentRecord? Commitment { get; set; }

        //public virtual Employee? Consultant { get; set; }

        //public virtual Customer Customer { get; set; } = null!;
    }


    public class OrderCreateDto
    {
        public int RequestId { get; set; }
    }
}

using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Application.DTO
{
    public class OrderDto
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
    }


    public class OrderCreateDto
    {
        public string OrderDetailId { get; set; }  // Giả sử là một chuỗi chứa nhiều OrderDetailId
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

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

        public int ServiceId { get; set; }

        public string ServiceType { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = null!;
    }


    public class OrderCreateDto
    {
        public DateTime OrderDate { get; set; }

        public int ServiceId { get; set; }

        public decimal TotalPrice { get; set; }
    }

    public class orderPaymentDto
    {
        public VnPaymentResponseFromFe? request { get; set; }

        public int requestId { get; set; }

        public string paymentType { get; set; }
    }

    public class orderCreateCombine
    {
        public OrderCreateDto OrderCreateDto { get; set; }
        public orderPaymentDto orderPaymentDto { get; set; }
    }
}

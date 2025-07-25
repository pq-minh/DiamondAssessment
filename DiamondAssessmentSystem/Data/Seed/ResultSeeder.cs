using DiamondAssessmentSystem.Infrastructure.Models;

namespace DiamondAssessmentSystem.Data.Seed
{
    public static class ResultSeeder
    {
        public static List<Result> Results = new();

        public static void Seed(DiamondAssessmentDbContext context)
        {
            if (context.Results.Any()) return;

            var diamonds = context.Diamonds.ToList();
            var completedOrders = OrderSeeder.Orders.Where(o => o.Status == "Completed").ToList();

            // Map DiamondId theo đúng RequestId từ completedOrders
            var validDiamonds = diamonds
                .Where(d => completedOrders.Any(o => o.CustomerId == d.Request.Customer.CustomerId && o.ServiceId == d.Request.ServiceId))
                .ToList();

            int index = 0;

            foreach (var diamond in validDiamonds)
            {
                var status = (index % 2 == 0) ? "Completed" : "InProgress";

                Results.Add(new Result
                {
                    RequestId = diamond.RequestId,
                    DiamondId = diamond.DiamondId, // <-- Dùng ID thật từ DB
                    DiamondOrigin = "South Africa",
                    Shape = (index % 3 == 0) ? "Round" : (index % 3 == 1) ? "Princess" : "Oval",
                    Measurements = "5.10 x 5.12 x 3.15 mm",
                    CaratWeight = 0.8m + (index % 5) * 0.15m,
                    Color = "F",
                    Clarity = "VS2",
                    Cut = "Excellent",
                    Proportions = "60%",
                    Polish = "Excellent",
                    Symmetry = "Very Good",
                    Fluorescence = "None",
                    ModifiedDate = DateTime.Now.AddDays(-index),
                    Status = status
                });

                index++;
            }

            context.Results.AddRange(Results);
            context.SaveChanges();
        }
    }
}

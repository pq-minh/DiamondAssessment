using DiamondAssessmentSystem.Infrastructure.Models;

namespace DiamondAssessmentSystem.Data.Seed
{
    namespace DiamondAssessmentSystem.Data.Seed
    {
        public static class DiamondSeeder
        {
            public static List<Diamond> Diamonds = new();

            public static void Seed(DiamondAssessmentDbContext context)
            {
                if (context.Diamonds.Any()) return;

                var requests = context.Requests.ToList();
                var random = new Random();

                for (int i = 0; i < requests.Count; i++)
                {
                    var request = requests[i];

                    var diamond = new Diamond
                    {
                        RequestId = request.RequestId,
                        EmployeeId = request.EmployeeId,
                        DateReceived = DateTime.Now.AddDays(-5 - i),
                        DateReturn = DateTime.Now.AddDays(-i),
                        Status = "Processing"
                    };

                    Diamonds.Add(diamond);
                }

                context.Diamonds.AddRange(Diamonds);
                context.SaveChanges();
            }
        }
    }
}

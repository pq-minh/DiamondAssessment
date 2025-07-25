using DiamondAssessmentSystem.Data.Seed;
using DiamondAssessmentSystem.Data.Seed.DiamondAssessmentSystem.Data.Seed;
using DiamondAssessmentSystem.Infrastructure.Models;

namespace DiamondAssessmentSystem.Data
{
    public static class DbInitializer
    {
        public static void Seed(DiamondAssessmentDbContext context)
        {
            UserSeeder.Seed(context);
            EmployeeSeeder.Seed(context);
            CustomerSeeder.Seed(context);
            ServicePriceSeeder.Seed(context);

            RequestSeeder.Seed(context);
            OrderSeeder.Seed(context);
            PaymentSeeder.Seed(context);

            DiamondSeeder.Seed(context); 

            ResultSeeder.Seed(context);
            CertificateSeeder.Seed(context);
            SealingRecordSeeder.Seed(context);
            CommitmentRecordSeeder.Seed(context);
            ServicePriceAuditSeeder.Seed(context);
        }
    }
}

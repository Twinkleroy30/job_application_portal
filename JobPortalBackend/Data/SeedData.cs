using JobPortalBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JobPortalBackend.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();

            if (context.Jobs.Any())
            {
                return;   // DB has been seeded
            }

            context.Jobs.AddRange(
                new Job
                {
                    JobTitle = "Software Engineer",
                    CompanyName = "Tech Corp",
                    Location = "New York, NY",
                    Description = "Develop and maintain software applications.",
                    JobType = "Full-time",
                    SalaryRange = "$80,000 - $120,000",
                    ApplicationDeadline = DateTime.Now.AddMonths(1),
                    PostedDate = DateTime.Now
                },
                new Job
                {
                    JobTitle = "Frontend Developer",
                    CompanyName = "Web Solutions",
                    Location = "San Francisco, CA",
                    Description = "Build responsive web interfaces.",
                    JobType = "Contract",
                    SalaryRange = "$60/hr - $80/hr",
                    ApplicationDeadline = DateTime.Now.AddMonths(2),
                    PostedDate = DateTime.Now
                }
            );

            context.SaveChanges();
        }
    }
}

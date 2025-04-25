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

            // Seed users
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Id = 1,
                    FullName = "Test User",
                    Username = "testuser",
                    Email = "testuser@example.com",
                    Password = "password123",
                    Phone = "123-456-7890"
                });
                context.SaveChanges();
            }

            var jobsToSeed = new Job[]
            {
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
                },
                new Job
                {
                    JobTitle = "Data Analyst",
                    CompanyName = "Data Insights",
                    Location = "Remote",
                    Description = "Analyze and interpret complex data sets.",
                    JobType = "Part-time",
                    SalaryRange = "$40/hr - $60/hr",
                    ApplicationDeadline = DateTime.Now.AddMonths(3),
                    PostedDate = DateTime.Now
                },
                new Job
                {
                    JobTitle = "Project Manager",
                    CompanyName = "Global Projects",
                    Location = "Chicago, IL",
                    Description = "Oversee project timelines and deliverables.",
                    JobType = "Full-time",
                    SalaryRange = "$90,000 - $130,000",
                    ApplicationDeadline = DateTime.Now.AddMonths(1),
                    PostedDate = DateTime.Now
                },
                new Job
                {
                    JobTitle = "UX/UI Designer",
                    CompanyName = "Creative Minds",
                    Location = "Austin, TX",
                    Description = "Design user-friendly interfaces and experiences.",
                    JobType = "Freelance",
                    SalaryRange = "$50/hr - $70/hr",
                    ApplicationDeadline = DateTime.Now.AddMonths(2),
                    PostedDate = DateTime.Now
                },
                new Job
                {
                    JobTitle = "DevOps Engineer",
                    CompanyName = "Cloud Solutions",
                    Location = "Seattle, WA",
                    Description = "Manage cloud infrastructure and deployment.",
                    JobType = "Full-time",
                    SalaryRange = "$100,000 - $140,000",
                    ApplicationDeadline = DateTime.Now.AddMonths(1),
                    PostedDate = DateTime.Now
                },
                new Job
                {
                    JobTitle = "Database Administrator",
                    CompanyName = "Data Systems",
                    Location = "Boston, MA",
                    Description = "Maintain and optimize database systems.",
                    JobType = "Part-time",
                    SalaryRange = "$50/hr - $80/hr",
                    ApplicationDeadline = DateTime.Now.AddMonths(3),
                    PostedDate = DateTime.Now
                },

                new Job
                {
                    JobTitle = "Network Engineer",
                    CompanyName = "Tech Networks",
                    Location = "Los Angeles, CA",
                    Description = "Design and implement network solutions.",
                    JobType = "Contract",
                    SalaryRange = "$70/hr - $90/hr",
                    ApplicationDeadline = DateTime.Now.AddMonths(2),
                    PostedDate = DateTime.Now
                },
                new Job
                {
                    JobTitle = "Cybersecurity Analyst",
                    CompanyName = "SecureTech",
                    Location = "Washington, DC",
                    Description = "Protect systems and networks from cyber threats.",
                    JobType = "Full-time",
                    SalaryRange = "$80,000 - $120,000",
                    ApplicationDeadline = DateTime.Now.AddMonths(1),
                    PostedDate = DateTime.Now
                },  
                // Add other jobs here as needed...
            };

            foreach (var job in jobsToSeed)
            {
                bool exists = context.Jobs.Any(j =>
                    j.JobTitle == job.JobTitle &&
                    j.CompanyName == job.CompanyName &&
                    j.Location == job.Location);

                if (!exists)
                {
                    context.Jobs.Add(job);
                }
            }

            context.SaveChanges();
        }
    }
}

using System;
using job_portal_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace job_portal_api.Services
{
    public class JobGeneratorService
    {
        private readonly Random _random = new Random();
        
        private readonly string[] _jobTitles = {
            "Software Engineer", "Frontend Developer", "Backend Developer",
            "Full Stack Developer", "Data Scientist", "DevOps Engineer",
            "Product Manager", "UX Designer", "QA Engineer"
        };

        private readonly string[] _companies = {
            "TechCorp", "InnovateSoft", "Digital Solutions",
            "WebCraft", "DataSystems", "CloudNine"
        };

        private readonly string[] _locations = {
            "New York", "San Francisco", "Chicago",
            "Austin", "Seattle", "Remote"
        };

        private readonly string[] _jobTypes = {
            "Full-time", "Part-time", "Contract"
        };

        private readonly string[] _experienceLevels = {
            "Entry", "Mid", "Senior"
        };

        private readonly string[] _skills = {
            "C#", "JavaScript", "Python", "Java", "SQL",
            "React", "Angular", "Node.js", "AWS", "Azure"
        };

        public List<Job> GenerateRandomJobs(int count, int employerId, User employer)
        {
            var jobs = new List<Job>();
            
            for (int i = 0; i < count; i++)
            {
                jobs.Add(new Job
                {
                    Title = _jobTitles[_random.Next(_jobTitles.Length)],
                    Description = $"We are looking for a skilled {_jobTitles[_random.Next(_jobTitles.Length)]} to join our team.",
                    Company = _companies[_random.Next(_companies.Length)],
                    Location = _locations[_random.Next(_locations.Length)],
                    JobType = _jobTypes[_random.Next(_jobTypes.Length)],
                    ExperienceLevel = _experienceLevels[_random.Next(_experienceLevels.Length)],
                    Salary = _random.Next(60000, 150000),
                    SkillsRequired = GetRandomSkills(),
                    PostedDate = DateTime.UtcNow.AddDays(-_random.Next(0, 30)),
                    Deadline = DateTime.UtcNow.AddDays(_random.Next(15, 60)),
                    IsActive = true,
                    EmployerId = employerId,
                    Employer = employer
                });
            }

            return jobs;
        }

        private string[] GetRandomSkills()
        {
            return _skills
                .OrderBy(x => _random.Next())
                .Take(_random.Next(3, 6))
                .ToArray();
        }
    }
}

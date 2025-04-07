using System.ComponentModel.DataAnnotations;

namespace job_portal_api.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Company { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string JobType { get; set; } = "Full-time"; // Full-time, Part-time, Contract

        [Required]
        public string ExperienceLevel { get; set; } = "Mid"; // Entry, Mid, Senior

        [Required]
        public decimal Salary { get; set; }

        public string[] SkillsRequired { get; set; } = Array.Empty<string>();
        public string? Requirements { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;
        public DateTime? Deadline { get; set; }
        public bool IsActive { get; set; } = true;

        // Foreign key
        public int EmployerId { get; set; }
        public User Employer { get; set; } = null!;
    }
}

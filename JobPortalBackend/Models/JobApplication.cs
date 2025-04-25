using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortalBackend.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }

        public int JobId { get; set; }

        public string ApplicantName { get; set; }

        public string ApplicantEmail { get; set; }

        public string ResumeUrl { get; set; }

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("JobId")]
        public Job Job { get; set; }
    }
}

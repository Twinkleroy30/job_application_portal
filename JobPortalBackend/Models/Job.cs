using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobPortalBackend.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("job_title")]
        public string JobTitle { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("company_name")]
        public string CompanyName { get; set; }

        [JsonPropertyName("job_type")]
        public string JobType { get; set; }

        [JsonPropertyName("salary_range")]
        public string SalaryRange { get; set; }

        [JsonPropertyName("application_deadline")]
        public DateTime ApplicationDeadline { get; set; }

        [JsonPropertyName("posted_date")]
        public DateTime PostedDate { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using JobPortalBackend.Data;
using JobPortalBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace JobPortalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/jobs
        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return Ok(jobs);
        }

        // GET: api/jobs/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        // POST: api/jobs
        [HttpPost]
        public async Task<IActionResult> CreateJob(Job newJob)
        {
            newJob.Id = 0; // Ensure Id is zero so EF Core generates it
            _context.Jobs.Add(newJob);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJob), new { id = newJob.Id }, newJob);
        }

        // PUT: api/jobs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] Job job)
        {
            if (id != job.Id)
            {
                return BadRequest("Job ID mismatch");
            }

            var existingJob = await _context.Jobs.FindAsync(id);
            if (existingJob == null)
            {
                return NotFound();
            }

            existingJob.JobTitle = job.JobTitle;
            existingJob.CompanyName = job.CompanyName;
            existingJob.Location = job.Location;
            existingJob.Description = job.Description;
            existingJob.JobType = job.JobType;
            existingJob.SalaryRange = job.SalaryRange;
            existingJob.ApplicationDeadline = job.ApplicationDeadline;
            existingJob.PostedDate = job.PostedDate;

            _context.Entry(existingJob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/jobs/{id}/apply
        [HttpPost("{id}/apply")]
        public async Task<IActionResult> ApplyForJob(int id, [FromForm] string applicantName, [FromForm] string applicantEmail, IFormFile resume)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            if (resume == null || resume.Length == 0)
            {
                return BadRequest("Resume file is required.");
            }

            // Save the file to a folder, e.g., "Uploads"
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(resume.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await resume.CopyToAsync(stream);
            }

            // Create JobApplication object
            var application = new JobApplication
            {
                JobId = id,
                ApplicantName = applicantName,
                ApplicantEmail = applicantEmail,
                ResumeUrl = uniqueFileName,
                ApplicationDate = DateTime.UtcNow
            };

            // Save the application to the database
            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Application received successfully." });
        }

        [HttpGet("applied/{applicantEmail}")]
        public async Task<IActionResult> GetAppliedJobs(string applicantEmail)
        {
            applicantEmail = applicantEmail?.Trim().ToLower();

            Console.WriteLine($"GetAppliedJobs called with email: {applicantEmail}");

            var appliedJobs = await (from application in _context.JobApplications
                                     join job in _context.Jobs on application.JobId equals job.Id
                                     where application.ApplicantEmail.ToLower() == applicantEmail
                                     select new
                                     {
                                         job.Id,
                                         job.JobTitle,
                                         job.CompanyName,
                                         job.Location,
                                         job.Description,
                                         job.JobType,
                                         job.SalaryRange,
                                         job.ApplicationDeadline,
                                         job.PostedDate,
                                         application.ApplicationDate,
                                         application.ResumeUrl
                                     }).ToListAsync();

            Console.WriteLine($"Found {appliedJobs.Count} applied jobs for email: {applicantEmail}");

            return Ok(appliedJobs);
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}

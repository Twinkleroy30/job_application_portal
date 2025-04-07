using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using job_portal_api.Data;
using job_portal_api.Models;
using job_portal_api.Services;

namespace job_portal_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JobGeneratorService _jobGenerator;

        public JobsController(ApplicationDbContext context, JobGeneratorService jobGenerator)
        {
            _context = context;
            _jobGenerator = jobGenerator;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            return await _context.Jobs
                .Include(j => j.Employer)
                .Where(j => j.IsActive)
                .ToListAsync();
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Jobs
                .Include(j => j.Employer)
                .FirstOrDefaultAsync(j => j.Id == id && j.IsActive);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // POST: api/Jobs
        [Authorize(Roles = "Employer")]
        [HttpPost]
        public async Task<ActionResult<Job>> CreateJob([FromBody] Job job)
        {
            var employerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User ID not found"));
            job.EmployerId = employerId;
            job.PostedDate = DateTime.UtcNow;
            job.IsActive = true;

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job);
        }

        // PUT: api/Jobs/5
        [Authorize(Roles = "Employer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            var existingJob = await _context.Jobs.FindAsync(id);
            if (existingJob == null)
            {
                return NotFound();
            }

            var employerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User ID not found"));
            if (existingJob.EmployerId != employerId)
            {
                return Forbid();
            }

            existingJob.Title = job.Title;
            existingJob.Description = job.Description;
            existingJob.Company = job.Company;
            existingJob.Location = job.Location;
            existingJob.JobType = job.JobType;
            existingJob.Salary = job.Salary;
            existingJob.Requirements = job.Requirements;
            existingJob.Deadline = job.Deadline;

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

        // DELETE: api/Jobs/5
        [Authorize(Roles = "Employer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            var employerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User ID not found"));
            if (job.EmployerId != employerId)
            {
                return Forbid();
            }

            job.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Employer")]
        [HttpPost("generate")]
        public async Task<ActionResult> GenerateRandomJobs(int count = 5)
        {
            var employerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User ID not found"));
            var employer = await _context.Users.FindAsync(employerId);
            
            if (employer == null)
            {
                return NotFound("Employer not found");
            }

            var jobs = _jobGenerator.GenerateRandomJobs(count, employerId, employer);
            await _context.Jobs.AddRangeAsync(jobs);
            await _context.SaveChangesAsync();

            return Ok($"{count} random jobs generated successfully");
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
} 
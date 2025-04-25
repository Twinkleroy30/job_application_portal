using Microsoft.AspNetCore.Mvc;
using JobPortalBackend.Data;
using JobPortalBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace JobPortalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        
        // GET: api/users/{id}/applications
        [HttpGet("{id}/applications")]
        public async Task<IActionResult> GetUserApplications(int id)
        {
            var userEmail = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => u.Email)
                .FirstOrDefaultAsync();

            if (userEmail == null)
            {
                return NotFound();
            }

            var applications = await _context.JobApplications
                .Where(app => app.ApplicantEmail == userEmail)
                .Include(app => app.Job)
                .Select(app => new {
                    app.Id,
                    app.JobId,
                    JobTitle = app.Job.JobTitle,
                    CompanyName = app.Job.CompanyName,
                    Location = app.Job.Location,
                    ApplicationDate = app.ApplicationDate
                })
                .ToListAsync();

            return Ok(applications);
        }
    }
}

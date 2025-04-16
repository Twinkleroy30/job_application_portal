using Microsoft.AspNetCore.Mvc;
using JobPortalBackend.Data;
using JobPortalBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/favorites
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(Favorite favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFavorites), new { userId = favorite.UserId }, favorite);
        }

        // PUT: api/favorites
        [HttpPut]
        public async Task<IActionResult> UpdateFavorites(Favorite favorite)
        {
            var existingFavorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == favorite.UserId && f.JobId == favorite.JobId);

            if (existingFavorite == null)
            {
                return NotFound();
            }

            // Update properties if needed
            existingFavorite.JobId = favorite.JobId; // Example of updating, if necessary

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/favorites/{userId}/{jobId}
        [HttpDelete("{userId}/{jobId}")]
        public async Task<IActionResult> RemoveFromFavorites(int userId, int jobId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.JobId == jobId);

            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/favorites/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetFavorites(int userId)
        {
            var favorites = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.JobId)
                .ToListAsync();

            var jobs = await _context.Jobs
                .Where(j => favorites.Contains(j.Id))
                .ToListAsync();

            return Ok(jobs);
        }
    }
}

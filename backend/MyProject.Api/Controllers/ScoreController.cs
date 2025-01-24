using Microsoft.AspNetCore.Mvc;
using MyProject.Api.Data; 

namespace MyProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ScoreController(AppDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult GetTopUsers()
        {
            int topCount = 10;
            var topUsers = _context.HighScores.OrderByDescending(s => s.TotalScore)
                .ThenBy(s => s.Date)
                .Take(topCount)
                .Select(u => new { u.Email, u.TotalScore, u.Date })
                .ToList();
            
            return Ok(topUsers);
        }
    }
}

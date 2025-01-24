using Microsoft.EntityFrameworkCore;

namespace MyProject.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<QuestionAndAnswers> Questions { get; set; }
        public DbSet<HighScore> HighScores { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}

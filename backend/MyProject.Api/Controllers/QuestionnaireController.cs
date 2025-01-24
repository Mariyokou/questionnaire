using Microsoft.AspNetCore.Mvc;
using MyProject.Api.Data; 

namespace MyProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IScoreCalculatorService _scoreCalculatorService;

        public QuestionnaireController(AppDbContext context, IScoreCalculatorService scoreCalculatorService)
        {
            _context = context;
            _scoreCalculatorService = scoreCalculatorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            var questionAndAnswers = _context.Questions.Select(q => new QuestionAndAnswers
            {
                Id = q.Id,
                Question = q.Question,
                Type = q.Type,
                Options = q.Options,
                CorrectAnswers = null
            }).ToList();

            return Ok(questionAndAnswers);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAnswers([FromBody] QuestionnaireData data)
        {
            int totalScore = 0;

            foreach (var answer in data.Answers)
            {
                var question = _context.Questions.FirstOrDefault(q => q.Id == answer.Key);

                if (question == null)
                {
                    return NotFound(new { message = $"Question with Id {answer.Key} not found." });
                }

                totalScore += _scoreCalculatorService.CalculateQuestionScore(question, answer.Value);
            }

            var highScore = new HighScore
            {
                Email = data.Email,
                TotalScore = totalScore, 
                Date = DateTime.Now
            };

            _context.HighScores.Add(highScore);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Score submitted successfully!", totalScore });
        }
    }
}

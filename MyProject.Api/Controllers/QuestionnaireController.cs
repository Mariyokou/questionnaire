using Microsoft.AspNetCore.Mvc;
using MyProject.Api.Data; 

namespace MyProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestionnaireController(AppDbContext context)
        {
            _context = context;
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

                if (question.Type == QuestionType.Text || question.Type == QuestionType.SingleChoice)
                {
                    var isCorrect = answer.Value.SequenceEqual(question.CorrectAnswers);
                    
                    if (isCorrect)
                    {
                        totalScore += 100;
                    }
                } else if (question.Type == QuestionType.MultipleChoice)
                {
                    var isCorrect = answer.Value.SequenceEqual(question.CorrectAnswers);
                    int goodAnswersCount = question.CorrectAnswers.Count;
                    int correctlyCheckedCount = answer.Value.Intersect(question.CorrectAnswers).Count();
                    
                    totalScore += (100/goodAnswersCount)*correctlyCheckedCount;
                }
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

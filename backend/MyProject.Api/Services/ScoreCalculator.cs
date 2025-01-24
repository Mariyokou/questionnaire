public interface IScoreCalculatorService
{
    int CalculateQuestionScore(QuestionAndAnswers question, List<string> answer);
}

public class ScoreCalculatorService : IScoreCalculatorService
{
    public int CalculateQuestionScore(QuestionAndAnswers question, List<string> answer)
    {
        int totalScore = 0;

        if (question.Type == QuestionType.Text || question.Type == QuestionType.SingleChoice)
        {
            var isCorrect = answer.SequenceEqual(question.CorrectAnswers);
            if (isCorrect)
            {
                totalScore += 100;
            }
        }
        else if (question.Type == QuestionType.MultipleChoice)
        {
            int goodAnswersCount = question.CorrectAnswers.Count;
            int correctlyCheckedCount = answer.Intersect(question.CorrectAnswers).Count();
            totalScore += (100 / goodAnswersCount) * correctlyCheckedCount;
        }

        return totalScore;
    }
}

using Xunit;
using System.Collections.Generic;

public class ScoreCalculatorServiceTests
{
    private readonly IScoreCalculatorService _scoreCalculatorService;

    public ScoreCalculatorServiceTests()
    {
        _scoreCalculatorService = new ScoreCalculatorService();
    }

    // #region Text Question Tests

    [Fact]
    public void CalculateQuestionScore_ReturnsCorrectScore_ForTextQuestion_WhenAnswerIsCorrect()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.Text,
            CorrectAnswers = new List<string> { "Correct Answer" }
        };
        var answer = new List<string> { "Correct Answer" };

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(100, score); 
    }

    [Fact]
    public void CalculateQuestionScore_ReturnsZero_ForTextQuestion_WhenAnswerIsIncorrect()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.Text,
            CorrectAnswers = new List<string> { "Correct Answer" }
        };
        var answer = new List<string> { "Incorrect Answer" };

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(0, score);
    }

    [Fact]
    public void CalculateQuestionScore_ReturnsZero_ForTextQuestion_WhenCaseInsensitive()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.Text,
            CorrectAnswers = new List<string> { "Correct Answer" }
        };
        var answer = new List<string> { "correct answer" };

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(0, score); 
    }

    // #endregion



    // #region SingleChoice Question Tests

    [Fact]
    public void CalculateQuestionScore_ReturnsCorrectScore_ForSingleChoice_WhenAnswerIsCorrect()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.SingleChoice,
            CorrectAnswers = new List<string> { "Single Choice Answer" }
        };
        var answer = new List<string> { "Single Choice Answer" };

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(100, score);
    }

    [Fact]
    public void CalculateQuestionScore_ReturnsZero_ForSingleChoice_WhenAnswerIsIncorrect()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.SingleChoice,
            CorrectAnswers = new List<string> { "Single Choice Answer" }
        };
        var answer = new List<string> { "Incorrect Choice" };

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(0, score); 
    }

    [Fact]
    public void CalculateQuestionScore_ReturnsZero_ForSingleChoice_WhenNoAnswerGiven()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.SingleChoice,
            CorrectAnswers = new List<string> { "Single Choice Answer" }
        };
        var answer = new List<string>();

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(0, score);
    }

    // #endregion


    // #region MultipleChoice Question Tests

    [Fact]
    public void CalculateQuestionScore_ReturnsFullScore_ForMultipleChoice_WhenAllAnswersAreCorrect()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.MultipleChoice,
            CorrectAnswers = new List<string> { "Option 1", "Option 2" }
        };
        var answer = new List<string> { "Option 1", "Option 2" };

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(100, score);
    }

    [Fact]
    public void CalculateQuestionScore_ReturnsZero_ForMultipleChoice_WhenAnswerIsIncorrect()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.MultipleChoice,
            CorrectAnswers = new List<string> { "Option 1", "Option 2" }
        };
        var answer = new List<string> { "Option 3" }; 

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(0, score);
    }

    [Fact]
    public void CalculateQuestionScore_ReturnsCorrectScore_ForMultipleChoice_WhenSomeAnswersAreCorrect()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.MultipleChoice,
            CorrectAnswers = new List<string> { "Option 1", "Option 2" }
        };
        var answer = new List<string> { "Option 1" };

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(50, score);
    }
   
    [Fact]
    public void CalculateQuestionScore_ReturnsCorrectScore_ForMultipleChoice_WhenOneAnswerIsIncorrect()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.MultipleChoice,
            CorrectAnswers = new List<string> { "Option 1", "Option 2" }
        };
        var answer = new List<string> { "Option 1", "Option 3" };

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(50, score);
    }

    [Fact]
    public void CalculateQuestionScore_ReturnsCorrectScore_ForMultipleChoice_CorrectlyRounded()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.MultipleChoice,
            CorrectAnswers = new List<string> { "Option 1", "Option 2", "Option 3" }
        };
        var answer = new List<string> { "Option 1", "Option 3" };

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(67, score);
    }

    [Fact]
    public void CalculateQuestionScore_ReturnsZero_ForMultipleChoice_WhenNoAnswersAreSelected()
    {
        var question = new QuestionAndAnswers
        {
            Type = QuestionType.MultipleChoice,
            CorrectAnswers = new List<string> { "Option 1", "Option 2" }
        };
        var answer = new List<string>();

        var score = _scoreCalculatorService.CalculateQuestionScore(question, answer);

        Assert.Equal(0, score); 
    }

    // #endregion
}

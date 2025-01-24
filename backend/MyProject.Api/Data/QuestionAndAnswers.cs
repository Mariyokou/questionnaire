public class QuestionAndAnswers
{
    public int Id { get; set; }  
    public string Question { get; set; }  
    public QuestionType Type { get; set; }  
    public List<string> Options { get; set; } = new List<string>();  
    public List<string> CorrectAnswers { get; set; }
}

public enum QuestionType
{
    MultipleChoice,
    SingleChoice,
    Text
}
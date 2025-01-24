public class QuestionnaireData
{
    public string Email { get; set; }
    public Dictionary<int, List<string>> Answers { get; set; }

    public QuestionnaireData()
    {
        Answers = new Dictionary<int, List<string>>();
    }
}
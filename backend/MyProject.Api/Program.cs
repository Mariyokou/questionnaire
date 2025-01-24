using MyProject.Api.Data; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        policy.WithOrigins("http://localhost:3000") 
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IScoreCalculatorService, ScoreCalculatorService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.MapControllers(); 

SeedData(app); 

app.Run();

void SeedData(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!context.Questions.Any())
        {
            context.Questions.AddRange(new List<QuestionAndAnswers>
            {
                new QuestionAndAnswers
                {
                    Question = "What is the capital of France?",
                    Type = QuestionType.SingleChoice,
                    Options = new List<string> { "Paris", "London", "Berlin", "Madrid" },
                    CorrectAnswers = new List<string> { "Paris" }
                },
                new QuestionAndAnswers
                {
                    Question = "Which of the following is a programming language?",
                    Type = QuestionType.MultipleChoice,
                    Options = new List<string> { "Java", "HTML", "CSS", "Word", "Javascript" },
                    CorrectAnswers = new List<string> { "Java", "Javascript" }
                },
                new QuestionAndAnswers
                {
                    Question = "What is 2 + 2?",
                    Type = QuestionType.Text,
                    CorrectAnswers = new List<string> { "4" }
                },
                new QuestionAndAnswers 
                { 
                    Question = "What is the capital of Lithuania?", 
                    Type = QuestionType.Text,
                    CorrectAnswers = new List<string> { "Vilnius" }
                },
                new QuestionAndAnswers 
                { 
                    Question = "What are the colors of traffic light?", 
                    Type = QuestionType.MultipleChoice,
                    Options = new List<string> { "Red", "Blue", "Green", "Yellow", "Pink" },
                    CorrectAnswers = new List<string> { "Red", "Green", "Yellow" }
                },
                new QuestionAndAnswers 
                { 
                    Question = "Does a triangle have odd number of angles?", 
                    Type = QuestionType.SingleChoice,
                    Options = new List<string> { "Yes", "No" },
                    CorrectAnswers = new List<string> { "Yes" }
                },
                new QuestionAndAnswers 
                { 
                    Question = "Which of these is not an operating system?", 
                    Type = QuestionType.SingleChoice,
                    Options = new List<string> { "Windows", "MacOS", "Linox", "None of above" },
                    CorrectAnswers = new List<string> { "Linox" }
                },
                new QuestionAndAnswers 
                { 
                    Question = "Which of the following are JavaScript frontend frameworks?", 
                    Type = QuestionType.MultipleChoice,
                    Options = new List<string> { "React", "Laravel", "Vue" },
                    CorrectAnswers = new List<string> { "React", "Vue" }
                },
                new QuestionAndAnswers 
                { 
                    Question = "What is the chemical symbol for water?", 
                    Type = QuestionType.Text,
                    CorrectAnswers = new List<string> { "H2O" }
                },
                new QuestionAndAnswers 
                { 
                    Question = "Which of the following is Lithuania city?", 
                    Type = QuestionType.SingleChoice,
                    Options = new List<string> { "Ryga", "Tallin", "Kaunas" },
                    CorrectAnswers = new List<string> { "Kaunas" }
                }
            });

            context.SaveChanges();
        }
    }
}

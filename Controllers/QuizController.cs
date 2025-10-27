using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using System.Text.Json;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private List <Quiz> LoadQUizzes()
        {
            var json = System.IO.File.ReadAllText("Data/quizzes.json");
            return JsonSerializer.Deserialize<List<Quiz>>(json);
        }
        public IActionResult Index(int id)
        {
            var quiz = LoadQUizzes().FirstOrDefault(x => x.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }
            return View("Take", quiz);
        }
        [HttpPost]
        public IActionResult Submit(int id, List<int>answers)
        {
            var quiz = LoadQUizzes().FirstOrDefault(X => X.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }
            int score = 0;
            for (int i = 0; i < quiz.Questions.Count; i++)
            {
                Console.WriteLine(quiz.Questions[i].CorrectAnswer);
                if (i<answers.Count && answers[i] == quiz.Questions[i].CorrectAnswer)
                {
                    score++;
                }
            }
            ViewBag.Score = score;
            ViewBag.Total = quiz.Questions.Count;
            ViewBag.Quiz = quiz;           // send the quiz object
            ViewBag.Answers = answers;     // send submitted answers
            return View("Result");
        }
    }
}

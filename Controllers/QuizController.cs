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
         
    }
}

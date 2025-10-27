using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            var json = System.IO.File.ReadAllText("Data/quizzes.json");
            var quizzes = JsonSerializer.Deserialize<List<Quiz>>(json);
            return View(quizzes);
        }

    }
}

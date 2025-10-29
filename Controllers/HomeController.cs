using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class HomeController : Controller//handles requests for the home page.
    {

        public IActionResult Index() //default action method 
        {
            var json = System.IO.File.ReadAllText("Data/quizzes.json");//Reads JSON
            var quizzes = JsonSerializer.Deserialize<List<Quiz>>(json);//convert JSON into a list
            return View(quizzes); //Pass quizzes to the page.
        }

    }
}

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using System.Text.Json;

namespace QuizApp.Controllers
{
    public class QuizController : Controller//handles actions for displaying and submitting quizzes.
    {
        private List<Quiz> LoadQUizzes()//method for loading quizzes from json
        {
            var json = System.IO.File.ReadAllText("Data/quizzes.json");//Reads JSON
            return JsonSerializer.Deserialize<List<Quiz>>(json);//convert JSON into a list
        }
        public IActionResult Index(int id)//Displays a specific quiz
        {
            var quiz = LoadQUizzes().FirstOrDefault(x => x.Id == id);//Find the quiz with that id
            if (quiz == null)//check is this quiz exist
            {
                return NotFound();
            }
            return View("Take", quiz);//returns take view with the specific quiz
        }
        [HttpPost]
        public IActionResult Submit(int id, List<int> answers)//method for submiting when user completes quiz.
        {
            var quiz = LoadQUizzes().FirstOrDefault(X => X.Id == id);//Find the quiz with that id
            if (quiz == null)//check is this quiz exist
            {
                return NotFound();
            }
            int score = 0;//
            for (int i = 0; i < quiz.Questions.Count; i++)//Loop through each question 
            {
                if (i < answers.Count && answers[i] == quiz.Questions[i].CorrectAnswer)//compare the answer to the correct one
                {
                    score++;
                }
            }
            //Storing data in ViewBag to use it in  the Result view
            ViewBag.Score = score;
            ViewBag.Total = quiz.Questions.Count;
            ViewBag.Quiz = quiz;          
            ViewBag.Answers = answers;
            return View("Result");//Loads Result view 
        }
    }
}
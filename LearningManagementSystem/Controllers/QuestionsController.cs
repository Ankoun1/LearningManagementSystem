using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Repository;
using DAL.Repository.Models;
using BL.Questions;
using Web.ViewModels.Question;

namespace LearningManagementSystem.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IQuestionBL questBL;

        public QuestionsController(IQuestionBL questBL)
        {
            this.questBL = questBL;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int courseId)
        {
            var result = await questBL.IndexQuestion(courseId);
            return View(result);
        }

        // GET: Courses/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var modul = questBL.DetailsQuestion(id);
            if (modul == null)
            {
                return NotFound();
            }

            return View(modul);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int testId, CreateQuestionFormModel question)
        {
            if (!ModelState.IsValid)
            {
                return View(question);

            }
            questBL.CreateQuestion(testId, question);
           
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GenerateAnswer()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerateAnswer(int answerId, int userId = 1)
        {
            if (answerId == 0)
            {
                return NotFound();

            }
            questBL.GenerateAnswerQuestion(answerId,userId);
           
            return RedirectToAction(nameof(Index));
        }
      

        //[HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }


            if (!questBL.DeleteQuestion(id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

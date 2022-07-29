using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Repository;
using DAL.Repository.Models;
using BL.Tests;
using Web.ViewModels.Test;

namespace LearningManagementSystem.Controllers
{
    public class TestsController : Controller
    {
        private readonly ITestBL testBL;

        public TestsController(ITestBL testBL)
        {
            this.testBL = testBL;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int courseId)
        {
            var result = await testBL.IndexTest(courseId);
            return View(result);
        }

        // GET: Courses/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var test = testBL.DetailsTest(id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
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
        public IActionResult Create(int courseId, CreateTestFormModel test)
        {
            if (!ModelState.IsValid)
            {
                return View(test);

            }
            if (!testBL.CreateTest(courseId, test))
            {
                return BadRequest();
            }
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


            if (!testBL.DeleteTest(id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

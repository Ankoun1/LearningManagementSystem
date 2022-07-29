using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Repository;
using DAL.Repository.Models;
using BL.Courses;
using Web.ViewModels.Course;

namespace LearningManagementSystem.Controllers
{
    public class CoursesController : Controller
    {        
        private readonly ICourseBL courseBL;

        public CoursesController(ICourseBL  courseBL)
        {            
            this.courseBL = courseBL;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int userId = 0)
        {
            var result = await courseBL.IndexCorse(userId);
            return View(result);
        }

        // GET: Courses/Details/5
        public  IActionResult Details(int userId,int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var course = courseBL.DetailsCourse(userId,id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
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
        public IActionResult Create(CreateCourseFormModel course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);               
               
            }
            if (!courseBL.CreateCourse(course))
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }
        
       // [HttpPost]
       // [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(int userId,int courseId)
        {
            userId = 1;
            if (userId == 0 || courseId == 0)
            {
                return BadRequest();               
               
            }
            if (!courseBL.RegisteredCourse(userId,courseId))
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int userId,int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var courseInfo = courseBL.DetailsCourse(userId,id);


            return View(new UpdateCourseFormModel { Description = courseInfo.Description, StartDate = courseInfo.StartDate,EndDate = courseInfo.EndDate });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCourseFormModel course)
        {
            ///User.GetId();
            if (!ModelState.IsValid)
            {
                return View(course);

            }
            if (!await courseBL.UpdateCourse(id, course))
            {
                return View(course);
            }

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenereteStudentScore(int courseId, int studentId = 1)
        {
            ///User.GetId();
            if ( studentId == 0 ||  courseId == 0)
            {
                return NotFound();

            }
            if (!courseBL.GenereteEndResultStudent(courseId,studentId))
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


            if (!courseBL.DeleteCourse(id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

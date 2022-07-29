using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Repository;
using DAL.Repository.Models;
using BL.Moduls;
using Web.ViewModels.Modul;

namespace LearningManagementSystem.Controllers
{
    public class ModulsController : Controller
    {
        private readonly IModulBL modulBL;

        public ModulsController(IModulBL modulBL)
        {
            this.modulBL = modulBL;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int courseId)
        {
            var result = await modulBL.IndexModul(courseId);
            return View(result);
        }

        // GET: Courses/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var modul = modulBL.DetailsModul(id);
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
        public IActionResult Create(int courseId,CreateModulFormModel modul)
        {
            if (!ModelState.IsValid)
            {
                return View(modul);

            }
            if (!modulBL.CreateModul(courseId,modul))
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var modulInfo = modulBL.DetailsModul(id);


            return View(new UpdateModulFormModel { Description = modulInfo.Description});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UpdateModulFormModel modul)
        {
            ///User.GetId();
            if (!ModelState.IsValid)
            {
                return View(modul);

            }
            if (!modulBL.UpdateModul(id, modul))
            {
                return View(modul);
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


            if (!modulBL.DeleteModul(id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

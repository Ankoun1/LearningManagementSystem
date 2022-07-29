using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Repository;
using DAL.Repository.Models;
using BL.Materials;
using Web.ViewModels.Material;

namespace LearningManagementSystem.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly IMaterialBL materialBL;

        public MaterialsController(IMaterialBL materialBL)
        {
            this.materialBL = materialBL;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int courseId)
        {
            var result = await materialBL.IndexMaterial(courseId);
            return View(result);
        }

        // GET: Courses/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var material = materialBL.DetailsMaterial(id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
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
        public IActionResult Create(int courseId, CreateMaterialFormModel material)
        {
            if (!ModelState.IsValid)
            {
                return View(material);

            }
            if (!materialBL.CreateMaterial(courseId, material))
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
            var materialInfo = materialBL.DetailsMaterial(id);


            return View(new UpdateMaterialFormModel { Link = materialInfo.Link });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UpdateMaterialFormModel material)
        {
            ///User.GetId();
            if (!ModelState.IsValid)
            {
                return View(material);

            }
            if (!materialBL.UpdateMaterial(id, material))
            {
                return View(material);
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


            if (!materialBL.DeleteMaterial(id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

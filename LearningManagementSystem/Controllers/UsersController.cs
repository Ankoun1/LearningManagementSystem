using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Repository;
using DAL.Repository.Models;
using Web.ViewModels.User;
using BL.Users;
using System.Security.Claims;

namespace LearningManagementSystem.Controllers
{
    public class UsersController : Controller
    {    

        private readonly IUserBL userBL;

        public UsersController(IUserBL userBL)
        {            
            this.userBL = userBL;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var result = await userBL.IndexUsers();
            return View(result);
        }

        public async Task<IActionResult> StudentsCours(int courseId)
        {
            var result = await userBL.StudentsCourse(courseId);
            return View(result);
        }
        
        public async Task<IActionResult> TeachersCours(int courseId)
        {
            var result = await userBL.TeachersCourse(courseId);
            return View(result);
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            
            if (id == 0)
            {
                return BadRequest();
            }

            var user = userBL.InfoUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
            // Redirect("Courses/ShippingDelivery");
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public  IActionResult Create(RegisterUserFormModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
                
            }
            if (!this.userBL.CreateUser(user))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Edit/5
      
      
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserFormModel userModel)
        {
            var user = userBL.ExistUser(userModel);
            if (user == null)
            {
                return NotFound();
            }
            
            return View(userModel);
            ///RedirectToAction(nameof(Details));
        }        
        
        public  IActionResult AdminUpdateUser(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var userInfo = userBL.InfoRole(id);
          
            
            return View(new UpdateRoleFormModel {Role = userInfo.Role});
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminUpdateUser(int id, UpdateRoleFormModel userRole)
        {
            ///User.GetId();
            if (!ModelState.IsValid)
            {
                return View(userRole);
              
            }

            if(id == 0)
            {
                return BadRequest();
            }

            if (await userBL.UpdateRole(id,userRole))
            {
                return View(userRole);
            }

           return  RedirectToAction(nameof(Index));
        }
        
        public  IActionResult Edit(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var userInfo = userBL.InfoUser(id);
         
            
            return View(new UpdateUserFormModel { FullName = userInfo.FullName,Email = userInfo.Email});
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateUserFormModel user)
        {
            ///User.GetId();
            if (!ModelState.IsValid)
            {
                return View(user);
              
            }
            if (!await userBL.UpdateUser(id,user))
            {
                return View(user);
            }

           return  RedirectToAction(nameof(Index));
        }

        //[HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }


            if (!userBL.DeleteUser(id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        
       
    }
}

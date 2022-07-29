using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Repository;
using DAL.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.User;
using static Web.ViewModels.Constants.GlobalConstants.UserConstants;

namespace BL.Users
{
  
    public class UserBL : IUserBL
    {
        private readonly AcademyDbContext data;
        private readonly IPasswordHasher passwordHasher;

        public UserBL(AcademyDbContext data, IPasswordHasher passwordHasher)
        {
            this.data = data;
            this.passwordHasher = passwordHasher;
        }


        private void CreateAdmin()
        {
            if (!data.Users.Any())
            {
                CreateRole();

                var hashedPassword1 = passwordHasher.HashPassword(AdminPassword1);
                var hashedPassword2 = passwordHasher.HashPassword(AdminPassword2);

                data.Users.AddRange(new User { FullName = AdminName1, Password = hashedPassword1, Email = AdminEmail1, IsDelited = false },
                                    new User { FullName = AdminName2, Password = hashedPassword2, Email = AdminEmail2, IsDelited = false });

                int roleId = data.Roles.Where(x => x.NameRole == RoleAdmin).Select(x => x.Id).FirstOrDefault();

                data.UsersRoles.AddRange(new UsersRole { UserId = 1, RoleId = roleId }, new UsersRole { UserId = 2, RoleId = roleId });
                data.SaveChanges();
            }
        }
        private void CreateRole()
        {
            if (!data.Roles.Any())
            {
                data.Roles.AddRange(new Role { NameRole = RoleAdmin }, new Role { NameRole = RoleOperator }, new Role { NameRole = RoleStudent }, new Role { NameRole = RoleTeacher });
                data.SaveChanges();
            }
        }



        public bool CreateUser(RegisterUserFormModel userModel)
        {
            CreateAdmin();

            bool success = false;
            if(data.Users.Any(x => x.Email == userModel.Email))
            {
                return false;
            }
            if (!(userModel.Email == AdminEmail1 || userModel.Email == AdminEmail2 || userModel.Role == RoleAdmin))
            {
                var hashedPassword = passwordHasher.HashPassword(userModel.Password);
                var user = new User { FullName = userModel.FullName, Password = hashedPassword, Email = userModel.Email, IsDelited = false };
                data.Users.Add(user);
                data.SaveChanges();
                if (userModel.Role == RoleStudent)
                {
                    var student = new Student { UserId = user.Id,IsDelited = false };
                    data.Students.Add(student);
                }
                else
                {
                    data.Teachers.Add(new Teacher { UserId = user.Id ,IsDelited = false});
                }

                data.SaveChanges();

                success = true;
            }
            return success;
        }

        public async Task<bool> UpdateUser(int userId, UpdateUserFormModel userModel)
        {
            bool success = false;

            var user = InfoUser(userId);
            
            if (user != null)
            {
                user.FullName = userModel.FullName;
                user.Email = userModel.Email;
                
                await data.SaveChangesAsync();
                success = true;
            }
            return success;
        }

        public InformationUserFormModel  InfoUser(int userId)
        {

            var user = data.Users.Where(x => x.Id == userId && x.IsDelited == false).FirstOrDefault();
            if(user == null)
            {
                return null;
            }
            var userRole = data.UsersRoles.Where(x => x.UserId == userId && x.IsDelited == false).Select(x => x.Role.NameRole).FirstOrDefault();
            return new InformationUserFormModel {Id = user.Id, FullName = user.FullName, Password = user.Password, Email = user.Email, Role = userRole };
        }

        public User ExistUser(LoginUserFormModel user)
        {
            var hashedPassword = passwordHasher.HashPassword(user.Password);


            return data
                .Users
                .Where(u => u.Email == user.Email && u.Password == hashedPassword && u.IsDelited == false)
                .FirstOrDefault();
        }

        public async Task<List<InformationUserFormModel>> IndexUsers()
        {
            return await data.Users.Where(x => x.IsDelited == false).Select(x => new InformationUserFormModel { Id = x.Id, FullName = x.FullName, Email = x.Email, Role = x.UsersRoles.Where(y => y.UserId == x.Id).Select(y => y.Role.NameRole).FirstOrDefault() }).ToListAsync();
        }

        public async Task<bool> UpdateRole(int userId, UpdateRoleFormModel userModel)
        {
         
            var userRole = data.UsersRoles.Where(x => x.UserId == userId).FirstOrDefault();
            if(userRole == null || userModel.Role == RoleAdmin)
            {
                return false;
            }
            var roleId = await data.Roles.Where(x => x.NameRole == userModel.Role).Select(x => x.Id).FirstOrDefaultAsync();
            data.UsersRoles.Add(new UsersRole { UserId = userId, RoleId = roleId });

            await data.SaveChangesAsync();

            return true;
        }

        public InformationRoleFormModel InfoRole(int userId)
        {
            var userRole = data.UsersRoles.Where(x => x.UserId == userId && x.IsDelited == false).Select(x => x.Role.NameRole).FirstOrDefault();

            if (userRole == null)
            {
                return null;
            }
            return new InformationRoleFormModel {Role = userRole };
        }

        public bool Vot(int studentId, int categoruId)
        {
            if(data.Votes.Any(x => x.StudentId == studentId && x.CourseId == categoruId && x.IsDelited == false))
            {
                return false;
            }
            data.Votes.Add(new Vote { StudentId = studentId, CourseId = categoruId });
            data.SaveChanges();
            return true;
        }

        public bool DeleteUser(int userId)
        {
            var user = data.Users.Where(x => x.Id == userId && x.IsDelited == false).FirstOrDefault();

            if(user == null)
            {
                return false;
            }

            if(data.UsersRoles.Any(x => x.UserId == userId && x.Role.NameRole == RoleAdmin))
            {
                return false;
            }

            data.UsersRoles.Where(x => x.UserId == userId).ForEachAsync(x => x.IsDelited = true);
            data.Students.Where(x => x.UserId == userId).ForEachAsync(x => x.IsDelited = true);
            data.Teachers.Where(x => x.UserId == userId).ForEachAsync(x => x.IsDelited = true);
            data.Students.Where(x => x.UserId == userId).SelectMany(x => x.StudentsCourses).ForEachAsync(x => x.IsDelited = true);
            data.Teachers.Where(x => x.UserId == userId).SelectMany(x => x.TeachersCourses).ForEachAsync(x => x.IsDelited = true);
            //var sId = data.Students.Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefault();
           // var tId = data.Teachers.Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefault();
           // data.StudentsCourses.Where(x => x.StudentId == sId).ForEachAsync(x => x.IsDelited = true);
           // data.TeachersCourses.Where(x => x.TeacherId == sId).ForEachAsync(x => x.IsDelited = true);

            user.IsDelited = true;

            data.SaveChanges();

            return true;
        }

        public async  Task<List<InformationUserFormModel>> StudentsCourse(int coursId)
        {
            var studentsId = await data.StudentsCourses.Where(x => x.CourseId == coursId).Select(x => x.StudentId).ToListAsync();
            var infoUserList = new List<InformationUserFormModel>();

            foreach (var id in studentsId)
            {
                infoUserList.Add(data.Students.Where(x => x.Id == id).Select(x => new InformationUserFormModel { FullName = x.User.FullName, Email = x.User.Email }).FirstOrDefault());
            }

            return infoUserList;
        }

        public async Task<List<InformationUserFormModel>> TeachersCourse(int coursId)
        {
            var teachersId = await data.TeachersCourses.Where(x => x.CourseId == coursId).Select(x => x.TeacherId).ToListAsync();
            var infoUserList = new List<InformationUserFormModel>();

            foreach (var id in teachersId)
            {
                infoUserList.Add(data.Students.Where(x => x.Id == id).Select(x => new InformationUserFormModel { FullName = x.User.FullName, Email = x.User.Email }).FirstOrDefault());
            }

            return infoUserList;
        }
        
    }
}    
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using DAL.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Course;

namespace BL.Courses
{
  
    public class CourseBL : ICourseBL
    {
        private readonly AcademyDbContext data;

        public CourseBL(AcademyDbContext data)
        {
            this.data = data;
        }
        public bool CreateCourse(CreateCourseFormModel courseModel)
        {
            if(data.Courses.Any(x => x.Titel == courseModel.Titel))
            {
                return false;
            }
            DateTime startDate = new DateTime(2022, 8, 1);
            DateTime endDate = (DateTime)courseModel.EndDate;
           

            var course = new Course {StartDate = courseModel.StartDate, EndDate = courseModel.EndDate, Titel = courseModel.Titel,Difficulty = courseModel.Difficulty, Description = courseModel.Description, IsDelited = false };
            data.Courses.Add(course);
            data.SaveChanges();
            for (int i = 0; i < 4; i++)
            {
                data.Moduls.Add(new Modul { Titel = "Modul" + i, Description = "Modul", CourseId = course.Id , IsDelited = false });
            }

            for (int i = 0; i < 3; i++)
            {
                data.Materials.Add(new Material {Type = "Material" + i, Link = $"c/documents/material{i}.txt", CourseId = course.Id , IsDelited = false });
            }
            data.SaveChanges();
           
            var startDateTimeTest1 = endDate.AddDays(-2).AddHours(9);
            var startDateTimeTest2 = endDate.AddDays(-1).AddHours(9);
            var endDateTimeTest = endDate.AddHours(12);
            data.Tests.AddRange(new Test { Type = "test1", Link = "c/documents/test1.txt", StartDateTime = startDateTimeTest1, EndDateTime = endDateTimeTest, Continuity = TimeSpan.FromMinutes(30), CourseId = course.Id,IsDelited = false },
                               new Test { Type = "test2", Link = "c/documents/test2.txt", StartDateTime = startDateTimeTest2, EndDateTime = endDateTimeTest, Continuity = TimeSpan.FromMinutes(30), CourseId = course.Id });
            data.SaveChanges();
            for (int i = 1; i <= 2; i++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    data.Questions.Add(new Question { Description = $"Question{j}", TestId = i,IsDelited = false});
                    data.SaveChanges();
                    for (int c  = 1; c  <= 3; c ++)
                    {
                        var answer = new Answer { Description = $"Answer{c}", IsItTrue = false, QuestionId = j,IsDelited = false };
                        if (i == 3)
                        {
                            answer.IsItTrue = true;
                        }
                        data.Answers.Add(answer);
                        
                    }
                }
                
            }
            data.SaveChanges();
            return true;
        }

        public bool DeleteCourse(int courseId)
        {
            var course = data.Courses.Where(x => x.Id == courseId && x.IsDelited == false).FirstOrDefault();
            if (course == null)
            {
                return false;
            }

            course.IsDelited = true;
            data.StudentsCourses.Where(x => x.CourseId == courseId).ForEachAsync(x => x.IsDelited = true);
            data.Materials.Where(x => x.CourseId == courseId).ForEachAsync(x => x.IsDelited = true);
            data.Tests.Where(x => x.CourseId == courseId).ForEachAsync(x => x.IsDelited = true);
            data.Tests.Where(x => x.CourseId == courseId).SelectMany(x => x.Questions).ForEachAsync(x => x.IsDelited = true);
            data.Tests.Where(x => x.CourseId == courseId).SelectMany(x => x.Questions).SelectMany(x => x.Answers).ForEachAsync(x => x.IsDelited = true); ;

            data.SaveChanges();
            return true;
        }

        public InformationCourseFormModel DetailsCourse(int userId,int courseId)
        {
            return data.Courses.Where(x => x.Id == courseId && x.IsDelited == false).Select(x => new InformationCourseFormModel { Id = x.Id, UserId = userId, Titel = x.Titel, Description = x.Description, StartDate = x.StartDate, EndDate = x.EndDate, Difficulty = x.Difficulty }).FirstOrDefault();
           
        }

        public bool GenereteEndResultStudent( int courseId,int studentId)
        {
            var assessmentsResult = data.Tests.Where(x => x.CourseId == x.CourseId && x.IsDelited == false).SelectMany(x => x.Assessments).Where(x => x.StudentId == studentId).Select(x => x.Result).Average(x => x);
            var studentCourse = data.StudentsCourses.Where(x => x.StudentId == studentId && x.CourseId == courseId).FirstOrDefault();

            if(studentCourse == null)
            {
                return false;
            }
            studentCourse.Score = (byte)assessmentsResult;
            data.SaveChanges();
            return true;
        }

        public async Task<List<InformationCourseFormModel>> IndexCorse(int userId)
        {
            return await data.Courses.Where(x => x.IsDelited == false).Select(x => new InformationCourseFormModel {Id = x.Id,UserId = userId, Titel = x.Titel, Description = x.Description,StartDate = x.StartDate,EndDate = x.EndDate, Difficulty = x.Difficulty }).ToListAsync();
        }

        public bool RegisteredCourse(int studentId, int courseId)
        {   
            if(studentId == 0 || courseId == 0 )
            {
                return false;
            }
            var userRegistersId = data.StudentsCourses.Where(x => x.StudentId == studentId && x.IsDelited == false).Select(x => x.CourseId).ToList();

            if(userRegistersId.Count() == 10)
            {
                return false;
            }

            byte count = 0;
            foreach (var item in userRegistersId)
            {
                if(data.Courses.Any(x => x.Id == item && x.Difficulty >= 4))
                {
                    count++;
                }
            }          
            if(count == 5)
            {
                return false;
            }
            if(!data.Courses.Any(x => x.Id == courseId && x.IsDelited == false && x.EndDate < DateTime.Now))
            {
                return false;
            }
            
            data.StudentsCourses.Add(new StudentsCourse { StudentId = studentId, CourseId = courseId });
            data.SaveChanges();

            return true;
        }

        public async Task<bool> UpdateCourse(int courseId,UpdateCourseFormModel courseModel)
        {
            var course = data.Courses.Where(x => x.Id == courseId && x.IsDelited == false).FirstOrDefault();

            if(course == null)
            {
                return false;
            }
            course.Description = courseModel.Description;
            course.StartDate = courseModel.StartDate;
            course.EndDate = courseModel.EndDate;
            await data.SaveChangesAsync();
            return true;
        }
    }
}

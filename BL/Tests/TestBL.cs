using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using DAL.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Test;

namespace BL.Tests
{
   public class TestBL : ITestBL
    {
        private readonly AcademyDbContext data;

        public TestBL(AcademyDbContext data)
        {
            this.data = data;
        }
        
        public bool AssessmentTest(int testId,int studentId)
        {
            var questionsCount = data.Questions.Where(x => x.TestId == testId && x.IsDelited == false).Count();

            var dateNow = DateTime.Now;
            var endDate = data.Tests.Where(x => x.Id == testId && x.IsDelited == false).Select(x => x.EndDateTime).FirstOrDefault();

            if (questionsCount == 0)
            {
                return false;
            }
            
            if (dateNow <= endDate)
            {
                return false;
            }
            var assessment = data.Assessments.Where(x => x.TeastId == testId && x.StudentId == studentId).FirstOrDefault();
            int result = questionsCount - assessment.NegativCount;
            if(result < 2)
            {
                assessment.Result = 2;
            }
            else
            {
                assessment.Result = (byte)result;
            }            
            
            data.SaveChanges();
            return true;
        }
       
        public bool CreateTest(int courseId, CreateTestFormModel testModel)
        {
            if (data.Tests.Any(x => x.Link == testModel.Link))
            {
                return false;
            }
            var courseComponnts = data.Courses.Where(x => x.Id == courseId).Select(x => new { StartDate = x.StartDate, EndDate = x.EndDate }).FirstOrDefault();

            Random random = new Random();
            int r = random.Next(1, 10);

            var startDateTimeTest = (DateTime)courseComponnts.EndDate;
            var endDateTimeTest = startDateTimeTest.AddHours(12);
            var test = new Test { Type = testModel.Type, Link = testModel.Link, StartDateTime = startDateTimeTest.AddDays(-r).AddHours(r), EndDateTime = endDateTimeTest,Continuity = TimeSpan.FromMinutes(30), CourseId = courseId, IsDelited = false };
            data.Tests.Add(test);
            data.SaveChanges();
            for (int i = 1; i <= 6; i++)
            {
                data.Questions.Add(new Question { Description = $"Question{i}", TestId = test.Id, IsDelited = false });
                for (int j = 1; j <= 3; j++)
                {
                    var answer = new Answer { Description = $"Answer{j}", IsItTrue = false, QuestionId = i, IsDelited = false };
                    if (i == 3)
                    {
                        answer.IsItTrue = true;
                    }
                    data.Answers.Add(answer);

                }
            }
            data.SaveChanges();

            return true;
        }
        

        public bool DeleteTest(int testId)
        {
            var test = data.Tests.Where(x => x.Id == testId && x.IsDelited == false).FirstOrDefault();
            if (test == null)
            {
                return false;
            }

            test.IsDelited = true;
            data.SaveChanges();

            return true;
        }

        public InformationTestFormModel DetailsTest(int testId)
        {
            return data.Tests.Where(x => x.Id == testId && x.IsDelited == false).Select(x => new InformationTestFormModel {Id = x.Id, Type = x.Type, Link= x.Link,StartDateTime = x.StartDateTime,EndDateTime = x.EndDateTime }).FirstOrDefault();
        }

     

        public async Task<List<InformationTestFormModel>> IndexTest(int courseId)
        {
            return await data.Tests.Where(x => x.CourseId == courseId && (x.StartDateTime <= DateTime.Now || x.EndDateTime > DateTime.Now ) && x.IsDelited == false).Select(x => new InformationTestFormModel { Id = x.Id, Type = x.Type, Link = x.Link, StartDateTime = x.StartDateTime, EndDateTime = x.EndDateTime }).ToListAsync();
        }

       
    }
}

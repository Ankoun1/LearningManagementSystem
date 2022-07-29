using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using DAL.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Question;

namespace BL.Questions
{
    public class QuestionBL : IQuestionBL
    {
        private readonly AcademyDbContext data;

        public QuestionBL(AcademyDbContext data)
        {
            this.data = data;
        }

        public void CreateQuestion(int testId, CreateQuestionFormModel questionModel)
        {
            data.Questions.Add(new Question {Description = questionModel.Description,TestId = testId, IsDelited = false });
            data.SaveChanges();            
        }

        public bool DeleteQuestion(int questionId)
        {
            var question = data.Questions.Where(x => x.Id == questionId && x.IsDelited == false).FirstOrDefault();
            if (question == null)
            {
                return false;
            }
            data.Answers.Where(x => x.QuestionId == question.Id).ForEachAsync(x => x.IsDelited = true);
            question.IsDelited = true;
            data.SaveChanges();

            return true;
        }

        public InformationQuestionFormModel DetailsQuestion(int questionId)
        {
            var infoAnswers = data.Answers.Where(x => x.QuestionId == questionId && x.IsDelited == false).Select(x => new InformationAnswerFormModel {Id = x.Id, Description = x.Description, IsItTrue = x.IsItTrue }).ToArray();
            return data.Questions.Where(x => x.Id == questionId && x.IsDelited == false).Select(x => new InformationQuestionFormModel {Id = x.Id, Description = x.Description ,Answers = infoAnswers}).FirstOrDefault();
        }

        public bool GenerateAnswerQuestion(int answerId,int studentId)
        {
            var answer = data.Answers.Where(x => x.Id == answerId && x.IsDelited == false).FirstOrDefault();            

            if (answer == null)
            {
                return false;
            }


            var questionId = answer.QuestionId;

            var test = data.Questions.Where(x => x.Id == questionId && x.IsDelited == false).Select(x => new {StartdfateTime = x.Test.StartDateTime, EndDateTime = x.Test.EndDateTime, Time = x.Test.Continuity }).FirstOrDefault();

            var dateTimeNow = DateTime.Now;
            if (test.StartdfateTime > dateTimeNow || test.EndDateTime <= dateTimeNow)
            {
                return false;
            }
            //v brausera tribva da se izpolzva timespana kato broiach = false
            var testId = data.Tests.Where(x => x.Id == answer.QuestionId && x.IsDelited == false).Select(x => x.Id).FirstOrDefault();

            if (testId == 0)
            {
                return false;
            }

            var assessment = data.Assessments.Where(x => x.TeastId == testId && x.StudentId == studentId ).FirstOrDefault();

            if (assessment == null)
            {
                data.Assessments.Add(new Assessment { Result = 0, StudentId = studentId, TeastId = testId , NegativCount = 1});
                data.SaveChanges();
            }

            if (answer.IsItTrue == false)
            {
                assessment.NegativCount++;
                data.SaveChanges();
            }
            
            return true;
        }

        public async Task<List<InformationQuestionFormModel>> IndexQuestion(int questionId)
        {            
            return await data.Questions.Where(x => x.TestId == questionId  && x.IsDelited == false).Select(x => new InformationQuestionFormModel {Id = x.Id, Description = x.Description }).ToListAsync();
        }       
    }
}

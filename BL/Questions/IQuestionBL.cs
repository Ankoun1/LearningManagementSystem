using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.ViewModels.Question;

namespace BL.Questions
{
    public interface IQuestionBL
    {
        void CreateQuestion(int testId, CreateQuestionFormModel questionModel);

        Task<List<InformationQuestionFormModel>> IndexQuestion(int testId);

        InformationQuestionFormModel DetailsQuestion(int questionId);        

        bool DeleteQuestion(int questionId);

        bool GenerateAnswerQuestion(int answerId,int studentId );
    }
}

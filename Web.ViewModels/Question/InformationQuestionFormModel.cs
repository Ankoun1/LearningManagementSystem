using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.ViewModels.Question
{
    public class InformationQuestionFormModel
    {        
        public int Id { get; init; }

        public string Description { get; set; }

        public InformationAnswerFormModel[] Answers { get; set; }
        
    }
}

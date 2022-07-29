using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Web.ViewModels.Constants.GlobalConstants.QuestionConstants;

namespace Web.ViewModels.Question
{
    public class CreateQuestionFormModel
    {
        [Required]
        [MinLength(DescriptionMinLength)]
        public string Description { get; init; }       
      
    }
}

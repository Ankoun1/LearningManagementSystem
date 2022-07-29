using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.ViewModels.Question
{
    public class UpdateAnswerFormModel
    {
        [Range(typeof(bool), "true", "true")]
        public bool IsItTrue { get; set; }
    }
}

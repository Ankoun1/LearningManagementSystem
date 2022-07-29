using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.ViewModels.Question
{
    public class InformationAnswerFormModel
    {
        public int Id { get; init; }

        public string Description { get; set; }

        public bool IsItTrue { get; set; }
    }
}

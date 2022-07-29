using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.ViewModels.Course
{
    public class InformationCourseFormModel
    {       
        public int Id { get; init; }

        public int UserId { get; init; }

        public string Titel { get; set; }
       
        public string Description { get; set; }
       
        public byte Difficulty { get; set; }
        
        public DateTime? StartDate { get; init; }
       
        public DateTime? EndDate { get; init; }
    }
}


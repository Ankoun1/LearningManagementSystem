using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Web.ViewModels.Constants.GlobalConstants.CourseConstants;

namespace Web.ViewModels.Course
{
    public class CreateCourseFormModel
    {
        [Required]
        [StringLength(MaxLengthTitel, MinimumLength = MinLengthTitel)]
        public string Titel { get; init; }

        [Required]
        [StringLength(MaxLengthDesc, MinimumLength = MinLengthDesc)]
        public string Description { get; init; }

        [Range(MinDifficulty,MaxDifficulty)]
        public byte Difficulty { get; init; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; init; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; init; }        
    }
}

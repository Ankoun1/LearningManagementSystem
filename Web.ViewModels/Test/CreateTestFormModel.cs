using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Web.ViewModels.Constants.GlobalConstants.TestConstants;

namespace Web.ViewModels.Test
{
    public class CreateTestFormModel
    {
        [Required]
        [StringLength(TypeMaxLength, MinimumLength = TypeMinLength)]
        public string Type { get; init; }

        [Required]
        [StringLength(LinkMaxLength, MinimumLength = LinkMinLength)]
        public string Link { get; init; }
    }
}

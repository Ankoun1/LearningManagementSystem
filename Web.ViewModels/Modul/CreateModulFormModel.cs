using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Web.ViewModels.Constants.GlobalConstants.ModulConstants;

namespace Web.ViewModels.Modul
{
    public class CreateModulFormModel
    {
        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; init; }

        [Required]
        [StringLength(TitelMaxLength, MinimumLength = TitelMinLength)]
        public string Titel { get; init; }
    }
}

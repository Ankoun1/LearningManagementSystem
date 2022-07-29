using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Web.ViewModels.Constants.GlobalConstants.MaterialConstants;
namespace Web.ViewModels.Material
{
    public class CreateMaterialFormModel
    {
        [Required]
        [StringLength(MaterialMaxLength, MinimumLength = MaterialMinLength)]
        public string Type { get; init; }

        [Required]
        [Url]
        public string Link { get; init; }
    }
}

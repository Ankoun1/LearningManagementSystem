using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Web.ViewModels.Constants.GlobalConstants.UserConstants;

namespace Web.ViewModels.User
{
    public class UpdateUserFormModel
    {
        [Required]
        [StringLength(MaxLengthName, MinimumLength = MinLengthName)]
        [RegularExpression(FullNameRegex, ErrorMessage = FullNameError)]
        public string FullName { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }          
    }
}

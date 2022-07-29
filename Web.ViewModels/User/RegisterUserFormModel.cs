using System.ComponentModel.DataAnnotations;
using static Web.ViewModels.Constants.GlobalConstants.UserConstants;

namespace Web.ViewModels.User
{
    public class RegisterUserFormModel
    {
        [Required]
        [StringLength(MaxLengthName, MinimumLength = MinLengthName)]
        [RegularExpression(FullNameRegex, ErrorMessage = FullNameError)]
        public string FullName { get; init; }

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; init; }

        [EmailAddress]
        public string Email { get; init; }

        public int? TeacherId { get; init; }

        public int? StudentId { get; init; }

        [Required]
        public string Role { get; init; }
    }
}

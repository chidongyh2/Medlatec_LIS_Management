using System.ComponentModel.DataAnnotations;

namespace LIS.Authentication.Requests
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password require at least one upper case letter, one lower case letter and one numeric digit")]
        public string Password { get; set; }
    }
}

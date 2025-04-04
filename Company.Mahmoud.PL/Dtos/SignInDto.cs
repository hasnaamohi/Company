using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class SignInDto
    {
        [Required(ErrorMessage = " LastName Is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = " Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}

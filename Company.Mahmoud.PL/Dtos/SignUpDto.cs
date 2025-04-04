using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class SignUpDto
    {
        [Required (ErrorMessage =" Name Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = " FirstName Is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = " LastName Is Required")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = " LastName Is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = " Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = " Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="The ConFirm Password Does Not Equal Password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}

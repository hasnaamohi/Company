using System.ComponentModel.DataAnnotations;

namespace Company.Mahmoud.PL.Dtos
{
    public class DepartmentDto
    {
        [Required(ErrorMessage="the code Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "the name Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "the date Required")]
        public DateTime CreateAt { get; set; }
    }
}

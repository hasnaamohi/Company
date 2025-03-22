

using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class UpdateDepartmentDto
    {
        [Required(ErrorMessage = "the code Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "the name Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "the date Required")]
        public DateTime CreateAt { get; set; }
    }
}

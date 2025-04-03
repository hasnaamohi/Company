using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class EmployeeDto
    {
        

        [Required (ErrorMessage = "the code Required")]
        public string Name { get; set; }

        [Range(22,60,ErrorMessage="The Age Must be Between 20:60")]
        public int Age { get; set; }

        [DataType(DataType.EmailAddress ,ErrorMessage="the Email not valid")]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage ="address must be like 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }
        [DisplayName("Date of Creation")]
        public DateTime CreateAt { get; set; }= DateTime.Now;
        [DisplayName("Department")]
        public int? DepartmentId { get; set; }
        public string? ImageName { get; set; }
        
        public IFormFile? Image {  get; set; }
    }
}

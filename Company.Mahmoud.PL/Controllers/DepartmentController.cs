using Company.Mahmoud.DAL.Models;
using Company.Mahmoud.PL.Dtos;
using Company.Mahmoud.PLL.Interfaces;
using Company.Mahmoud.PLL.Repositry;
using Microsoft.AspNetCore.Mvc;

namespace Company.Mahmoud.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepositry _departmentRepositry;
        public DepartmentController(IDepartmentRepositry departmentRepositry)
        {
            _departmentRepositry = departmentRepositry;
        }
        public IActionResult Index()
        {
            
            var departments= _departmentRepositry.GetAll();    
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public IActionResult Create(DepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt

                };
                var count =_departmentRepositry.add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                } 
            }
            return View(model);
        }
    }
}

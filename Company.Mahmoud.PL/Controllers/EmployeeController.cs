using Company.Mahmoud.DAL.Models;
using Company.Mahmoud.PL.Dtos;
using Company.Mahmoud.PLL.Interfaces;
using Company.Mahmoud.PLL.Repositry;
using Company.PL.Dtos;
using Company.PLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepositry _EmployeeRepositry;
        public EmployeeController(IEmployeeRepositry EmployeeRepositry)
        {
            _EmployeeRepositry = EmployeeRepositry;
        }
        public IActionResult Index()
        {

            var employees = _EmployeeRepositry.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    
                    Name = model.Name,
                    Address = model.Address,
                    
                    Email = model.Email,
                    IsActive = model.IsActive,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Salary = model.Salary,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone ,
                    Age=model.Age

                };
                var count = _EmployeeRepositry.add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) { return BadRequest("Invaild , Enter Employee Id"); };
            var employee = _EmployeeRepositry.GetById(id.Value);
            if (employee is null) { return NotFound($"Empolyee with id :{id} Not Found"); }

            return View(viewName, employee);


        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, Employee model)
        {
            if (ModelState.IsValid)
            {
                if (id!=model.Id)
                { return BadRequest("Invaild , Enter Employee Id"); }
                var count =_EmployeeRepositry.update(model);
                if(count>0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }


       
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) { return BadRequest("Invaild , Enter Employee Id"); }
                var count = _EmployeeRepositry.delete(model);
                if (count > 0) { return RedirectToAction(nameof(Index)); }
            }
            return View(model);

        }
    }
}

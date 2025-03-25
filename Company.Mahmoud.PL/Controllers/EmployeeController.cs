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
        private readonly IDepartmentRepositry _departmentRepositry;

        public EmployeeController(IEmployeeRepositry EmployeeRepositry,IDepartmentRepositry departmentRepositry)
        {
            _EmployeeRepositry = EmployeeRepositry;
            _departmentRepositry = departmentRepositry;
        }
        public IActionResult Index(string SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            { 
               employees = _EmployeeRepositry.GetAll();
            }
            else
            {
                employees = _EmployeeRepositry.GetByName(SearchInput);
            }
            
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepositry.GetAll();
            ViewData["departments"] = departments;
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
                    Age=model.Age,
                    DepartmentId = model.DepartmentId

                };
                var count = _EmployeeRepositry.add(employee);
                if (count > 0)
                {
                    TempData["Message"]="Employee Is Created";
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

        #region EditParialViewError
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepositry.GetAll();
            ViewData["departments"] = departments;
            if (id is null) { return BadRequest("Invaild , Enter Employee Id"); };
            var employee = _EmployeeRepositry.GetById(id.Value);
            if (employee is null) { return NotFound($"Empolyee with id :{id} Not Found"); }
            var employeeDto = new EmployeeDto()
            {

                Name = employee.Name,
                Address = employee.Address,

                Email = employee.Email,
                IsActive = employee.IsActive,
                CreateAt = employee.CreateAt,
                HiringDate = employee.HiringDate,
                Salary = employee.Salary,
                IsDeleted = employee.IsDeleted,
                Phone = employee.Phone,
                Age = employee.Age

            };
            return View(employeeDto);


        }

           // return Details(id, "Edit");
      

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                //  if (id!=model.Id)
                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Address = model.Address,

                    Email = model.Email,
                    IsActive = model.IsActive,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Salary = model.Salary,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone,
                    Age = model.Age

                };

                //{// return BadRequest("Invaild , Enter Employee Id"); }
                var count = _EmployeeRepositry.update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
        #endregion

        #region editErorr2
        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{

        //    return Details(id, "Edit");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public IActionResult Edit([FromRoute] int id, EmployeeDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id != model.Id)

        //        { return BadRequest("Invaild , Enter Employee Id"); }
        //        var count = _EmployeeRepositry.update(model);
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }

        //    return View(model);
        //} 
        #endregion


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

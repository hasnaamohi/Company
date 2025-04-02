using Company.Mahmoud.DAL.Models;
using Company.Mahmoud.PL.Dtos;
using Company.Mahmoud.PLL.Interfaces;
using Company.Mahmoud.PLL.Repositry;
using Company.PL.Dtos;
using Company.PLL;
using Company.PLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Mahmoud.PL.Controllers
{
    public class DepartmentController : Controller
    {
        // private readonly IDepartmentRepositry _departmentRepositry;
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            //_departmentRepositry = departmentRepositry;
               _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            
            var departments= _unitOfWork.DepartmentRepositry.GetAll();    
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
                 _unitOfWork.DepartmentRepositry.add(department);
                var count = _unitOfWork.Complete();
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
            if (id is null) { return BadRequest("Invaild , Enter Department Id"); };
            var department = _unitOfWork.DepartmentRepositry.GetById(id.Value);
            if (department is null) { return NotFound($"department with id :{id} Not Found"); }

            return View(viewName, department);


        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, Department model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                { return BadRequest("Invaild , Enter Employee Id"); }
                _unitOfWork.DepartmentRepositry.update(model);
                var count = _unitOfWork.Complete();
                if (count > 0)
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
        public IActionResult Delete([FromRoute] int id, Department model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) { return BadRequest("Invaild , Enter department Id"); }
                 _unitOfWork.DepartmentRepositry.delete(model);
                var count = _unitOfWork.Complete();
                if (count > 0) { return RedirectToAction(nameof(Index)); }
            }
            return View(model);

        }

    }
}

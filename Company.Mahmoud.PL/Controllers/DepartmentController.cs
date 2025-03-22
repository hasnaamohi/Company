using Company.Mahmoud.DAL.Models;
using Company.Mahmoud.PL.Dtos;
using Company.Mahmoud.PLL.Interfaces;
using Company.Mahmoud.PLL.Repositry;
using Company.PL.Dtos;
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
        [HttpGet]
        public IActionResult Details(int? id, string viewName="Details")
        {
            if (id is null) { return BadRequest("Invaild , Enter Department Id"); };
           var department= _departmentRepositry.GetById(id.Value);
            if (department is null) { return NotFound($"department with id :{id} Not Found"); }

            return View(viewName,department);
        
            
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if  (id is null) {return BadRequest("Invaild , Enter Department Id"); }
            var department = _departmentRepositry.GetById(id.Value);
            if (department == null) { return NotFound($"department with id :{id} Not Found"); }

            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            //if (ModelState.IsValid)
            //{
            //    if (id != department.Id) { return BadRequest("Invaild , Enter Department Id"); }
            //    var count = _departmentRepositry.update(department);
            //    if (count > 0) { return RedirectToAction(nameof(Index)); }
            //}
            //return View(department);
            return Details(id, "Edit");
        }
        #region comment
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute]int id ,UpdateDepartmentDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department();
        //        {
        //            department.Id = id;
        //            department.Name = model.Name;
        //            department.Code = model.Code;
        //            department.CreateAt=model.CreateAt;

        //        }
        //        var count = _departmentRepositry.update(department);
        //        if (count > 0) { return RedirectToAction(nameof(Index)); }
        //    }
        //    return View(model);
        //} 
        #endregion
        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (id is null) { return BadRequest("Invaild , Enter Department Id"); }
            var department = _departmentRepositry.GetById(id.Value);
            if (department == null) { return NotFound($"department with id :{id} Not Found"); }

            return View(department);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            //if (ModelState.IsValid)
            //{
            //    if (id != department.Id) { return BadRequest("Invaild , Enter Department Id"); }
            //    var count = _departmentRepositry.delete(department);
            //    if (count > 0) { return RedirectToAction(nameof(Index)); }
            //}
            //return View(department);
            return Details(id, "Delete");
        }

    }
}

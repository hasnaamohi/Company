using Company.Mahmoud.DAL.Models;
using Company.Mahmoud.PL.Dtos;
using Company.Mahmoud.PLL.Interfaces;
using Company.Mahmoud.PLL.Repositry;
using Company.PL.Dtos;
using Company.PLL;
using Company.PLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Mahmoud.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        // private readonly IDepartmentRepositry _departmentRepositry;
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            //_departmentRepositry = departmentRepositry;
               _unitOfWork = unitOfWork;

        }
        public async Task < IActionResult> Index()
        {
            
            var departments=await _unitOfWork.DepartmentRepositry.GetAllAsync();    
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt

                };
                await _unitOfWork.DepartmentRepositry.addAsync(department);
                var count =await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                } 
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) { return BadRequest("Invaild , Enter Department Id"); }
            var department =await _unitOfWork.DepartmentRepositry.GetByIdAsync(id.Value);
            if (department is null) { return NotFound($"department with id :{id} Not Found"); }

            return View(viewName, department);


        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id is null) 
            { 
                return BadRequest("Invaild , Enter Department Id"); 
            }
            var department = await _unitOfWork.DepartmentRepositry.GetByIdAsync(id.Value);
            if (department is null) 
            {
                return NotFound($"department with id :{id} Not Found");
            }

            var dto = new DepartmentDto()
            {
                Name = department.Name,
                Code = department.Code,
                CreateAt = department.CreateAt
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id = id,
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt,

                };
                //if (id != model.Id)
                //{ return BadRequest("Invaild , Enter Employee Id"); }
                _unitOfWork.DepartmentRepositry.update(department);
                var count =await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            { 
                return BadRequest("Invaild , Enter Department Id");
            }
            var department = await _unitOfWork.DepartmentRepositry.GetByIdAsync(id.Value);
            if (department is null) 
            { 
                return NotFound($"department with id :{id} Not Found");
            }

            var Dto = new DepartmentDto()
            {
                Name = department.Name,
                Code = department.Code,
                CreateAt = department.CreateAt
            };
            return View(Dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, DepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id = id,
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt,

                };
                //if (id != model.Id) { return BadRequest("Invaild , Enter department Id"); }
                _unitOfWork.DepartmentRepositry.delete(department);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }

    }
}

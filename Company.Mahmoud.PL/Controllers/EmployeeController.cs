using AutoMapper;
using Company.Mahmoud.DAL.Models;
using Company.Mahmoud.PL.Dtos;
using Company.Mahmoud.PLL.Interfaces;
using Company.Mahmoud.PLL.Repositry;
using Company.PL.Dtos;
using Company.PL.Helper;
using Company.PLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepositry _EmployeeRepositry;
        //private readonly IDepartmentRepositry _departmentRepositry;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepositry EmployeeRepositry,
            //IDepartmentRepositry departmentRepositry,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
           _unitOfWork = unitOfWork;
            //_EmployeeRepositry = EmployeeRepositry;
            //_departmentRepositry = departmentRepositry;
            _mapper = mapper;
        }
        public async Task<IActionResult>Index(string SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            { 
               employees = await _unitOfWork.EmployeeRepositry.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepositry.GetByNameAsync(SearchInput);
            }
            
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepositry.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                }
              

                var employee=_mapper.Map<Employee>(model);
                 await _unitOfWork.EmployeeRepositry.addAsync(employee);
                var count =await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    TempData["Message"]="Employee Is Created";
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) { return BadRequest("Invaild , Enter Employee Id"); };
            var employee =await _unitOfWork.EmployeeRepositry.GetByIdAsync(id.Value);
            if (employee is null) { return NotFound($"Empolyee with id :{id} Not Found"); }

            return View(viewName, employee);


        }

        #region EditParialViewError
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var departments =await _unitOfWork.DepartmentRepositry.GetAllAsync();
            ViewData["departments"] = departments;
            if (id is null) { return BadRequest("Invaild , Enter Employee Id"); };
            var employee =await _unitOfWork.EmployeeRepositry.GetByIdAsync(id.Value);
            if (employee is null) { return NotFound($"Empolyee with id :{id} Not Found"); }
            #region manualCasting
            //var employeeDto = new EmployeeDto()
            //{

            //    Name = employee.Name,
            //    Address = employee.Address,

            //    Email = employee.Email,
            //    IsActive = employee.IsActive,
            //    CreateAt = employee.CreateAt,
            //    HiringDate = employee.HiringDate,
            //    Salary = employee.Salary,
            //    IsDeleted = employee.IsDeleted,
            //    Phone = employee.Phone,
            //    Age = employee.Age

            //}; 
            #endregion
            var employeeDto=_mapper.Map<EmployeeDto>(employee);
            return View(employeeDto);


        }

           // return Details(id, "Edit");
      

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageName is not null && model.Image is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "images");
                }
                if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                }
                #region ManualMapping
                //  if (id!=model.Id)
                //var employee = new Employee()
                //{
                //    Id = id,
                //    Name = model.Name,
                //    Address = model.Address,

                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    Salary = model.Salary,
                //    IsDeleted = model.IsDeleted,
                //    Phone = model.Phone,
                //    Age = model.Age

                //}; 
                #endregion
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                //{// return BadRequest("Invaild , Enter Employee Id"); }
               _unitOfWork.EmployeeRepositry.update(employee);
                var count = await _unitOfWork.CompleteAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                  employee.Id=id;
               // if (id != model.Id) { return BadRequest("Invaild , Enter Employee Id"); }
                 _unitOfWork.EmployeeRepositry.delete(employee);
                var count =await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(model.ImageName, "images");
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
    }
}

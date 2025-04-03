using Company.Mahmoud.DAL.Data.Context;
using Company.Mahmoud.DAL.Models;
using Company.PLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.PLL.Repositry
{
    public class EmployeeRepositry : GenericRepositry<Employee>, IEmployeeRepositry
    {
        private readonly CompanyDbContextcs _context;

        public EmployeeRepositry(CompanyDbContextcs context):base(context) 
        {
            _context = context;
        }

        public async Task<List<Employee>> GetByNameAsync(string name)
        {

            return await _context.Employees.Include(E=>E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            
        }


        #region BeforeUseGeneric
        //private readonly CompanyDbContextcs _context;
        //public EmployeeRepositry(CompanyDbContextcs context)
        //{
        //    context = _context;
        //}
        //public IEnumerable<Employee> GetAll()
        //{
        //    return _context.Employees.ToList();
        //}
        //public Employee? GetById(int id)
        //{
        //    return _context.Employees.Find(id);
        //}

        //public int add(Employee model)
        //{
        //   _context.Employees.Add(model);
        //    return _context.SaveChanges();

        //}


        //public int update(Employee model)
        //{
        //    _context.Employees.Update(model);
        //    return _context.SaveChanges();
        //}

        //public int delete(Employee model)
        //{
        //    _context.Employees.Remove(model);
        //    return _context.SaveChanges();
        //} 
        #endregion

    }
}

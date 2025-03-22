using Company.Mahmoud.DAL.Data.Context;
using Company.Mahmoud.DAL.Models;
using Company.Mahmoud.PLL.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Mahmoud.PLL.Repositry
{
    public class DepartmentRepositry : IDepartmentRepositry
    {
        private readonly CompanyDbContextcs  _Context;
        public DepartmentRepositry(CompanyDbContextcs Context)
        {
            _Context = Context;
        }
        public IEnumerable<Department> GetAll()
        {
            return _Context.Departments.ToList();
        }

        public Department? GetById(int id)
        {
            _Context.Departments.Find(id);
            return _Context.Departments.First();
        }
        public int add(Department model)
        {
            _Context.Departments.Add(model);
            return _Context.SaveChanges();
            
        }


        public int update(Department model)
        {
            _Context.Departments.Update(model);
            return _Context.SaveChanges();
        }

        public int delete(Department model)
        {
            _Context.Departments.Remove(model);
            return _Context.SaveChanges();
        }
    }
}

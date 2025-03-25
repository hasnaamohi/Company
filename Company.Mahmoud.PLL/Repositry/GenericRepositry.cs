using Company.DAL.Models;
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
    public class GenericRepositry<T> : IGenericRepositry<T> where T : BaseEntity
    {
        private readonly CompanyDbContextcs _context;

        public GenericRepositry(CompanyDbContextcs context)
        {
            _context = context;
        }

         public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_context.Employees.Include(E => E.Department).ToList();
            }
            return _context.Set<T>().ToList();
        }

        public T? GetById(int id)

        {
            if (typeof(T) == typeof(Employee))
            {
                return _context.Employees.Include(E => E.Department).FirstOrDefault(E => E.Id == id) as T;
            }
            return _context.Set<T>().Find(id);
        }
        public int add(T model)
        {
            _context.Set<T>().Add(model);
            return _context.SaveChanges();
        }



        public int update(T model)
        {
            _context.Set<T>().Update(model);
            return _context.SaveChanges();
        }

        public int delete(T model)
        {
            _context.Set<T>().Remove(model);
            return _context.SaveChanges();
        }

    }
}

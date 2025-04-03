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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(E => E.Department).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)

        {
            if (typeof(T) == typeof(Employee))
            {
                return await _context.Employees.Include(E => E.Department).FirstOrDefaultAsync(E => E.Id == id) as T;
            }
            return _context.Set<T>().Find(id);
        }
        public async Task addAsync(T model)
        {
            await _context.Set<T>().AddAsync(model);
           
        }



        public void update(T model)
        {
            _context.Set<T>().Update(model);
           
        }

        public void delete(T model)
        {
            _context.Set<T>().Remove(model);
           
        }

    }
}

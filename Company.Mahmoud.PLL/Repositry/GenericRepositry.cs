using Company.DAL.Models;
using Company.Mahmoud.DAL.Data.Context;
using Company.PLL.Interfaces;
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
            return _context.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
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

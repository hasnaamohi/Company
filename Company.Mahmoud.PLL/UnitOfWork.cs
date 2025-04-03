using Company.Mahmoud.DAL.Data.Context;
using Company.Mahmoud.PLL.Interfaces;
using Company.Mahmoud.PLL.Repositry;
using Company.PLL.Interfaces;
using Company.PLL.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.PLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContextcs _context;

        public IDepartmentRepositry DepartmentRepositry {  get; }

        public IEmployeeRepositry EmployeeRepositry { get; }
        public UnitOfWork(CompanyDbContextcs context)
        {
            _context = context;
            DepartmentRepositry = new DepartmentRepositry(_context);
            EmployeeRepositry = new EmployeeRepositry(_context);
         
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
           await _context.DisposeAsync();
        }
    }
}

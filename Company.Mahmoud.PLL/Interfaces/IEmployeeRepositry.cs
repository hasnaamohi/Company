using Company.Mahmoud.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.PLL.Interfaces
{
    public interface IEmployeeRepositry
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        int add(Employee model);
        int update(Employee model);
        int delete(Employee model);
    }
}

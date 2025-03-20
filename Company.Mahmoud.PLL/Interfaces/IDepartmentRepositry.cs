using Company.Mahmoud.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Mahmoud.PLL.Interfaces
{
    public interface IDepartmentRepositry
    {
        IEnumerable<Department> GetAll();
        Department? GetById(int id);
        int add(Department model);
        int update(Department model);
        int delete(Department model);
    }
}

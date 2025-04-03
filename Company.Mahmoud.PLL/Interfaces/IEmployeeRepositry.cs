using Company.Mahmoud.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.PLL.Interfaces
{
    public interface IEmployeeRepositry:IGenericRepositry<Employee>
    {
        
        Task<List<Employee>> GetByNameAsync(string name);

    }
}

using Company.Mahmoud.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.PLL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public  IDepartmentRepositry DepartmentRepositry { get;}
        public IEmployeeRepositry EmployeeRepositry{ get; }
        int Complete();
    }
}

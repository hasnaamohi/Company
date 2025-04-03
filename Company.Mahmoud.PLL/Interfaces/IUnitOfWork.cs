using Company.Mahmoud.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.PLL.Interfaces
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        public  IDepartmentRepositry DepartmentRepositry { get;}
        public IEmployeeRepositry EmployeeRepositry{ get; }
        Task<int> CompleteAsync();
    }
}

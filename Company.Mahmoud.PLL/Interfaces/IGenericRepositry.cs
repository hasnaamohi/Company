using Company.DAL.Models;
using Company.PLL.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.PLL.Interfaces
{
   public interface IGenericRepositry<T> where T : BaseEntity

    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void add(T model);
        void update(T model);
        void delete(T model);
    }

}

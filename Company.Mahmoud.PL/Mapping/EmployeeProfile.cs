using AutoMapper;
using Company.Mahmoud.DAL.Models;
using Company.PL.Dtos;

namespace Company.PL.Mapping
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>();

        }
    }
}

using AutoMapper;
using Route.C41.DAL.Models;
using Route.C41.PL.ViewModels;


namespace Route.C41.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
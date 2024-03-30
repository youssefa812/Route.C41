using Microsoft.Extensions.DependencyInjection;
using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Repositories;
using Route.C41.PL.Helpers;
using AutoMapper;

namespace Route.C41.PL.Extensions
{
	public static class ApplicationServicessExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			//services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			//services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			services.AddScoped<IUnitOfWork, UnitfoWork>();

			services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));


            return services;
		}
	}
}

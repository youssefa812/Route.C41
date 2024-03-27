using Microsoft.Extensions.DependencyInjection;
using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Repositories;

namespace Route.C41.PL.Extensions
{
	public static class ApplicationServicessExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			services.AddScoped<IEmployeeRepository, EmployeeRepository>();

			return services;
		}
	}
}

using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Interfaces
{
	public interface IEmployeeRepository : IGenaricRepository<Employee>
	{
		IQueryable<Employee> GetEmployeesByAddress(string address);

		IQueryable<Employee> SearchByName(string Name);
	}
}

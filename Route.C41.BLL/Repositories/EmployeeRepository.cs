using Microsoft.EntityFrameworkCore;
using Route.C41.BLL.Interfaces;
using Route.C41.DAL.Data;
using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Repositories
{
	public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
	{
		//private readonly ApplicationDbContext _dbContext;

		public EmployeeRepository(ApplicationDbContext dbContext) // Ask CLR for Creating object from DbContext Class
			:base(dbContext)
        {
			//_dbContext = dbContext;
		}
        public IQueryable<Employee> GetEmployeesByAddress(string address)
		{
			return _dbContext.Employees.Where(E => E.Address.ToLower() == address.ToLower());
		}
		public override async Task<IEnumerable<Employee>> GetAllAsync()
			=> await _dbContext.Employees.Include(E => E.Department).ToListAsync();
		
		public IQueryable<Employee> SearchByName(string name)
			=> _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(E => E.Department);
	}
}

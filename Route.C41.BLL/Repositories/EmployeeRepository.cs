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
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationDbContext _dbContext; //  null

		public EmployeeRepository(ApplicationDbContext dbContext) // Ask CLR for Creating Object From "ApplicationDbContext"
		{
			_dbContext = dbContext;
		}

		public int Add(Employee entity)
		{
			_dbContext.Add(entity);
			return _dbContext.SaveChanges();
		}

		public int Update(Employee entity)
		{
			_dbContext.Update(entity);
			return _dbContext.SaveChanges();
		}
		public int Delete(Employee entity)
		{
			_dbContext.Employees.Remove(entity);
			return _dbContext.SaveChanges();
		}

		public Employee Get(int id)
		{
			//return _dbContext.Employees.Find(id);
			return _dbContext.Find<Employee>(id); //EF Core 3.1 NEW Feature
			///var Employee = _dbContext.Employees.Local.Where(D => D.Id == id).FirstOrDefault();
			///if (Employee == null)
			///    Employee = _dbContext.Employees.Where(D => D.Id == id).FirstOrDefault();
			///return Employee;
		}

		public IEnumerable<Employee> GetAll()
			=> _dbContext.Employees.AsNoTracking().ToList();


	}
}

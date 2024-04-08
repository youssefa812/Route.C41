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
	public class GenericRepository<T> : IGenaricRepository<T> where T : ModelBase
	{
		private protected readonly ApplicationDbContext _dbContext; //  null

		public GenericRepository(ApplicationDbContext dbContext) 
		{
			_dbContext = dbContext;
		}

		public void Add(T entity)
		{
			//_dbContext.Set<T>().Add(entity);
			_dbContext.Add(entity);
		}

		public void Update(T entity)
		{
			_dbContext.Update(entity);
		}
		public void Delete(T entity)
		{
			_dbContext.Remove(entity);
		}

		public async Task<T> GetAsync(int id)
		{
			//return _dbContext.Set<T>().Find(id);
			return await _dbContext.FindAsync<T>(id); //EF Core 3.1 NEW Feature
			///var Employee = _dbContext.Employees.Local.Where(D => D.Id == id).FirstOrDefault();
			///if (Employee == null)
			///    Employee = _dbContext.Employees.Where(D => D.Id == id).FirstOrDefault();
			///return Employee;
		}

        virtual public async Task<IEnumerable<T>> GetAllAsync()
			=> await _dbContext.Set<T>().AsNoTracking().ToListAsync();
    }
}

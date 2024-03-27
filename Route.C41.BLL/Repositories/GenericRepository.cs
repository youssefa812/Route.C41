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

		public int Add(T entity)
		{
			//_dbContext.Set<T>().Add(entity);
			_dbContext.Add(entity);
			return _dbContext.SaveChanges();
		}

		public int Update(T entity)
		{
			_dbContext.Update(entity);
			return _dbContext.SaveChanges();
		}
		public int Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			return _dbContext.SaveChanges();
		}

		public T Get(int id)
		{
			//return _dbContext.Set<T>().Find(id);
			return _dbContext.Find<T>(id); //EF Core 3.1 NEW Feature
			///var Employee = _dbContext.Employees.Local.Where(D => D.Id == id).FirstOrDefault();
			///if (Employee == null)
			///    Employee = _dbContext.Employees.Where(D => D.Id == id).FirstOrDefault();
			///return Employee;
		}

        virtual public IEnumerable<T> GetAll()
			=> _dbContext.Set<T>().AsNoTracking().ToList();
    }
}

using Route.C41.BLL.Interfaces;
using Route.C41.DAL.Data;
using Route.C41.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Repositories
{
	public class UnitfoWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _applicationDbContext;

		private Hashtable _repositories;

		public UnitfoWork(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
			_repositories = new Hashtable();
		}

		public async Task<int> Complete()
			=> await _applicationDbContext.SaveChangesAsync();

		public async ValueTask DisposeAsync()
			=>await _applicationDbContext.DisposeAsync();
		public IGenaricRepository<T> Repository<T>() where T : ModelBase
		{
			var key = typeof(T).Name; // Employee

			if (!_repositories.ContainsKey(key))
			{

				if (key == nameof(Employee))
				{
					var repository = new EmployeeRepository(_applicationDbContext);
					_repositories.Add(key, repository);

				}
				else
				{
					var repository = new GenericRepository<T>(_applicationDbContext);
					_repositories.Add(key, repository);

				}
			}

			return _repositories[key] as IGenaricRepository<T>;
		}

	}
}

﻿using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Interfaces
{
	public interface IGenaricRepository<T> where T : ModelBase
	{
		Task <IEnumerable<T>> GetAllAsync();

		Task<T> GetAsync(int id);

		void Add(T entity);

		void Update(T entity);

		void Delete(T entity);
	}
}

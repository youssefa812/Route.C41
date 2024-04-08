using Route.C41.BLL.Repositories;
using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenaricRepository<T> Repository<T>() where T : ModelBase;
        Task<int> Complete();
    }
}

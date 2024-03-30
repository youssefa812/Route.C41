using Route.C41.BLL.Interfaces;
using Route.C41.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Repositories
{
    public class UnitfoWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IEmployeeRepository EmployeeRepository { get; set; } = null;
        public IDepartmentRepository DepartmentRepository { get; set; } = null;
        public UnitfoWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            EmployeeRepository = new EmployeeRepository(applicationDbContext);
            DepartmentRepository = new DepartmentRepository(applicationDbContext);
        }

        public int Complete()
        {
            return _applicationDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}

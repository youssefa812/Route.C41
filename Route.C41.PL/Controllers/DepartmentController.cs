using Microsoft.AspNetCore.Mvc;
using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Repositories;
using Route.C41.DAL.Models;

namespace Route.C41.PL.Controllers
{
    // Inhertiance : DepartmentController is a Controller
    // Association [Composition] : DepartmentController has a DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentsRepo;

        public DepartmentController(IDepartmentRepository departmentsRepo) // Ask CLR for Creating an Object from Class Implmenting IDepartmentRepository
        {
            _departmentsRepo = departmentsRepo;
        }

        // /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentsRepo.GetAll();

            return View(departments);
        }

        // /Department/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid) // Server Side Validation
            {
                var Count = _departmentsRepo.Add(department);
                if(Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }
    }
}

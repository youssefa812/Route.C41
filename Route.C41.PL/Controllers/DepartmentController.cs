using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Repositories;
using Route.C41.DAL.Models;
using Route.C41.PL.ViewModels;
using System;
using System.Collections.Generic;

namespace Route.C41.PL.Controllers
{
	// Inhertiance : DepartmentController is a Controller
	// Association [Composition] : DepartmentController has a DepartmentRepository
	public class DepartmentController : Controller
	{
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentsRepo;
		private readonly IWebHostEnvironment _env;

		public DepartmentController(IMapper mapper, IDepartmentRepository departmentsRepo, IWebHostEnvironment env) // Ask CLR for Creating an Object from Class Implmenting IDepartmentRepository
		{
            _mapper = mapper;
            _departmentsRepo = departmentsRepo;
			_env = env;
		}

		// /Department/Index
		public IActionResult Index()
		{
			var departments = _departmentsRepo.GetAll();

            var mappedDeps = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

            return View(mappedDeps);
        }

		// /Department/Create
		//[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(DepartmentViewModel departmentVm)
		{
			if (ModelState.IsValid) // Server Side Validation
			{
                var department = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
                var count = _departmentsRepo.Add(department);
                if (count > 0)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			return View(departmentVm);
		}

		// /Department/Details/10
		//[HttpGet]
		public IActionResult Details(int? id, string viewName = "Details")
		{
			if (!id.HasValue)
				return BadRequest(); // 400

			var department = _departmentsRepo.Get(id.Value);

			if (department is null)
				return NotFound(); // 404

            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);

            return View(viewName, mappedDep);
        }

		// /Department/Edit/10
		// /Department/Edit
		//[HttpGet]
		public IActionResult Edit(int? id)
		{
			return Details(id, "Edit");
			///if (!id.HasValue)
			///	return BadRequest(); // 400
			///var department = _departmentsRepo.Get(id.Value);
			///if(department is null)
			///	return NotFound(); // 404
			///return View(department);

		}

		[HttpPost] 
		[ValidateAntiForgeryToken]
		public IActionResult Edit(DepartmentViewModel departmentVm)
		{
			
			if (!ModelState.IsValid)
				return View(departmentVm);

			try
			{
                var department = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
                return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				// 1. Log Exeption 
				// 2. Frindly Message
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error Has occurred during Updating the Department");
				
				return View(departmentVm);
			}
		}

		[HttpPost]
		public IActionResult Delete(int? id)
		{
			if (!id.HasValue)
				return BadRequest();

			var department = _departmentsRepo.Get(id.Value);

			if (department is null)
				return NotFound();

			try
			{
				_departmentsRepo.Delete(department);
			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the department");

				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}
	}
}


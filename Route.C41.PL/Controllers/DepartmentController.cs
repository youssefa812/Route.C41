using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Repositories;
using Route.C41.DAL.Models;
using System;

namespace Route.C41.PL.Controllers
{
	// Inhertiance : DepartmentController is a Controller
	// Association [Composition] : DepartmentController has a DepartmentRepository
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepository _departmentsRepo;
		private readonly IWebHostEnvironment _env;

		public DepartmentController(IDepartmentRepository departmentsRepo, IWebHostEnvironment env) // Ask CLR for Creating an Object from Class Implmenting IDepartmentRepository
		{
			_departmentsRepo = departmentsRepo;
			_env = env;
		}

		// /Department/Index
		public IActionResult Index()
		{
			var departments = _departmentsRepo.GetAll();

			return View(departments);
		}

		// /Department/Create
		//[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Department department)
		{
			if (ModelState.IsValid) // Server Side Validation
			{
				var Count = _departmentsRepo.Add(department);
				if (Count > 0)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			return View(department);
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

			return View(viewName, department);
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
		public IActionResult Edit([FromRoute]int id,Department department)
		{
			if(id != department.Id)
				return BadRequest();

			if (!ModelState.IsValid)
				return View(department);

			try
			{
				_departmentsRepo.Update(department);
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
				
				return View(department);
			}
		}

	}
}

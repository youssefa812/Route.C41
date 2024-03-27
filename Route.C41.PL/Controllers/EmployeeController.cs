using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL.Interfaces;
using Route.C41.DAL.Models;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Route.C41.PL.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IEmployeeRepository _employeeRepo;
		private readonly IHostEnvironment _env;

		public EmployeeController(IEmployeeRepository employeeRepository, IHostEnvironment env)
		{
			_employeeRepo = employeeRepository;
			_env = env;
		}

		// /Employee/Index
		//[HttpGet]
		public IActionResult Index()
		{
			//ViewData["Message"] = "Hello ViewData";
			//ViewBag.Message = "Hello ViewBag";

			var employees = _employeeRepo.GetAll();
			return View(employees);
		}

		// /Employee/Create
		//[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Employee employee)
		{
			if (ModelState.IsValid) // Server Side Validation
			{
				var count = _employeeRepo.Add(employee);
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}

			return View(employee);
		}

		// /Employee/Details/10
		// /Employee/Details
		[HttpGet]
		public IActionResult Details(int? id, string viewName = "Details")
		{
			if (!id.HasValue)
				return BadRequest(); // 400

			var employee = _employeeRepo.Get(id.Value);

			if (employee is null)
				return NotFound();

			return View(viewName, employee);

		}

		// /Employee/Edit/10
		// /Employee/Edit
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			return Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit([FromRoute] int id, Employee employee)
		{
			if (id != employee.Id)
				return BadRequest(); // 400
			if (!ModelState.IsValid) 
				return View(employee);

			try
			{
				_employeeRepo.Update(employee);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				// 1. Log Exeption 
				// 2. Frindly Message
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error Has occurred during Updating the Employee");

				return View(employee);

			}
		}

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			return Details(id, "Delete");
		}


		[HttpPost]
		public IActionResult Delete(Employee employee)
		{
			try
			{
				_employeeRepo.Update(employee);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				// 1. Log Exeption 
				// 2. Frindly Message
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error Has occurred during Deleting the Employee");

				return View(employee);

			}
		}
	}
}

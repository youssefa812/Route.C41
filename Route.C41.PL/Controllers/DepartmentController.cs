using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Repositories;
using Route.C41.DAL.Models;
using Route.Session3.PL.Helpers;
using Route.C41.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Route.C41.PL.Controllers
{
	// Inhertiance : DepartmentController is a Controller
	// Association [Composition] : DepartmentController has a DepartmentRepository
	public class DepartmentController : Controller
	{
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentRepository _departmentsRepo;
		private readonly IWebHostEnvironment _env;

		public DepartmentController(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment env) // Ask CLR for Creating an Object from Class Implmenting IDepartmentRepository
		{
            _mapper = mapper;
            _unitOfWork = unitOfWork;
			_env = env;
		}

		// /Department/Index
		public async Task<IActionResult> Index()
		{
			var departments =await _unitOfWork.Repository<Department>().GetAllAsync();

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
		public async Task<IActionResult> Create(DepartmentViewModel departmentVm)
		{
			if (ModelState.IsValid) // server side validation
			{
				var department = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
				_unitOfWork.Repository<Department>().Add(department);
				await _unitOfWork.Complete();
				return RedirectToAction(nameof(Index));
			}
			return View(departmentVm);
		}

		// /Department/Details/10
		//[HttpGet]
		public async Task<IActionResult> Details(int? id, string viewName = "Details")
		{
			if (!id.HasValue)
				return BadRequest(); // 400

			var department = await _unitOfWork.Repository<Department>().GetAsync(id.Value);

			if (department is null)
				return NotFound(); // 404

            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);

            return View(viewName, mappedDep);
        }

		// /Department/Edit/10
		// /Department/Edit
		//[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			return await Details(id, "Edit");
			///if (!id.HasValue)
			///	return BadRequest(); // 400
			///var department = _departmentsRepo.Get(id.Value);
			///if(department is null)
			///	return NotFound(); // 404
			///return View(department);

		}

		[HttpPost] 
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DepartmentViewModel departmentVm)
		{
			
			if (!ModelState.IsValid)
				return View(departmentVm);

			try
			{
                var department = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
                _unitOfWork.Repository<Department>().Update(department);
				await _unitOfWork.Complete();
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
		public async Task<IActionResult> Delete(int? id)
		{
			if (!id.HasValue)
				return BadRequest();

			var department = await _unitOfWork.Repository<Department>().GetAsync(id.Value);

			if (department is null)
				return NotFound();

			try
			{
				_unitOfWork.Repository<Department>().Delete(department);
				await _unitOfWork.Complete();
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


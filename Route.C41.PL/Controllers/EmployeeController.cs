﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Repositories;
using Route.C41.DAL.Models;
using Route.C41.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Route.C41.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public IActionResult Index(string searchInput)
        {
            var employees = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(searchInput))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.SearchByName(searchInput);

            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmps);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVm)
        {
            if (ModelState.IsValid) // server side validation
            {
                var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);

                _unitOfWork.EmployeeRepository.Add(employee);
                var count = _unitOfWork.Complete();

                if (count > 0)
                    TempData["Message"] = "Employee is Created Successfully";
                else
                    TempData["Message"] = "An Error Has Occured, Employee Not Created :(";

                return RedirectToAction(nameof(Index));
            }
            return View(employeeVm);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);

            if (employee is null)
                return NotFound();

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(ViewName, mappedEmp);
        }


        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel employeeVm)
        {
            if (!ModelState.IsValid)
                return View(employeeVm);

            try
            {
                var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.EmployeeRepository.Update(employee);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. log Exception
                // 2. Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the employee");

                return View(employeeVm);
            }
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);

            if (employee is null)
                return NotFound();

            try
            {
                _unitOfWork.EmployeeRepository.Delete(employee);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the employee");

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
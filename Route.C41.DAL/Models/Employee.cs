﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.DAL.Models
{
	public enum Gender
	{
		[EnumMember(Value = "Male")]
		Male = 1,
		[EnumMember(Value = "female")]
		Female = 2
	}
	public enum EmpType
	{
		[EnumMember(Value = "Full Time")]
		FullTime = 1,
		[EnumMember(Value = "Part Time")]
		PartTime = 2
	}
	public class Employee : ModelBase
	{
		[Required]
		[MaxLength(50, ErrorMessage = "Max Length of name is 50 chars")]
		[MinLength(5, ErrorMessage = "Min Length of name is 5 chars")]
		public string Name { get; set; }

		[Range(22, 30)]
		public int Age { get; set; }

		[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
		, ErrorMessage = "Address must be like 123-Street-City-Country")]
		public string Address { get; set; }

		[DataType(DataType.Currency)]
		public decimal Salary { get; set; }

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		[Phone]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Hiring Date")]
		public DateTime HiringDate { get; set; }

		public EmpType EmployeeType { get; set; }

		public Gender Gender { get; set; }

		public DateTime CreationDate { get; set; } = DateTime.Now;
	
		// ForeginKey
		public int? DepartmentId { get; set; }

		// Navigational Property
		// [InverseProperty(nameof(Models.Department.Employees))]
		public Department Department { get; set; }
	}
}
